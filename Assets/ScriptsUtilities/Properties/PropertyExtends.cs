using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using UnityEditor;

namespace ScriptsUtilities.Properies
{
    public static class PropertyExtends
    {
        public static void SetValue (this SerializedProperty property, object value)
        {
            BindingFlags fieldsProperty = BindingFlags.NonPublic | BindingFlags.Instance;

            string path = property.propertyPath;
            path = path.Replace(".Array.data[", ".").Replace("]", "");
            string[] elements = path.Split('.');
            path = elements[0];

            object targetObject = property.serializedObject.targetObject;
            FieldInfo fieldInfo = targetObject.GetType().GetField(path, fieldsProperty);

            object propertyObject = fieldInfo.GetValue(targetObject);

            if (propertyObject!= null && propertyObject.GetType().IsArray)
            {
                int id = int.Parse(elements[elements.Length - 1]);
                (propertyObject as object[])[id] = value;
            }
            else
            {
                fieldInfo.SetValue(targetObject, value);
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
                var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (f != null)
                    return f.GetValue(source);

                var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

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
}