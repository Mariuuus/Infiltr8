using System;
using UnityEngine;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject dialogueBox;
    private DialogController _dialogController;

    private Action _callback;
    public static DialogManager Instance { get; private set; }

    public void OnEnd()
    {
        _callback?.Invoke();
    }

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
        //DontDestroyOnLoad(gameObject); -> loses reference!
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

    public bool StartDialoque(DialogData d, Action callback=null)
    {
        _callback = callback;
        dialogueBox.SetActive(true);
        if(_dialogController == null) _dialogController = dialogueBox.GetComponent<DialogController>();
        if (_dialogController != null)
        { 
            return _dialogController.LoadNewDialogue(d);
        }
        return false;
    }
}
