/*
 * Controller - Holds a reference to ThirdPersonInput, used to setup various inputs from anywhere
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEngine;
using UnityEngine.InputSystem;

namespace ANM.Scriptables.Managers
{
    [CreateAssetMenu(menuName = "Scriptables/Single Instance/Controller")]
    public class Controller : ScriptableObject
    {
        public ThirdPersonInput input;


        private void OnEnable()
        {
            if (input == null)
            {
                input = new ThirdPersonInput();
            }
            input.Enable();
            input.CharacterInput.Enable();
            input.UI.Cancel.performed += EndGameInputHandler;
        }

        private static void EndGameInputHandler(InputAction.CallbackContext callbackContext){
            Application.Quit();
        }

        private void OnDisable()
        {
            if (input == null) return;
            input.UI.Cancel.performed -= EndGameInputHandler;
            input.Disable();
            input = null;
        }
    }
}
