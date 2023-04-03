using ScriptsUtilities.Views.ItemsContainer;
using System;
using UnityEngine;

namespace QuestsSystem
{
    [Serializable]
    public struct Messages
    {
        public string started;
        public string updated;
        public string failed;
        public string complited;
    }

    [Serializable]
    public struct Names
    {
        public bool haveFamily;
        public string familyName;
        public string questName;
    }

    public interface IQuestInfo
    {
        public string head { get; }
        public string body { get; }
        public string[] notes { get; }
        public Names names { get; }
        public Messages messages { get; }
    }

    public abstract class QuestInfo : MonoBehaviour, IQuestInfo, IInfo
    {
        public event Action OnChanged;

        public abstract string head { get; }
        public abstract string body { get; }

        [SerializeField] private string[] _notes;
        public string[] notes => _notes;

        [SerializeField] private Names _names;
        public Names names => _names;

        [SerializeField] private Messages _messages;

        public Messages messages => _messages;
    }
}