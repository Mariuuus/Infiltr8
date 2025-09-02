using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using __ProjectMain.Scripts.Managers.Audio;
using __ProjectMain.Scripts.Managers.Ingame;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    private List<string> lines { get; set; } 
    public int index { get; set; }
    
    public float textSpeed = 0.025f;
    [SerializeField]
    private TextMeshProUGUI dialogText;
    [SerializeField]
    private TextMeshProUGUI dialogName;
    [SerializeField] 
    private TextMeshProUGUI dialogueAmountText;
    [SerializeField]
    private GameObject dialogImage;

    [SerializeField] private Sprite usbSprite;
    [SerializeField] private Sprite laptopSprite;
    [SerializeField] private AudioClip soundEffect;
    
    private bool isInDialogue = false;
    private bool isInLine = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        //gameObject.SetActive(false);
        //dialogText.SetText(string.Empty);
        //dialogName.SetText("hackermax");
        // startDialogueLines();
    }

    public bool LoadNewDialogue(DialogData d)
    {
        
        Debug.Log("LoadNewDialogue");
        if (isInDialogue) return false;
        
        // gameObject.SetActive(true);
        dialogImage.SetActive(false);
        isInDialogue = true;
        if(IngameManager.Instance) IngameManager.Instance.Pause();
        index = 0;
        dialogText.SetText(String.Empty);
        dialogueAmountText.SetText("0 / 0");
        lines = d.lines;
        dialogName.SetText(d.dialogName);
        SetDialogSprite(d.character);
        startDialogueLines();
        return true;
    }
    
    public void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isInDialogue)
        {
            nextLine();      
        }
    }

    public void OnRightClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isInDialogue)
        {
            previousLine();
        }
    }

    public void SetDialogSprite(characters c)
    {
        switch (c)
        {
            case characters.USBStick:
                dialogImage.SetActive(true);
                dialogImage.GetComponent<Image>().sprite = usbSprite;
                break;
            case characters.Schlappy:
                dialogImage.SetActive(true);
                dialogImage.GetComponent<Image>().sprite = laptopSprite;
                break;
            case characters.None: 
                dialogImage.SetActive(false);
                break;
            default:
                break;
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
            if(IngameManager.Instance) IngameManager.Instance.Resume();
            isInDialogue = false;
            DialogManager.Instance.OnEnd();
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
        if(IngameManager.Instance) IngameManager.Instance.Resume();
        dialogName.SetText("Character Name/Scene Name");
        dialogueAmountText.SetText("0 / 0");
        dialogText.SetText(String.Empty);
        gameObject.SetActive(false);
        isInDialogue = false;
        DialogManager.Instance.OnEnd();
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
            //var inNum = (int)c;
            SfxManager.instance.PlaySfxClip(soundEffect, .3f, true);
            yield return new WaitForSeconds(textSpeed);
        }

        isInLine = false;
    }
}
