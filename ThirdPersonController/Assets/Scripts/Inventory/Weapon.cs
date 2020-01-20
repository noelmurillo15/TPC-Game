using System.Linq;
using SA.Input;
using UnityEngine;
using SA.Scriptable;
using SA.Scriptable.Variables;


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


        private ActionHolder GetActionHolder(InputType _input)
        {
            return actions.FirstOrDefault(t => t.input == _input);
        }

        public Action GetAction(InputType _input)
        {
            ActionHolder ah = GetActionHolder(_input);
            return ah?.action;
        }
    }

    [System.Serializable]
    public class ActionHolder
    {
        public InputType input;
        public Action action;
    }
}