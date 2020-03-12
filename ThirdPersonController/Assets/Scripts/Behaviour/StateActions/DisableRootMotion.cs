/*
* DisableRootMotion - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Disable RootMotion")]
    public class DisableRootMotion : StateAction
    {
        public override void Execute(StateManager state)
        {
            state.myAnimator.applyRootMotion = false;
        }
    }
}
