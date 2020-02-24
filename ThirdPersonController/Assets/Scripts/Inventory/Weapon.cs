/*
 * Weapon SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using System.Linq;
using ANM.Input;
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


        private ActionHolder GetActionHolder(InputType input)
        {
            return actions.FirstOrDefault(t => t.input == input);
        }

        public Action GetAction(InputType input)
        {
            var ah = GetActionHolder(input);
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