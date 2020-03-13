/*
* RollMovement - Root Motion Movement when in Rolling State
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Roll Movement")]
    public class RollMovement : StateAction
    {
        public AnimationCurve speedCurve;
        public float rollSpeed = 3f;
        public float backstepSpeed = 2f;
        
        
        public override void Execute(StateManager state)
        {
            state.myRigidbody.drag = 0f;
            state.generalDelta += Time.deltaTime;
            
            Vector3 velocity = state.myRigidbody.velocity;
            Vector3 targetVelocity = (state.isBackstep) 
                ? state.rollDirection * backstepSpeed 
                : state.rollDirection * rollSpeed;
            
            targetVelocity.y = 0f;
            targetVelocity *= speedCurve.Evaluate(state.generalDelta);
            targetVelocity.y = velocity.y;
            state.myRigidbody.velocity = targetVelocity;
        }
    }
}