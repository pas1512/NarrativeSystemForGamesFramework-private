using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestsSystem
{
    public enum QuestUpdateType
    {
        New,
        Update,
        Failed,
        Complited
    }

    public enum ContainerUpdateType
    {
        Added,
        Removed,
        Changed
    }

    public class QuestContainer : MonoBehaviour
    {
        public event Action<QuestUpdateType, Quest> OnQuestUpdated;
        public event Action<ContainerUpdateType, Quest> OnContainerUpdated;

        [SerializeField] private List<Quest> _quests = new List<Quest>();
        [SerializeField] private int _framesPerUpdate = 25;
        private int _framesFromLastUpdate = 0;

        private void Update()
        {
            if(_framesFromLastUpdate >= _framesPerUpdate)
            {
                _framesFromLastUpdate = 0;

                if(_quests.Count > 0)
                    UpdateQuests();
            }

            ++_framesFromLastUpdate;
        }

        public void AddQuest(Quest quest)
        {
            OnContainerUpdated?.Invoke(ContainerUpdateType.Added, quest);
            OnQuestUpdated?.Invoke(QuestUpdateType.New, quest);
            _quests.Add(quest);
        }

        private void UpdateQuests()
        {
            foreach (var quest in _quests.ToArray())
            {
                if(quest.failed)
                {
                    OnQuestUpdated?.Invoke(QuestUpdateType.Failed, quest);

                    if(quest.autoUpdated)
                    {
                        OnContainerUpdated?.Invoke(ContainerUpdateType.Removed, quest);
                        _quests.Remove(quest);
                    }
                }
                else if(quest.complited)
                {
                    if(quest.haveNext)
                    {
                        if (quest.autoUpdated)
                        {
                            OnContainerUpdated?.Invoke(ContainerUpdateType.Changed, quest);
                            _quests.Add(quest.next);
                            _quests.Remove(quest);
                        }

                        OnQuestUpdated?.Invoke(QuestUpdateType.Update, quest);
                    }
                    else
                    {
                        if (quest.autoUpdated)
                        {
                            OnContainerUpdated?.Invoke(ContainerUpdateType.Removed, quest);
                            _quests.Remove(quest);
                        }

                        OnQuestUpdated?.Invoke(QuestUpdateType.Complited, quest);
                    }
                }
            }
        }
    }
}