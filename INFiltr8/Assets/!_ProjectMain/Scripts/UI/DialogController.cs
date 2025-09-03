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
    
    [SerializeField] private Image previousButton;
    [SerializeField] private Image nextButton;
    
    private bool _isInDialogue = false;
    private bool _isInLine = false;

    public bool LoadNewDialogue(DialogData d)
    {
        if (_isInDialogue) return false;
        dialogImage.SetActive(false);
        _isInDialogue = true;
        if(IngameManager.Instance) IngameManager.Instance.Pause();
        index = 0;
        dialogText.SetText(String.Empty);
        dialogueAmountText.SetText("0 / 0");
        lines = d.lines;
        dialogName.SetText(d.dialogName);
        SetDialogSprite(d.character);
        StartDialogueLines();
        return true;
    }
    
    public void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _isInDialogue)
        {
            NextLine();      
        }
    }

    public void OnRightClick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && _isInDialogue)
        {
            PreviousLine();
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

    private void NextLine()
    {
        if (_isInLine)
        {
            StopCoroutine(nameof(WriteLine));
            dialogText.SetText(lines[index]);
            SfxManager.instance.PlaySfxClip(soundEffect, .6f, false);
            _isInLine = false;
            return;
        }
        
        if (index < lines.Count - 1)
        {
            index++;
            StartDialogueLines();
        }
        else
        {
            gameObject.SetActive(false);
            if(IngameManager.Instance) IngameManager.Instance.Resume();
            _isInDialogue = false;
            DialogManager.Instance.OnEnd();
        }
    }

    private void PreviousLine()
    {
        if (index > 0)
        {
            if (_isInLine) StopCoroutine(nameof(WriteLine));
            index--;
            StartDialogueLines();
        }
    }
    private void StartDialogueLines()
    {
        previousButton.color = index == 0 ? Color.clear : Color.white;
        StartCoroutine(nameof(WriteLine));
    }
    IEnumerator WriteLine()
    {
        _isInLine = true;
        dialogText.SetText(String.Empty);
        dialogueAmountText.SetText(index + 1 + "/" + lines.Count);
        string temp = "";
        foreach (char c in lines[index])
        {
            temp += c;
            dialogText.SetText(temp);
            SfxManager.instance.PlaySfxClip(soundEffect, .3f, true);
            yield return new WaitForSeconds(textSpeed);
        }

        _isInLine = false;
    }
}
