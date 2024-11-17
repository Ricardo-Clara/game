using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target")) {

            CreateBulletImpactEffect(collision);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall")) {

            CreateBulletImpactEffect(collision);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy")) {

            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage);

            CreateBloodSprayEffect(collision);

            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Head")) {

            collision.gameObject.GetComponent<Enemy>().TakeDamage(bulletDamage * 2);

            CreateBloodSprayEffect(collision);

            Destroy(gameObject);
        }
    }

    private void CreateBloodSprayEffect(Collision objectHit)
    {
        ContactPoint contact = objectHit.contacts[0];

        GameObject bloodSprayPrefab = Instantiate(GlobalReferences.Instance.bloodEffect, contact.point, Quaternion.LookRotation(contact.normal));

        bloodSprayPrefab.transform.SetParent(objectHit.gameObject.transform);
    }

    void CreateBulletImpactEffect(Collision objectHit) {
        ContactPoint contact = objectHit.contacts[0];
        Vector3 instancePoint = contact.point + contact.normal * 0.01f;
        GameObject hole = Instantiate(GlobalReferences.Instance.bulletImpactEffect, instancePoint, Quaternion.FromToRotation(Vector3.up, contact.normal));

        hole.transform.SetParent(objectHit.gameObject.transform);
    }
}
