using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class TransformerHumanSwitchButton : MonoBehaviour
{
    public string buttonActionName;

    private PlayerInput input;
    public GameObject[] go;
    private int active = 0;

    protected void Awake() {
        input = GetComponent<PlayerInput>();

        if (input == null) {
            Debug.Log($"input not found in {gameObject}");
        }

        InputAction inputAction = input.actions.FindAction(buttonActionName);
        if (inputAction == null) {
            Debug.Log($"inputAction not found in {gameObject}");
        }

        inputAction.started += onPressed;

        if (go.Length <= 0) {
            Debug.Log($"go not found in {gameObject}");
        }

        go[0].SetActive(true);
        for (int i = 1; i < go.Length; i++) {
            go[i].SetActive(false);
        }
    }

    void onPressed(InputAction.CallbackContext context) {
        go[active].SetActive(false);
        if (++active >= go.Length) {
            active = 0;
        }
        go[active].SetActive(true);
    }
}
