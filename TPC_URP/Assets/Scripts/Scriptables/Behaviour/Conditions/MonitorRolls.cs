/*
* MonitorRolls - Based on Roll Input (currently Xbox B || PC Left Shift) will tell the State to
*     BackStep (press roll input with no direction), Roll(press roll input with a direction) or
*     Sprint (hold roll input for longer than 0.5 seconds)
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Scriptables.Actions;

namespace ANM.Scriptables.Behaviour.Conditions
{
    [CreateAssetMenu(menuName = "Behaviours/Conditions/Monitor Roll Input")]
    public class MonitorRolls : Condition
    {
        public InputManager inpManager;
        private float _bTimer;
        private static readonly int Vertical = Animator.StringToHash("vertical");


        public override bool CheckCondition(StateManager state)
        {
            var retVal = false;

            if (inpManager.b.isPressed)
            {
                _bTimer += Time.deltaTime;
                if (_bTimer > .5f)
                {
                    //    Sprint
                }
            }
            else
            {
                if (_bTimer > 0f)
                {
                    retVal = true;
                    state.generalDelta = 0f;
                    state.isBackstep = false;
                    
                    if (state.moveAmount > 0f)
                    {
                        state.myAnimator.SetFloat(Vertical, 1);
                        state.rollDirection = state.rotateDirection;
                    }
                    else
                    {
                        state.isBackstep = true;
                        state.myAnimator.SetFloat(Vertical, 0);
                        state.rollDirection = -state.myTransform.forward;
                    }

                    state.PlayAnimation("Rolls");
                }

                _bTimer = 0f;
            }

            return retVal;
        }
    }
}