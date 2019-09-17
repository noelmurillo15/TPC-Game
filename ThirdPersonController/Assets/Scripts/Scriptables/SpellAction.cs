using UnityEngine;


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