using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScriptsUtilities.Properies.TypeSelector
{
    public static class DrawerCache
    {
        private static Dictionary<string, PropertyCache> _cache;

        public static PropertyCache Use(SerializedProperty property, GUIContent label)
        {
            if(_cache == null) 
                _cache = new Dictionary<string, PropertyCache>();

            string propertyPath = property.propertyPath;
            PropertyCache cachedData;
            bool notLoaded = !_cache.TryGetValue(propertyPath, out cachedData);
            
            if(notLoaded)
            {
                cachedData = new PropertyCache(property, label);
                _cache.Add(propertyPath, cachedData);
            }

            return cachedData;
        }

        public static void DebugLog()
        {
            Debug.Log(string.Join(", ", _cache.Keys));
        }
    }
}