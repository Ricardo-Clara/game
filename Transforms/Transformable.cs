using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transformable : MonoBehaviour
{
    public abstract void move(Vector3 dPosition);
    public abstract void rotate(Vector3 dRotation);
    public abstract void scale(Vector3 dScale);
}
