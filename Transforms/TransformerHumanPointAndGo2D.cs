using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerHumanPointAndGo2D : TransformerHumanPointAndGo
{
    protected override void screen2world()
    {
        float nearPlane = cam.nearClipPlane - cam.gameObject.transform.position.z;
        Debug.Log("nearPlane: " + nearPlane);
        Vector3 screenPos3 = new Vector3(screenPos.x, screenPos.y, nearPlane);
        worldPos = cam.ScreenToWorldPoint(screenPos3);
    }
}
