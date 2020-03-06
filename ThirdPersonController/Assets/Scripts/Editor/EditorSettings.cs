/*
 * EditorSettings - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEngine;
using ANM.Editor.Nodes;

namespace ANM.Editor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Editor Settings")]
    public class EditorSettings : ScriptableObject
    {
        public StateNode stateNode;
        [HideInInspector] public BehaviourGraph CurrentGraph;
        [HideInInspector] public TransitionNode TransitionNode;
        [HideInInspector] public CommentNode CommentNode;
    }
}
