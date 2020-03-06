/*
 * BehaviourEditor - Custom Unity Editor Window for Behaviour Node Graph creation/editing
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using System.Linq;
using UnityEditor;
using UnityEngine;
using ANM.Editor.Nodes;
using System.Collections.Generic;

namespace ANM.Editor
{
    public class BehaviourEditor : EditorWindow
    {
        private static List<BaseNode> _windows = new List<BaseNode>();

        private enum UserActions
        {
            ADD_STATE,
            ADD_TRANSITION,
            DELETE_NODE,
            COMMENT_NODE
        }

        private BaseNode _selectedNode;
        private Vector3 _mousePosition;
        private bool _clickedOnWindow;
        private bool _makeTransition;
        private int _selectedIndex;

        private static GraphNode _graphNode;
        public static BehaviourGraph CurrentGraph;


        [MenuItem("Behaviour Editor/Editor")]
        private static void ShowEditor()
        {
            var editor = GetWindow<BehaviourEditor>();
            editor.minSize = new Vector2(800, 600);
        }

        private void OnEnable()
        {
            if (_graphNode == null)
            {
                _graphNode = CreateInstance<GraphNode>();
                _graphNode.windowRect = new Rect(10, position.height * 0.7f, 200, 100);
                _graphNode.windowTitle = "Graph";
            }

            _windows.Clear();
            _windows.Add(_graphNode);
            LoadGraph();
        }

        #region GUI Methods

        private void OnGUI()
        {
            var e = Event.current;
            _mousePosition = e.mousePosition;
            UserInput(e);
            DrawWindows();
        }

        private void DrawWindows()
        {
            BeginWindows();
            foreach (var node in _windows)
            {
                node.DrawCurve();
            }

            for (var i = 0; i < _windows.Count; i++)
            {
                _windows[i].windowRect = GUI.Window(i,
                    _windows[i].windowRect, DrawNodeWindow, _windows[i].windowTitle);
            }

            EndWindows();
        }

        private static void DrawNodeWindow(int id)
        {
            _windows[id].DrawWindow();
            GUI.DragWindow();
        }

        private void UserInput(Event e)
        {
            switch (e.button)
            {
                case 1 when !_makeTransition:
                {
                    if (e.type == EventType.MouseDown)
                        RightClick(e);
                    break;
                }
                case 0 when !_makeTransition:
                {
                    if (e.type == EventType.MouseDown)
                        LeftClick(e);
                    break;
                }
            }
        }

        private void LeftClick(Event e)
        {

        }

        private void RightClick(Event e)
        {
            _selectedIndex = -1;
            _clickedOnWindow = false;

            for (var i = 0; i < _windows.Count; i++)
            {
                if (!_windows[i].windowRect.Contains(e.mousePosition)) continue;
                _selectedIndex = i;
                _clickedOnWindow = true;
                _selectedNode = _windows[i];
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
            if (CurrentGraph != null)
            {
                menu.AddItem(new GUIContent("Add State"),
                    false, ContextCallback, UserActions.ADD_STATE);
                menu.AddItem(new GUIContent("Add Comment"),
                    false, ContextCallback, UserActions.COMMENT_NODE);
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
            if (_selectedNode is StateNode stateNode)
            {
                if (stateNode.currentState != null)
                {
                    menu.AddSeparator("");
                    menu.AddItem(new GUIContent("Add Transition"), false,
                        ContextCallback, UserActions.ADD_TRANSITION);
                }
                else
                {
                    menu.AddSeparator("");
                    menu.AddDisabledItem(new GUIContent("Add Transition"));
                }

                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false,
                    ContextCallback, UserActions.DELETE_NODE);
            }

            if (_selectedNode is TransitionNode)
            {
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete"), false,
                    ContextCallback, UserActions.DELETE_NODE);
            }

            if (_selectedNode is CommentNode)
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
                    AddStateNode(_mousePosition);
                    break;
                case UserActions.COMMENT_NODE:
                    AddCommentNode(_mousePosition);
                    break;
                case UserActions.ADD_TRANSITION:
                    if (_selectedNode is StateNode st)
                    {
                        var transition = st.AddTransition();
                        AddTransitionNode(st.currentState.transitions.Count, transition, st);
                    }

                    break;
                case UserActions.DELETE_NODE:
                    switch (_selectedNode)
                    {
                        case StateNode stateNode:
                            stateNode.ClearReferences();
                            _windows.Remove(stateNode);
                            break;
                        case TransitionNode transitionNode:
                            _windows.Remove(transitionNode);
                            var curState = transitionNode.enterState.currentState;
                            if (curState.transitions.Contains(transitionNode.targetTransition))
                                curState.transitions.Remove(transitionNode.targetTransition);
                            break;
                        case CommentNode commentNode:
                            _windows.Remove(commentNode);
                            break;
                    }

                    break;
            }
        }

        #endregion

        #region Helper Methods

        public static TransitionNode AddTransitionNode(int index, Transition transition, StateNode from)
        {
            Rect fromRect = from.windowRect;
            fromRect.x += 50;
            float targetY = fromRect.y - fromRect.height;

            if (from.currentState != null)
            {
                targetY += (index * 100);
            }

            fromRect.y = targetY;
            var pos = new Vector2(fromRect.x, fromRect.y);
            return AddTransitionNode(pos, transition, from);
        }

        public static TransitionNode AddTransitionNode(Vector2 pos, Transition transition, StateNode from)
        {
            var transitionNode = CreateInstance<TransitionNode>();
            transitionNode.Init(from, transition);
            transitionNode.windowRect = new Rect(pos.x, pos.y, 200, 80);
            transitionNode.windowTitle = "Condition Check";
            _windows.Add(transitionNode);
            from.dependencies.Add(transitionNode);
            return transitionNode;
        }

        public static StateNode AddStateNode(Vector2 pos)
        {
            var newStateNode = CreateInstance<StateNode>();
            newStateNode.windowRect = new Rect(pos.x, pos.y, 200, 300);
            newStateNode.windowTitle = "State";
            _windows.Add(newStateNode);
            CurrentGraph.SetStateNode(newStateNode);
            return newStateNode;
        }

        public static CommentNode AddCommentNode(Vector2 pos)
        {
            var newCommentNode = CreateInstance<CommentNode>();
            newCommentNode.windowRect = new Rect(pos.x, pos.y, 180, 80);
            newCommentNode.windowTitle = "Comment Node";
            _windows.Add(newCommentNode);
            return newCommentNode;
        }

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

        public static void ClearWindowsFromList(List<BaseNode> dependencyNodes)
        {
            foreach (var bNode in dependencyNodes.Where(bNode => _windows.Contains(bNode)))
            {
                _windows.Remove(bNode);
            }
        }

        public static void LoadGraph()
        {
            CurrentGraph.Init();
            List<SavedStateNode> l = new List<SavedStateNode>();
            l.AddRange(CurrentGraph.savedStateNodes);
            CurrentGraph.savedStateNodes.Clear();

            for (int i = l.Count - 1; i >= 0; i--)
            {
                StateNode node = AddStateNode(l[i].position);
                node.currentState = l[i].state;
                CurrentGraph.SetStateNode(node);

                //    TODO : Load our Transitions
            }
        }

        #endregion
    }
}
