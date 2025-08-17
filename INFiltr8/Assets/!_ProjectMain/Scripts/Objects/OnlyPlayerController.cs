using System;
using __ProjectMain.Scripts.Managers.MainMenu;
using __ProjectMain.Scripts.Player;
using UnityEngine;
using LevelLoaderManager = __ProjectMain.Scripts.Managers.Level.LevelLoaderManager;

namespace __ProjectMain.Scripts.Objects
{
    public class OnlyPlayerController : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("grabbable"))
            {
                var player = LevelLoaderManager.Instance.playerObject.GetComponent<GrabController>();
                player.DropObject(player.HeldObj);
            }
        }
    }
}