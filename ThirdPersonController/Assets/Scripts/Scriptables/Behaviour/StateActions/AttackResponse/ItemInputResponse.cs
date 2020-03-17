/*
* ItemInputResponse - 
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

    [CreateAssetMenu(menuName = "Behaviours/StateAction/Item Input Response")]
    public class ItemInputResponse : StateAction
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
            var rhItem = state.inventory.rightHandItem as AbstractEquippableItem;
            state.PlayAnimation(isBumper ? rhItem?.throwAnim.value : rhItem?.useAnim.value);
        }

        private static void PlayLeftHandAttack(StateManager state, bool isBumper)
        {
            var lhItem = state.inventory.leftHandItem as AbstractEquippableItem;
            state.PlayAnimation(isBumper ? lhItem?.throwAnim.value : lhItem?.useAnim.value, true);
        }
    }
}
