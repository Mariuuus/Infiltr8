using __ProjectMain.Scripts.Managers;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class GoalController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("The speed at which the object spins around its Y-axis.")]
        [SerializeField]
        private float spinSpeed = 100f;

        [Tooltip("The maximum height difference for the sine wave movement.")] [SerializeField]
        private float floatHeight = 1f;

        [Tooltip("The speed at which the object floats up and down.")] [SerializeField]
        private float floatSpeed = 2f;

        private Vector3 _initialPosition;
        private float _timeOffset; // To make each object's float movement unique

        void Start()
        {
            _initialPosition = transform.position;
            _timeOffset = Random.Range(0f, 100f);
        }

        void Update()
        {
            transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
            float newY = _initialPosition.y + Mathf.Sin((Time.time + _timeOffset) * floatSpeed) * floatHeight;
            transform.position = new Vector3(_initialPosition.x, newY, _initialPosition.z);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) GameDataManager.Instance.SwitchToOverview();

        }
    }
}