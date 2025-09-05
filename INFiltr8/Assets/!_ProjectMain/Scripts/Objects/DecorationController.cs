using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class DecorationController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> decorations = new List<GameObject>();
        public float rotation = 0f;
        private GameObject currentDecoration = null;
        private void Start()
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        private void FixedUpdate()
        {
            return;
        }

        public void setDecoration(Decorations decorationType)
        {
            switch (decorationType)
            {
                case Decorations.Camera:
                    updateMesh(decorations[0]);
                    break;
                case Decorations.Lavalamp:
                    updateMesh(decorations[1]);
                    break;
                case Decorations.Neonsign:
                    updateMesh(decorations[2]);
                    break;
                case Decorations.Telephone:
                    updateMesh(decorations[3]);
                    break;
                case Decorations.Table:
                    updateMesh(decorations[4]);
                    break;
                case Decorations.Wardrobe:
                    updateMesh(decorations[5]);
                    break;
                default:
                    break;
            }
        }

        private void updateMesh(GameObject prefab)
        {
            if (currentDecoration != null)
            {
                Destroy(currentDecoration.gameObject);
            }
            
            if (prefab == null) return;

            currentDecoration = Instantiate(prefab, transform.position, new Quaternion(0f, this.rotation, 0f, 0f));

            if (prefab == decorations[0])
            {
                currentDecoration.transform.position = new Vector3(transform.position.x, 0.25f, transform.position.z);
            }
            
            currentDecoration.transform.SetParent(transform);
        }
    }
}