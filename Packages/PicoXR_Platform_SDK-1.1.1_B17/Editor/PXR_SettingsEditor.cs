/************************************************************************************
 【PXR SDK】
 Copyright 2015-2020 Pico Technology Co., Ltd. All Rights Reserved.

************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.XR.PXR;

namespace Unity.XR.PXR.Editor
{
    [CustomEditor(typeof(PXR_Settings))]
    public class PXR_SettingsEditor : UnityEditor.Editor
    {
        private const string StereoRenderingModeAndroid = "stereoRenderingModeAndroid";
        private const string UseDefaultRenderTexture = "useDefaultRenderTexture";
        private const string EyeRenderTextureResolution = "eyeRenderTextureResolution";
        private const string AntiAliasing = "antiAliasing";
        private const string RenderTextureDepth = "renderTextureDepth";

        static GUIContent guiStereoRenderingMode = EditorGUIUtility.TrTextContent("Stereo Rendering Mode");
        static GUIContent guiUseDefaultRenderTexture = EditorGUIUtility.TrTextContent("Use Default Render Texture");
        static GUIContent guiEyeRenderTextureResolution = EditorGUIUtility.TrTextContent("Render Texture Resolution");
        static GUIContent guiAntiAliasing = EditorGUIUtility.TrTextContent("Render Texture Anti-Aliasing");
        static GUIContent guiRenderTextureDepth = EditorGUIUtility.TrTextContent("Render Texture Bit Depth");


        private SerializedProperty stereoRenderingModeAndroid;
        private SerializedProperty useDefaultRenderTexture;
        private SerializedProperty eyeRenderTextureResolution;
        private SerializedProperty antiAliasing;
        private SerializedProperty renderTextureDetph;

        void OnEnable()
        {
            if (stereoRenderingModeAndroid == null) 
                stereoRenderingModeAndroid = serializedObject.FindProperty(StereoRenderingModeAndroid);
            if (useDefaultRenderTexture == null) 
                useDefaultRenderTexture = serializedObject.FindProperty(UseDefaultRenderTexture);
            if (eyeRenderTextureResolution == null) 
                eyeRenderTextureResolution = serializedObject.FindProperty(EyeRenderTextureResolution);
            if (antiAliasing == null) 
                antiAliasing = serializedObject.FindProperty(AntiAliasing);
            if (renderTextureDetph == null) 
                renderTextureDetph = serializedObject.FindProperty(RenderTextureDepth);

            switch (QualitySettings.antiAliasing)
            {
                case 0:
                    ((PXR_Settings)target).antiAliasing = PXR_Settings.RenderTextureAntiAliasing.X1;
                    break;
                case 2:
                    ((PXR_Settings)target).antiAliasing = PXR_Settings.RenderTextureAntiAliasing.X2;
                    break;
                case 4:
                    ((PXR_Settings)target).antiAliasing = PXR_Settings.RenderTextureAntiAliasing.X4;
                    break;
                case 8:
                    ((PXR_Settings)target).antiAliasing = PXR_Settings.RenderTextureAntiAliasing.X8;
                    break;
            }
        }

        public override void OnInspectorGUI()
        {
            if (serializedObject == null || serializedObject.targetObject == null)
                return;

            serializedObject.Update();

            BuildTargetGroup selectedBuildTargetGroup = EditorGUILayout.BeginBuildTargetSelectionGrouping();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorGUILayout.HelpBox("PicoXR settings cannot be changed when the editor is in play mode.", MessageType.Info);
                EditorGUILayout.Space();
            }
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            if (selectedBuildTargetGroup == BuildTargetGroup.Android)
            {
                EditorGUILayout.PropertyField(stereoRenderingModeAndroid, guiStereoRenderingMode);
                EditorGUILayout.PropertyField(useDefaultRenderTexture, guiUseDefaultRenderTexture);
                if (!((PXR_Settings)target).useDefaultRenderTexture)
                {
                    EditorGUILayout.PropertyField(eyeRenderTextureResolution, guiEyeRenderTextureResolution);
                }
                EditorGUILayout.PropertyField(antiAliasing, guiAntiAliasing);
                EditorGUILayout.PropertyField(renderTextureDetph, guiRenderTextureDepth);

                switch (((PXR_Settings)target).antiAliasing)
                {
                    case PXR_Settings.RenderTextureAntiAliasing.X1:
                        QualitySettings.antiAliasing = 0;
                        break;
                    case PXR_Settings.RenderTextureAntiAliasing.X2:
                        QualitySettings.antiAliasing = 2;
                        break;
                    case PXR_Settings.RenderTextureAntiAliasing.X4:
                        QualitySettings.antiAliasing = 4;
                        break;
                    case PXR_Settings.RenderTextureAntiAliasing.X8:
                        QualitySettings.antiAliasing = 8;
                        break;
                }
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndBuildTargetSelectionGrouping();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
