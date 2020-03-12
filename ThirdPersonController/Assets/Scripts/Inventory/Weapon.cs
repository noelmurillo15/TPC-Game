/*
 * Weapon SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/2/2020
 */

using System.Linq;
using ANM.Managers;
using UnityEngine;
using ANM.Scriptables.Variables;
using Actions = ANM.Scriptables.Action;

namespace ANM.Inventory
{
    [CreateAssetMenu(menuName = "Items/Weapon")]
    public class Weapon : Item
    {
        public StringVariable oneHandIdle;
        public StringVariable twoHandIdle;
        public GameObject modelPrefab;
        public ActionHolder[] actions;
        public LeftHandPosition leftHandPosition;

        
        public Weapon()
        {
            type = ItemType.WEAPON;
        }
        
        private ActionHolder GetActionHolder(StateManager.InputButton input)
        {
            return actions.FirstOrDefault(t => t.input == input);
        }

        public Actions GetAction(StateManager.InputButton input)
        {
            var ah = GetActionHolder(input);
            return ah?.action;
        }
    }

    [System.Serializable]
    public class ActionHolder
    {
        public StateManager.InputButton input;
        public Actions action;
    }
}