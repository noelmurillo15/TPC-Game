/*
 * CommentNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEngine;

namespace ANM.Editor.Nodes
{
    public class CommentNode : BaseNode
    {
        private string _comment = "This is a comment";


        public override void DrawWindow()
        {
            _comment = GUILayout.TextArea(_comment, 200);
        }
    }
}
