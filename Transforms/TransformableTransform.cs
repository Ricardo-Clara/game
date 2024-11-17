using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformableTransform : Transformable
{
    public override void move(Vector3 dPosition) {
        transform.Translate(dPosition);
    }

    public override void rotate(Vector3 dRotation) {
       transform.Rotate(dRotation);
    }

    public override void scale(Vector3 dScale) {
        transform.localScale += dScale;
    }
}
