using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerHumanMoveLocal : TransformerHumanMove
{
    public float fwdSpeed = 5;
    public float sideSpeed = 5;

    public override Vector3 dMove()
    {
        return Vector3.right * (dPos.x * sideSpeed) + Vector3.forward * (dPos.y * fwdSpeed);
    }
}
