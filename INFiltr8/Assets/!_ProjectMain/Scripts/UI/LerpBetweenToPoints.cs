using System;
using System.Collections;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class LerpBetweenToPoints : MonoBehaviour
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private float duration = 2f;

        private Coroutine _lerpCoroutine;

        public void StartLerp()
        {
            if (_lerpCoroutine != null)
                StopCoroutine(_lerpCoroutine);

            _lerpCoroutine = StartCoroutine(LerpPosition());
        }

        private IEnumerator LerpPosition()
        {
            float timeElapsed = 0f;

            while (timeElapsed < duration)
            {
                float t = timeElapsed / duration;
                float smoothT = Mathf.SmoothStep(0f, 1f, t);
                transform.position = Vector3.Lerp(startPoint.position, endPoint.position, smoothT);
                transform.rotation =  Quaternion.Euler(Vector3.Lerp(startPoint.rotation.eulerAngles, endPoint.rotation.eulerAngles, smoothT));

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            transform.position = endPoint.position;
            _lerpCoroutine = null;
        }
    }
}