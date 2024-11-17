using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterMovement : MonoBehaviour
{
    protected Vector2 dPos;
    private CharacterController controller;
    public float speed = 10f;
    public float gravity = -4.9f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    void OnMove(InputValue value) {
        dPos = value.Get<Vector2>();
    }
    
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        Vector3 move = transform.right * dPos.x + transform.forward * dPos.y;

        controller.Move(speed * Time.deltaTime * move);

        velocity.y = gravity;

        controller.Move(velocity * Time.deltaTime);
    }

}
