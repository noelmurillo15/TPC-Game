/*
* DisableRootMotion - Disables an Animators root motion
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Movement/Disable RootMotion")]
    public class DisableRootMotion : StateAction
    {
        public override void Execute(StateManager state)
        {
            state.myAnimator.applyRootMotion = false;
        }
    }
}
