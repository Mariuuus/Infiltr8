using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class HighlightOnActivation : MonoBehaviour
    {
		[SerializeField]
        private GameObject indicator;
        private List<GameObject> _activeIndicators = new List<GameObject>();
        private void PlaceIndicators()
        {
            var comp = GetComponent<ActivationPlateController>();
            Vector3 platePos = transform.position;
            Vector3 doorPos = comp.Door.transform.position;

            platePos.y = -.5f;
            doorPos.y = -.5f;

            Vector3 midpoint = (platePos + doorPos) / 2f;
            Vector3 direction = doorPos - platePos;
            float distance = direction.magnitude;

            var newObj = Instantiate(indicator, midpoint, Quaternion.identity);

            newObj.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            newObj.transform.Rotate(0, 90, 0);

            Vector3 originalScale = newObj.transform.localScale;
            float scaleFactor = distance / originalScale.x;
            newObj.transform.localScale = new Vector3(originalScale.x* scaleFactor, originalScale.y, originalScale.z );

            _activeIndicators.Add(newObj);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StopCoroutine(nameof(DissolveIndicators));
                PlaceIndicators();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(nameof(DissolveIndicators));
            }
        }
        
        private IEnumerator DissolveIndicators()
        {
            float duration = 1f;
            float elapsed = 0f;

            List<Renderer> renderers = new List<Renderer>();
            foreach (var obj in _activeIndicators)
            {
                var r = obj.GetComponentInChildren<Renderer>();
                if (r != null)
                    renderers.Add(r);
            }

            while (elapsed < duration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
                foreach (var r in renderers)
                {
                    foreach (var mat in r.materials)
                    {
                        if (mat.HasProperty("_Color"))
                        {
                            Color c = mat.color;
                            mat.color = new Color(c.r, c.g, c.b, alpha);
                        }
                    }
                }
                elapsed += Time.deltaTime;
                yield return null;
            }

            foreach (var obj in _activeIndicators)
            {
                Destroy(obj);
            }

            _activeIndicators.Clear();
        }
    }
}