using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.Managers.LevelEditor;
using __ProjectMain.Scripts.Utilities.Exceptions;
using TMPro;
using UnityEngine;

namespace __ProjectMain.Scripts.UI
{
    public class DialogAreaSettings : MonoBehaviour
    {
        private DialogAreaComponent _dialogAreaComponent;
        public TMP_InputField dialogNameField;
        public TMP_InputField newLineField;
        
        [SerializeField] private GameObject container;
        [SerializeField] private GameObject listElement;

        public void Show(DialogAreaComponent dialogAreaComponent)
        {
            this._dialogAreaComponent = dialogAreaComponent;
            this.dialogNameField.text = _dialogAreaComponent.dialog.dialogName;
            UpdateDialogLinesList();
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }

        public void OnDialogNameChange(string dialogName)
        {
            _dialogAreaComponent.dialog.dialogName = dialogName;
            LevelEditorFileManager.Instance.QuickSave();
        }

        public void OnAddDialogLine()
        {
            string newLine = newLineField.text;
            if (newLine.Length == 0) throw new InvalidLevelEditorActionException("new Dialog Line cant be empty");
            _dialogAreaComponent.dialog.lines.Add(newLine);
            UpdateDialogLinesList();
            LevelEditorFileManager.Instance.QuickSave();
            newLineField.text = string.Empty;
        }

        public void onAddDialogImage()
        {
            // TODO: think of a way on how to "upload" a picture, or we always just use the standard icon (walkie talkie)
            // Marius: maybe we should use a selector to choose 4/5 different characters (-> more diverse, but "fairly easy" to implement)
        }

        public void UpdateDialogLinesList()
        {
            for (int i = container.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(container.transform.GetChild(i).gameObject);
            }

            int index = 0;
            foreach (var line in _dialogAreaComponent.dialog.lines)
            {
                GameObject newElement = Instantiate(listElement, container.transform);
                newElement.GetComponent<DialogAreaLinesList>().Init(line, index, this);
                index++;
            }
        }

        public void DeleteLineFromList(int index)
        {
            _dialogAreaComponent.dialog.lines.RemoveAt(index);
            LevelEditorFileManager.Instance.QuickSave();
            UpdateDialogLinesList();
        }
    }
}