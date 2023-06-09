﻿using System;
using System.Collections;
using System.Reflection;
using UnityEditor;
using System.Linq;

namespace ScriptsUtilities.Properies
{
#if UNITY_EDITOR
    public static class PropertyExtends
    {
        public static void SetValue(this SerializedProperty prop, object value)
        {
            var path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            var elements = path.Split('.');

            foreach (var element in elements.Take(elements.Length - 1))
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }

            if (ReferenceEquals(obj, null)) 
                return;

            try
            {
                var element = elements.Last();

                if (element.Contains("["))
                {
                    var tp = obj.GetType();
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    
                    var field = tp.GetField(elementName, 
                        BindingFlags.Public | 
                        BindingFlags.NonPublic | 
                        BindingFlags.Instance);
                    
                    var arr = field.GetValue(obj) as IList;

                    if (arr != null) 
                        arr[index] = value;
                }
                else
                {
                    var tp = obj.GetType();

                    var field = tp.GetField(element, 
                        BindingFlags.Public | 
                        BindingFlags.NonPublic | 
                        BindingFlags.Instance);
                    
                    if (field != null)
                        field.SetValue(obj, value);
                }

            }
            catch
            {
                return;
            }
        }

        public static object GetValue(this SerializedProperty property)
        {
            var path = property.propertyPath.Replace(".Array.data[", "[");
            object obj = property.serializedObject.targetObject;
            var elements = path.Split('.');

            foreach (var element in elements)
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = GetValue_Imp(obj, elementName, index);
                }
                else
                {
                    obj = GetValue_Imp(obj, element);
                }
            }

            return obj;
        }

        public static Type GetValueType(this SerializedProperty property)
        {
            string name = property.type;
            name = name.Replace("managedReference<", "").Replace(">", "");
            return Type.GetType(name);
        }

        private static object GetValue_Imp(object source, string name)
        {
            if (source == null)
                return null;

            var type = source.GetType();

            while (type != null)
            {
                var f = type.GetField(name, 
                    BindingFlags.NonPublic | 
                    BindingFlags.Public | 
                    BindingFlags.Instance);

                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, 
                    BindingFlags.NonPublic | 
                    BindingFlags.Public | 
                    BindingFlags.Instance | 
                    BindingFlags.IgnoreCase);

                if (p != null)
                    return p.GetValue(source, null);

                type = type.BaseType;
            }

            return null;
        }

        private static object GetValue_Imp(object source, string name, int index)
        {
            var enumerable = GetValue_Imp(source, name) as IEnumerable;

            if (enumerable == null)
                return null;

            var enm = enumerable.GetEnumerator();

            for (int i = 0; i <= index; i++)
            {
                if (!enm.MoveNext())
                    return null;
            }

            return enm.Current;
        }
    }
#endif
}