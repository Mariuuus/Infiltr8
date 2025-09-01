using __ProjectMain.Scripts.Objects;
using UnityEngine;

namespace __ProjectMain.Scripts.Player
{
    public class HighlightController : MonoBehaviour
    {
        private IHighlightByPlayer _currentHighlight;

        public void ChangeHighlightByPlayer(IHighlightByPlayer highlightByPlayer)
        {
            Debug.Log("Change highlight by player");
            if (_currentHighlight != null && _currentHighlight != highlightByPlayer)
            {
                _currentHighlight.StopHighlight();
            }
            _currentHighlight = highlightByPlayer;
        }

        public void RemoveHighlightReference()
        {
            _currentHighlight = null;
        }
    }
}