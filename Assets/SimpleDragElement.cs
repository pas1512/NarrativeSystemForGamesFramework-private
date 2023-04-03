using ScriptsUtilities.Views.MouseDraged;
using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;

public class SimpleDragElement : DragedElement
{
    public override RectTransform GetControl() => (RectTransform) transform;

    public override bool Init(InputButton button) => true;

    public override void Complete() { }
}