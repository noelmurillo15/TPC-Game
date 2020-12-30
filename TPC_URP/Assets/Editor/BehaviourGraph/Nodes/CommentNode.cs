/*
 * CommentNode -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEngine;

namespace ANM.Editor.Nodes
{
    [CreateAssetMenu(menuName = "Scriptables/BehaviourEditor/Nodes/Comment Node")]
    public class CommentNode : DrawNode
    {
        private string _comment = "This is a comment";


        public override void DrawWindow(BaseNode node)
        {
            if (node.comment != null)
                _comment = node.comment;

            _comment = GUILayout.TextArea(_comment, 200);
            node.comment = _comment;
        }

        public override void DrawCurve(BaseNode node)
        {

        }
    }
}
