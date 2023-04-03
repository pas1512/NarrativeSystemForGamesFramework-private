using UnityEngine;

namespace QuestsSystem
{
    public interface IQuestObserver
    {
        public bool Failed();
        public bool Complited();
        public float SuccessRate();
    }

    public abstract class QuestObserver : MonoBehaviour, IQuestObserver
    {
        public abstract bool Failed();
        public abstract bool Complited();
        public abstract float SuccessRate();
    }
}