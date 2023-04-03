using ScriptsUtilities.Views.ItemViewContainer;
using UnityEngine;
using UnityEngine.UI;

namespace QuestsSystem.View
{
    public class QuestView : InfoView
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _head;
        [SerializeField] private Text _body;
        [SerializeField] private Text _notes;
        private QuestInfo _info;

        public void Init(QuestInfo info)
        {
            _info = info;
            ObserTo(info);
        }

        protected override void UpdateInfo()
        {
            _name.text = _info.names.questName;
            _head.text = _info.head;
            _body.text = _info.body;
            _notes.text = ConvertNotes(_info.notes);
        }

        public string ConvertNotes(string[] notes)
        {
            string result = "";

            foreach (var note in notes)
                result += $"- {note};\n";

            return result;
        }
    }
}