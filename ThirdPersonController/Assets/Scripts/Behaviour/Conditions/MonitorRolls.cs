/*
* MonitorRolls - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Behaviour.Actions;

namespace ANM.Behaviour.Conditions
{
    [CreateAssetMenu(menuName = "Behaviours/Conditions/Roll Movement")]
    public class MonitorRolls : Condition
    {
        public InputManager inpManager;
        private float _bTimer;
        private static readonly int Vertical = Animator.StringToHash("vertical");


        public override bool CheckCondition(StateManager state)
        {
            bool retVal = false;

            if (inpManager.B.isPressed)
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
                    if (state.moveAmount > 0f)
                    {
                        state.myAnimator.SetFloat(Vertical, 1);
                        state.rollDirection = state.rotateDirection;
                    }
                    else
                    {
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