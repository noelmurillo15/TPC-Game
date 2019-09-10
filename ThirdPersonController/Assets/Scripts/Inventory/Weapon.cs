using UnityEngine;
using SA.Scriptable;


namespace SA.Inventory
{
    [CreateAssetMenu(menuName = "Items/Weapon")]
    public class Weapon : ScriptableObject
    {
        public StringVariable oh_idle;
        public StringVariable th_idle;
        public GameObject modelPrefab;
        public ActionHolder[] actions;


        public ActionHolder GetActionHolder(InputType _input)
        {
            for (int x = 0; x < actions.Length; x++)
            {
                if (actions[x].input == _input)
                {
                    return actions[x];
                }
            }

            return null;
        }

        public Action GetAction(InputType _input)
        {
            ActionHolder ah = GetActionHolder(_input);
            return ah.action;
        }
    }

    [System.Serializable]
    public class ActionHolder
    {
        public InputType input;
        public Action action;
    }
}

public enum InputType
{   //  Xbox Input
    RB, LB, RT, LT
}