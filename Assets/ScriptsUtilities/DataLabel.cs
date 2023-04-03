using UnityEngine;

namespace ScriptsUtilities
{
    public class DataLabel<T>
    {
        private string _name;
        private T _data;
        private Color _color;

        public DataLabel(string name)
        {
            _name = name;
            _color = Color.white;
        }

        public DataLabel(string name, Color color)
        {
            _name = name;
            _color = color;
        }

        public void Show()
        {
            GUI.skin.label.normal.textColor = _color;
            GUILayout.Label($"{_name}: {_data}");
        }

        public void SetData(T data)
        {
            _data = data;
        }

        public void SetName(string name)
        {
            _name = name;
        }
    }
}