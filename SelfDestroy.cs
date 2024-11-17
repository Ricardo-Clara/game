using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float destroyTime;

    private void Start()
    {
        StartCoroutine(DestroySelf(destroyTime));
    }

    private IEnumerator DestroySelf(float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);

        Destroy(gameObject);
    }
}
