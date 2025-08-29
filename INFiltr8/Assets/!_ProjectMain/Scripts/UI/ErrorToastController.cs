using System.Collections;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class ErrorToastController : MonoBehaviour
    {
        
        public const float DelayAutoHideTime = 5f;
        
        [SerializeField]  private TMP_Text errorText;
        public void ShowError(string message)
        {
            errorText.text = message;
            gameObject.SetActive(true);
            StartCoroutine(nameof(HideErrorWithDelay));
        }

        public void HideError()
        {
            gameObject.SetActive(false);
        }

        public IEnumerator HideErrorWithDelay()
        {
            yield return new WaitForSeconds(DelayAutoHideTime);
            HideError();
        }
    }
}