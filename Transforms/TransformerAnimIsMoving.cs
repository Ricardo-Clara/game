using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerAnimIsMoving : Transformer
{
   public string animatorParameter;
   private int movingHash;

   protected override void Awake()
   {
      base.Awake();
      movingHash = Animator.StringToHash(animatorParameter);
   }

    public override void animate()
    {
        animator.SetBool(movingHash, false);

        if (dT.fwdSpeed() > 0.1f) animator.SetBool(movingHash, true);
    }
}
