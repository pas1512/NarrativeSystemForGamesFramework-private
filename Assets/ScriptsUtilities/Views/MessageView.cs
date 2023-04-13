using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScriptsUtilities.Views
{
    [RequireComponent(typeof(RectTransform))]
    public class MessageView : MonoBehaviour
    {
        public event Action<int> OnClosed;
        [SerializeField] private Text _text;

        public void Show(string text)
        {
            _text.text = text;
            gameObject.SetActive(true);
        }

        public void Close(int value)
        {
            OnClosed?.Invoke(value);
            gameObject.SetActive(false);
        }
    }
}
