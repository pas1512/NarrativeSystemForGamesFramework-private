using System;
using System.Collections.Generic;
using System.Reflection;

namespace ScriptsUtilities.Properies
{
    public class PropertyFieldsCache
    {
        private Dictionary<string, object> _cechedFiealds;

        public PropertyFieldsCache()
        {
            _cechedFiealds = new Dictionary<string, object>();
        }

        public object LoadFromCacheOrCreate(Type type)
        {
            if(type.IsAbstract) 
                return null;

            object createdObject = Activator.CreateInstance(type);

            if (_cechedFiealds == null)
                return createdObject;

            FieldInfo[] fields = GetAllFields(type);

            foreach (var field in fields)
            {
                if (_cechedFiealds.ContainsKey(field.Name))
                    field.SetValue(createdObject, _cechedFiealds[field.Name]);
            }

            return createdObject;
        }

        public void SaveInCache(object @object)
        {
            FieldInfo[] fields = GetAllFields(@object.GetType());

            foreach (FieldInfo field in fields)
            {
                if (_cechedFiealds.ContainsKey(field.Name))
                    _cechedFiealds[field.Name] = field.GetValue(@object);
                else
                    _cechedFiealds.Add(field.Name, field.GetValue(@object));
            }
        }

        private FieldInfo[] GetAllFields(Type type)
        {
            Type curentType = type;
            List<FieldInfo> list = new List<FieldInfo>();

            do
            {
                FieldInfo[] fields = curentType.GetFields(
                                         BindingFlags.NonPublic |
                                         BindingFlags.Instance);
                list.AddRange(fields);
                curentType = curentType.BaseType;
            } while (curentType != typeof(object));

            return list.ToArray();
        }
    }
}