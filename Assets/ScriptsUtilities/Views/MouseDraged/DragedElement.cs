using UnityEngine;
using static UnityEngine.EventSystems.PointerEventData;

namespace ScriptsUtilities.Views.MouseDraged
{
    public abstract class DragedElement : MonoBehaviour
    {
        public abstract bool Init(InputButton button);
        public abstract RectTransform GetControl();
        public abstract void Complete();
    }
}
