/*
 * Controller - Holds a reference to ThirdPersonInput, used to setup various inputs from anywhere
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEngine;

namespace ANM.Scriptables
{
    [CreateAssetMenu(menuName = "Single Instances/Controller")]
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
            Debug.Log("Third Person Controller has been fully Enabled!");
        }

        private void OnDisable()
        {
            if (input == null) return;
            input.Disable();
            input = null;
        }
    }
}
