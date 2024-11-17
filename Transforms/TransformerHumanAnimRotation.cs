using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerHumanAnimRotation : TransformerHumanRotate
{
    public string animatorRotationX, animatorRotationY;
    private int rotXHash, rotYHash;

    protected override void Awake() {
        base.Awake();
        rotXHash = Animator.StringToHash(animatorRotationX);
        rotYHash = Animator.StringToHash(animatorRotationY);
    }

    public override void animate()
    {
        if (rotXHash != 0) animator.SetFloat(rotXHash, dRot.y);
        if (rotYHash != 0) animator.SetFloat(rotYHash, dRot.x);
    }
}
