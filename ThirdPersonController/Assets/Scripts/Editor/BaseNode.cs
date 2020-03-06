/*
 * BaseNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEditor;
using UnityEngine;
using ANM.Editor.Nodes;
using UnityEditorInternal;
using System.Collections.Generic;

namespace ANM.Editor
{
    [System.Serializable]
    public class BaseNode
    {
        public DrawNode drawNode;
        public StateNodeReferences stateNodeRefs;

        public Rect windowRect;
        public string windowTitle;


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
        [HideInInspector] public bool collapse;
        [HideInInspector] public bool isDuplicate;
        [HideInInspector] public bool previousCollapse;
        [HideInInspector] public State currentState;
        [HideInInspector] public State previousState;
        [HideInInspector] public ReorderableList onEnterList;
        [HideInInspector] public ReorderableList onStateList;
        [HideInInspector] public ReorderableList onExitList;
        [HideInInspector] public SerializedObject serializedState;
        [HideInInspector] public List<BaseNode> dependencies = new List<BaseNode>();
    }
}
