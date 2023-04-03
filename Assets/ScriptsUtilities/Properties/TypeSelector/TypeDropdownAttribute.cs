using System;
using UnityEngine;

namespace ScriptsUtilities.Properies.TypeSelector
{
    public class TypeDropdownAttribute : PropertyAttribute
    {
        private Type _baseType;
        public Type baseType => _baseType;

        public TypeDropdownAttribute(Type baseType)
        {
            _baseType = baseType;
        }
    }
}