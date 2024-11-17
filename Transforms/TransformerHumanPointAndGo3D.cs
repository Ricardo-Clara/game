using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerHumanPointAndGo3D : TransformerHumanPointAndGo
{
    protected override void screen2world()
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            worldPos = hit.point;
        }
    }
}
