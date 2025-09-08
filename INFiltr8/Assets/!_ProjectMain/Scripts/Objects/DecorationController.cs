using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class DecorationController : MonoBehaviour
    {
        public Variations variant = Variations.Blue;
        [SerializeField] private List<GameObject> decorations = new List<GameObject>();
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
                case Decorations.TubeLamp:
                    updateMesh(decorations[2]);
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
            currentDecoration = Instantiate(prefab, transform.position, quaternion.identity);

            // decorations[0] = camera
            // decorations[1] = lavalamp
            // decorations[2] = tube_lamp
            if (prefab == decorations[0])
            {
                // add position for camera
            }
            else if (prefab == decorations[1])
            {
                currentDecoration.GetComponentInChildren<VariationController>().SetVariation(variant);
                currentDecoration.transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z);
            } else if (prefab == decorations[2])
            {
                currentDecoration.GetComponentInChildren<TubeLampVariantController>().SetVariation(variant);
                currentDecoration.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.4f);
               // currentDecoration.transform.rotation = new Quaternion(-90.0f, 0f, 0f, 0f);
            }
            
            currentDecoration.transform.SetParent(transform);
        }

        public void SetYPosition(float offset)
        {
            currentDecoration.transform.position = new Vector3(transform.position.x, transform.position.y + offset,
                transform.position.z);
        }
    }
}