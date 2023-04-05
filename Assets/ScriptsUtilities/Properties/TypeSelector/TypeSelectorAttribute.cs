using System;
using UnityEngine;

namespace ScriptsUtilities.Properies.TypeSelector
{
    public class TypeSelectorAttribute : PropertyAttribute
    {
        private Type _baseType;
        public Type baseType => _baseType;

        public TypeSelectorAttribute(Type baseType)
        {
            _baseType = baseType;
        }
    }
}