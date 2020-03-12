/*
* MovementAnimations - 
* Created by : Allan N. Murillo
* Last Edited : 3/11/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Movement Animation")]
    public class MovementAnimations : StateAction
    {
        public string verticalFloatName;
        
        
        public override void Execute(StateManager state)
        {
            state.myAnimator.SetFloat(verticalFloatName, state.moveAmount, 0.2f, state.deltaTime);
        }
    }
}
