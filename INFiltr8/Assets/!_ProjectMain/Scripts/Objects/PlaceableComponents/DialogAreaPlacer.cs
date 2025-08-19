using __ProjectMain.Scripts.LevelEditor.Components;
using __ProjectMain.Scripts.Utilities.LevelEditor;
using UnityEngine;

namespace __ProjectMain.Scripts.Objects.PlaceableComponents
{
    public class DialogAreaPlacer : MonoBehaviour, IPlaceable<DialogAreaComponent>
    {
        public void Place(DialogAreaComponent component, params object[] args)
        {
            transform.localScale = PlacerUtils.CalcScale(component.startPosition, component.endPosition, 2);
            transform.position = PlacerUtils.CalcPosition(component.startPosition, component.endPosition,0.5f);

            var dialogController = GetComponent<AreaDialogController>();
            dialogController.SetDialog(component.dialog);
        }
    }
}