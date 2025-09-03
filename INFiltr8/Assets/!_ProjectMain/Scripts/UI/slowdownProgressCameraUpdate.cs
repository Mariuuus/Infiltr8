using UnityEngine;

public class slowdownProgressCameraUpdate : MonoBehaviour
{
    public Camera mainCamera; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Update()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up);
    }
}
