using UnityEngine;
using System.Collections.Generic;
using ScriptsUtilities.Views.ItemViewContainer;

namespace ScriptsUtilities.Views.ItemsContainer
{
    public abstract class InfoViewContainer<T, I> : MonoBehaviour where T : InfoView where I : IInfo
    {
        [SerializeField] private T _template;

        private List<T> _members;

        protected virtual void InitInfoElement(T element, I info)
        {
            element.ObserTo(info);
            element.gameObject.SetActive(true);
            _members.Add(element);
        }

        public void Init(I[] elements)
        {
            if(_members == null)
                _members = new List<T>();

            ClearMembers();

            foreach(var element in elements)
            {
                T infoView = Instantiate(_template, transform);
                InitInfoElement(infoView, element);
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