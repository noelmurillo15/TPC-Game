/*
 * BehaviourEditor - Custom Unity Editor Window for Behaviour Node Graph creation/editing
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEditor;
using UnityEngine;
using System.Linq;
using ANM.Editor.Nodes;

namespace ANM.Editor
{
    public class BehaviourEditor : EditorWindow
    {
        public static EditorSettings EditorSettings;

        private enum UserActions
        {
            ADD_STATE,
            ADD_CONDITION,
            DELETE_NODE,
            ADD_COMMENT
        }

        private BaseNode _selectedNode;
        private Vector3 _mousePosition;
        private bool _clickedOnWindow;
        private bool _makeTransition;
        private int _selectedIndex;


        [MenuItem("Behaviour Editor/Editor")]
        private static void ShowEditor()
        {
            var editor = GetWindow<BehaviourEditor>();
            editor.minSize = new Vector2(800, 600);
        }

        private void OnEnable()
        {
            EditorSettings = Resources.Load("EditorSettings") as EditorSettings;
        }

        #region GUI Methods

        private void OnGUI()
        {
            var e = Event.current;
            _mousePosition = e.mousePosition;
            UserInput(e);
            DrawWindows();

            if (e.type != EventType.MouseDrag) return;
            EditorSettings.currentGraph.DeleteWindowsThatNeedTo();
            Repaint();
        }

        private void DrawWindows()
        {
            BeginWindows();

            EditorGUILayout.LabelField(" ", GUILayout.Width(100));
            EditorGUILayout.LabelField("Assign Graph :", GUILayout.Width(100));

            EditorSettings.currentGraph = (BehaviourGraph) EditorGUILayout.ObjectField(
                EditorSettings.currentGraph, typeof(BehaviourGraph),
                false, GUILayout.Width(200));

            if (EditorSettings.currentGraph != null)
            {
                foreach (var node in EditorSettings.currentGraph.windows)
                {
                    node.DrawCurve();
                }

                for (var i = 0; i < EditorSettings.currentGraph.windows.Count; i++)
                {
                    EditorSettings.currentGraph.windows[i].windowRect = GUI.Window(i,
                        EditorSettings.currentGraph.windows[i].windowRect, DrawNodeWindow,
                        EditorSettings.currentGraph.windows[i].windowTitle);
                }
            }

            EndWindows();
        }

        private static void DrawNodeWindow(int id)
        {
            EditorSettings.currentGraph.windows[id].DrawWindow();
            GUI.DragWindow();
        }

        private void UserInput(Event e)
        {
            if (EditorSettings.currentGraph == null) return;

            switch (e.button)
            {
                case 1 when !_makeTransition:
                {
                    if (e.type == EventType.MouseDown)
                        RightClick(e);
                    break;
                }
            }
        }

        private static void LeftDrag(Event e)
        {
            foreach (var node in EditorSettings.currentGraph.windows.Where(node =>
                node.windowRect.Contains(e.mousePosition)))
            {
                // if (CurrentGraph == null) continue;
                // CurrentGraph.SetNode(node);
                // break;
            }
        }

        private void LeftClick(Event e)
        {

        }

        private void RightClick(Event e)
        {
            _selectedIndex = -1;
            _clickedOnWindow = false;

            for (var i = 0; i < EditorSettings.currentGraph.windows.Count; i++)
            {
                if (!EditorSettings.currentGraph.windows[i].windowRect.Contains(e.mousePosition)) continue;
                _selectedIndex = i;
                _clickedOnWindow = true;
                _selectedNode = EditorSettings.currentGraph.windows[i];
                break;
            }

            if (!_clickedOnWindow)
            {
                AddNewNode(e);
            }
            else
            {
                ModifyNode(e);
            }
        }

        private void AddNewNode(Event e)
        {
            var menu = new GenericMenu();
            menu.AddSeparator("");
            if (EditorSettings.currentGraph != null)
            {
                menu.AddItem(new GUIContent("Add State"),
                    false, ContextCallback, UserActions.ADD_STATE);
                menu.AddItem(new GUIContent("Add Comment"),
                    false, ContextCallback, UserActions.ADD_COMMENT);
            }
            else
            {
                menu.AddDisabledItem(new GUIContent("Add State"));
                menu.AddDisabledItem(new GUIContent("Add Comment"));
            }

            menu.ShowAsContext();
            e.Use();
        }

        private void ModifyNode(Event e)
        {
            var menu = new GenericMenu();
            if (_selectedNode.drawNode is StateNode stateNode)
            {
                if (_selectedNode.stateRefs.currentState != null)
                {
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Add Condition"), false,
                        ContextCallback, UserActions.ADD_CONDITION);
                }
                else
                {
                    menu.AddSeparator("");
                    menu.AddDisabledItem(new GUIContent("Add Condition"));
                }

                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false,
                    ContextCallback, UserActions.DELETE_NODE);
            }

            if (_selectedNode.drawNode is TransitionNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false,
                    ContextCallback, UserActions.DELETE_NODE);
            }

            if (_selectedNode.drawNode is CommentNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false,
                    ContextCallback, UserActions.DELETE_NODE);
            }

            menu.ShowAsContext();
            e.Use();
        }

        private void ContextCallback(object o)
        {
            var ua = (UserActions) o;
            switch (ua)
            {
                case UserActions.ADD_STATE:
                    EditorSettings.AddNodeOnGraph(EditorSettings.stateNode,
                        200f, 100f, "State", _mousePosition);
                    break;
                case UserActions.ADD_CONDITION:
                    var transitionNode = EditorSettings.AddNodeOnGraph(EditorSettings.transitionNode,
                        200f, 100f, "Condition", _mousePosition);
                    transitionNode.enterNode = _selectedNode.id;
                    var transition = EditorSettings.stateNode.AddTransition(_selectedNode);
                    transitionNode.transRefs.transitionId = transition.id;
                    break;
                case UserActions.ADD_COMMENT:
                    var commentNode = EditorSettings.AddNodeOnGraph(EditorSettings.commentNode,
                        180f, 60f, "Comment", _mousePosition);
                    commentNode.comment = "This is a Comment!";
                    break;
                case UserActions.DELETE_NODE:
                    EditorSettings.currentGraph.DeleteNode(_selectedNode.id);
                    break;
            }

            EditorUtility.SetDirty(EditorSettings);
        }

        #endregion

        #region Helper Methods

        public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor)
        {
            var startPos = new Vector3(
                (left)
                    ? start.x + start.width
                    : start.x, start.y + (start.height * 0.5f), 0f);

            var endPos = new Vector3(end.x + (end.width * 0.5f), end.y + end.height * 0.5f, 0f);
            var startTan = startPos + Vector3.right * 50;
            var endTan = endPos + Vector3.left * 50;
            var shadow = new Color(0, 0, 0, 0.06f);

            for (var i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos,
                    startTan, endTan, shadow, null, (i + 1) * 0.5f);
            }

            Handles.DrawBezier(startPos, endPos,
                startTan, endTan, curveColor, null, 1);
        }

        #endregion
    }
}
