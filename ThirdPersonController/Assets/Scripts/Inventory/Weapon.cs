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
        public LeftHandPosition leftHandPosition;


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
            if (ah == null) return null;
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