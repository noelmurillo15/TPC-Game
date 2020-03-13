/*
* EnableRootMotion - Enables an Animators root motion
* Created by : Allan N. Murillo
* Last Edited : 3/12/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Enable RootMotion")]
    public class EnableRootMotion : StateAction
    {
        public override void Execute(StateManager state)
        {
            state.myAnimator.applyRootMotion = true;
        }
    }
}
