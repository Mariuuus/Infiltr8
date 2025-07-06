using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DialogController : MonoBehaviour
{
    private List<string> lines { get; set; }
    public int index { get; set; }
    public float textSpeed = 0.025f;
    [SerializeField]
    public TextMeshProUGUI dialogText;
    [SerializeField]
    public TextMeshProUGUI dialogName;
    [SerializeField]
    public GameObject dialogImage;

    private bool isInDialogue = false;
    private bool isInLine = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        gameObject.SetActive(false);
    }

    public bool LoadNewDialogue(DialogData d)
    {
        if (isInDialogue) return false;
        
        gameObject.SetActive(true);
        isInDialogue = true;
        index = 0;
        dialogText.SetText(String.Empty);
        lines = d.msg;
        dialogName.SetText(d.npc_name);
        dialogImage.GetComponent<Image>().sprite = d.avatar;
        startDialogueLines();
        return true;
    }
    
    public void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            nextLine();      
        }
    }

    public void OnRightClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            previousLine();
        }
    }

    private void nextLine()
    {
        if (isInLine) return;
        
        if (index < lines.Count - 1)
        {
            index++;
            startDialogueLines();
        }
        else
        {
            gameObject.SetActive(false);
            isInDialogue = false;
        }
    }

    private void previousLine()
    {
        if (isInLine) return;

        if (index > 0)
        {
            index--;
            startDialogueLines();
        }
    }

    public void skipDialogue()
    {
        dialogText.SetText(String.Empty);
        gameObject.SetActive(false);
    }

    private void startDialogueLines()
    {
        StartCoroutine(writeLine());
    }
    IEnumerator writeLine()
    {
        isInLine = true;
        dialogText.SetText(String.Empty);
        string temp = "";
        foreach (char c in lines[index])
        {
            temp += c;
            dialogText.SetText(temp);
            yield return new WaitForSeconds(textSpeed);
        }

        isInLine = false;
    }
}
