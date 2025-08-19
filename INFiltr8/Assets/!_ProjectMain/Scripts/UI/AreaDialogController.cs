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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dialogTriggered)
        {
            dialogTriggered = true;
            Debug.Log("triggered");
            DialogManager.Instance.StartDialoque(_dialog);
        }
    }

    public void SetDialog(DialogData d)
    {
        this._dialog = d;
    }
}
