/*
* InputManager - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Behaviour.Actions;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "MonoActions/Move Animation")]
    public class MovementAnimations : StateAction
    {
        public string verticalFloatName;
        public InputManager inputManager;
        
        
        public override void Execute(StateManager stateManager)
        {
            stateManager.myAnimator.SetFloat(verticalFloatName, inputManager.moveAmount, 0.2f, stateManager.deltaTime);
        }
    }
}
