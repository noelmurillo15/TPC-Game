/*
* MovementAnimations - Plays the locomotion animation based on vertical movement
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Movement/Move Animation")]
    public class MovementAnimations : StateAction
    {
        [SerializeField] private string verticalFloatName = "vertical";


        public override void Execute(StateManager state)
        {
            state.myAnimator.SetFloat(verticalFloatName, state.moveAmount, 0.2f, state.deltaTime);
        }
    }
}
