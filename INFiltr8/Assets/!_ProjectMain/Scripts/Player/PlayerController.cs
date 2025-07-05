using System;
using __ProjectMain.Scripts.Managers.Ingame;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2;
    [SerializeField]
    private float rotationSpeed = 2;

    [SerializeField] 
    private Rigidbody rb;
    private Vector3 _input;


    void Update()
    {
        if (IngameManager.Instance?.Paused == true) return;
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
    }

    void FixedUpdate()
    {
        if (IngameManager.Instance?.Paused == true) return;
        if (_input != Vector3.zero)
        {
            //Debug.LogError("Moves");
            Quaternion targetRotation = Quaternion.LookRotation(_input, Vector3.up);
            Quaternion newRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(newRotation);

            rb.linearVelocity = transform.forward * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

}
