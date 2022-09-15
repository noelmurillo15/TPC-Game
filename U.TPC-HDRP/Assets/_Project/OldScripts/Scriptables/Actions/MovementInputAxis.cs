/*
* MovementInputAxis ( OLD ) - Reads Movement value from ThirdPersonInput ActionMap
* Created by : Allan N. Murillo
* Last Edited : 3/8/2021
*/

using UnityEngine;
using ANM.TPC.Input;

namespace ANM.Scriptables.Actions
{
    [CreateAssetMenu(menuName = "Scriptables/MonoActions/Inputs/Movement Axis")]
    public class MovementInputAxis : Action
    {
        public Vector2 value;
        public Controller controls;

        private void OnEnable()
        {
            Debug.Log("[MoveInputAxis]: OnEnable");
#if !UNITY_EDITOR
            Debug.Log("Enabling Movement Input Axis");
            if (controls == null) return;
            controls.GetInput().CharacterInput.Movement.Enable();
#endif
        }

        private void OnDisable()
        {
            Debug.Log("[MoveInputAxis]: OnDisable");
#if !UNITY_EDITOR
            Debug.Log("Disabling Movement Input Axis");
            if (controls == null) return;
            controls.GetInput().CharacterInput.Movement.Disable();
#endif
        }

        public override void Execute()
        {
            Debug.Log("[MoveInputAxis]: Execute is Empty");
            //value = controls.GetInput().CharacterInput.Movement.ReadValue<Vector2>();
        }
    }
}
