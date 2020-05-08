/*
* IsGroundedNoPlacement - Checks if the state's position is close to the ground
* Created by : Allan N. Murillo
* Last Edited : 5/7/2020
*/

using ANM.Managers;
using UnityEngine;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Movement/IsGrounded No Placement")]
    public class IsGroundedNoPlacement : StateAction
    {
        [SerializeField] private float groundDistance = 0.8f;
        [SerializeField] private float onAirDistance = 0.85f;


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
        }
    }
}
