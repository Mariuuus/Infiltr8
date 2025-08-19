using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using __ProjectMain.Scripts.UI.MainMenuElements;

namespace __ProjectMain.Scripts.UI
{
    public class DialogAreaLinesList : MonoBehaviour
    {
        [SerializeField] private TMP_Text lineText;
        [SerializeField] private Button removeButton;
        private DialogAreaSettings settings;

        private int index = 0;
        public void Init(string line, int index, DialogAreaSettings settings)
        {
            lineText.text = line;
            this.index = index;
            this.settings = settings;
            removeButton.onClick.AddListener(() => this.settings.DeleteLineFromList(index));
        }
    }
}