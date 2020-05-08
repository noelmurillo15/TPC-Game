/*
* IsGrounded - Forces the state's position onto the ground (within 1.4f max distance)
* Created by : Allan N. Murillo
* Last Edited : 5/7/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Movement/IsGrounded")]
    public class IsGrounded : StateAction
    {
        [SerializeField] private float groundDistance = 1.4f;
        [SerializeField] private float onAirDistance = 1f;


        public override void Execute(StateManager state)
        {
            var origin = state.myTransform.position;
            origin.y += 0.7f;
            var dir = -Vector3.up;
            var distance = groundDistance;
            if (!state.isGrounded)
                distance = onAirDistance;

            state.isGrounded = Physics.SphereCast(origin, 0.3f, dir,
                out var hit, distance, state.ignoreForGroundCheck);

            if (!state.isGrounded) return;
            var targetPosition = state.myTransform.position;
            targetPosition.y = hit.point.y;
            state.myTransform.position = targetPosition;
        }
    }
}
