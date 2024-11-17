using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; set; }

    [Header("Ammo")]
    public TextMeshProUGUI magazineAmmoUI;
    public TextMeshProUGUI totalAmmoUI;
    

    [Header("Weapon")]
    public List<Image> weaponSlotsUI;


    [Header("Throwables")]
    public Image lethalUI;
    public TextMeshProUGUI lethalAmountUI;

    [Header("Default Sprites")]
    public Sprite emptySlot;
    public Sprite greySlot;

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    private void Update() {
        Weapon activeWeapon = WeaponManager.Instance.activeWeaponSlot.GetComponentInChildren<Weapon>();

        List<GameObject> weaponSlots = WeaponManager.Instance.weaponSlots;

        if (activeWeapon) {
            magazineAmmoUI.text = $"{activeWeapon.bulletsLeft}";
            totalAmmoUI.text = $"{WeaponManager.Instance.CheckAmmoLeft(activeWeapon.currentWeaponModel)}";
        } else {
            magazineAmmoUI.text = "";
            totalAmmoUI.text = "";
        }

        for (int i = 0; i < weaponSlots.Count; i++) {
            Weapon weapon = weaponSlots[i].GetComponentInChildren<Weapon>();

            if (weaponSlots[i] == WeaponManager.Instance.activeWeaponSlot) {
                weaponSlotsUI[i].transform.GetChild(0).gameObject.SetActive(true);
            } else {
                weaponSlotsUI[i].transform.GetChild(0).gameObject.SetActive(false);
            }

            if (weapon) {
                weaponSlotsUI[i].sprite = GetWeaponSprite(weapon.currentWeaponModel);
            } else {
                weaponSlotsUI[i].sprite = greySlot;
            }
        }

        if (WeaponManager.Instance.lethalsCount <= 0) {
            lethalUI.sprite = greySlot;
        }
    }
    

    private Sprite GetWeaponSprite(Weapon.WeaponModel model)
    {
        return model switch
        {
            Weapon.WeaponModel.Pistol => Resources.Load<GameObject>("Pistol").GetComponent<SpriteRenderer>().sprite,
            Weapon.WeaponModel.Rifle => Resources.Load<GameObject>("Ak-47").GetComponent<SpriteRenderer>().sprite,
            Weapon.WeaponModel.Shotgun => Resources.Load<GameObject>("Shotgun").GetComponent<SpriteRenderer>().sprite,
            Weapon.WeaponModel.Sniper => Resources.Load<GameObject>("Sniper").GetComponent<SpriteRenderer>().sprite,
            Weapon.WeaponModel.SMG => Resources.Load<GameObject>("Submachine").GetComponent<SpriteRenderer>().sprite,
            Weapon.WeaponModel.RPG => Resources.Load<GameObject>("RPG").GetComponent<SpriteRenderer>().sprite,
            _ => null,
        };
    }

    internal void UpdateThrowablesUI()
    {
        lethalAmountUI.text = $"{WeaponManager.Instance.lethalsCount}";

        switch (WeaponManager.Instance.equipedLethalType) {
            case Throwable.ThrowableType.Grenade:
                lethalUI.sprite = Resources.Load<GameObject>("Grenade").GetComponent<SpriteRenderer>().sprite;
                break;
        }
    }
}
