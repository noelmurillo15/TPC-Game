/*
 * CommentNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEngine;

namespace ANM.Editor.Nodes
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Comment Node")]
    public class CommentNode : DrawNode
    {
        private string _comment = "This is a comment";

        
        public override void DrawWindow(BaseNode b)
        {
            _comment = GUILayout.TextArea(_comment, 200);
        }

        public override void DrawCurve(BaseNode node)
        {

        }
    }
}
