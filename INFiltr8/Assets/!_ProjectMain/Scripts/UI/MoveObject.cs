using UnityEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class MoveObject : MonoBehaviour
    {
        public bool active = false;
        public float moveRadius = 0.2f;
        public float moveSpeed = 1f;
        public float rotationAmount = 5f;
        public float changeInterval = 1.5f;

        Vector3 startPosition;
        Vector3 targetPosition;
        Quaternion startRotation;
        Quaternion targetRotation;
        float timer;

        void Start()
        {
            startPosition = transform.position;
            startRotation = transform.rotation;
            targetPosition = startPosition;
            targetRotation = startRotation;
        }

        public void Init(Transform start)
        {
            startPosition = start.position;   
            //startRotation = start.rotation;   
        }

        void Update()
        {
            if (!active) return;

            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                Vector3 offset = new Vector3(Random.Range(-moveRadius, moveRadius), 0f, Random.Range(-moveRadius, moveRadius));
                targetPosition = startPosition + offset;
                targetRotation = startRotation * Quaternion.Euler(0f, Random.Range(-rotationAmount, rotationAmount), 0f);
                timer = changeInterval;
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }

        public void OnSpeak(bool speaking)
        {
            active = speaking;
        }
    }
}
