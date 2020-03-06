/*
 * GraphNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEditor;

namespace ANM.Editor.Nodes
{
    public class GraphNode : BaseNode
    {
        private BehaviourGraph _previousGraph;
        
        
        public override void DrawWindow()
        {
            if (BehaviourEditor.CurrentGraph == null)
            {
                EditorGUILayout.LabelField("Add Graph to modify?");
            }

            BehaviourEditor.CurrentGraph = (BehaviourGraph) EditorGUILayout.ObjectField(
                BehaviourEditor.CurrentGraph, typeof(BehaviourGraph), false);

            if (BehaviourEditor.CurrentGraph == null)
            {
                if (_previousGraph != null)
                {
                    //    TODO : Clear Windows
                    _previousGraph = null;
                }
                EditorGUILayout.LabelField("No Graph Assigned!");
                return;
            }

            if (_previousGraph != BehaviourEditor.CurrentGraph)
            {
                _previousGraph = BehaviourEditor.CurrentGraph;
                //    TODO : Load Graph
            }
        }
    }
}
