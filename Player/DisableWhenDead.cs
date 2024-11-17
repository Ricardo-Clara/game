using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhenDead : MonoBehaviour
{
    public GameObject player;
    private void Update() {
        if (player.GetComponent<Player>().isDead) {
            gameObject.SetActive(false);
        }
    }
}
