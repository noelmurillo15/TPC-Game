using UnityEngine;


namespace SA.Scriptable
{
    [System.Serializable]
    public class Action
    {
        public ActionType actionType;
        public Object animationAction;
    }

    public enum ActionType
    {
        ATTACK, BLOCK, SPELL, PARRY
    }
}