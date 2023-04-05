using System;
using UnityEditor;
using UnityEngine;

namespace ScriptsUtilities.Properies.TypeSelector
{
    public class PropertyCache
    {
        public Type selectedType { get; set; }
        public int selectedTypeId { get; set; }

        private PropertyFieldsCache _fieldsCache;
        public PropertyFieldsCache fields => _fieldsCache;

        public PropertyCache()
        {
            _fieldsCache = new PropertyFieldsCache();
        }
    }
}