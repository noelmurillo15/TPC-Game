/*
 * StateNode -
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using State = ANM.Scriptables.Behaviour.State;
using Transition = ANM.Scriptables.Behaviour.Transition;

namespace ANM.Editor.Nodes
{
    [CreateAssetMenu(menuName = "Scriptables/BehaviourEditor/Nodes/State Node")]
    public class StateNode : DrawNode
    {
        private const float CollapseHeight = 80f;


        public override void DrawWindow(BaseNode node)
        {
            var expandHeight = 420f;

            if (node.stateRefs.currentState == null)
            {
                EditorGUILayout.LabelField("Add State to Modify:");
            }
            else
            {
                node.windowRect.height = node.collapse ? CollapseHeight : expandHeight;
                node.collapse = EditorGUILayout.Toggle("Collapse Window: ", node.collapse);
            }

            node.stateRefs.currentState = (State) EditorGUILayout.ObjectField(
                node.stateRefs.currentState, typeof(State), false);

            if (node.previousCollapse != node.collapse)
            {
                node.previousCollapse = node.collapse;
            }

            if (node.stateRefs.previousState != node.stateRefs.currentState)
            {
                node.isDuplicate = BehaviourEditor.EditorSettings.currentGraph.IsStateDuplicate(node);
                node.stateRefs.previousState = node.stateRefs.currentState;
                if (!node.isDuplicate)
                {
                    var pos = new Vector3(node.windowRect.x, node.windowRect.y, 0);
                    pos.x += node.windowRect.width * 2f;
                    SetupReorderableLists(node);

                    for (var i = 0; i < node.stateRefs.currentState.transitions.Count; i++)
                    {
                        pos.y += i * 100;
                        BehaviourEditor.AddTransitionNodeFromTransition(
                            node.stateRefs.currentState.transitions[i], node, pos);
                    }
                }

                BehaviourEditor.ForceSetDirty = true;
            }

            if (node.isDuplicate)
            {
                EditorGUILayout.LabelField("State is a duplicate!");
                node.windowRect.height = 80;
                return;
            }

            node.isAssigned = false;

            if (node.stateRefs.currentState == null) return;

            node.isAssigned = true;

            if (node.collapse) return;

            if (node.stateRefs.serializedState == null)
                SetupReorderableLists(node);

            node.stateRefs.serializedState?.Update();

            EditorGUILayout.LabelField(" ");
            node.stateRefs.onEnterList.DoLayoutList();
            node.stateRefs.onUpdateList.DoLayoutList();
            node.stateRefs.onFixedList.DoLayoutList();
            node.stateRefs.onExitList.DoLayoutList();

            node.stateRefs.serializedState?.ApplyModifiedProperties();
            var listCount = node.stateRefs.onEnterList.count
                            + node.stateRefs.onUpdateList.count
                            + node.stateRefs.onFixedList.count
                            + node.stateRefs.onExitList.count;

            if (listCount < 4) return;
            expandHeight += (listCount - 3) * 20f;
            node.windowRect.height = expandHeight;
        }

        private static void SetupReorderableLists(BaseNode node)
        {
            node.stateRefs.serializedState = new SerializedObject(node.stateRefs.currentState);

            node.stateRefs.onEnterList = new ReorderableList(node.stateRefs.serializedState,
                node.stateRefs.serializedState.FindProperty("onEnter"),
                true, true, true, true);

            node.stateRefs.onUpdateList = new ReorderableList(node.stateRefs.serializedState,
                node.stateRefs.serializedState.FindProperty("onUpdate"),
                true, true, true, true);

            node.stateRefs.onFixedList = new ReorderableList(node.stateRefs.serializedState,
                node.stateRefs.serializedState.FindProperty("onFixed"),
                true, true, true, true);

            node.stateRefs.onExitList = new ReorderableList(node.stateRefs.serializedState,
                node.stateRefs.serializedState.FindProperty("onExit"),
                true, true, true, true);

            HandleReorderableList(node.stateRefs.onEnterList, "On Enter");
            HandleReorderableList(node.stateRefs.onUpdateList, "On Update");
            HandleReorderableList(node.stateRefs.onFixedList, "On Fixed");
            HandleReorderableList(node.stateRefs.onExitList, "On Exit");
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

        public static Transition AddTransition(BaseNode node)
        {
            return node.stateRefs.currentState.AddTransition();
        }
    }
}
