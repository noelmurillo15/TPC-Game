/*
* InputHandler -
* Created by : Allan N. Murillo
* Last Edited : 3/8/2021
*/

using UnityEngine;
using ANM.TPC.Input;
using ANM.TPC.Behaviour;
using ANM.TPC.StateManagers;

namespace ANM.TPC.StateActions
{
    public class InputHandler : StateAction
    {
        [Header("Input Buttons")] 
        public bool a, b, x, y;
        public bool rt, lt, rb, lb;

        private PlayerStateManager _psm;
        private bool _isAttacking;

        public InputHandler(PlayerStateManager psm)
        {
            _psm = psm;
            var controller = Resources.Load<Controller>("PlayerController");
            RegisterInput(controller);
        }


        public override bool Execute()
        {
            var retVal = false;
            _isAttacking = false;
            retVal = HandleAttacking();
            return retVal;
        }

        private bool HandleAttacking()
        {
            if (rb || rt || lb || lt) _isAttacking = true;
            if (y) _isAttacking = false;

            if (_isAttacking)
            {
                //    Find attack animation name based on current items
                //    Play Attack Animation
                _psm.PlayTargetAnimation("Attack 1", true);
                _psm.ChangeState(PlayerStateManager.AttackId);
            }

            return _isAttacking;
        }

        private void RegisterInput(Controller controls)
        {
            if (controls == null)
            {
                Debug.LogWarning("Input Controller not found");
                return;
            }

            controls.OnMovementEvent += moveVector =>
            {
                _psm.vertical = moveVector.y;
                _psm.horizontal = moveVector.x;
                _psm.moveAmount = Mathf.Clamp01(
                    Mathf.Abs(_psm.horizontal) + Mathf.Abs(_psm.vertical));
            };
            controls.OnRotationEvent += rotateVector =>
            {
                _psm.mouseX = rotateVector.x;
                _psm.mouseY = rotateVector.y;
            };

            controls.OnAEvent += isPressed => a = isPressed;
            controls.OnBEvent += isPressed => b = isPressed;
            controls.OnXEvent += isPressed => x = isPressed;
            controls.OnYEvent += isPressed => y = isPressed;

            controls.OnLbEvent += isPressed => lb = isPressed;
            controls.OnRbEvent += isPressed => rb = isPressed;
            controls.OnRtEvent += isPressed => rt = isPressed;
            controls.OnLtEvent += isPressed => lt = isPressed;

            controls.OnLockonToggleEvent += () =>
            {
                _psm.isLockedOn = !_psm.isLockedOn;
                if (!_psm.isLockedOn) _psm.OnClearLookOverride();
                else _psm.OnAssignLookOverride(_psm.myTarget);
            };
        }
    }
}
