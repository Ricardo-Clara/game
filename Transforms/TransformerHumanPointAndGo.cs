using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class TransformerHumanPointAndGo : Transformer
{
    public float speed = 10;
    public string actionName;
    public Camera cam;

    private PlayerInput input;
    protected Vector2 screenPos;
    protected Vector3 worldPos;

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<PlayerInput>();
        if(input == null) Debug.LogError("TransformerHumanPointAndGo: No PlayerInput component found on this object.");

        InputAction inputAction = input.actions.FindAction(actionName);
        if(inputAction == null) Debug.LogError("TransformerHumanPointAndGo: No action named " + actionName + " found in PlayerInput component.");

        inputAction.started += onAction;

        worldPos = transform.position;
        if(cam == null) cam = Camera.main;
    }

    void onAction(InputAction.CallbackContext context)
    {
        screenPos = context.ReadValue<Vector2>();
        screen2world();
    }

    protected abstract void screen2world();
}
