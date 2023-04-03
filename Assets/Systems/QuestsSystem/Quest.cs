using System;
using UnityEngine;

namespace QuestsSystem
{
    [RequireComponent(typeof(QuestObserver))]
    [RequireComponent(typeof(QuestRewarder))]
    [RequireComponent(typeof(QuestInfo))]
    public class Quest : MonoBehaviour
    {
        [SerializeField] private bool _needsCompletion;
        [SerializeField] private bool _repeatedReward;
        [SerializeField] private bool _autoUpdated;
        [SerializeField] private QuestObserver _questObserver;
        [SerializeField] private QuestRewarder _questRewarder;
        [SerializeField] private QuestInfo _questInfo;
        [SerializeField] private Quest _next;

        public bool repeatedReward => _repeatedReward;
        public bool autoUpdated => _autoUpdated;
        public bool haveNext => _next != null;
        public bool failed => _questObserver.Failed();
        public bool complited => _questObserver.Complited();
        public float quality => _questObserver.SuccessRate();
        public QuestInfo info => _questInfo;
        public Quest next => _next;

        private void Start()
        {
            _questObserver = GetComponent<QuestObserver>();
            _questRewarder = GetComponent<QuestRewarder>();
            _questInfo = GetComponent<QuestInfo>();
        }

        public bool AllowedToReward()
        {
            if (failed)
                return false;

            if (!_needsCompletion)
                return true;

            return complited;
        }

        public void Reward() => _questRewarder.HandleReward(quality);
    }
}