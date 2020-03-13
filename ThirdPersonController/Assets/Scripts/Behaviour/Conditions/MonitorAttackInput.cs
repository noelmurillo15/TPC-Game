/*
* MonitorAttackInput - Based on Input, plays an attack animation
* Created by : Allan N. Murillo
* Last Edited : 3/12/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.Conditions
{
    [CreateAssetMenu(menuName = "Behaviours/Conditions/Monitor Attack Input")]
    public class MonitorAttackInput : Condition
    {
        public override bool CheckCondition(StateManager state)
        {
            if (state.rb)
            {
                state.PlayAttackAnimation(StateManager.InputButton.RB);
                return true;
            }

            if (state.lb)
            {
                state.PlayAttackAnimation(StateManager.InputButton.LB);
                return true;
            }

            if (state.rt)
            {
                state.PlayAttackAnimation(StateManager.InputButton.RT);
                return true;
            }

            if (state.lt)
            {
                state.PlayAttackAnimation(StateManager.InputButton.LT);
                return true;
            }
            return false;
        }
    }
}