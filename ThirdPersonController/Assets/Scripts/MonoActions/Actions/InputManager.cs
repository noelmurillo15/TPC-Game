using System;
using UnityEngine;


namespace SA.MonoActions
{
    [CreateAssetMenu]
    public class InputManager : Action
    {
        public InputAxis horizontal;
        public InputAxis vertical;

        public float moveAmount;

        public override void Execute()
        {
            horizontal.Execute();
            vertical.Execute();

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal.value) + Math.Abs(vertical.value));
        }
    }
}