/*
 * StateNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace ANM.Editor.Nodes
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/State Node")]
    public class StateNode : DrawNode
    {
        public override void DrawWindow(BaseNode b)
        {
            var standardHeight = 300f;

            if (b.stateRefs.currentState == null)
            {
                EditorGUILayout.LabelField("Add State to Modify:");
            }
            else
            {
                b.windowRect.height = b.collapse ? 100 : standardHeight;
                b.collapse = EditorGUILayout.Toggle("Collapse Window: ", b.collapse);
            }

            b.stateRefs.currentState = (State) EditorGUILayout.ObjectField(
                b.stateRefs.currentState, typeof(State), false);

            if (b.previousCollapse != b.collapse)
            {
                b.previousCollapse = b.collapse;
            }

            if (b.stateRefs.previousState != b.stateRefs.currentState)
            {
                b.isDuplicate = BehaviourEditor.EditorSettings.currentGraph.IsStateDuplicate(b);
            }

            if (b.isDuplicate)
            {
                EditorGUILayout.LabelField("State is a duplicate!");
                b.windowRect.height = 80;
                return;
            }

            if (b.stateRefs.currentState == null) return;

            var serializedState = new SerializedObject(b.stateRefs.currentState);

            var onEnterList = new ReorderableList(serializedState, serializedState.FindProperty("onEnter"),
                true, true, true, true);

            var onStateList = new ReorderableList(serializedState, serializedState.FindProperty("onState"),
                true, true, true, true);

            var onExitList = new ReorderableList(serializedState, serializedState.FindProperty("onExit"),
                true, true, true, true);

            if (b.collapse) return;
            serializedState.Update();

            HandleReorderableList(onEnterList, "On Enter");
            HandleReorderableList(onStateList, "On State");
            HandleReorderableList(onExitList, "On Exit");

            EditorGUILayout.LabelField("");
            onEnterList.DoLayoutList();
            onStateList.DoLayoutList();
            onExitList.DoLayoutList();

            serializedState.ApplyModifiedProperties();
            var listCount = onEnterList.count + onStateList.count + onExitList.count;

            if (listCount < 4) return;
            standardHeight += (listCount - 3) * 20f;
            b.windowRect.height = standardHeight;
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

        public override void DrawCurve(BaseNode node)
        {

        }

        public Transition AddTransition(BaseNode node)
        {
            return node.stateRefs.currentState.AddTransition();
        }

        public void ClearReferences()
        {
            //BehaviourEditor.ClearWindowsFromList(dependencies);
            //dependencies?.Clear();
        }
    }
}
