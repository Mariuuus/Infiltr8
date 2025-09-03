using System;
using System.Collections;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.Types;
using __ProjectMain.Scripts.Managers.Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace __ProjectMain.Scripts.Objects
{
    public class HackableDevice : MonoBehaviour
    {
        public DeviceComponent Component { get; private set; }
        public bool UnHacked { get; private set; } = true;

        public void Init(DeviceComponent component)
        {
            Component = component;
            SetMaterial(hackColor);
        }
        
        [SerializeField] private HackStatus hackColor;
        [SerializeField] private int cooldownTime;
    
        [SerializeField] private Material redMat;
        [SerializeField] private Material blueMat;
        [SerializeField] private Material greenMat;
        [SerializeField] private Material yellowMat;
        [SerializeField] private Material unHackedMaterial;
        
        
        [SerializeField] private MeshRenderer pRenderer;
    
        [SerializeField] private Canvas countdownUI;
        
        [SerializeField] private AudioClip hackSound;
        
        private Canvas _countDownUIInstance;
        private bool _onCooldown = false;
        private ActivationPlateController _plate = null;

        public void SetController(ActivationPlateController door) => _plate = door;
        public void ResetController() => _plate = null;

        public void Start()
        {
            UnHacked = true;
            ResetHackStatus();
        }

        public HackStatus GetHackColor()
        {
            return hackColor;
        }

        public void ResetHackStatus()
        {
            _plate?.RemoveHackStatus(GetHackColor());
            pRenderer.material = unHackedMaterial;
            UnHacked = true;
        }
    
        private void SetMaterial(HackStatus color) {
            Material material = null;
            UnHacked = false;
            
            switch (color) {
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
        
            pRenderer.material = material;
        }

        public void ChangeMaterial(HackStatus color)
        {
            if (_onCooldown) return;
            if(!UnHacked) _plate?.ChangeHackStatus(hackColor, color);
            if(UnHacked) _plate?.AddHackStatus(color);
            
            SfxManager.instance.PlaySfxClip(hackSound,.4f);

            StartCoroutine(Cooldown());
            StartCoroutine(UpdateUIFill()); 
            SetMaterial(color);
            hackColor = color;
        }
        
        IEnumerator UpdateUIFill()
        {
            if (_countDownUIInstance != null)
            {
                float elapsed = 0f;
                Image image = _countDownUIInstance.GetComponentInChildren<Image>();
            
                while (elapsed < cooldownTime && _onCooldown)
                {
                    elapsed += Time.deltaTime;
                    image.fillAmount = Mathf.Lerp(1f, 0f, elapsed / cooldownTime);
                    yield return null; 
                }
            
                image.fillAmount = 0f;
            }
        }
    
        IEnumerator Cooldown()
        {   
            _onCooldown = true;
            int remaining = cooldownTime;
            _countDownUIInstance = Instantiate(countdownUI, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            _countDownUIInstance.transform.SetParent(transform);

            while (remaining > 0)
            {
                yield return new WaitForSeconds (1);
                remaining--;
            }
        
            Destroy(_countDownUIInstance.GameObject());
            _onCooldown = false;
            yield return null;
        } 
    }
}