using System.Collections.Generic;
using UnityEngine;

namespace QuestsSystem.View
{
    public class QuestsView : MonoBehaviour
    {
        [SerializeField] private QuestContainer _questContainer;
        [SerializeField] private RectTransform _questsList;
        [SerializeField] private QuestView _questTemplate;
        private List<QuestView> _quests;

        private void Start()
        {
            _quests = new List<QuestView>();
        }

        public void OnEnable()
        {
            _questContainer.OnContainerUpdated += ResetQuests;


        }

        public QuestView GenerateQuestView(Quest quest)
        {
            return null;
        }

        public void ResetQuests(ContainerUpdateType type, Quest quest)
        {
            switch (type)
            {
                case ContainerUpdateType.Added:

                    break;
                case ContainerUpdateType.Removed:
                    break;
                case ContainerUpdateType.Changed:
                    break;
                default:
                    break;
            }
        }
    }
}