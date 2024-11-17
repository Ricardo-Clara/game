using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int projectileDamage;


    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<Player>().TakeDamage(projectileDamage);

            CreateProjectileHitEffect(collision);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall")) {
            CreateProjectileHitEffect(collision);
            
            Destroy(gameObject);
        }
    }

    private void CreateProjectileHitEffect(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 instancePoint = contact.point + contact.normal * 0.01f;
        GameObject impactMark = Instantiate(GlobalReferences.Instance.projectileHitEffect, instancePoint, Quaternion.FromToRotation(Vector3.up, contact.normal));

        impactMark.transform.SetParent(collision.gameObject.transform);
    }
}
