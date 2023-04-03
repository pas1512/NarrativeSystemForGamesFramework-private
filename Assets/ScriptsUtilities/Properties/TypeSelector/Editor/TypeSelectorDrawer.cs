﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptsUtilities.Properies.TypeSelector.Editor
{
    [CustomPropertyDrawer(typeof(TypeDropdownAttribute))]
    public class TypeSelectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //===========BeginPropertyAndSaveIndent============
            EditorGUI.BeginProperty(position, label, property);
            
            var indent = EditorGUI.indentLevel;
            
            EditorGUI.indentLevel = 0;
            //=================================================



            //==================MainActions====================
            PropertyCache propertyCache = DrawerCache.Use(property, label);

            Type baseType = (attribute as TypeDropdownAttribute).baseType;
            string[] options = GetPropertyTypes(baseType);

            if (property.GetValueType() == null)
            {
                object newValues = propertyCache.fields.LoadFromCacheOrCreate(baseType);
                property.SetValue(newValues);
            }
            else
            {
                object propertyValue = property.GetValue();
                propertyCache.fields.SaveInCache(propertyValue);
                propertyCache.selectedType = propertyValue.GetType();
                propertyCache.selectedTypeId = Array.IndexOf(options, propertyValue.GetType().Name);
            }

            PropertyDrawUtility utility = propertyCache.utility;
            utility.UpdatePosition(position);

            using (var check = new EditorGUI.ChangeCheckScope())
            {
                int selectedTypeId = propertyCache.selectedTypeId;
                DrawTypeSelectorGUI(utility, options, selectedTypeId, out selectedTypeId);

                if (check.changed)
                {
                    propertyCache.selectedTypeId = selectedTypeId;

                    Type selectedType = Assembly.GetAssembly(fieldInfo.FieldType).GetType(options[selectedTypeId]);
                    propertyCache.selectedType = selectedType;

                    object newValues = propertyCache.fields.LoadFromCacheOrCreate(selectedType);
                    property.SetValue(newValues);
                }
            }
            //=================================================



            //===========EndPropertyAndReturnIndent============
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
            //=================================================
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            PropertyCache propertyCache = DrawerCache.Use(property, label);
            PropertyDrawUtility utility = propertyCache.utility;
            return utility.selectorHeight + utility.valuesHeight;
        }

        private void DrawTypeSelectorGUI(PropertyDrawUtility utility, string[] typesArray,int currentSelectedId, out int selectedId)
        {
            selectedId = EditorGUI.Popup(utility.selectorRect, currentSelectedId, typesArray);
            EditorGUI.PropertyField(utility.valuesRect, utility.property, utility.valuesLabel, true);
        }

        private string[] GetPropertyTypes(Type propertyType)
        {
            IEnumerable<Type> allTypes = Assembly.GetAssembly(propertyType).GetTypes();
            
            var selectedTypes = from type in allTypes
                                where !type.IsAbstract &&
                                !type.IsInterface &&
                                (type.IsSubclassOf(propertyType) ||
                                type == propertyType)
                                select type.FullName;

            return selectedTypes.ToArray();
        }
    }
}