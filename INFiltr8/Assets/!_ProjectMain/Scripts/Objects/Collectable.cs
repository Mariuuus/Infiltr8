using System;
using System.Collections.Generic;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using Random = UnityEngine.Random;

namespace __ProjectMain.Scripts.Objects
{
    public class Collectable : MonoBehaviour
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
        private Distro _distro;
        void Start()
        {
            _initialPosition = transform.position;
            _timeOffset = Random.Range(0f, 100f);
        }

        public void Init(DistroType distroType)
        {
            _distro = DistroDataUtils.GetDistroByType(distroType);
            var rend = GetComponent<Renderer>();
            Material mat = rend.material;
            mat.SetTexture("_Distro", DistroDataUtils.TextureFromSprite(_distro.distroSprite));  
        }

        void Update()
        {
            transform.Rotate(0, 0, spinSpeed * Time.deltaTime);
            float newY = _initialPosition.y + Mathf.Sin((Time.time + _timeOffset) * floatSpeed) * floatHeight;
            transform.position = new Vector3(_initialPosition.x, newY, _initialPosition.z);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collected" +  _distro.distroName);
            //Trigger in CollectableManager
            Destroy(gameObject);
        }
    }
}