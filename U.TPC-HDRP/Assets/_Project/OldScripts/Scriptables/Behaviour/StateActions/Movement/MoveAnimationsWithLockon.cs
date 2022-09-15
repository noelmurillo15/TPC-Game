/*
* MoveAnimationsWithLockon - Plays the lockon locomotion animation based on horizontal movement
* Created by : Allan N. Murillo
* Last Edited : 5/11/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/StateAction/Movement/Lockon Move Animation")]
    public class MoveAnimationsWithLockon : StateAction
    {
        private static readonly int Horizontal = Animator.StringToHash("horizontal");
        private static readonly int Vertical = Animator.StringToHash("vertical");


        public override void Execute(StateManager state)
        {
            state.myAnimator.SetFloat(Vertical, state.vertical, 0.2f, state.delta);
            state.myAnimator.SetFloat(Horizontal, state.horizontal, 0.2f, state.delta);
        }
    }
}
