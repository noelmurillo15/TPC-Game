/*
* InitializeRigidbody -
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Init
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/StateAction/Init/Rigidbody")]
    public class InitializeRigidbody : StateAction
    {
        public override void Execute(StateManager state)
        {
            state.myRigidbody.angularDrag = 999;
            state.myRigidbody.drag = 4;
            state.myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX
                                            | RigidbodyConstraints.FreezeRotationY
                                            | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
