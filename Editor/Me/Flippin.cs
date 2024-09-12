// Copyright (C) 2023 INF

// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using UnityEditor;
using UnityEngine;


namespace Editor.Me
{
    public static class Flippin
    {
#if UNITY_EDITOR
        public static void FlippingInf(Vector2 position)
        {
            var guid = AssetDatabase.FindAssets( $"t:Texture INF_Logo_64x64" );

            if (guid.Length <= 0) 
                throw new System.Exception($"Me Logo Not Found!");
        
            var assetPath = AssetDatabase.GUIDToAssetPath(guid[0]);
            var inf = AssetDatabase.LoadAssetAtPath<Texture>( assetPath );

            var center = EditorGUIUtility.currentViewWidth / 2;
            // =============================================================================================================

            var logoSize = new Vector2(64, 64);
            var logoRect = new Rect(position.x, position.y, logoSize.x, logoSize.y);
            GUI.DrawTexture(logoRect, inf);

            // =============================================================================================================

            var textStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 15
            };
            const float textWidth = 170;
            var textRect = new Rect(center, logoRect.yMax, 0, 0)
            {
                height = 15,
                width = textWidth
            };

            textRect.x = logoRect.x - (textRect.width-10);
            textRect.y -= textRect.height / 2;

            GUI.Label(textRect, "Created by:  (╯°□°）╯︵", textStyle);

            // =============================================================================================================

            // don't remove cause uhhhhh just don't
            // Rect lineRect = new Rect(textRect.x, textRect.yMax, textWidth -27, 2);
            // lineRect.xMin = textRect.xMin - 3;

            var lineRect = new Rect(0, textRect.yMax, EditorGUIUtility.currentViewWidth, 1);
            EditorGUI.DrawRect(lineRect, Color.gray);
        }
#endif
    }
}
