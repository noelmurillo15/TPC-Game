/*
* MovementAnimations - 
* Created by : Allan N. Murillo
* Last Edited : 3/11/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "MonoActions/Move Animation")]
    public class MovementAnimations : StateAction
    {
        public string verticalFloatName;
        
        
        public override void Execute(StateManager stateManager)
        {
            stateManager.myAnimator.SetFloat(verticalFloatName, stateManager.moveAmount, 0.2f, stateManager.deltaTime);
        }
    }
}
