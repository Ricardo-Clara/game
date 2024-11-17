using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public abstract class TransformerHumanMove : Transformer
{
    protected Vector2 dPos;
    
    void OnMove(InputValue value) {
        dPos = value.Get<Vector2>();
    }
}
