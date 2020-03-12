/*
* MovementInputAxis - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;

namespace ANM.Behaviour.Actions
{
    [CreateAssetMenu(menuName = "Behaviours/MonoActions/Movement Axis")]
    public class MovementInputAxis : Action
    {
        public Vector2 value;
        public Scriptables.Controller controls;

        private void OnEnable()
        {
#if !UNITY_EDITOR
            Debug.Log("Enabling Movement Input Axis");
            if (controls == null) return;
            controls.input.CharacterInput.Movement.Enable();
#endif
        }

        private void OnDisable()
        {
#if !UNITY_EDITOR
            Debug.Log("Disabling Movement Input Axis");
            if (controls == null) return;
            controls.input.CharacterInput.Movement.Disable();
#endif
        }

        public override void Execute()
        {
            value = controls.input.CharacterInput.Movement.ReadValue<Vector2>();
        }
    }
}
