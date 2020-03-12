/*
* MoveWithRootMotion - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Move With RootMotion")]
    public class MoveWithRootMotion : StateAction
    {
        public override void Execute(StateManager state)
        {
            Vector3 v = state.myRigidbody.velocity;
            Vector3 tv = state.myAnimator.deltaPosition;
            tv *= 60f;
            tv.y = v.y;
            state.myRigidbody.velocity = tv;
        }
    }
}
