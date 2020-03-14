/*
* MonitorAttackInput - 
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Inventory;
using ANM.Scriptables.Variables;

namespace ANM.Behaviour.Conditions
{
    [CreateAssetMenu(menuName = "Behaviours/Conditions/Monitor Attack Input")]
    public class MonitorAttackInput : Condition
    {
        public InputButtonVariable buttonVariable;
        public StateAction weaponInputResponse;
        public StateAction itemInputResponse;

        private AbstractWeapon _weapon;
        private AbstractEquippableItem _item;


        public override bool CheckCondition(StateManager state)
        {
            if (state.rb || state.rt)
            {
                buttonVariable.Set(state);
                var equippedItem = state.inventory.rightHandWeapon;

                _item = equippedItem as AbstractEquippableItem;
                if (_item != null)
                {
                    itemInputResponse.Execute(state);
                    return true;
                }

                _weapon = equippedItem as AbstractWeapon;
                if (_weapon != null)
                {
                    weaponInputResponse.Execute(state);
                    return true;
                }
            }

            if (state.lb || state.lt)
            {
                buttonVariable.Set(state);
                var equippedItem = state.inventory.leftHandWeapon;

                _item = equippedItem as AbstractEquippableItem;
                if (_item != null)
                {
                    itemInputResponse.Execute(state);
                    return true;
                }

                _weapon = equippedItem as AbstractWeapon;
                if (_weapon != null)
                {
                    weaponInputResponse.Execute(state);
                    return true;
                }
            }

            return false;
        }
    }
}
