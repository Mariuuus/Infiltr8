using __ProjectMain.Data;
using __ProjectMain.Scripts.Utilities.Files;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.UI.Slots
{
    public class GameSlot : MonoBehaviour
    {
        private string _postFix;
        [SerializeField] public TMP_Text titleText;
        [SerializeField] public Button deleteButton;
        [SerializeField] public TMP_Text descriptionText;
        private GameSlotsManager _gameSlotsManager;

        public void Init(GameSlotsManager reference, string pPostFix, bool exists, int maxLvls)
        {
            _postFix = pPostFix;
            _gameSlotsManager = reference;
            if (exists)
            {
                var data = GameDataUtils.LoadData(_postFix);
                descriptionText.text = "Progress: " + (data.progress) + "/" + maxLvls;
                deleteButton.gameObject.SetActive(true);
                deleteButton.onClick.AddListener(() =>
                {
                    _gameSlotsManager.DeleteFile(_postFix);
                });
            }
            else
            {
                deleteButton.gameObject.SetActive(false);
                descriptionText.text = "N/A";
            }
            titleText.text = "Save Slot " + _postFix;
        }

        public void OnLoadCLick()
        {
            _gameSlotsManager.LoadFile(_postFix);
        }
        
        public void OnDeleteClick()
        {
            _gameSlotsManager.DeleteFile(_postFix);
        }
    }
}