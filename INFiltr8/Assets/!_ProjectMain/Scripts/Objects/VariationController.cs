using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace __ProjectMain.Scripts.Objects
{
    public class VariationController : MonoBehaviour

    {
        [SerializeField] private Material redVariant;
        [SerializeField] private Material blueVariant;
        [SerializeField] private Material yellowVariant;
        [SerializeField] private Material greenVariant;


        public void SetVariation(Variations variant)
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            Light spotLight = transform.parent.GetComponentInChildren<Light>();
            
            switch (variant)
            {
                case Variations.Blue:
                    renderer.material = blueVariant;
                    spotLight.color = new Color(0,  0.6039219f, 1, 1.0f);
                    break;
                case Variations.Red:
                    renderer.material = redVariant;
                    spotLight.color = new Color(1, 0, 0.0586f, 1);
                    break;
                case Variations.Yellow:
                    renderer.material = yellowVariant;
                    spotLight.color = new Color(0.8962f, 0.88532f, 0.1056871f, 1);
                    break;
                case Variations.Green:
                    renderer.material = greenVariant;
                    spotLight.color = new Color(0, 1, 0.3326967f, 1);
                    break;
                default:
                    break;
            }
        }
    }
}