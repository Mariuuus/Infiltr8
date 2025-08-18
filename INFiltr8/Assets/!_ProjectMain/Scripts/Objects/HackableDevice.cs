using System;
using System.Collections;
using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.LevelEditor.Types;
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
        private Canvas _countDownUIInstance;
        private bool _onCooldown = false;
        private DoorController _door = null;

        public void SetController(DoorController door) => _door = door;
        public void ResetController() => _door = null;

        public void Start()
        {
            UnHacked = true;
            ResetHackStatus();
        }

        public HackStatus GetHackColor()
        {
            return hackColor;
        }

        private void ResetHackStatus()
        {
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
        
            _door?.HackChangeOfObjectInTrigger(hackColor, color);

            StartCoroutine(Cooldown());
            StartCoroutine(IpdateUIPos());
            StartCoroutine(UpdateUIFill()); 
            SetMaterial(color);
            hackColor = color;
        }

        IEnumerator IpdateUIPos()
        {
            while (_onCooldown)
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