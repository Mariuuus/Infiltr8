using __ProjectMain.Scripts.Managers.Ingame;
using UnityEngine;

namespace __ProjectMain.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2;
        [SerializeField] private float rotationSpeed = 2;
        [SerializeField] public float playerRange = 3f;
        [SerializeField] private GameObject interactionImage;

        private GameObject _interactionInstance;
    
        public GameObject ClosestObject { get; private set; }
    
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
                Quaternion targetRotation = Quaternion.LookRotation(_input, Vector3.up);
                rb.MoveRotation(targetRotation);
                rb.linearVelocity = transform.forward * moveSpeed;
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }


            // Check for closest InteractableObj
            Vector3 playerPos = transform.position;
            Collider[] hitColliders = Physics.OverlapSphere(playerPos, playerRange);

            GameObject before = ClosestObject;
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Interactable"))
                {
                    if (ClosestObject)
                    {
                        float newDistance = (hitCollider.transform.position - playerPos).magnitude;
                        float oldDistance = (ClosestObject.transform.position - playerPos).magnitude;

                        if (newDistance < oldDistance)
                        {
                            ClosestObject = hitCollider.gameObject;
                        }
                    }
                    else
                    {
                        ClosestObject = hitCollider.gameObject;
                    }
                }
            }
            if (ClosestObject != null)
            {
                //Check For Distance
                float dist = (ClosestObject.transform.position - playerPos).magnitude;
                if (dist > playerRange)
                {
                    
                    ClosestObject = null;
                    UpdateInteractionUI();
                    return;
                }

                RaycastHit hit;
                if (Physics.Raycast(playerPos, (ClosestObject.transform.position - playerPos), out hit,
                        GetComponent<PlayerController>().playerRange))
                {
                    if (hit.collider.CompareTag("Wall"))
                    {
                        ClosestObject = before;
                    }
                }
                UpdateInteractionUI();
            }
        }
    
        public void UpdateInteractionUI()
        {
            if (_interactionInstance)
            {
                Destroy(_interactionInstance);    
            }
    
            if (ClosestObject  == null) return;
    
            _interactionInstance = Instantiate(interactionImage, ClosestObject .transform.position + new Vector3(0, 1, 0),
                Quaternion.Euler(40, 0, 0));
        }
    }
}
