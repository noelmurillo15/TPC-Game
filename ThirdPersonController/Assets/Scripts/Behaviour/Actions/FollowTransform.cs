﻿/*
 * FollowTransform -
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Behaviour.Actions
{
    [CreateAssetMenu(menuName = "MonoActions/Follow Transform")]
    public class FollowTransform : Action
    {
        public TransformVariable targetTransform;
        public TransformVariable currentTransform;
        public float speed = 9;


        public override void Execute()
        {
            if (targetTransform.value == null) return;
            if (currentTransform.value == null) return;

            var targetPosition = Vector3.Lerp(currentTransform.value.position,
                targetTransform.value.position, Time.deltaTime * speed);

            currentTransform.value.position = targetPosition;
        }
    }
}
