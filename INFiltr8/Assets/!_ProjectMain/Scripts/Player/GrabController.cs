using UnityEngine;
using UnityEngine.InputSystem;

public class GrabController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Collider closestObject = null;
    private float grabRange = 5.0f;
    private InputAction grabAction;
    private bool isGrabbing = false;
    void Start()
    {
        grabAction = InputSystem.actions.FindAction("Interact");
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
                            isGrabbing = true;
                        }
                    }
                  
                }
                else
                {
                    isGrabbing = false;
                }
            }
        }

        if (isGrabbing && closestObject != null)
        {
            closestObject.transform.position = playerPos + new Vector3(1, 0, 1);
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
                    Debug.DrawRay(transform.position, (closestObject.transform.position - playerPos) * hit.distance, Color.yellow);
                }
            }
        }
    }
}

