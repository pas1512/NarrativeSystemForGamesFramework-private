using System;
using UnityEditor;
using UnityEngine;

namespace ScriptsUtilities.Properies.TypeSelector
{
    public class PropertyCache
    {
        public Type selectedType { get; set; }
        public int selectedTypeId { get; set; }

        private PropertyDrawUtility _utility;
        public PropertyDrawUtility utility => _utility;

        private PropertyFieldsCache _fieldsCache;
        public PropertyFieldsCache fields => _fieldsCache;

        public PropertyCache(SerializedProperty property, GUIContent label)
        {
            _utility = new PropertyDrawUtility(property, label);
            _fieldsCache = new PropertyFieldsCache();
        }
    }
}