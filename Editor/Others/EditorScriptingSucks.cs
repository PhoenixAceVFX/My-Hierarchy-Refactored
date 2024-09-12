// Copyright (C) 2023 INF

// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using EGL = UnityEditor.EditorGUILayout;
    using GL = UnityEngine.GUILayout;


    namespace Editor.Others
    {
        /// <summary>
        /// Some helper methods and stuff that makes writing editor code less painful and depression inducing moment
        /// [ Features ]
        /// | 1 Makes your editor code looks less like garbage
        /// | 2 Mitigates the probably of you commiting suicide from axienty producde during the process of writing said code.
        /// | 3 Reduces hate inducing rage during the processing of writing editor code 
        /// | 4- Offers methods that helps draw GUI quickly in less lines than having a renundant huge bullsh*t block spanning over 10 lines            
        /// 
        /// (╯°□°）╯︵ Made by: INF
        /// (╯°□°）╯︵ Refactored by: RunaXR
        /// </summary>
        public sealed class ScriptingBandAid : UnityEditor.Editor
        {
            public Color defaultBackgroundColor;
            public static float ViewWidth => EditorGUIUtility.currentViewWidth;

            public void CacheDefaultColors() => defaultBackgroundColor = GUI.backgroundColor;

            private void ResetBackgroundColor() => GUI.backgroundColor = defaultBackgroundColor;
            private static void SetBackgroundColor(Color newColor) => GUI.backgroundColor = newColor;

            // =========================================================================================================

            public void CreateToggle(
                bool condition, 
                Color trueColor, 
                Color falseColor, 
                GUIContent content, 
                Action click, 
                GUIStyle style = null, 
                params GUILayoutOption[] options
                )
            {
                SetBackgroundColor(condition ? trueColor : falseColor);

                if (style != null){
                    if ( GL.Button( content, style ) )
                        click();
                } else if (options != null) {
                    if ( GL.Button( content, options ) )
                        click();                    
                } else
                {
                    if ( GL.Button(content) )
                        click();                      
                }

                ResetBackgroundColor();
            }
        }

        public enum GroupDir
        {
            Horizontal,
            Vertical
        }

        public class GroupConstraint : IDisposable
            {
                private readonly GroupDir _direction;   
                private float _rightMargin;
                private readonly Rect _rect;

                public GroupConstraint(GroupDir direction)
                {
                    _direction = direction;
                    _rect = direction == GroupDir.Horizontal ? EGL.BeginHorizontal() : EGL.BeginVertical();
                }

                public IDisposable GetRect(out Rect r)
                {
                    r = _rect;
                    return this;
                }
            

                public void Dispose()
                {
                    if (_direction == GroupDir.Horizontal){
                        GL.Space(_rightMargin);
                        EGL.EndHorizontal();
                    } else {
                        EGL.EndVertical();
                    }
                }
        }
    }
#endif
