using System;
using System.Collections;
using System.Collections.Generic;
using __ProjectMain.Scripts.Managers.Level;
using __ProjectMain.Scripts.Player;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class HighlightOnActivation : MonoBehaviour, IHighlightByPlayer
    {
		[SerializeField]
        private GameObject indicator;
        private List<GameObject> _activeIndicators = new List<GameObject>();
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material defaultMaterial;
        
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

        public void TriggerHighlight()
        {
            GetComponent<Renderer>().material = highlightMaterial;
            PlaceIndicators();
            StopCoroutine(nameof(DissolveIndicators));
        }

        public void StopHighlightDelayed()
        {
            StartCoroutine(nameof(DissolveIndicators));
        }

        public void StopHighlight()
        {
            foreach (var obj in _activeIndicators)
            {
                Destroy(obj);
            }
            GetComponent<Renderer>().material = defaultMaterial;
            _activeIndicators.Clear();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerHighlight();
                GetComponent<ActivationPlateController>().Door.TriggerHighlight();
                LevelLoaderManager.Instance.playerObject.GetComponent<HighlightController>().ChangeHighlightByPlayer(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) StopHighlightDelayed();
        }
        
        private IEnumerator DissolveIndicators()
        {
            float duration = 3f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            StopHighlight();
            GetComponent<ActivationPlateController>().Door.StopHighlight();
            LevelLoaderManager.Instance.playerObject.GetComponent<HighlightController>().RemoveHighlightReference();
        }
    }
}