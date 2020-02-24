/*
 * SpellAction SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;
using SA.Scriptable.Variables;

namespace SA.Scriptable
{
    public class SpellAction : ScriptableObject
    {
        public StringVariable start_animation;
        public StringVariable cast_animation;
        public float animSpeed = 1f;
        public bool changeSpeed = false;
    }
}