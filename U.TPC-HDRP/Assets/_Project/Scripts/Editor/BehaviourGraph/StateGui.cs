/*
 * StateGui Editor -
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using State = ANM.Scriptables.Behaviour.State;

namespace ANM.Editor
{
    [CustomEditor((typeof(State)))]
    public class StateGui : UnityEditor.Editor
    {
        private SerializedObject _serializedState;
        private ReorderableList _onEnterList;
        private ReorderableList _onFixedList;
        private ReorderableList _onUpdateList;
        private ReorderableList _transitions;
        private ReorderableList _onExitList;

        private bool _showDefaultGui = false;
        private bool _showActions = true;
        private bool _showTransitions = true;


        private void OnEnable()
        {
            _serializedState = null;
        }

        public override void OnInspectorGUI()
        {
            _showDefaultGui = EditorGUILayout.Toggle("Default Gui", _showDefaultGui);
            if (_showDefaultGui)
            {
                base.OnInspectorGUI();
                return;
            }

            _showActions = EditorGUILayout.Toggle("Show Actions : ", _showActions);

            if (_serializedState == null)
                SetupReorderableLists();

            _serializedState.Update();

            if (_showActions)
            {
                EditorGUILayout.LabelField("Actions that execute on OnStateEnter");
                _onEnterList.DoLayoutList();
                EditorGUILayout.LabelField("Actions that execute on OnStateUpdate");
                _onUpdateList.DoLayoutList();
                EditorGUILayout.LabelField("Actions that execute on OnStateFixedUpdate");
                _onFixedList.DoLayoutList();
                EditorGUILayout.LabelField("Actions that execute on onStateExit");
                _onExitList.DoLayoutList();
            }

            _showTransitions = EditorGUILayout.Toggle("Show Transitions : ", _showTransitions);

            if (_showTransitions)
            {
                EditorGUILayout.LabelField("Conditions to Exit the State");
                _transitions.DoLayoutList();
            }

            _serializedState.ApplyModifiedProperties();
        }

        private void SetupReorderableLists()
        {
            var curState = (State) target;
            _serializedState = new SerializedObject(curState);

            _onEnterList = new ReorderableList(_serializedState, _serializedState.FindProperty("onEnter"),
                true, true, true, true);

            _onUpdateList = new ReorderableList(_serializedState, _serializedState.FindProperty("onUpdate"),
                true, true, true, true);

            _onFixedList = new ReorderableList(_serializedState, _serializedState.FindProperty("onFixed"),
                true, true, true, true);

            _onExitList = new ReorderableList(_serializedState, _serializedState.FindProperty("onExit"),
                true, true, true, true);

            _transitions = new ReorderableList(_serializedState, _serializedState.FindProperty("transitions"),
                true, true, true, true);

            HandleReorderableList(_onEnterList, "On Enter");
            HandleReorderableList(_onUpdateList, "On Update");
            HandleReorderableList(_onFixedList, "On Fixed");
            HandleReorderableList(_onExitList, "On Exit");
            HandleTransitionReorderable(_transitions, "Condition --> New State");
        }

        private static void HandleTransitionReorderable(ReorderableList transitions, string newState)
        {
            transitions.drawHeaderCallback = (rect) => { EditorGUI.LabelField(rect, newState); };
            transitions.drawElementCallback = (rect, index, active, focused) =>
            {
                var element = transitions.serializedProperty.GetArrayElementAtIndex(index);

                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width * .3f,
                    EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("condition"), GUIContent.none);

                EditorGUI.ObjectField(new Rect(rect.x + +(rect.width * .35f), rect.y, rect.width * .3f,
                    EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("targetState"), GUIContent.none);

                EditorGUI.LabelField(new Rect(rect.x + +(rect.width * .75f), rect.y, rect.width * .2f,
                    EditorGUIUtility.singleLineHeight), "Disable");

                EditorGUI.PropertyField(new Rect(rect.x + +(rect.width * .9f), rect.y, rect.width * .2f,
                    EditorGUIUtility.singleLineHeight), element.FindPropertyRelative("disable"), GUIContent.none);
            };
        }

        private static void HandleReorderableList(ReorderableList list, string targetName)
        {
            list.drawHeaderCallback = (rect) => { EditorGUI.LabelField(rect, targetName); };
            list.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width,
                    EditorGUIUtility.singleLineHeight), element, GUIContent.none);
            };
        }
    }
}
