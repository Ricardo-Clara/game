using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerMoveFwdAcc : TransformerHumanMove
{
  public float fwdMaxSpeed = 5;
  public float bwdMaxSpeed = 5;

  [Range(0, 1)] public float acceleration = 0.1f;
  [Range(0, 1)] public float drag = 0.025f;
  protected float curSpeed = 0;

  public override Vector3 dMove() {
    curSpeed += dPos.y * acceleration;
    if (curSpeed < -bwdMaxSpeed) curSpeed = -bwdMaxSpeed;
    if (curSpeed > fwdMaxSpeed) curSpeed = fwdMaxSpeed;

    if (curSpeed != 0) curSpeed += -Mathf.Sign(curSpeed) * drag;

    
    return Vector3.forward * curSpeed;
  }
}
