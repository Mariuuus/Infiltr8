using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
      
    [SerializeField] private float range = 500.0f;

    [SerializeField] private float rotationSpeed = 3.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
          Vector3 offset = new Vector3(+0.15f, 0.30f, 0f);
          RaycastHit hit;
          if (Physics.Raycast(transform.position + offset, transform.TransformDirection(Vector3.back), out hit, math.INFINITY))
          { 
                Debug.DrawRay(transform.position + offset, transform.TransformDirection(Vector3.back) * math.INFINITY, Color.yellow); 
                Debug.Log("Did Hit"); 
          }
          else
          { 
                Debug.DrawRay(transform.position + offset,transform.TransformDirection(Vector3.back) * math.INFINITY, Color.white); 
                Debug.Log("Did not Hit"); 
          }
    }
}