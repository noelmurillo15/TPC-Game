/*
* WeaponInputResponse - 
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Scriptables.Inventory;
using ANM.Scriptables.Variables;

namespace ANM.Scriptables.Behaviour.StateActions.AttackResponse
{
    using InputBtn = StateManager.InputButton;

    [CreateAssetMenu(menuName = "Behaviours/StateAction/Weapon Input Response")]
    public class WeaponInputResponse : StateAction
    {
        public InputButtonVariable button;

        public override void Execute(StateManager state)
        {
            switch (button.value)
            {
                case InputBtn.RB:
                case InputBtn.RT:
                    PlayRightHandAttack(state, button.value == InputBtn.RB);
                    break;
                case InputBtn.LB:
                case InputBtn.LT:
                    PlayLeftHandAttack(state, button.value == InputBtn.LB);
                    break;
            }
        }

        private static void PlayRightHandAttack(StateManager state, bool isBumper)
        {
            var rhWeapon = state.inventory.rightHandItem as AbstractWeapon;
            state.PlayAnimation(isBumper ? rhWeapon?.anim1.value : rhWeapon?.anim3.value);
        }

        private static void PlayLeftHandAttack(StateManager state, bool isBumper)
        {
            var lhWeapon = state.inventory.leftHandItem as AbstractWeapon;
            state.PlayAnimation(isBumper ? lhWeapon?.anim2.value : lhWeapon?.anim4.value, true);
        }
    }
}
