using UnityEngine;
using System.Collections.Generic;
using ScriptsUtilities.Views.ItemViewContainer;

namespace ScriptsUtilities.Views.ItemsContainer
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class InfoViewContainer<ContainerType, InfoViewType, InfoType> : MonoBehaviour 
        where InfoViewType : InfoView 
        where InfoType : IInfo
        where ContainerType : InfoContainer<InfoType>
    {
        [SerializeField] private InfoViewType _template;
        [SerializeField] private RectTransform _containerElement;

        [SerializeField] private ContainerType _container;
        protected ContainerType container => _container;

        private List<InfoViewType> _members;

        protected virtual void Start()
        {
            _members = new List<InfoViewType>();

            foreach (var element in _container.info)
            {
                InfoViewType infoView = Instantiate(_template, _containerElement);
                InitInfoElement(infoView, element);
            }
        }

        protected virtual void OnValidate()
        {
            if (_containerElement == null)
                _containerElement = (RectTransform)transform;
        }

        protected virtual void OnEnable()
        {
            _container.OnInfoChanged += RenewMembers;
        }

        protected virtual void OnDisable()
        {
            _container.OnInfoChanged -= RenewMembers;
        }

        protected virtual void InitInfoElement(InfoViewType element, InfoType info)
        {
            element.ObserTo(info);
            element.gameObject.SetActive(true);
            _members.Add(element);
        }

        private void RenewMembers()
        {
            if (_members == null)
                _members = new List<InfoViewType>();

            ClearMembers();

            foreach (var element in _container.info)
            {
                InfoViewType infoView = Instantiate(_template, transform);
                InitInfoElement(infoView, element);
            }
        }

        private void ClearMembers()
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