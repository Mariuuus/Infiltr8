using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class ActivationPlateUIController : MonoBehaviour
    {
        [SerializeField] private GameObject uiPrefab;
        private GameObject _uiObj;
        private TMP_Text _uiText;

        public void UpdateUI(int isCurrently, int max)
        {
            if (_uiObj == null)
            {
                _uiObj = Instantiate(uiPrefab);
                _uiText = _uiObj.GetComponentInChildren<TMP_Text>();
                _uiObj.transform.position = new Vector3(transform.position.x, -.4f, transform.position.z);
            }
            _uiText.color = isCurrently > max ? new Color32(200, 27, 27, 255) : new Color32(255, 255, 255, 255);
            _uiText.text = isCurrently + "/" + max;
        }
    }
}