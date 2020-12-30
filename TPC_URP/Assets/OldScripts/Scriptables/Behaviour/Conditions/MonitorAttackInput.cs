/*
* MonitorAttackInput -
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Scriptables.Inventory;
using ANM.Scriptables.Variables;

namespace ANM.Scriptables.Behaviour.Conditions
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/Conditions/Monitor Attack Input")]
    public class MonitorAttackInput : Condition
    {
        public InputButtonVariable buttonVariable;
        public StateAction weaponInputResponse;
        public StateAction itemInputResponse;


        public override bool CheckCondition(StateManager state)
        {
            if (state.rb || state.rt)
            {
                buttonVariable.Set(state);
                var equippedItem = state.inventory.rightHandItem;
                if (equippedItem as AbstractEquippableItem)
                {
                    itemInputResponse.Execute(state);
                    return true;
                }

                if (equippedItem as AbstractWeapon)
                {
                    weaponInputResponse.Execute(state);
                    return true;
                }
            }

            if (!state.lb && !state.lt) return false;
            {
                buttonVariable.Set(state);
                var equippedItem = state.inventory.leftHandItem;
                if (equippedItem as AbstractEquippableItem)
                {
                    itemInputResponse.Execute(state);
                    return true;
                }

                if (!(equippedItem as AbstractWeapon)) return false;
                weaponInputResponse.Execute(state);
                return true;
            }
        }
    }
}
