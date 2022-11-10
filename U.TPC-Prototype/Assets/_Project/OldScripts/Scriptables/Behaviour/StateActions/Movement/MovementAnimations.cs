/*
* MovementAnimations - Plays the locomotion animation based on vertical movement
* Created by : Allan N. Murillo
* Last Edited : 5/11/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/StateAction/Movement/Move Animation")]
    public class MovementAnimations : StateAction
    {
        private static readonly int Vertical = Animator.StringToHash("vertical");


        public override void Execute(StateManager state)
        {
            state.myAnimator.SetFloat(Vertical, state.moveAmount, 0.2f, state.delta);
        }
    }
}
