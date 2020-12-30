/*
* InputHandler -
* Created by : Allan N. Murillo
* Last Edited : 8/18/2020
*/

using UnityEngine;
using ANM.TPC.Behaviour;
using ANM.Scriptables.Managers;
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

            controls.input.CharacterInput.Movement.performed += context =>
            {
                var inp = context.ReadValue<Vector2>();
                _psm.vertical = inp.y;
                _psm.horizontal = inp.x;
                _psm.moveAmount = Mathf.Clamp01(
                    Mathf.Abs(_psm.horizontal) + Mathf.Abs(_psm.vertical));
            };
            controls.input.CharacterInput.CameraRotation.performed += context =>
            {
                var inp = context.ReadValue<Vector2>();
                _psm.mouseX = inp.x;
                _psm.mouseY = inp.y;
            };

            controls.input.CharacterInput.A.performed += context => a = true;
            controls.input.CharacterInput.A.canceled += context => a = false;
            controls.input.CharacterInput.X.performed += context => x = true;
            controls.input.CharacterInput.X.canceled += context => x = false;
            controls.input.CharacterInput.Y.performed += context => y = true;
            controls.input.CharacterInput.Y.canceled += context => y = false;
            controls.input.CharacterInput.Roll.performed += context => b = true;
            controls.input.CharacterInput.Roll.canceled += context => b = false;

            controls.input.CharacterInput.LB.performed += context => lb = true;
            controls.input.CharacterInput.LB.canceled += context => lb = false;
            controls.input.CharacterInput.LT.performed += context => lt = true;
            controls.input.CharacterInput.LT.canceled += context => lt = false;
            controls.input.CharacterInput.RT.performed += context => rt = true;
            controls.input.CharacterInput.RT.canceled += context => rt = false;
            controls.input.CharacterInput.RB.performed += context => rb = true;
            controls.input.CharacterInput.RB.canceled += context => rb = false;

            controls.input.CharacterInput.LockOnToggle.performed += context =>
            {
                _psm.isLockedOn = !_psm.isLockedOn;
                if(!_psm.isLockedOn) _psm.OnClearLookOverride();
                else _psm.OnAssignLookOverride(_psm.myTarget);
            };
        }
    }
}
