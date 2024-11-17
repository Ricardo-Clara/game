using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransformerHumanRotate : Transformer
{
    protected Vector2 dRot;

    void OnLook(InputValue value) {
        dRot = value.Get<Vector2>();
    }
}
