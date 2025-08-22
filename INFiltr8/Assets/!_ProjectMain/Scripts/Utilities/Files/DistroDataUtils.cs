using System.Collections.Generic;
using System.IO;
using __ProjectMain.Scripts.Objects;
using UnityEngine;

namespace __ProjectMain.Scripts.Utilities.Files
{
    public class DistroDataUtils
    {
        public static Distro[] Distros;

        static DistroDataUtils()
        {
            Distros = Resources.LoadAll<Distro>( "Images/Distros/Data");
        }

        public static Distro GetDistroByType(DistroType type)
        {
            foreach (var distro in Distros)
            {
                if (distro.distroName == type)
                {
                    return distro;
                }
            }
            throw new FileNotFoundException("There seems to be no Distro with that type");
        }
        
        public static Texture2D TextureFromSprite(Sprite sprite)
        {
            if(!Mathf.Approximately(sprite.rect.width, sprite.texture.width)){
                Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
                Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
                    (int)sprite.textureRect.y, 
                    (int)sprite.textureRect.width, 
                    (int)sprite.textureRect.height );
                newText.SetPixels(newColors);
                newText.Apply();
                return newText;
            } else
                return sprite.texture;
        }
    }
}