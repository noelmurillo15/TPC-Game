/*
 * StateNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Collections.Generic;

namespace ANM.Editor.Nodes
{
    public class StateNode : BaseNode
    {
        public List<BaseNode> dependencies = new List<BaseNode>();

        private bool _collapse;
        public State currentState;
        private State _previousState;

        private ReorderableList _onEnterList;
        private ReorderableList _onStateList;
        private ReorderableList _onExitList;
        private SerializedObject _serializedState;

        public override void DrawWindow()
        {
            var standardHeight = 300f;

            if (currentState == null)
            {
                EditorGUILayout.LabelField("Add State to Modify:");
            }
            else
            {
                windowRect.height = !_collapse ? standardHeight : 100;
                _collapse = EditorGUILayout.Toggle(" ", _collapse);
            }

            currentState = (State) EditorGUILayout.ObjectField(
                currentState, typeof(State), false);

            if (_previousState != currentState)
            {
                _serializedState = null;
                _previousState = currentState;
                BehaviourEditor.CurrentGraph.SetStateNode(this);

                for (int i = 0; i < currentState.transitions.Count; i++)
                {
                    
                }
            }

            if (currentState == null) return;

            if (_serializedState == null)
            {
                _serializedState = new SerializedObject(currentState);
                _onEnterList = new ReorderableList(_serializedState, _serializedState.FindProperty("onEnter"),
                    true, true, true, true);
                _onStateList = new ReorderableList(_serializedState, _serializedState.FindProperty("onState"),
                    true, true, true, true);
                _onExitList = new ReorderableList(_serializedState, _serializedState.FindProperty("onExit"),
                    true, true, true, true);
            }

            if (_collapse) return;
            _serializedState.Update();

            HandleReorderableList(_onEnterList, "On Enter");
            HandleReorderableList(_onStateList, "On State");
            HandleReorderableList(_onExitList, "On Exit");

            EditorGUILayout.LabelField("");
            _onEnterList.DoLayoutList();
            _onStateList.DoLayoutList();
            _onExitList.DoLayoutList();
            _serializedState.ApplyModifiedProperties();

            var listCount = _onEnterList.count + _onStateList.count + _onExitList.count;
            if (listCount < 4) return;
            standardHeight += (listCount - 3) * 20f;
            windowRect.height = standardHeight;
        }

        private static void HandleReorderableList(ReorderableList list, string targetName)
        {
            list.drawHeaderCallback = (rect) =>
            {
                EditorGUI.LabelField(rect, targetName);
            };
            list.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width,
                    EditorGUIUtility.singleLineHeight), element, GUIContent.none);
            };
        }

        public Transition AddTransition()
        {
            return currentState.AddTransition();
        }

        public void ClearReferences()
        {
            BehaviourEditor.ClearWindowsFromList(dependencies);
            dependencies?.Clear();
        }
    }
}
