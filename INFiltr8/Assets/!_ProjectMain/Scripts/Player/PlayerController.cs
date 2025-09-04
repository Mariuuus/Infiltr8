using System.Collections;
using __ProjectMain.Scripts.Managers.Ingame;
using __ProjectMain.Scripts.Objects;
using __ProjectMain.Scripts.UI.Controls;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 2;
        //[SerializeField] private float rotationSpeed = 2;
        [SerializeField] public float playerRange = 3f;
        [SerializeField] private GameObject interactionImage;
        [SerializeField] private ParticleSystem slowdownParticles;
        [SerializeField] private int slowdownTime = 5;
        [SerializeField] private float slowdownFactor = 2;
        [SerializeField] private Canvas slowdownUI;

        private GameObject _interactionInstance;
        private Coroutine _uiFillRoutine;
        private int _remainingTime = 0;
        private Canvas _slowdownUIInstance;
        public GameObject ClosestObject { get; private set; }
        public bool inSlowdown = false;
    
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
                        UpdateInteractionUI();
                    }
                }
            }
            if(before != ClosestObject) UpdateInteractionUI();
        }

        public void Freeze()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        
        public void UnFreeze()
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }

        public void SlowPlayer()
        {
            if (!inSlowdown)
            {
                _remainingTime = slowdownTime;
                StartCoroutine(slowdownForSeconds());
                _uiFillRoutine = StartCoroutine(UpdateUIFill());
            }
            else
            {
                // if we retrigger the slowdown while we are in a slowdown already
                if (_uiFillRoutine != null)
                {
                    StopCoroutine(_uiFillRoutine);
                    _uiFillRoutine = null;
                }
                _uiFillRoutine = StartCoroutine(UpdateUIFill());
                _remainingTime = slowdownTime;
            }
        }

        IEnumerator slowdownForSeconds()
        {
            inSlowdown = true;
            _slowdownUIInstance =
                Instantiate(slowdownUI, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            _slowdownUIInstance.transform.SetParent(transform);
            if (_interactionInstance) Destroy(_interactionInstance);
           // var slowdownParticlesInstance = Instantiate(slowdownParticles, transform.position, Quaternion.identity);
           // slowdownParticlesInstance.transform.SetParent(transform);
            moveSpeed /= slowdownFactor;
            while (_remainingTime > 0)
            {
               
                yield return new WaitForSeconds(1);
                _remainingTime--;
            }
            moveSpeed *= slowdownFactor;
            Destroy(_slowdownUIInstance.gameObject);
            // Destroy(slowdownParticlesInstance.gameObject);
            inSlowdown = false;
            UpdateInteractionUI();
            yield return null;
        }

        IEnumerator UpdateUIFill()
        {
            if (_slowdownUIInstance != null)
            {
                float elapsed = 0f;
                Image image = _slowdownUIInstance.GetComponentInChildren<Image>();
                
                while (elapsed < slowdownTime && inSlowdown)
                {
                    elapsed += Time.deltaTime;
                    image.fillAmount = Mathf.Lerp(1f, 0f, elapsed / slowdownTime);
                    yield return null; 
                }
            
                image.fillAmount = 0f;
                _uiFillRoutine = null;
            }
        }
    
        public void UpdateInteractionUI()
        {
            if (_interactionInstance)
            {
                Destroy(_interactionInstance);    
            }
            
            if (ClosestObject  == null || GetComponent<GrabController>().IsGrabbing || inSlowdown) return;
            
            Debug.Log(ClosestObject.name);
            _interactionInstance = Instantiate(interactionImage, ClosestObject .transform.position + new Vector3(0, 1, 0),
                Quaternion.Euler(40, 0, 0));
            _interactionInstance.transform.SetParent(ClosestObject.transform);
            _interactionInstance.GetComponentInChildren<LaptopUI>().Init(ClosestObject.GetComponent<HackableDevice>() ?? null );
        }
    }
}
