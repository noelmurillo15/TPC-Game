/*
* WaitForSpellAction -
* Created by : Allan N. Murillo
* Last Edited : 5/19/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Scriptables.Behaviour;

namespace ANM.Scriptables.Inventory.SpellActions
{
    [CreateAssetMenu(menuName = "Scriptables/Conditions/WaitForSpellAction")]
    public class WaitForSpellAction : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            var result = false;

            result = state.equippedSpellAction == null || state.equippedSpellAction.Cast(state);

            if (result)
            {
                state.equippedSpellAction = null;
            }

            return result;
        }
    }
}
