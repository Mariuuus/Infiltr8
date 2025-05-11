namespace __ProjectMain.Scripts.Managers.LevelEditor.Editor
{
    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Reflection;
    using System.Collections.Generic;

    /*
     * Generated Editor.
     */
    [CustomPropertyDrawer(typeof(PlaceableComponentWithCallback))]
    public class PlaceableComponentWithCallbackDrawer : PropertyDrawer
    {
        private string[] methodNames;
        private int selectedIndex = -1;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Begin property
            EditorGUI.BeginProperty(position, label, property);

            // Calculate rects
            var componentRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var methodRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2,
                position.width, EditorGUIUtility.singleLineHeight);

            // Draw the component field
            SerializedProperty componentProp = property.FindPropertyRelative("component");
            EditorGUI.PropertyField(componentRect, componentProp);

            // Get the actual component instance (not serialized, so we use reflection)
            var componentObj = componentProp.objectReferenceValue;

            if (componentObj != null)
            {
                Type componentType = componentObj.GetType();

                // Get all public instance methods with no parameters and void return type
                MethodInfo[] methods = componentType.GetMethods(
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                List<string> validMethods = new List<string>();

                foreach (var method in methods)
                {
                    if (method.GetParameters().Length == 0 && method.ReturnType == typeof(void))
                    {
                        if (!method.Name.StartsWith("get_") && !method.Name.StartsWith("set_"))
                        {
                            validMethods.Add(method.Name);
                        }
                    }
                }

                methodNames = validMethods.ToArray();

                SerializedProperty selectedMethodProp = property.FindPropertyRelative("selectedMethodName");

                // Find current selected index
                selectedIndex = Array.IndexOf(methodNames, selectedMethodProp.stringValue);
                if (selectedIndex < 0) selectedIndex = 0;

                // Draw popup
                int newIndex = EditorGUI.Popup(methodRect, "Select Method", selectedIndex, methodNames);

                if (newIndex != selectedIndex)
                {
                    selectedIndex = newIndex;
                    selectedMethodProp.stringValue = methodNames[selectedIndex];
                }
            }
            else
            {
                EditorGUI.LabelField(methodRect, "Assign a component to select methods");
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Two lines: one for component, one for method dropdown
            return EditorGUIUtility.singleLineHeight * 2 + 4;
        }
    }
}