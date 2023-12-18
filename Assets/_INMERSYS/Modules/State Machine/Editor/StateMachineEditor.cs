using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Inmersys.StateMachine
{
    [CustomEditor(typeof(StateMachine))]
    public class StateMachineEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {
            var stateMachine = (StateMachine)target;
            
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("States"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("ExcludeFromTheGroup"));
            serializedObject.ApplyModifiedProperties();
            
            
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button(new GUIContent("Finish", "Finish State Machine"), EditorStyles.miniButtonLeft))
            {
                stateMachine.FinishStateMachine();
            }
            if(GUILayout.Button(new GUIContent("Jump", "Jump State Machine"), EditorStyles.miniButtonRight))
            {
                stateMachine.JumpStateMachine();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(5f);
            
            if (GUILayout.Button("Finish Current State"))
                stateMachine.FinishCurrentState();
            
        }
        
    }
}