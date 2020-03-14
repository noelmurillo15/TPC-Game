/*
* ItemInputResponse - 
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Inventory;
using ANM.Scriptables.Variables;

namespace ANM.Behaviour.StateActions.AttackResponse
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Item Input Response")]
    public class ItemInputResponse : StateAction
    {
        public InputButtonVariable buttonVariable;


        public override void Execute(StateManager state)
        {
            AbstractEquippableItem item;
            switch (buttonVariable.value)
            {
                case StateManager.InputButton.RB:
                    item = state.inventory.rightHandWeapon as AbstractEquippableItem;
                    state.PlayAnimation(item?.throwAnim.value);
                    break;
                case StateManager.InputButton.LB:
                    item = state.inventory.leftHandWeapon as AbstractEquippableItem;
                    state.PlayAnimation(item?.throwAnim.value, true);
                    break;
                case StateManager.InputButton.RT:
                    item = state.inventory.rightHandWeapon as AbstractEquippableItem;
                    state.PlayAnimation(item?.useAnim.value);
                    break;
                case StateManager.InputButton.LT:
                    item = state.inventory.leftHandWeapon as AbstractEquippableItem;
                    state.PlayAnimation(item?.useAnim.value, true);
                    break;
            }
        }
    }
}
