using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformx 
{
    public Vector3 position;
    public Vector3 eulerAngles;
    public Vector3 localScale;

    public void reset() {
        position = eulerAngles = localScale = Vector3.zero;
    }
}
