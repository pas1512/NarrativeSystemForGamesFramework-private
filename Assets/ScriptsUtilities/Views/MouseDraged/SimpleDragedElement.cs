using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;

namespace ScriptsUtilities.Views.MouseDraged
{
    public class SimpleDragedElement : DragedElement
    {
        public override RectTransform GetControl() => (RectTransform)transform;

        public override bool Init(InputButton button) => true;

        public override void Complete() { }
    }
}