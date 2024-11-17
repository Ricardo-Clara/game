using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TransformerHumanAnimButton : Transformer
{
    public string buttonActionName;
    public string animatorParameter;

    private PlayerInput input;
    private int animParamHash;
    private bool isPressed;

    protected override void Awake() {
        base.Awake();
        input = GetComponent<PlayerInput>();

        if (input == null) {
            Debug.Log($"input not found in {gameObject}");
        }

        InputAction inputAction = input.actions.FindAction(buttonActionName);
        if (inputAction == null) {
            Debug.Log($"inputAction not found in {gameObject}");
        }

        animParamHash = Animator.StringToHash(animatorParameter);

        inputAction.started += context => isPressed = true;
        inputAction.canceled += context => isPressed = false;
    }

    void onButton(InputAction.CallbackContext context) {
        isPressed = context.ReadValueAsButton();
    }

    public override void animate()
    {
        animator.SetBool(animParamHash, isPressed);
    }

    void onRun(InputAction.CallbackContext context) {
        if (context.started) isPressed = true;
        else if (context.canceled) isPressed = false;
    }
}
