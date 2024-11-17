using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformerGravity : Transformer
{
  private static float minGravitySpeed = -0.1f;

  public float gravityAcceleration = -9.8f;
  private float gravitySpeed = minGravitySpeed;
  

    public override Vector3 dMove()
    {
        if (dT.movSpeed().y >= 0) {
            gravitySpeed = minGravitySpeed;
        } else {
            gravitySpeed += gravityAcceleration * Time.deltaTime;
        }
        return new Vector3(0, gravitySpeed, 0);
    }
}
