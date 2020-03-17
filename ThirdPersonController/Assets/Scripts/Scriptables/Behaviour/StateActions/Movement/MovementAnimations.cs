/*
* MovementAnimations - Plays the locomotion animation based on vertical (MoveForward) movement
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Movement/Play Animation")]
    public class MovementAnimations : StateAction
    {
        public string verticalFloatName = "vertical";
        
        
        public override void Execute(StateManager state)
        {
            state.myAnimator.SetFloat(verticalFloatName, state.moveAmount, 0.2f, state.deltaTime);
        }
    }
}
