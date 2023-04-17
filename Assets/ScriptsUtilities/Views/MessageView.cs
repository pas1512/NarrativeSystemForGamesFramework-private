using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScriptsUtilities.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class MessageView : MonoBehaviour
    {
        [SerializeField] private Text _text;
        private Action<int> _callback;

        public void Show(string text)
        {
            _text.text = text;
            gameObject.SetActive(true);
        }

        public void Show(string text, Action<int> callback)
        {
            _text.text = text;
            gameObject.SetActive(true);
            _callback = callback;
        }

        public void Close(int value)
        {
            _callback?.Invoke(value);
            _callback = null;
            gameObject.SetActive(false);
        }
    }
}
