/*
* CameraInputAxis - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;

namespace ANM.Behaviour.Actions
{
    [CreateAssetMenu(menuName = "Behaviours/MonoActions/Camera Axis")]
    public class CameraInputAxis : Action
    {
        public Vector2 value;
        public Scriptables.Controller controls;

        private void OnEnable()
        {
#if !UNITY_EDITOR
            Debug.Log("Enabling Camera Input Axis");
            if (controls == null) return;
            controls.input.CharacterInput.CameraRotation.Enable();
#endif
        }
        
        private void OnDisable()
        {
#if !UNITY_EDITOR
            Debug.Log("Disabling Camera Input Axis");
            if (controls == null) return;
            controls.input.CharacterInput.CameraRotation.Disable();
#endif
        }
        
        public override void Execute()
        {
            value = controls.input.CharacterInput.CameraRotation.ReadValue<Vector2>();
        }
    }
}