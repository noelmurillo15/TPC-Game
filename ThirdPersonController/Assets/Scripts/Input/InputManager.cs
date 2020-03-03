using UnityEngine;
using ANM.Utilities;
using ANM.Framework.Variables;

namespace ANM.Input
{
    [CreateAssetMenu]
    public class InputManager : StrategyPatternAction
    {
        public MouseInputAxis cameraViewInput;
        
        public float moveAmount;
        public Vector3 moveDirection;
        public TransformVariable cameraTransform;
        
        
        public override void Execute()
        {
            cameraViewInput.Execute();

            moveAmount = Mathf.Clamp01(
                Mathf.Abs(cameraViewInput.value.x) + Mathf.Abs(cameraViewInput.value.y));

            if (cameraTransform.value == null) return;
            moveDirection = cameraTransform.value.forward * cameraViewInput.value.y;
            moveDirection += cameraTransform.value.right * cameraViewInput.value.x;
        }
    }
}