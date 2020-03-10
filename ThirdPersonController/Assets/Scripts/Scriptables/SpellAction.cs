/*
 * SpellAction SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Scriptables
{
    public class SpellAction : ScriptableObject
    {
        public StringVariable startAnimation;
        public StringVariable castAnimation;
        public float animSpeed = 1f;
        public bool changeSpeed = false;
    }
}
