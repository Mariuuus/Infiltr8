using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
      
    [SerializeField] private float maxDistance = 5.0f;
    [SerializeField] private GameObject FOV;
    public int rotationCooldownTime = 2;
    public float rotationSpeed = 3.0f;
    public bool rotateRight = true;
    public bool disableRotation = false;
    private int mask;

    private bool rotationCoolDown = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Init();
    }

    public void Init()
    {
       // StartCoroutine(rotateCamera());
        mask = LayerMask.GetMask("Wall");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = transform.position + new Vector3(0f, 0.30f, 0f);
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        Vector3 directionLeft = Quaternion.AngleAxis(-30f, Vector3.up) * transform.forward;
        Vector3 directionRight = Quaternion.AngleAxis(30f, Vector3.up) * transform.forward;

        //RaycastHit hit;
        RaycastHit hitLeft;
        RaycastHit hitRight;
        RaycastHit hitMiddle;
        RaycastHit hitRightMiddle;
        RaycastHit HitLeftMiddle;
        
        drawRayCast(origin, direction, out hitMiddle, maxDistance / 1.35f, mask, true);
        //drawRayCast(origin, direction, out hit, maxDistance, mask, false);
        drawRayCast(origin, directionLeft, out hitLeft, maxDistance * 1.15f, mask, false);
        drawRayCast(origin, directionRight, out hitRight, maxDistance * 1.15f, mask, false);
        drawRayCast(origin, directionLeft, out HitLeftMiddle, maxDistance * 0.9f, mask, true);
        drawRayCast(origin, directionRight, out hitRightMiddle, maxDistance * 0.9f, mask, true);

        if (disableRotation) return;
        float rotateDirection = rotateRight ? 1f : -1f;
        transform.Rotate(0f, rotateDirection * rotationSpeed * Time.deltaTime, 0f);
    }

    private void drawRayCast(Vector3 origin, Vector3 direction, out RaycastHit hit, float maxDistance, LayerMask layer, bool isDisabling)
    {
        if (Physics.Raycast(origin, direction, out hit, maxDistance, layer))
        {
            if (isDisabling)
            {
                Debug.DrawRay(origin, direction * hit.distance, Color.red);     
            }
            else
            {
                Debug.DrawRay(origin, direction * hit.distance, Color.yellow);  
            }
            changeRotationDirection();
            if (isDisabling)
            {
               FOV.GetComponent<CameraFOVController>().EnableInactiveMode();
            }
        }
        else
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.white);
            if (isDisabling)
            {
                FOV.GetComponent<CameraFOVController>().DisableInactiveMode();
            }
        }
    }

    IEnumerator rotateCamera()
    {
        Vector3 startRot = transform.eulerAngles;

        while(true)
        {
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / rotationSpeed;
                float angle = Mathf.Lerp(0f, 360f, t);
                transform.eulerAngles = startRot + new Vector3(0f, rotateRight ? angle : -angle, 0f);
                yield return null;
            }
        }
    }

    private void changeRotationDirection()
    {
        if (rotationCoolDown) return;
        this.rotateRight = !rotateRight;
        StartCoroutine(rotationCooldownCoroutine());
    }
    
    IEnumerator rotationCooldownCoroutine()
    {
        rotationCoolDown = true;
        int remainingTime = rotationCooldownTime;
        while (remainingTime > 0)
        {
               
            yield return new WaitForSeconds(1);
            remainingTime--;
        }

        rotationCoolDown = false;
        yield return null;
    }
}