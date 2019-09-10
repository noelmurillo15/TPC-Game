using UnityEngine;


namespace SA.Scriptable
{
    [CreateAssetMenu(menuName = "Action/AttackAction")]
    public class AttackAction : ScriptableObject
    {
        public StringVariable attackAnimation;
        public bool canBeParried = true;
        public bool changeSpeed = false;
        public float animSpeed = 1f;
        public bool canParry = false;
        public bool canBackStab = false;
    }
}