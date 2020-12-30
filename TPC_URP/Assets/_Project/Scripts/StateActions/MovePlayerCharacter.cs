/*
* MovePlayerCharacter -
* Created by : Allan N. Murillo
* Last Edited : 8/18/2020
*/

using UnityEngine;
using ANM.TPC.Behaviour;
using ANM.TPC.StateManagers;

namespace ANM.TPC.StateActions
{
    public class MovePlayerCharacter : StateAction
    {
        private static readonly int Sideways = Animator.StringToHash("sideways");
        private static readonly int Forward = Animator.StringToHash("forward");
        private readonly PlayerStateManager _psm;

        public MovePlayerCharacter(PlayerStateManager psm) => _psm = psm;


        public override bool Execute()
        {
            float frontY = 0;
            Vector3 targetVelocity;
            var moveAmount = _psm.moveAmount;

            if (_psm.isLockedOn)
            {
                targetVelocity = _psm.myTransform.forward * (_psm.vertical * _psm.movementSpeed);
                targetVelocity += _psm.myTransform.right * (_psm.horizontal * _psm.movementSpeed);
            }
            else
            {
                targetVelocity = _psm.myTransform.forward * (moveAmount * _psm.movementSpeed);
            }

            Vector3 origin = _psm.myTransform.position + (targetVelocity.normalized * _psm.frontRayOffset);
            origin.y += _psm.frontRayOffset;

            if (Physics.Raycast(origin, -Vector3.up, out var hit, 1f, _psm.ignoreForGroundCheck))
            {
                var y = hit.point.y;
                frontY = y - _psm.myTransform.position.y;
            }

            Vector3 currentVelocity = _psm.myRigidbody.velocity;

            if (_psm.isGrounded)
            {
                if (moveAmount > 0.1f)
                {
                    _psm.myRigidbody.isKinematic = false;
                    _psm.myRigidbody.drag = 0f;
                    if (Mathf.Abs(frontY) > 0.02f)
                        targetVelocity.y = ((frontY > 0) ? frontY + 0.2f : frontY - 0.2f) * _psm.movementSpeed;
                }
                else
                {
                    if (Mathf.Abs(frontY) > 0.02f)
                    {
                        _psm.myRigidbody.isKinematic = true;
                        targetVelocity.y = 0;
                        _psm.myRigidbody.drag = 4;
                    }
                }

                CalculateRotationBasedOnInput();
            }
            else
            {
                _psm.myRigidbody.isKinematic = false;
                _psm.myRigidbody.drag = 0f;
                targetVelocity.y = currentVelocity.y;
            }

            _psm.myRigidbody.velocity = targetVelocity;
            HandleAnimations();
            return false;
        }

        private void CalculateRotationBasedOnInput()
        {
            Vector3 targetDirection;
            float moveOverride = _psm.moveAmount;
            if (_psm.isLockedOn)
            {
                moveOverride = 1f;
                targetDirection = _psm.myTarget.position - _psm.myTransform.position;
            }
            else
            {
                float h = _psm.horizontal;
                float v = _psm.vertical;
                targetDirection = _psm.camera.forward * v;
                targetDirection += _psm.camera.right * h;
            }

            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
                targetDirection = _psm.myTransform.forward;

            var tr = Quaternion.LookRotation(targetDirection);
            var targetRotation = Quaternion.Slerp(_psm.myTransform.rotation, tr,
                _psm.delta * moveOverride * _psm.rotationSpeed);

            _psm.myTransform.rotation = targetRotation;
        }

        private void HandleAnimations()
        {
            if (_psm.isGrounded)
            {
                float forwardAmount = 0;
                if (_psm.isLockedOn)
                {
                    float vInput = Mathf.Abs(_psm.vertical);
                    if (vInput > 0 && vInput <= 0.5f) forwardAmount = 0.5f;
                    else if (vInput > 0.5f) forwardAmount = 1f;
                    if (_psm.vertical < 0) forwardAmount = -forwardAmount;
                    _psm.myAnimator.SetFloat(Forward, forwardAmount, 0.2f, _psm.delta);

                    float sidewaysAmount = 0;
                    float hInput = Mathf.Abs(_psm.horizontal);
                    if (hInput > 0 && hInput <= 0.5f) sidewaysAmount = 0.5f;
                    else if (hInput > 0.5f) sidewaysAmount = 1f;
                    if (_psm.horizontal < 0) sidewaysAmount = -sidewaysAmount;
                    _psm.myAnimator.SetFloat(Sideways, sidewaysAmount, 0.2f, _psm.delta);
                    return;
                }

                float mA = _psm.moveAmount;
                if (mA > 0 && mA <= 0.5f)
                    forwardAmount = 0.5f;
                else if (mA > 0.5f)
                    forwardAmount = 1f;

                _psm.myAnimator.SetFloat(Sideways, 0f, 0.2f, _psm.delta);
                _psm.myAnimator.SetFloat(Forward, forwardAmount, 0.2f, _psm.delta);
            }
            else
            {
                //    TODO : in Air logic
                /*_psm.myAnimator.SetFloat(Forward, 0f, 0.2f, _psm.delta);
                _psm.myAnimator.SetFloat(Sideways, 0f, 0.2f, _psm.delta);*/
            }
        }
    }
}
