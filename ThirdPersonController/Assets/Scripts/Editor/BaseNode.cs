﻿/*
 * BaseNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEngine;
using ANM.Editor.Nodes;

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
        [HideInInspector] public State currentState;
        [HideInInspector] public State previousState;
    }

    [System.Serializable]
    public class TransitionNodeReferences
    {
        [HideInInspector] public Condition previousCondition;
        public int transitionId;
    }
}
