using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    public TextMeshProUGUI dialogueAmountText;
    [SerializeField]
    public GameObject dialogImage;
    
    private bool isInDialogue = false;
    private bool isInLine = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        gameObject.SetActive(false);
        //dialogText.SetText(string.Empty);
        //dialogName.SetText("hackermax");
        // startDialogueLines();
    }

    public bool LoadNewDialogue(DialogData d)
    {
        if (isInDialogue) return false;
        
        gameObject.SetActive(true);
        isInDialogue = true;
        index = 0;
        dialogText.SetText(String.Empty);
        dialogueAmountText.SetText("0 / 0");
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
        dialogName.SetText("Character Name/Scene Name");
        dialogueAmountText.SetText("0 / 0");
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
        dialogueAmountText.SetText(index + 1 + "/" + lines.Count);
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
