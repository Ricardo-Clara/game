using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    private PlayerInput playerInput;

    public static WeaponManager Instance { get; set; }
    public List<GameObject> weaponSlots;
    public GameObject activeWeaponSlot;

    [Header ("Ammo")]
    public int totalRifleAmmo = 0;
    public int totalShotgunAmmo = 0;
    public int totalSniperAmmo = 0;
    public int totalUziAmmo = 0;
    public int totalRPGRounds = 0;


    [Header ("Throwable General")]
    public float throwForce = 10f;
    public GameObject throwableSpawn;
    public float forceMultiplier = 0;
    public float forceMultiplierMax = 2f;

    [Header ("Lethals")]
    public int maxLethals = 2;
    public int lethalsCount = 0;
    private bool isHoldingThrow = false;
    public Throwable.ThrowableType equipedLethalType;
    public GameObject grenadePrefab;

    [Header ("Tacticals")]
    public int maxTacticals = 2;
    public int tacticalsCount = 0;
    public Throwable.ThrowableType equipedTacticalType;
    public GameObject flashPrefab;
    

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
            playerInput = GetComponent<PlayerInput>();
        }
    }

    private void Start() {
        activeWeaponSlot = weaponSlots[0];
        equipedLethalType = Throwable.ThrowableType.None;
        // equipedTacticalType = Throwable.ThrowableType.None;
    }

    private void OnEnable() {
        playerInput.actions["Slot1"].started += ctx => SwitchWeaponSlot(0);
        playerInput.actions["Slot2"].started += ctx => SwitchWeaponSlot(1);
        playerInput.actions["Slot3"].started += ctx => SwitchWeaponSlot(2);
        playerInput.actions["Slot4"].started += ctx => SwitchWeaponSlot(3);
        playerInput.actions["Slot5"].started += ctx => SwitchWeaponSlot(4);
        playerInput.actions["Slot6"].started += ctx => SwitchWeaponSlot(5);

        playerInput.actions["Throw"].started += ctx => isHoldingThrow = true;
        playerInput.actions["Throw"].canceled += ctx => ThrowGrenade();

    }

    private void ThrowGrenade()
    {
        if (lethalsCount > 0) {
                ThrowLethal();
        }

        forceMultiplier = 0;
        isHoldingThrow = false;
    }

    private void Update() {
        foreach (GameObject weaponSlot in weaponSlots) {
            if (weaponSlot == activeWeaponSlot) {
                weaponSlot.SetActive(true);
            }
            else {
                weaponSlot.SetActive(false);
            }
        }

        if (isHoldingThrow) {
            forceMultiplier += Time.deltaTime;

            if (forceMultiplier > forceMultiplierMax) {
                forceMultiplier = forceMultiplierMax;
            }
        }
    }

    

    #region Weapon
    public void PickUpWeapon(GameObject weapon) {
        AddWeaponToActiveSlot(weapon);
        NotificationManager.Instance.WeaponNotification(weapon.GetComponent<Weapon>());
    }

    private void AddWeaponToActiveSlot(GameObject pickedUpWeapon)
    {

        DropCurrentWeapon(pickedUpWeapon);
        
        pickedUpWeapon.transform.SetParent(activeWeaponSlot.transform, false);

        Weapon weapon = pickedUpWeapon.GetComponent<Weapon>();

        pickedUpWeapon.transform.SetLocalPositionAndRotation(weapon.spawnPosition, Quaternion.Euler(weapon.spawnRotation));

        weapon.isActiveWeapon = true;
        weapon.animator.enabled = true;
    }

    private void DropCurrentWeapon(GameObject pickedUpWeapon)
    {
        if (activeWeaponSlot.transform.childCount > 0) {
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;

            weaponToDrop.GetComponent<Weapon>().isActiveWeapon = false;
            weaponToDrop.GetComponent<Weapon>().animator.enabled = false;

            weaponToDrop.transform.SetParent(pickedUpWeapon.transform.parent);
            weaponToDrop.transform.SetLocalPositionAndRotation(pickedUpWeapon.transform.localPosition, pickedUpWeapon.transform.localRotation);
            weaponToDrop.transform.localScale = pickedUpWeapon.transform.localScale;
        }
    }

    public void SwitchWeaponSlot(int slotNumber) {
        if (activeWeaponSlot.transform.childCount > 0) {
            Weapon newWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            newWeapon.isActiveWeapon = false;
        }

        activeWeaponSlot = weaponSlots[slotNumber];

        if (activeWeaponSlot.transform.childCount > 0) {
            Weapon newWeapon = activeWeaponSlot.transform.GetChild(0).GetComponent<Weapon>();
            newWeapon.isActiveWeapon = true;
        }
    }
    #endregion

    #region Ammo
    internal void PickUpAmmo(AmmoBox ammoBox)
    {
        switch (ammoBox.ammoType)
        {
            case AmmoBox.AmmoType.Rifle:
                totalRifleAmmo += ammoBox.ammoAmount;
                break;
            case AmmoBox.AmmoType.Shotgun:
                totalShotgunAmmo += ammoBox.ammoAmount;
                break;
            case AmmoBox.AmmoType.Sniper:
                totalSniperAmmo += ammoBox.ammoAmount;
                break;
            case AmmoBox.AmmoType.Uzi:
                totalUziAmmo += ammoBox.ammoAmount;
                break;
            case AmmoBox.AmmoType.RPG:
                totalRPGRounds += ammoBox.ammoAmount;
                break;
        }
        NotificationManager.Instance.AmmoNotification(ammoBox);
    }

    internal void DecreaseTotalAmmo(int bulletsToDecrease, Weapon.WeaponModel model)
    {
        switch (model)
        {
            case Weapon.WeaponModel.Pistol:
                break;
            case Weapon.WeaponModel.Rifle:
                totalRifleAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.Shotgun:
                totalShotgunAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.Sniper:
                totalSniperAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.SMG:
                totalUziAmmo -= bulletsToDecrease;
                break;
            case Weapon.WeaponModel.RPG:
                totalRPGRounds -= bulletsToDecrease;
                break;
        }
    }

    public int CheckAmmoLeft(Weapon.WeaponModel model)
    {
        return model switch
        {
            Weapon.WeaponModel.Pistol => 7,
            Weapon.WeaponModel.Rifle => totalRifleAmmo,
            Weapon.WeaponModel.Shotgun => totalShotgunAmmo,
            Weapon.WeaponModel.Sniper => totalSniperAmmo,
            Weapon.WeaponModel.SMG => totalUziAmmo,
            Weapon.WeaponModel.RPG => totalRPGRounds,
            _ => 0,
        };
    }
    #endregion

    #region Throwables
    public void PickUpThrowable(Throwable throwable)
    {
        switch (throwable.throwableType)
        {
            case Throwable.ThrowableType.Grenade:
                PickupThrowableAsLethal(Throwable.ThrowableType.Grenade);
                NotificationManager.Instance.ThrowableNotification();
                break;
        }
    }


    private void PickupThrowableAsLethal(Throwable.ThrowableType lethal)
    {
        if (equipedLethalType == lethal || equipedLethalType == Throwable.ThrowableType.None) {
            equipedLethalType = lethal;
            
            if (lethalsCount < maxLethals) {
                lethalsCount += 1;
                Destroy(InteractionManager.Instance.hoveredThrowable.gameObject);
                HUDManager.Instance.UpdateThrowablesUI();
            }
        }
        else {
        }
    }

    private void ThrowLethal()
    {
        GameObject lethalPrefab = GetThrowablePrefab(equipedLethalType);

        GameObject throwable = Instantiate(lethalPrefab, throwableSpawn.transform.position, Camera.main.transform.rotation);
        Rigidbody rb = throwable.GetComponent<Rigidbody>();

        rb.AddForce(Camera.main.transform.forward * (throwForce * forceMultiplier), ForceMode.Impulse);

        throwable.GetComponent<Throwable>().hasBeenThrown = true;

        lethalsCount -= 1;

        if (lethalsCount <= 0) {
            equipedLethalType = Throwable.ThrowableType.None;
        }

        HUDManager.Instance.UpdateThrowablesUI();
    }

    private GameObject GetThrowablePrefab(Throwable.ThrowableType equipedType)
    {
        return equipedType switch
        {
            Throwable.ThrowableType.Grenade => grenadePrefab,
            _ => new(),
        };
    }
    #endregion
}
