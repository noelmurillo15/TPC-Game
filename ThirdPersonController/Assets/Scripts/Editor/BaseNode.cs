/*
 * BaseNode -
 * Created by : Allan N. Murillo
 * Last Edited : 3/7/2020
 */

using UnityEngine;
using UnityEditor;
using ANM.Editor.Nodes;
using UnityEditorInternal;
using ANM.Behaviour.Conditions;
using State = ANM.Behaviour.State;

namespace ANM.Editor
{
    [System.Serializable]
    public class BaseNode
    {
        public int id;
        public DrawNode drawNode;

        public Rect windowRect;
        public string windowTitle;

        public int enterNode;
        public int targetNode;
        public bool isDuplicate;
        public bool isAssigned;
        public string comment;

        public bool collapse;
        [HideInInspector] public bool previousCollapse;

        [SerializeField] public StateNodeReferences stateRefs;
        [SerializeField] public TransitionNodeReferences transRefs;


        public void DrawWindow()
        {
            if (drawNode != null)
            {
                drawNode.DrawWindow(this);
            }
        }

        public void DrawCurve()
        {
            if (drawNode != null)
            {
                drawNode.DrawCurve(this);
            }
        }
    }

    [System.Serializable]
    public class StateNodeReferences
    {
        public State currentState;
        [HideInInspector] public State previousState;
        public SerializedObject serializedState;
        public ReorderableList onEnterList;
        public ReorderableList onStateList;
        public ReorderableList onExitList;
    }

    [System.Serializable]
    public class TransitionNodeReferences
    {
        [HideInInspector] public Condition previousCondition;
        public int transitionId;
    }
}
