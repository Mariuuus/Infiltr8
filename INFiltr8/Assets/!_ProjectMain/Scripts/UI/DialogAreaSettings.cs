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
        public DialogAreaComponent _dialogAreaComponent;
        public TMP_InputField dialogNameField;
        [SerializeField] private GameObject container;
        [SerializeField] private GameObject listElement;

        public void Show(DialogAreaComponent dialogAreaComponent)
        {
            this._dialogAreaComponent = dialogAreaComponent;
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

        public void OnAddDialogLine(string newLine)
        {
            if (newLine.Length == 0) throw new InvalidLevelEditorActionException("new Dialog Line cant be empty");
            _dialogAreaComponent.dialog.lines.Add(newLine);
            updateDialogLinesList();
            LevelEditorFileManager.Instance.QuickSave();
        }

        public void onAddDialogImage()
        {
            // TODO: think of a way on how to "upload" a picture, or we always just use the standard icon (walkie talkie)
            // Marius: maybe we should use a selector to choose 4/5 different characters (-> more diverse, but "fairly easy" to implement)
        }

        public void updateDialogLinesList()
        {
            for (int i = container.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(container.transform.GetChild(i).gameObject);
            }
            
            foreach (var line in _dialogAreaComponent.dialog.lines)
            {
                GameObject newElement = Instantiate(listElement, container.transform);
                
                //TODO: Update lines in UI 
            }
        }
    }
}