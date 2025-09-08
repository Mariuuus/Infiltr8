using UnityEngine;

namespace __ProjectMain.Scripts.Objects
{
    public class TubeLampVariantController : MonoBehaviour
    {
        public void SetVariation(Variations variant)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            Light light = GetComponentInChildren<Light>();
            renderer.material.EnableKeyword("_EMISSION");
            
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
                default:
                    break;
            }
        }
    }
}