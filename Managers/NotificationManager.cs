using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance { get; set; }

    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI pickupText;
    public float displayTime = 2f;
    public float fadeTime = 1f;

    private Queue<string> notificationQueue = new();    

    private void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }

        notificationText.text = "";
        pickupText.text = "Press E to pick up";
    }

    public void EnablePickupText() {
        pickupText.gameObject.SetActive(true);
    }

    public void DisablePickupText() {
        pickupText.gameObject.SetActive(false);
        print("disable");
    }

    public void AmmoNotification(AmmoBox ammoBox) {
        string text = $"+Picked up {ammoBox.ammoAmount} rounds for {ammoBox.ammoType}";

        EnqueueNotification(text);
    }


    public void WeaponNotification(Weapon weapon) {
        string text = $"+Picked up {weapon.currentWeaponModel}";

        EnqueueNotification(text);
    }

    public void ThrowableNotification() {
        string text = $"+Picked up Grenade";

        EnqueueNotification(text);
    }

    private void EnqueueNotification(string text) {
        notificationQueue.Enqueue(text);

        if (notificationQueue.Count == 1) {
            StartCoroutine(ShowNextNotification());
        }
    }

    private IEnumerator ShowNextNotification()
    {
        string currentNotification = notificationQueue.Peek();

        notificationText.text = currentNotification;
        notificationText.CrossFadeAlpha(1f, fadeTime, false);

        yield return new WaitForSeconds(displayTime);

        notificationText.CrossFadeAlpha(0f, fadeTime, false);
        yield return new WaitForSeconds(fadeTime);

        notificationText.text = "";
        notificationQueue.Dequeue();

        if (notificationQueue.Count > 0) {
            StartCoroutine(ShowNextNotification());
        }
    }
}
