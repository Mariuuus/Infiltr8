using System;
using UnityEngine;

public class AreaDialogController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private DialogData _dialog;
    private bool dialogTriggered;
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        return; 
    }

    //TODO: check when player is standing inside but not triggered yet (like with laptops)
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dialogTriggered)
        {
            dialogTriggered = DialogManager.Instance.StartDialoque(_dialog);
        }
    }

    public void SetDialog(DialogData d)
    {
        this._dialog = d;
    }
}
