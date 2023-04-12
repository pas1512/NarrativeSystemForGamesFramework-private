using System;
using UnityEditor;
using UnityEngine;

namespace ScriptsUtilities.Properies.TypeSelector
{
#if UNITY_EDITOR
    public class PropertyDrawUtility
    {
        private Rect _position;
        private GUIContent _label;

        private SerializedProperty _property;
        public SerializedProperty property => _property;
        public GUIContent valuesLabel => MakePropertyLabel(property);

        private bool _positionSeted;
        public bool positionSeted => _positionSeted;
        public bool positionNotSeted => !_positionSeted;

        public float selectorHeight => EditorGUIUtility.singleLineHeight;

        public float valuesHeight =>
            _property == null ? 
            EditorGUIUtility.singleLineHeight :
            EditorGUI.GetPropertyHeight(_property, valuesLabel, true);

        public Rect selectorRect => GetTypeSelectorRect(_position, _label);

        public Rect valuesRect => GetValuesRect(_position, _property);

        public PropertyDrawUtility(SerializedProperty property, GUIContent label)
        {
            _property = property;
            _label = label;
        }

        public PropertyDrawUtility(SerializedProperty property, GUIContent label, Rect position)
        {
            _property = property;
            _label = label;
            _position = position;
        }

        private Rect GetTypeSelectorRect(Rect position, GUIContent label)
        {
            if (label == null)
                label = new GUIContent("None");

            int id = GUIUtility.GetControlID(FocusType.Passive);
            Rect preLeb = EditorGUI.PrefixLabel(position, id, label);
            return new Rect(preLeb.x, preLeb.y, preLeb.width, selectorHeight);
        }

        private Rect GetValuesRect(Rect position, SerializedProperty property)
        {
            if(property == null)
                return new Rect(position.x, position.y + selectorHeight, position.width, EditorGUIUtility.singleLineHeight);
            
            float valuesHeight = EditorGUI.GetPropertyHeight(property, valuesLabel, true);
            float positionY = position.y + selectorHeight;
            return new Rect(position.x, positionY, position.width, valuesHeight);
        }

        private GUIContent MakePropertyLabel(SerializedProperty property)
        {
            if (property == null)
                return new GUIContent("None");

            Type propertyType = property.GetValueType();

            if (propertyType == null)
                return new GUIContent("None");

            return new GUIContent(property.GetValueType().Name);
        }
    }
#endif
}