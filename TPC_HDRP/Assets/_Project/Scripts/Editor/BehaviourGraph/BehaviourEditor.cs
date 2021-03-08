/*
 * BehaviourEditor - Custom Unity Editor Window for Behaviour Node Graph creation/editing
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEditor;
using UnityEngine;
using System.Linq;
using ANM.Managers;
using ANM.Editor.Nodes;
using ANM.Scriptables.Behaviour;

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
            ADD_COMMENT,
            MAKE_TRANSITION,
            ADD_PORTAL,
            RESET_PANNING
        }

        private BaseNode _selectedNode;
        private Vector3 _mousePosition;
        private Vector2 _scrollPosition;
        private Vector2 _scrollStartPosition;
        private bool _clickedOnWindow;
        private int _transitFromId;
        private Rect _mouseRect = new Rect(0, 0, 1, 1);

        private GUIStyle _activeStyle;
        private GUIStyle _defaultStyle;

        private static State _prevState;
        public static bool ForceSetDirty;

        private static StateManager _stateManager;
        private static StateManager _prevStateManager;


        [MenuItem("Behaviour Editor/Editor")]
        private static void ShowEditor()
        {
            var editor = GetWindow<BehaviourEditor>();
            editor.minSize = new Vector2(960, 600);
            editor.titleContent.text = "Behaviour Editor";
            editor.titleContent.tooltip = "Used to visually script behaviour patterns using nodes";
        }

        private void OnEnable()
        {
            EditorSettings = Resources.Load("BehaviourEditorSettings") as EditorSettings;
            _activeStyle = EditorSettings?.activeSkin.GetStyle("window");
            if (_activeStyle == null) return;
            _activeStyle.onNormal.textColor = Color.cyan;
            _activeStyle.normal.textColor = Color.green;
        }

        #region GUI Methods

        private void OnGUI()
        {
            _defaultStyle = new GUIStyle(GUI.skin.window)
            {
                padding = new RectOffset(8, 8, 24, 4),
            };

            if (Selection.activeTransform != null)
            {
                _stateManager = Selection.activeTransform.GetComponentInChildren<StateManager>();
                if (_prevStateManager != _stateManager)
                {
                    _prevStateManager = _stateManager;
                    Repaint();
                }
            }

            var e = Event.current;
            _mousePosition = e.mousePosition;
            UserInput(e);
            DrawWindows();

            if (EditorSettings.currentGraph != null)
            {
                if (e.type == EventType.MouseDrag || GUI.changed)
                {
                    EditorSettings.currentGraph.DeleteWindowsThatNeedTo();
                    Repaint();
                }
            }

            if (EditorSettings.makeTransition)
            {
                _mouseRect.x = _mousePosition.x;
                _mouseRect.y = _mousePosition.y;
                var from = EditorSettings.currentGraph.GetNodeWithIndex(_transitFromId).windowRect;
                DrawNodeCurve(from, _mouseRect, true, Color.yellow);
                Repaint();
            }

            if (!ForceSetDirty) return;
            EditorUtility.SetDirty(EditorSettings);
            EditorUtility.SetDirty(EditorSettings.currentGraph);

            for (var i = 0; i < EditorSettings.currentGraph.windows.Count; i++)
            {
                var node = EditorSettings.currentGraph.windows[i];
                if (node.stateRefs.currentState != null)
                    EditorUtility.SetDirty(node.stateRefs.currentState);
            }

            ForceSetDirty = false;
        }

        private void DrawWindows()
        {
            BeginWindows();

            EditorGUILayout.LabelField(" ", GUILayout.Width(100));
            EditorGUILayout.LabelField("\tAssign Graph :", GUILayout.Width(100));

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
                    var node = EditorSettings.currentGraph.windows[i];
                    if (node.drawNode is StateNode)
                    {
                        if (_stateManager != null && node.stateRefs.currentState == _stateManager.currentState)
                            node.windowRect = GUI.Window(i, node.windowRect,
                                DrawNodeWindow, node.windowTitle, _activeStyle);
                        else
                            node.windowRect = GUI.Window(i, node.windowRect,
                                DrawNodeWindow, node.windowTitle, _defaultStyle);
                    }
                    else
                        node.windowRect = GUI.Window(i, node.windowRect,
                            DrawNodeWindow, node.windowTitle, _defaultStyle);
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
                case 1 when !EditorSettings.makeTransition:
                    if (e.type == EventType.MouseDown) RightClick(e);
                    break;

                case 0 when !EditorSettings.makeTransition:
                    if (e.type == EventType.MouseDown) LeftClick(e);
                    break;

                case 0 when EditorSettings.makeTransition:
                    if (e.type == EventType.MouseDown) MakeTransition();
                    break;

                case 2:
                    MiddleClick(e);
                    break;
            }
        }

        private void LeftClick(Event e)
        {

        }

        private void MiddleClick(Event e)
        {
            if (e.type == EventType.MouseDown)
                _scrollStartPosition = e.mousePosition;
            else if (e.type == EventType.MouseDrag)
                HandlePanning(e);
        }

        private void HandlePanning(Event e)
        {
            Vector2 diff = e.mousePosition - _scrollStartPosition;
            diff *= 0.6f;

            _scrollStartPosition = e.mousePosition;
            _scrollPosition += diff;

            for (var i = 0; i < EditorSettings.currentGraph.windows.Count; i++)
            {
                BaseNode node = EditorSettings.currentGraph.windows[i];
                node.windowRect.x += diff.x;
                node.windowRect.y += diff.y;
            }
        }

        private void ResetPanning()
        {
            for (var i = 0; i < EditorSettings.currentGraph.windows.Count; i++)
            {
                BaseNode node = EditorSettings.currentGraph.windows[i];
                node.windowRect.x -= _scrollPosition.x;
                node.windowRect.y -= _scrollPosition.y;
            }

            _scrollPosition = Vector2.zero;
        }

        private void RightClick(Event e)
        {
            _clickedOnWindow = false;

            foreach (var node in EditorSettings.currentGraph.windows
                .Where(node => node.windowRect.Contains(e.mousePosition)))
            {
                _clickedOnWindow = true;
                _selectedNode = node;
                break;
            }

            if (!_clickedOnWindow) AddNewNode(e);
            else ModifyNode(e);
        }

        private void MakeTransition()
        {
            _clickedOnWindow = false;
            EditorSettings.makeTransition = false;
            foreach (var node in EditorSettings.currentGraph.windows
                .Where(node => node.windowRect.Contains(_mousePosition)))
            {
                _clickedOnWindow = true;
                _selectedNode = node;
                break;
            }

            if (!_clickedOnWindow) return;

            if (_selectedNode.drawNode is StateNode || _selectedNode.drawNode is PortalNode)
            {
                if (_selectedNode.id == _transitFromId) return;
                var transNode = EditorSettings.currentGraph.GetNodeWithIndex(_transitFromId);
                transNode.targetNode = _selectedNode.id;

                var enterNode = EditorSettings.currentGraph.GetNodeWithIndex(transNode.enterNode);
                var transition = enterNode.stateRefs.currentState.GetTransition(transNode.transRefs.transitionId);

                transition.targetState = _selectedNode.stateRefs.currentState;
            }
        }

        #endregion

        #region Context Menu

        private void AddNewNode(Event e)
        {
            var menu = new GenericMenu();
            menu.AddSeparator("");
            if (EditorSettings.currentGraph != null)
            {
                menu.AddItem(new GUIContent("Add State"),
                    false, ContextCallback, UserActions.ADD_STATE);
                menu.AddItem(new GUIContent("Add Portal"),
                    false, ContextCallback, UserActions.ADD_PORTAL);
                menu.AddItem(new GUIContent("Add Comment"),
                    false, ContextCallback, UserActions.ADD_COMMENT);
                menu.AddItem(new GUIContent("Reset Panning"),
                    false, ContextCallback, UserActions.RESET_PANNING);
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
            if (_selectedNode.drawNode is StateNode)
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
                if (_selectedNode.isDuplicate || !_selectedNode.isAssigned)
                {
                    menu.AddDisabledItem(new GUIContent("Make Transition"));
                }
                else
                {
                    menu.AddItem(new GUIContent("Make Transition"), false,
                        ContextCallback, UserActions.MAKE_TRANSITION);
                }

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

            if (_selectedNode.drawNode is PortalNode)
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
                        200f, 80f, "State", _mousePosition);
                    break;

                case UserActions.ADD_CONDITION:
                    AddTransition(_selectedNode, _mousePosition);
                    break;

                case UserActions.ADD_PORTAL:
                    EditorSettings.AddNodeOnGraph(EditorSettings.portalNode,
                        200f, 50f, "Portal", _mousePosition);
                    break;

                case UserActions.ADD_COMMENT:
                    EditorSettings.AddNodeOnGraph(EditorSettings.commentNode,
                        200f, 90f, "Comment", _mousePosition);
                    break;

                case UserActions.MAKE_TRANSITION:
                    _transitFromId = _selectedNode.id;
                    EditorSettings.makeTransition = true;
                    break;

                case UserActions.DELETE_NODE:
                    if (_selectedNode.drawNode is TransitionNode)
                    {
                        var enterNode = EditorSettings.currentGraph.GetNodeWithIndex(_selectedNode.enterNode);
                        enterNode.stateRefs.currentState.RemoveTransition(_selectedNode.transRefs.transitionId);
                    }

                    EditorSettings.currentGraph.DeleteNode(_selectedNode.id);
                    break;

                case UserActions.RESET_PANNING:
                    ResetPanning();
                    break;
            }

            ForceSetDirty = true;
        }

        #endregion

        #region Helper Methods

        public static void DrawNodeCurve(Rect start, Rect end, bool left, Color curveColor)
        {
            var startPos = new Vector3(left ? start.x + start.width : start.x,
                start.y + (start.height * 0.5f), 0f);

            var endPos = new Vector3(end.x + (end.width * 0.5f), end.y + end.height * 0.5f, 0f);
            var startTan = startPos + Vector3.right * 50;
            var endTan = endPos + Vector3.left * 50;
            var shadow = new Color(0, 0, 0, 0.06f);

            for (var i = 0; i < 3; i++)
            {
                Handles.DrawBezier(startPos, endPos,
                    startTan, endTan, shadow, null, (i + 1) * 1);
            }

            Handles.DrawBezier(startPos, endPos,
                startTan, endTan, curveColor, null, 2);
        }

        public static BaseNode AddTransition(BaseNode enterNode, Vector3 pos)
        {
            var tNode = EditorSettings.AddNodeOnGraph(EditorSettings.transitionNode,
                200, 86, "Condition", pos);
            tNode.enterNode = enterNode.id;
            var transition = StateNode.AddTransition(enterNode);
            tNode.transRefs.transitionId = transition.id;
            return tNode;
        }

        public static BaseNode AddTransitionNodeFromTransition(Transition transition, BaseNode enterNode, Vector3 pos)
        {
            var tNode = EditorSettings.AddNodeOnGraph(EditorSettings.transitionNode,
                200, 86, "Condition", pos);
            tNode.transRefs.transitionId = transition.id;
            tNode.enterNode = enterNode.id;
            return tNode;
        }

        #endregion
    }
}
