// Copyright (C) 2023 INF

// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using Editor.Me;
using Editor.Others;
using Editor.SO_Scripts;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
using EGL = UnityEditor.EditorGUILayout;
    using GL = UnityEngine.GUILayout;
#endif

namespace Editor.Core
{
    public class HierarchySettingsWindow : EditorWindow
    {
        private ScriptingBandAid _bandAid;
        private readonly Color _onColor = Color.green;
        private readonly Color _offColor = Color.grey;
        private static MyHierarchySettings _settings;
        private static HierarchySettingsWindow _window;
        private static SerializedObject _settingsSo;

        [MenuItem("My Hierarchy/Settings")]
        private static void ShowWindow() {
            _window = GetWindow<HierarchySettingsWindow>();
            _window.titleContent = new GUIContent("Hierarchy Settings");
            _window.minSize = new Vector2( 450, 700 );
            _window.maxSize = _window.minSize;
            _window.Show();
        }

        private void OnEnable() => _bandAid = CreateInstance<ScriptingBandAid>();

        private void OnGUI()
        {
            if ( _settings == null )
                _settings = HierarchyRenderer.GetAsset_SO<MyHierarchySettings>("MyHierarchySettings", "My Hierarchy Settings");

            _settingsSo ??= new SerializedObject(_settings);

            _settingsSo.Update();
            _bandAid.CacheDefaultColors();
            EditorGUI.BeginChangeCheck();

            var buttonWidthx2 = ScriptingBandAid.ViewWidth / 2 - 5;


            GL.Space(10);
            EGL.PropertyField(_settingsSo.FindProperty( nameof(_settings.headerFontStyle) ));
            EGL.PropertyField(_settingsSo.FindProperty( nameof(_settings.headerAlignment) ));
            EGL.PropertyField(_settingsSo.FindProperty( nameof(_settings.groupFontStyle) ));
            EGL.PropertyField(_settingsSo.FindProperty( nameof(_settings.labelWidth)  ));


            EGL.Space(10);
            var gs = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter
            };
            EGL.LabelField("VISIBILITY:", gs);

            // ====================================================================================================================

            gs.alignment = TextAnchor.MiddleLeft;
            EGL.LabelField("Labels:", gs);
            Rect allRect;
            using (new GroupConstraint(GroupDir.Vertical).GetRect(out allRect))
            {
                using (new GroupConstraint(GroupDir.Horizontal))
                {
                    _bandAid.CreateToggle(
                        _settings.showLayers, 
                        _onColor, 
                        _offColor, 
                        new GUIContent("Show Layer"), 
                        ()=> _settings.showLayers = !_settings.showLayers,
                        null, GL.Width(buttonWidthx2), GL.Height(30));

                    _bandAid.CreateToggle(
                        _settings.showTags, 
                        _onColor, 
                        _offColor, 
                        new GUIContent("Show Tag"), 
                        ()=> _settings.showTags = !_settings.showTags,
                        null, GL.Width(buttonWidthx2), GL.Height(30)); 
                }

                using (new GroupConstraint(GroupDir.Horizontal))
                {
                    _bandAid.CreateToggle(
                        _settings.showStaticObjects, 
                        _onColor, 
                        _offColor, 
                        new GUIContent("Show Is Static"), 
                        ()=> _settings.showStaticObjects = !_settings.showStaticObjects,
                        null, GL.Width(buttonWidthx2), GL.Height(30));

                    _bandAid.CreateToggle(
                        _settings.showDepth, 
                        _onColor, 
                        _offColor, 
                        new GUIContent("Show Depth"), 
                        ()=> _settings.showDepth = !_settings.showDepth,
                        null, GL.Width(buttonWidthx2), GL.Height(30));   
                }  

                using (new GroupConstraint(GroupDir.Horizontal))
                {
                    _bandAid.CreateToggle(
                        _settings.showComponents, 
                        _onColor, 
                        _offColor, 
                        new GUIContent("Show Components"), 
                        ()=> _settings.showComponents = !_settings.showComponents,
                        null, GL.Width(buttonWidthx2), GL.Height(30));

                    _bandAid.CreateToggle(
                        _settings.hideIconlessComponents, 
                        _onColor, 
                        _offColor, 
                        new GUIContent("Hide Iconless Components"), 
                        ()=> _settings.hideIconlessComponents = !_settings.hideIconlessComponents,
                        null, GL.Width(buttonWidthx2), GL.Height(30));   
                }

                // ====================================================================================================================
                EGL.Space(10);
                EGL.LabelField("Object Relationship Lines", gs);

                using (new GroupConstraint(GroupDir.Horizontal))
                {
                    _bandAid.CreateToggle(
                        _settings.highlightSelectedSiblings, 
                        _onColor, 
                        _offColor, 
                        new GUIContent("Highlight Selected's Siblings"), 
                        ()=> _settings.highlightSelectedSiblings = !_settings.highlightSelectedSiblings,
                        null, GL.Width(buttonWidthx2), GL.Height(30));

                    _bandAid.CreateToggle(
                        _settings.highlightSelectedChildren, 
                        _onColor, 
                        _offColor, 
                        new GUIContent("Highlight Selected's Children"), 
                        ()=> _settings.highlightSelectedChildren = !_settings.highlightSelectedChildren,
                        null, GL.Width(buttonWidthx2), GL.Height(30));   
                } 

                using (new GroupConstraint(GroupDir.Horizontal))
                {
                    _bandAid.CreateToggle(
                        _settings.showRelationshipLines,
                        _onColor, 
                        _offColor, 
                        new GUIContent("Show Object Relationship"), 
                        ()=> _settings.showRelationshipLines = !_settings.showRelationshipLines,
                        null, GL.Width(buttonWidthx2*2+4), GL.Height(30)); 
                }
    
                // ====================================================================================================================

                EGL.Space(10);
                EGL.LabelField("Group Header", gs);

                using (new GroupConstraint(GroupDir.Horizontal))
                {
                    _bandAid.CreateToggle(
                        _settings.showLabelsOnGroup,
                        _onColor, 
                        _offColor, 
                        new GUIContent("Show Group Header Labels"), 
                        ()=> _settings.showLabelsOnGroup = !_settings.showLabelsOnGroup,
                        null, GL.Width(buttonWidthx2*2+4), GL.Height(30)); 
                }

                // ====================================================================================================================

                EGL.Space(20);
                EGL.LabelField("All", gs);

                _bandAid.CreateToggle(
                    _settings.activate, 
                    _onColor, 
                    _offColor, 
                    new GUIContent("Toggle Activation"), 
                    ()=> _settings.activate = !_settings.activate,
                    null, GL.Width(buttonWidthx2*2+4), GL.Height(30));
            }

            // ====================================================================================================================
            EGL.Space(100);
            Flippin.FlippingInf(new Vector2(180, allRect.yMax + 30));

            // ====================================================================================================================
    
            if ( EditorGUI.EndChangeCheck() )
            {
                EditorUtility.SetDirty( _settings );
                EditorApplication.RepaintHierarchyWindow();
            }
                
            _settingsSo.ApplyModifiedProperties();
        }
    }

}

