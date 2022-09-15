/*
* CameraInputAxis ( OLD ) - Reads CameraRotation value from ThirdPersonInput ActionMap
* Created by : Allan N. Murillo
* Last Edited : 3/8/2021
*/

using UnityEngine;
using ANM.TPC.Input;

namespace ANM.Scriptables.Actions
{
    [CreateAssetMenu(menuName = "Scriptables/MonoActions/Inputs/Camera Axis")]
    public class CameraInputAxis : Action
    {
        public Vector2 value;
        public Controller controls;

        private void OnEnable()
        {
            Debug.Log("[CamInputAxis]: OnEnable");
#if !UNITY_EDITOR
            Debug.Log("Enabling Camera Input Axis");
            if (controls == null) return;
            controls.GetInput().CharacterInput.CameraRotation.Enable();
#endif
        }

        private void OnDisable()
        {
            Debug.Log("[CamInputAxis]: OnDisable");
#if !UNITY_EDITOR
            Debug.Log("Disabling Camera Input Axis");
            if (controls == null) return;
            controls.GetInput().CharacterInput.CameraRotation.Disable();
#endif
        }

        public override void Execute()
        {
            Debug.Log("[CamInputAxis]: Execute is Empty");
            //value = controls.GetInput().CharacterInput.CameraRotation.ReadValue<Vector2>();
        }
    }
}
