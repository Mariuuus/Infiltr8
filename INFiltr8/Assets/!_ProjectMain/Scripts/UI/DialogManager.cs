using UnityEngine;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<DialogData> dialogues;
    private int count = 0;
    public GameObject dialogueBox;
    private DialogController _dialogController;

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
    
    public void nextDialogue()
    {
        if (count < dialogues.Count - 1)
        {
            startDialoque(dialogues[count]);
            count++;
        }
    }

    public void startDialoque(DialogData d)
    {
        if (_dialogController != null)
        {
            _dialogController.loadNewDialogue(d);
        }
    }
    
    public void addDialogue(DialogData d)
    {
        dialogues.Add(d);
    }
    
}
