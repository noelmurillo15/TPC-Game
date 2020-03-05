/*
 * BehaviourEditor - Custom Unity Editor Window for Behaviour Node Graph creation/editing
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using ANM.BehaviourNodeEditor.Nodes;

namespace ANM.BehaviourNodeEditor
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


        [MenuItem("Behaviour Editor/Editor")]
        private static void ShowEditor()
        {
            var editor = GetWindow<BehaviourEditor>();
            editor.minSize = new Vector2(800, 600);
        }

        private void OnEnable()
        {
            _windows.Clear();
        }

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
            menu.AddItem(new GUIContent("Add State"), false,
                ContextCallback, UserActions.ADD_STATE);
            menu.AddItem(new GUIContent("Add Comment"),
                false, ContextCallback, UserActions.COMMENT_NODE);
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
                    var newStateNode = CreateInstance<StateNode>();
                    newStateNode.windowRect = new Rect(
                        _mousePosition.x, _mousePosition.y, 200, 300);
                    newStateNode.windowTitle = "State";
                    _windows.Add(newStateNode);
                    break;
                case UserActions.COMMENT_NODE:
                    var newComment = CreateInstance<CommentNode>();
                    newComment.windowRect = new Rect(
                        _mousePosition.x, _mousePosition.y, 200, 100);
                    newComment.windowTitle = "Comment Node";
                    _windows.Add(newComment);
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

            var transitionNode = CreateInstance<TransitionNode>();
            transitionNode.Init(from, transition);
            transitionNode.windowRect = new Rect(
                fromRect.x + 300, fromRect.y + (fromRect.height * 0.7f), 200, 80);
            transitionNode.windowTitle = "Condition Check";
            _windows.Add(transitionNode);
            return transitionNode;
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
            if (dependencyNodes == null)
            {
                Debug.Log("Node Dependencies is NULL");
                return;
            }

            foreach (var bNode in dependencyNodes.Where(bNode => _windows.Contains(bNode)))
            {
                _windows.Remove(bNode);
            }
        }
    }
}
