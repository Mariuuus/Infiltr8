using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class DialogController : MonoBehaviour
{
    private List<string> lines { get; set; }
    private int index { get; set; }

    public float textSpeed = 0.025f;
    [SerializeField]
    public TextMeshProUGUI dialogText;
    [SerializeField]
    public TextMeshProUGUI dialogName;
    [SerializeField]
    public Sprite dialogImage;

    private bool isInDialogue = false;
    private bool isInLine = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       gameObject.SetActive(false);
    }

    public void loadNewDialogue(DialogData d)
    {
        if (isInDialogue) return;
        
        gameObject.SetActive(true);
        isInDialogue = true;
        index = 0;
        dialogText.SetText(String.Empty);
        lines = d.msg;
        dialogName.SetText(d.npc_name);
        dialogImage = d.avatar;
        startDialogueLines();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            nextLine();
        }
    }

    void nextLine()
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

    void startDialogueLines()
    {
        StartCoroutine(writeLine());
    }
    IEnumerator writeLine()
    {
        isInLine = true;
        dialogText.SetText(String.Empty);
        string temp = "";
        foreach (char c in lines[index].ToCharArray())
        {
            temp += c;
            dialogText.SetText(temp);
            yield return new WaitForSeconds(textSpeed);
        }

        isInLine = false;
    }
}
