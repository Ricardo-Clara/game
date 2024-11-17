using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerHumanRotateXYLocal : TransformerHumanRotate
{
    public float pitchSpeed = 20;
    public float yawSpeed = 20;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public override Vector3 dRotate() {
        return new Vector3(dRot.y * pitchSpeed, dRot.x * yawSpeed, 0);
    }
}
