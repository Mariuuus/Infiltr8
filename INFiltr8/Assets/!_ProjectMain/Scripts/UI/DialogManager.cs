using UnityEngine;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject dialogueBox;
    private DialogController _dialogController;
    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        if (dialogueBox != null)
        {
            _dialogController = dialogueBox.GetComponent<DialogController>();
        }
        else
        {
            Debug.LogError("missing dialogueBox component! Please add a dialogueBox to the dialogueManager");
        }
    }

    public bool StartDialoque(DialogData d)
    {
        // Debug.Log(dialogueBox.activeSelf);
        dialogueBox.SetActive(true);
        if (_dialogController != null)
        {
           // Debug.Log(dialogueBox.activeSelf);
            return _dialogController.LoadNewDialogue(d);
        }

        return false;
    }
}
