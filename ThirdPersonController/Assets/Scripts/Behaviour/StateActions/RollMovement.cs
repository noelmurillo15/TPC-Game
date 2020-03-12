/*
* RollMovement - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Roll Movement")]
    public class RollMovement : StateAction
    {
        public AnimationCurve speedCurve;
        public float speed = 3f;
        
        
        public override void Execute(StateManager state)
        {
            state.myRigidbody.drag = 0f;
            state.generalDelta += Time.deltaTime;
            
            Vector3 velocity = state.myRigidbody.velocity;
            Vector3 targetVelocity = state.rollDirection;
            
            targetVelocity *= speedCurve.Evaluate(state.generalDelta) * speed;
            targetVelocity.y = velocity.y;
            state.myRigidbody.velocity = targetVelocity;
        }
        
        // Vector3 relativeDir = stateManager.myTransform.InverseTransformDirection(stateManager.rotateDirection);
        // float v = relativeDir.z;
        // float h = relativeDir.x;
            
        //
        // if (relativeDir == Vector3.zero)
        // {
        //     //  if no directional input, play step back animation
        //     inputVar.moveDir = -myTransform.forward;
        //     inputVar.targetRollSpeed = controlStats.backStepSpeed;
        // }
        // else
        // {
        //     //  else roll using directional input
        //     inputVar.targetRollSpeed = controlStats.rollSpeed;
        // }
        //
        // //  Override root motion multiplier
        // animatorHook.rm_mult = inputVar.targetRollSpeed;
        //
        // //  Set Animations floats using relative Direction
        // myAnimator.SetFloat(StaticStrings.vertical, v);
        // myAnimator.SetFloat(StaticStrings.horizontal, h);
        //
        // //  Play Animation and change state
        // PlayActionAnimation(StaticStrings.rolls);
        // ChangeState(CharacterState.ROLL);
    }
}