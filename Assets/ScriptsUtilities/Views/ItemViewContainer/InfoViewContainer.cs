using UnityEngine;
using System.Collections.Generic;
using ScriptsUtilities.Views.ItemViewContainer;

namespace ScriptsUtilities.Views.ItemsContainer
{
    public abstract class InfoViewContainer : MonoBehaviour
    {
        [SerializeField] private InfoView _template;
        [SerializeField] private List<InfoView> _members;

        public void Init(IInfo[] elements)
        {
            if(_members == null)
                _members = new List<InfoView>();

            ClearMembers();

            foreach(var element in elements)
            {
                InfoView infoView = Instantiate(_template, transform);
                infoView.ObserTo(element);
                _members.Add(infoView);
            }
        }

        public void ClearMembers()
        {
            if (_members != null && _members.Count > 0)
            {
                GameObject removed;

                for (int i = _members.Count - 1; i >= 0; i--)
                {
                    removed = _members[i].gameObject;
                    Destroy(removed);
                }

                _members.Clear();
            }
        }
    }
}