using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : MonoBehaviour
{
    protected Vector2 dRot;
    public float sensitivity = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;
    public float min = -90f;
    public float max = 90f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        float mouseX = dRot.x * sensitivity * Time.deltaTime;
        float mouseY = dRot.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, min, max);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void OnLook(InputValue value) {
        dRot = value.Get<Vector2>();
    }
}
