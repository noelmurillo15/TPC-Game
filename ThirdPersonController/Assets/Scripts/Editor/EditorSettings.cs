/*
 * EditorSettings - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/7/2020
 */

using UnityEngine;
using ANM.Editor.Nodes;

namespace ANM.Editor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Editor Settings")]
    public class EditorSettings : ScriptableObject
    {
        public BehaviourGraph currentGraph;
        public StateNode stateNode;
        public TransitionNode transitionNode;
        public CommentNode commentNode;
        public PortalNode portalNode;
        public bool makeTransition;
        public GUISkin activeSkin;

        public BaseNode AddNodeOnGraph(DrawNode type, float width, float height, string title, Vector3 pos)
        {
            var baseNode = new BaseNode
            {
                drawNode = type, windowRect = {width = width, height = height}, windowTitle = title
            };

            baseNode.windowRect.x = pos.x;
            baseNode.windowRect.y = pos.y;
            currentGraph.windows.Add(baseNode);
            baseNode.transRefs = new TransitionNodeReferences();
            baseNode.stateRefs = new StateNodeReferences();
            baseNode.id = currentGraph.idCount;
            currentGraph.idCount++;
            return baseNode;
        }
    }
}
