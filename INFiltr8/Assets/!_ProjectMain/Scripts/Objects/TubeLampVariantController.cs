using System;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class TubeLampVariantController : MonoBehaviour
    {
        private bool isRainbow = false;

        public void SetVariation(Variations variant)
        {
            Light light = GetComponentInChildren<Light>();
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            renderer.material.EnableKeyword("_EMISSION");
            light.gameObject.SetActive(true);
            isRainbow = false;
            
            switch (variant)
            {
                case Variations.Blue:
                    renderer.material.SetColor("_EmissionColor", new Color(0,  0.03137f, 0.74902f, 1.0f) * 512f);
                    light.color = Color.cyan;
                    break;
                case Variations.Red:
                    renderer.material.SetColor("_EmissionColor", new Color(1, 0, 0.0586f, 1) * 32f);
                    light.color = new Color(1, 0, 0.0586f, 1);
                    break;
                case Variations.Yellow:
                    renderer.material.SetColor("_EmissionColor", new Color(0.8962f, 0.88532f, 0.1056871f, 1) * 32f);
                    light.color = new Color(0.8962f, 0.88532f, 0.1056871f, 1);
                    break;
                case Variations.Green:
                    renderer.material.SetColor("_EmissionColor", new Color(0, 1, 0.3326967f, 1) * 32f);
                    light.color = new Color(0, 1, 0.3326967f, 1);
                    break;
                case Variations.Rainbow:
                    this.isRainbow = true;
                    light.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            if (!isRainbow) return;

            MeshRenderer renderer = GetComponent<MeshRenderer>();
            float hue = Mathf.Repeat(Time.time * 0.35f, 1f);
            Color rainbow = Color.HSVToRGB(hue, 1f, 1f);
            renderer.material.SetColor("_EmissionColor", rainbow * 32f);
        }
    }
}