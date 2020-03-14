/*
* WeaponInputResponse - 
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Inventory;
using ANM.Scriptables.Variables;

namespace ANM.Behaviour.StateActions.AttackResponse
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Weapon Input Response")]
    public class WeaponInputResponse : StateAction
    {
        public InputButtonVariable buttonVariable;


        public override void Execute(StateManager state)
        {
            AbstractWeapon weapon;
            switch (buttonVariable.value)
            {
                case StateManager.InputButton.RB:
                    weapon = state.inventory.rightHandWeapon as AbstractWeapon;
                    state.PlayAnimation(weapon?.anim1.value);
                    break;
                case StateManager.InputButton.LB:
                    weapon = state.inventory.leftHandWeapon as AbstractWeapon;
                    state.PlayAnimation(weapon?.anim2.value, true);
                    break;
                case StateManager.InputButton.RT:
                    weapon = state.inventory.rightHandWeapon as AbstractWeapon;
                    state.PlayAnimation(weapon?.anim3.value);
                    break;
                case StateManager.InputButton.LT:
                    weapon = state.inventory.leftHandWeapon as AbstractWeapon;
                    state.PlayAnimation(weapon?.anim4.value, true);
                    break;
            }
        }
    }
}
