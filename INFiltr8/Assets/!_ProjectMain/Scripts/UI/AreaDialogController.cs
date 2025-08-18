using System;
using UnityEngine;

public class AreaDialogController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private DialogData dialog;
    void Start()
    {
        if (dialog == null)
        {
            Debug.LogError("please add a dialog to the npc");
        }
    }

    // Update is called once per frame
    void Update()
    {
        return; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("triggered");
            DialogManager.Instance.StartDialoque(dialog);
        }
    }
}
