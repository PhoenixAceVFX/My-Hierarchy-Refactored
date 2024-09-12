// Copyright (C) 2023 INF

// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using UnityEditor;
using UnityEngine;

namespace Components
{
    [System.Serializable]
    public class MyHierarchyHeader : MonoBehaviour
    {
        public Color fontColor = Color.white;
        public Color backgroundColor = Color.gray;
        private const string EditorOnlyTag = "EditorOnly";

        #if UNITY_EDITOR
        [MenuItem("GameObject/My Hierarchy/Header", false, 10)]
        public static void CreateHeader(MenuCommand menu)
        {
            var go = new GameObject();
            go.AddComponent<MyHierarchyHeader>();
            GameObjectUtility.SetParentAndAlign(go, menu.context as GameObject);
            Undo.RegisterCreatedObjectUndo(go, "Created MyHierarchy GO: " + go.name);
            Selection.activeObject = go;
            EditorApplication.RepaintHierarchyWindow();
        }
        #endif

        private void OnDrawGizmosSelected() => gameObject.tag = EditorOnlyTag;

        #if UNITY_EDITOR
        private void OnValidate() => EditorApplication.RepaintHierarchyWindow();
        #endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MyHierarchyHeader)), CanEditMultipleObjects]
    public class MyHierarcyHeaderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var gs = new GUIStyle(EditorStyles.helpBox)
            {
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                fontSize = 13
            };
            var color = Color.yellow * 0.85f;
            SetFontColor_AllStates(gs, new Color(color.r, color.g, color.b, 1));
            SetBackground_AllStates(gs, CreateTexture_2x2(new Color(0.25f, 0.25f, 0.25f, 1)));

            base.OnInspectorGUI();
            EditorGUILayout.Space(5);

        
            EditorGUILayout.LabelField(
                "WARNING!:\n - Do not parent another gameobject into this nor parent this to another" + 
                "\n - Do not add any other component either" +
                "\n - Don't reference this gameobject on anything" +
                "\n\n [ This gameobject will be removed at builds ]"
                , 
            gs);

            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Created By: (╯°□°)╯︵ INF", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Refactored By: RunaXR", EditorStyles.boldLabel);
        }

        public GUIStyle SetBackground_AllStates(GUIStyle style, Texture2D texture)
        {
            style.active.background = texture;
            style.hover.background = texture;
            style.normal.background = texture;
            return style;
        }

        public GUIStyle SetFontColor_AllStates(GUIStyle style, Color color)
        {
            style.active.textColor = color;
            style.hover.textColor = color;
            style.normal.textColor = color;
            return style;
        }

        public Texture2D CreateTexture_2x2(Color color)
        {
            var texture = new Texture2D(4, 4, TextureFormat.RGBA32, false);
            
            for (var i = 0; i < texture.width; i++) {
                for (var j = 0; j < texture.height; j++) {
                    texture.SetPixel(i,j, color);
                }
            }

            texture.Apply();
            return texture;
        }
    }
#endif
}

