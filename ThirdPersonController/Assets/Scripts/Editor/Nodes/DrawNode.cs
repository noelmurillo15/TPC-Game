/*
 * DrawNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEngine;

namespace ANM.Editor.Nodes
{
    public abstract class DrawNode : ScriptableObject
    {
        public abstract void DrawWindow(BaseNode node);
        public abstract void DrawCurve(BaseNode node);
    }
}
