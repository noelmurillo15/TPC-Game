/*
* IsGrounded - Forces the state's position onto the ground (within 1.4f max distance)
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Movement/Is Grounded")]
    public class IsGrounded : StateAction
    {
        public override void Execute(StateManager state)
        {
            var origin = state.myTransform.position;
            origin.y += 0.7f;
            var dir = -Vector3.up;
            const float distance = 1.4f;
            if (!Physics.Raycast(origin, dir, out var hit, distance)) return;
            var targetPosition = hit.point;
            state.transform.position = targetPosition;
        }
    }
}
