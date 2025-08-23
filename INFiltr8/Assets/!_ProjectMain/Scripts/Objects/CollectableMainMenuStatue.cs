using System;
using System.Collections;
using System.Collections.Generic;
using __ProjectMain.Data;
using __ProjectMain.Scripts.Managers;
using __ProjectMain.Scripts.UI;
using __ProjectMain.Scripts.Utilities.Files;
using UnityEngine;
using Random = UnityEngine.Random;

namespace __ProjectMain.Scripts.Objects
{
    public class CollectableMainMenuStatue : MonoBehaviour, IClickableMenuDecoration
    {
        private Distro _distro;

        private bool _inAnimation;
        private bool _collected;

        public void Init(DistroType distroType, bool collect)
        {
            _collected = collect;
            _distro = DistroDataUtils.GetDistroByType(distroType);
            var rend = GetComponent<Renderer>();
            Material mat = rend.material;
            mat.SetTexture("_Distro", DistroDataUtils.TextureFromSprite(_distro.distroSprite));
            if (!collect)
            {
                mat.SetFloat("_Opacity", 0.15f);
            }
        }

        public void OnHoverStart()
        {
            if (!_inAnimation && _collected) StartCoroutine(nameof(SpinWithJump));
        }

        public void OnHoverEnd()
        {
        }

        public void OnClick()
        {
        }
        
        IEnumerator SpinWithJump()
        {
            _inAnimation = true;
            float duration = 1f;
            float jumpHeight = 0.2f;
            Vector3 startPos = transform.position;
            Vector3 startRot = transform.eulerAngles;
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                transform.eulerAngles = startRot + new Vector3(0f, 360f * t, 0f);
                transform.position = startPos + Vector3.up * Mathf.Sin(t * Mathf.PI) * jumpHeight;

                yield return null;
            }

            transform.eulerAngles = startRot + new Vector3(0f, 360f, 0f);
            transform.position = startPos;
            _inAnimation = false;
        }
    }
}