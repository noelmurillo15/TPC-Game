/*
* IsGrounded - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "MonoActions/Is Grounded")]
    public class IsGrounded : StateAction
    {
        public override void Execute(StateManager stateManager)
        {
            Vector3 origin = stateManager.myTransform.position;
            origin.y += 0.7f;
            var dir = -Vector3.up;
            var distance = 1.4f;
            Debug.DrawRay(origin, dir * distance);

            if (!Physics.Raycast(origin, dir, out var hit, distance)) return;
            Vector3 targetPosition = hit.point;
            stateManager.transform.position = targetPosition;
        }
    }
}
