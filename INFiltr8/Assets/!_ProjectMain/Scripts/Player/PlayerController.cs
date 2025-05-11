using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private InputAction moveAction;
    
    [SerializeField]
    private float Speed = 2;

    [SerializeField] 
    private Rigidbody rb;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPos = transform.position;
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        
        rb.MovePosition(playerPos + new Vector3(moveValue.x, 0, moveValue.y).normalized * Time.deltaTime * Speed);
    }
}
