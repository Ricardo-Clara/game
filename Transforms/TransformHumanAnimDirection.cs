using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHumanAnimDirection : TransformerHumanMove
{
    public string animatorDirectionX, animatorDirectionZ;
    private int dirXHash, dirZHash;

    protected override void Awake() {
        base.Awake();
        dirXHash = Animator.StringToHash(animatorDirectionX);
        dirZHash = Animator.StringToHash(animatorDirectionZ);
    }

    public override void animate()
    {
        if (dirXHash != 0) animator.SetFloat(dirXHash, dPos.x);
        if (dirZHash != 0) animator.SetFloat(dirZHash, dPos.y);
    }
}
