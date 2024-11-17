using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHand : MonoBehaviour
{
    public int damage;

    private void Update() {
        if (transform.root.GetComponent<Enemy>().isDead) {
            gameObject.SetActive(false);
        }
    }
}
