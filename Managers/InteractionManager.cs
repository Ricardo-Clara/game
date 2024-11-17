using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Weapon hoveredWeapon = null;
    public AmmoBox hoveredAmmoBox = null;
    public Throwable hoveredThrowable = null;
    private GameObject player;

    

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        player = GameObject.FindWithTag("Player");
    }

    private void OnPickUp(InputValue value) {
        if (hoveredWeapon) {
            if (Vector3.Distance(player.transform.position, hoveredWeapon.transform.position) < 4f) {
                WeaponManager.Instance.PickUpWeapon(hoveredWeapon.gameObject);
            }
        } else if (hoveredAmmoBox) {
            if (Vector3.Distance(player.transform.position, hoveredAmmoBox.transform.position) < 4f) {
                WeaponManager.Instance.PickUpAmmo(hoveredAmmoBox);
                Destroy(hoveredAmmoBox.gameObject);
            }
        } else if (hoveredThrowable) {
            if (Vector3.Distance(player.transform.position, hoveredThrowable.transform.position) < 4f) {
                WeaponManager.Instance.PickUpThrowable(hoveredThrowable);
            }
        }
    }

    private void Update() {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.GetComponent<Weapon>() == null && hitObject.GetComponent<AmmoBox>() == null && hitObject.GetComponent<Throwable>() == null && NotificationManager.Instance.pickupText.gameObject.activeSelf) {
                NotificationManager.Instance.DisablePickupText();
            }
            
            // Weapon
            if (hitObject.GetComponent<Weapon>() && hitObject.GetComponent<Weapon>().isActiveWeapon == false) {

                if (hoveredWeapon) {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                }

                hoveredWeapon = hitObject.GetComponent<Weapon>();

                if (Vector3.Distance(player.transform.position, hoveredWeapon.transform.position) < 4f) {
                    hoveredWeapon.GetComponent<Outline>().enabled = true;
                    NotificationManager.Instance.EnablePickupText();
                }

            } else {
                if (hoveredWeapon) {
                    hoveredWeapon.GetComponent<Outline>().enabled = false;
                    hoveredWeapon = null;
                }
            }

            
            // AmmoBox
            if (hitObject.GetComponent<AmmoBox>()) {

                if (hoveredAmmoBox) {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }

                hoveredAmmoBox = hitObject.GetComponent<AmmoBox>();
                if (Vector3.Distance(player.transform.position, hoveredAmmoBox.transform.position) < 4f) {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = true;
                    NotificationManager.Instance.EnablePickupText();
                } 
            } else {
                if (hoveredAmmoBox) {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                    hoveredAmmoBox = null;
                }
            }

            // Throwable
            if (hitObject.GetComponent<Throwable>()) {

                if (hoveredThrowable) {
                    hoveredThrowable.GetComponent<Outline>().enabled = false;
                }

                hoveredThrowable = hitObject.GetComponent<Throwable>();

                if (Vector3.Distance(player.transform.position, hoveredThrowable.transform.position) < 4f) {
                    hoveredThrowable.GetComponent<Outline>().enabled = true;
                    NotificationManager.Instance.EnablePickupText();
                }
            } else {
                if (hoveredThrowable) {
                    hoveredThrowable.GetComponent<Outline>().enabled = false;
                    hoveredThrowable = null;
                }
            }
        }
    }
}
