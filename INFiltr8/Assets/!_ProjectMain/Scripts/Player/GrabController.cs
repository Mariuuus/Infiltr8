using __ProjectMain.Scripts.LevelEditor.Types;
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
    private InputAction colorChangeAction;
    private bool isGrabbing = false;

    [SerializeField] 
    private GameObject interactionImage;
    
    [SerializeField] 
    private Transform grabPos;
    
    private GameObject _interactionInstance;
    
    void Start()
    {
        grabAction = InputSystem.actions.FindAction("Interact");
        moveAction = InputSystem.actions.FindAction("Move");
        colorChangeAction = InputSystem.actions.FindAction("colorChange");
        
        //reset rigidbody with the settings here
        var rb = closestObject.GetComponent<Rigidbody>();
        if (!rb)
        { 
            AddRigidbody();
        }
        else
        {
            Destroy(rb);
            AddRigidbody();
        }
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
                            if (_interactionInstance != null)
                            {
                                Destroy(_interactionInstance); 
                                _interactionInstance = null; 
                            }
                            isGrabbing = true;
                            var rb = closestObject.GetComponent<Rigidbody>();
                            closestObject.transform.parent = grabPos;

                            if (rb)
                            { 
                                Destroy(rb);
                            }


                            closestObject.transform.localPosition = Vector3.zero;
                            //closestObject.transform.localRotation = Quaternion.identity;
                        }
                    }
                  
                }
                else
                {
                    setInteractionUI();
                    isGrabbing = false;
                    closestObject.transform.parent = null;
                    AddRigidbody();
                }
            }
        }

        if (isGrabbing && closestObject != null)
        {
            Vector3 grabVector = new Vector3(0, 0, 1.5f);
            closestObject.transform.position = Vector3.Slerp(closestObject.transform.position, playerPos + grabVector, 0.45f);
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
                     if (!hit.collider.CompareTag("Wall"))
                     {

                         if (colorChangeAction.WasPressedThisFrame())
                         {
                             //Debug.Log(colorChangeAction.activeControl.name);
                             switch (colorChangeAction.activeControl.name)
                             {
                                 case "1": closestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.BlueHacked); break;
                                 case "2": closestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.RedHacked); break;
                                 case "3": closestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.GreenHacked); break;
                                 case "4": closestObject.GetComponent<grabbableType>().changeMaterial(HackStatus.YellowHacked); break;
                             }
                         }

                         setInteractionUI();
                     } 
                     Debug.DrawRay(transform.position, (closestObject.transform.position - playerPos) * hit.distance, Color.yellow);
                }
            }
        }
    }

    private void AddRigidbody()
    {
        Rigidbody newRigidbody = closestObject.AddComponent<Rigidbody>();
        newRigidbody.mass = 1f;
        newRigidbody.linearDamping = 0f;
        newRigidbody.angularDamping = 0.05f;
        newRigidbody.useGravity = true;
        newRigidbody.isKinematic = false;
        newRigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    void LateUpdate()
    {
        if (isGrabbing && closestObject != null)
        {
            closestObject.transform.position = grabPos.position;
            closestObject.transform.rotation = grabPos.rotation;
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

