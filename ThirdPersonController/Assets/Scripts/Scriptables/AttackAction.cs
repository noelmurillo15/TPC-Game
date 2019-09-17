using UnityEngine;


namespace SA.Scriptable
{
    [CreateAssetMenu(menuName = "Actions/AttackAction")]
    public class AttackAction : ScriptableObject
    {   //  Holds Static String Animation Name and other info
        public StringVariable attackAnimation;
        public bool canBeParried = true;
        public bool changeSpeed = false;
        public float animSpeed = 1f;
        public bool canParry = false;
        public bool canBackStab = false;
    }
}