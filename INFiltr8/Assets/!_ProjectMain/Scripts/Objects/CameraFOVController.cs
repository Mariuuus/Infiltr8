using System;
using __ProjectMain.Scripts.Managers.TimeTracker;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFOVController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool isInactive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isInactive) return;
        Debug.Log("caught player");
        LevelTimeTracker.Instance?.SetMultiplier(2f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (isInactive) return;
        Debug.Log("player got out of sight");
        LevelTimeTracker.Instance?.SetMultiplier(1f);
    }

    public void EnableInactiveMode()
    {
        isInactive = true;
        var renderer = GetComponent<Renderer>();
        Material mat = renderer.material;
        mat.SetColor("_Color", new Color(0.74901f, 0.15686f, 0.00784f, 1f) * 1f);
    }

    public void DisableInactiveMode()
    {
        isInactive = false;
        var renderer = GetComponent<Renderer>();
        Material mat = renderer.material;
        mat.SetColor("_Color", new Color(0.74901f, 0.15686f, 0.00784f, 1f) * 32f);
    }
}
