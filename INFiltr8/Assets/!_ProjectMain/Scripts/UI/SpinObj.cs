using System;
using System.Collections;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class SpinObj: MonoBehaviour

    {
        private void Start()
        {
            StartCoroutine(nameof(SpinWithJump));
        }
        
                
        IEnumerator SpinWithJump()
        {
            float duration = 1f;
            float jumpHeight = 0.2f;
            Vector3 startPos = transform.position;
            Vector3 startRot = transform.eulerAngles;

            while (true)
            {
                float t = 0f;

                while (t < 1f)
                {
                    t += Time.deltaTime / duration;
                    transform.eulerAngles = startRot + new Vector3(0f, 360f * t, 0f);
                    transform.position = startPos + Vector3.up * Mathf.Sin(t * Mathf.PI) * jumpHeight;

                    yield return null;
                }
            }
        }
    }
}