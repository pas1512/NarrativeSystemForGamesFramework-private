using UnityEngine;

namespace QuestsSystem
{
    public interface IQuestRewarder
    {
        public void HandleReward(float quality);
    }

    public abstract class QuestRewarder : MonoBehaviour, IQuestRewarder
    {
        public abstract void HandleReward(float quality);
    }
}