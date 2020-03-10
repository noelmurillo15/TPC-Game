/*
* MouseInputAxis - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;

namespace ANM.Behaviour.Actions
{
    [CreateAssetMenu(menuName = "MonoActions/MouseInput")]
    public class MouseInputAxis : Action
    {
        private ThirdPersonInput _controls;
        public Vector2 value;

        private void OnEnable()
        {
            if (_controls != null) return;
            _controls = new ThirdPersonInput();
            _controls.CharacterInput.Movement.Enable();
        }

        public override void Execute()
        {
            value = _controls.CharacterInput.Movement.ReadValue<Vector2>();
        }
    }
}
