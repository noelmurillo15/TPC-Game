using UnityEngine;
using ANM.Utilities;
using UnityEngine.InputSystem;

namespace ANM.Input
{
    [CreateAssetMenu]
    public class MouseInputAxis : StrategyPatternAction
    {
        public Vector2 value;

        public override void Execute() => value = Mouse.current.delta.ReadValue();
    }
}