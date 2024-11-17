using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TransformableCharacter : TransformableTransform
{
    private CharacterController control;

    protected virtual void Awake() {
        control = GetComponent<CharacterController>();
        if (control == null) {
            control = GetComponentInChildren<CharacterController>();
        }
        if (control == null) {
            Debug.Log($"control not found in {gameObject}");
        }
    }

    public override void move(Vector3 dPosition)
    {
        control.Move(transform.TransformDirection(dPosition));   
    }
}
