using System;
using System.Collections;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Objects;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class grabbableType : MonoBehaviour
{
    [SerializeField] private HackStatus hackColor;
    [SerializeField] private int cooldownTime;
    
    [SerializeField] private Material redMat;
    [SerializeField] private Material blueMat;
    [SerializeField] private Material greenMat;
    [SerializeField] private Material yellowMat;
    
    [SerializeField] private Canvas countdownUI;
    private Canvas _countDownUIInstance;
    
    private bool onCooldown = false;

    private DoorController _door = null;

    private void Start()
    {
        Init();
    }

    public void SetController(DoorController door) => _door = door;
    public void ResetController() => _door = null;

    public void Init()
    {
        setMaterial(hackColor);

    }
    public HackStatus getHackColor()
    {
        return hackColor;
    }
    
    private void setMaterial(HackStatus Color) {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = null;
        
        switch (Color) {
            case HackStatus.BlueHacked:
                material = blueMat;
                break;
            case HackStatus.RedHacked:
                material = redMat;
                break;
            case HackStatus.GreenHacked:
                material = greenMat;
                break;
            case HackStatus.YellowHacked:
                material = yellowMat;
                break;
        }
        
        renderer.material = material;
    }

    public void changeMaterial(HackStatus Color)
    {
        if (onCooldown) return;
        
        _door?.HackChangeOfObjectInTrigger(hackColor, Color);

        StartCoroutine(cooldown());
        StartCoroutine(updateUIpos());
        StartCoroutine(updateUIFill()); 
        setMaterial(Color);
        hackColor = Color;
    }

    IEnumerator updateUIpos()
    {
        while (onCooldown)
        {
            if (_countDownUIInstance != null)
            {
                _countDownUIInstance.transform.position = transform.position + new Vector3(0, 3, 0);
            }
            else
            {
                yield break;
            }
            
            yield return null;
        }
        
    }

    IEnumerator updateUIFill()
    {
        if (_countDownUIInstance != null)
        {
            float elapsed = 0f;
            Image image = _countDownUIInstance.GetComponentInChildren<Image>();
            
            while (elapsed < cooldownTime && onCooldown)
            {
                elapsed += Time.deltaTime;
                image.fillAmount = Mathf.Lerp(1f, 0f, elapsed / cooldownTime);
                yield return null; 
            }
            
            image.fillAmount = 0f;
        }
    }
    
     IEnumerator cooldown()
    {   
        onCooldown = true;
        int remaining = cooldownTime;
        // float decreaseStep = 1.0f / cooldownTime;
        _countDownUIInstance = Instantiate(countdownUI, transform.position + new Vector3(0, 3, 0), Quaternion.identity);

        while (remaining > 0)
        {
            yield return new WaitForSeconds (1);
            //float currentFill = _countDownUIInstance.GetComponentInChildren<Image>().fillAmount;
            //float smoothed = Mathf.Lerp(currentFill, currentFill - decreaseStep, 0.5f);
            //_countDownUIInstance.GetComponentInChildren<Image>().fillAmount -= smoothed;
            remaining--;
        }
        
        Destroy(_countDownUIInstance.GameObject());
        onCooldown = false;
        yield return null;
    } 
}

