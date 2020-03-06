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

        public override void DrawWindow(BaseNode node)
        {
            var standardHeight = 300f;

            if (node.stateNodeRefs.currentState == null)
            {
                EditorGUILayout.LabelField("Add State to Modify:");
            }
            else
            {
                node.windowRect.height = !node.stateNodeRefs.collapse ? standardHeight : 100;
                node.stateNodeRefs.collapse = EditorGUILayout.Toggle(" ", node.stateNodeRefs.collapse);
            }

            node.stateNodeRefs.currentState = (State) EditorGUILayout.ObjectField(
                node.stateNodeRefs.currentState, typeof(State), false);

            if (node.stateNodeRefs.previousCollapse != node.stateNodeRefs.collapse)
            {
                node.stateNodeRefs.previousCollapse = node.stateNodeRefs.collapse;
                //BehaviourEditor.CurrentGraph.SetStateNode(this);
            }

            if (node.stateNodeRefs.previousState != node.stateNodeRefs.currentState)
            {
                node.stateNodeRefs.isDuplicate = BehaviourEditor.EditorSettings.CurrentGraph.IsStateNodeDuplicate(this);
                node.stateNodeRefs.serializedState = null;
                if (!node.stateNodeRefs.isDuplicate)
                {
                    //BehaviourEditor.CurrentGraph.SetNode(this);
                    node.stateNodeRefs.previousState = node.stateNodeRefs.currentState;
                    for (int i = 0; i < node.stateNodeRefs.currentState.transitions.Count; i++)
                    {

                    }
                }
            }

            if (node.stateNodeRefs.isDuplicate)
            {
                EditorGUILayout.LabelField("State is a duplicate!");
                node.windowRect.height = 100;
                return;
            }

            if (node.stateNodeRefs.currentState == null) return;

            if (node.stateNodeRefs.serializedState == null)
            {
                node.stateNodeRefs.serializedState = new SerializedObject(node.stateNodeRefs.currentState);

                node.stateNodeRefs.onEnterList = new ReorderableList(node.stateNodeRefs.serializedState,
                    node.stateNodeRefs.serializedState.FindProperty("onEnter"),
                    true, true, true, true);

                node.stateNodeRefs.onStateList = new ReorderableList(node.stateNodeRefs.serializedState,
                    node.stateNodeRefs.serializedState.FindProperty("onState"),
                    true, true, true, true);

                node.stateNodeRefs.onExitList = new ReorderableList(node.stateNodeRefs.serializedState,
                    node.stateNodeRefs.serializedState.FindProperty("onExit"),
                    true, true, true, true);
            }

            if (node.stateNodeRefs.collapse) return;
            node.stateNodeRefs.serializedState.Update();

            HandleReorderableList(node.stateNodeRefs.onEnterList, "On Enter");
            HandleReorderableList(node.stateNodeRefs.onStateList, "On State");
            HandleReorderableList(node.stateNodeRefs.onExitList, "On Exit");

            EditorGUILayout.LabelField("");
            node.stateNodeRefs.onEnterList.DoLayoutList();
            node.stateNodeRefs.onStateList.DoLayoutList();
            node.stateNodeRefs.onExitList.DoLayoutList();
            node.stateNodeRefs.serializedState.ApplyModifiedProperties();

            var listCount = node.stateNodeRefs.onEnterList.count
                            + node.stateNodeRefs.onStateList.count
                            + node.stateNodeRefs.onExitList.count;

            if (listCount < 4) return;
            standardHeight += (listCount - 3) * 20f;
            node.windowRect.height = standardHeight;
        }

        public override void DrawCurve(BaseNode node)
        {

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

        public Transition AddTransition()
        {
            return null; //node.currentState.AddTransition();
        }

        public void ClearReferences()
        {
            //BehaviourEditor.ClearWindowsFromList(dependencies);
            //dependencies?.Clear();
        }
    }
}
