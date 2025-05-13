using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Collider closestObject = null;
    private float grabRange = 5.0f;
    private InputAction grabAction;
    private InputAction moveAction;
    private bool isGrabbing = false;

    [SerializeField] 
    private GameObject interactionImage;

    private GameObject _interactionInstance;
    
    void Start()
    {
        grabAction = InputSystem.actions.FindAction("Interact");
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerPos = transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(playerPos, grabRange);
        
        if (closestObject != null)
        {
            if ((closestObject.transform.position - playerPos).magnitude > grabRange)
            {
                closestObject = null;
                Destroy(_interactionInstance);
            }
            
            if (grabAction.WasPressedThisFrame() && closestObject != null)
            {
                if (!isGrabbing)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(playerPos, (closestObject.transform.position - playerPos), out hit, grabRange))
                    {
                        if (hit.collider.CompareTag("Wall"))
                        {
                            Debug.Log("closest object is behind wall");
                            isGrabbing = false;
                        }
                        else
                        {
                            Destroy(_interactionInstance);
                            isGrabbing = true;
                        }
                    }
                  
                }
                else
                {
                    setInteractionUI();
                    isGrabbing = false;
                    closestObject.GetComponent<Rigidbody>().mass = 1;
                }
            }
        }

        if (isGrabbing && closestObject != null)
        {
            closestObject.GetComponent<Rigidbody>().mass = 0;
            Vector2 moveDir = moveAction.ReadValue<Vector2>();
            Vector3 grabVector;

            if (moveDir.x == 0 && moveDir.y == 0)
            {
                grabVector = new Vector3(0, 0, 1.5f);
            }
            else
            {
                grabVector = new Vector3(moveDir.x * 1.75f, 0, moveDir.y * 1.75f);
            }
            
            closestObject.transform.position = playerPos + grabVector;
        }
        
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("grabbable") && !isGrabbing)
            {
                if (closestObject != null)
                {
                    float newDistance = (collider.transform.position - playerPos).magnitude;
                    float oldDistance = (closestObject.transform.position - playerPos).magnitude;
                    
                    if (newDistance < oldDistance)
                    {
                        closestObject = collider;
                    }
                }
                else
                {
                    closestObject = collider;
                }
                
                RaycastHit hit;
                if (Physics.Raycast(playerPos, (closestObject.transform.position - playerPos), out hit, grabRange))
                {
                     if (!hit.collider.CompareTag("Wall")) {
                        setInteractionUI();
                     } 
                     Debug.DrawRay(transform.position, (closestObject.transform.position - playerPos) * hit.distance, Color.yellow);
                }
            }
        }
    }

    void setInteractionUI()
    {
        if (_interactionInstance != null)
        {
            Destroy(_interactionInstance);    
        }
        
        if (closestObject == null) return;
        
        _interactionInstance = Instantiate(interactionImage, closestObject.transform.position + new Vector3(0, 1, 0),
            Quaternion.Euler(40, 0, 0));
    }
}

