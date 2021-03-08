/*
* LockOnLogic -
* Created by : Allan N. Murillo
* Last Edited : 8/12/2020
*/

using System;
using UnityEngine;
using ANM.Managers;
using ANM.Scriptables.Variables;
using System.Collections.Generic;

namespace ANM.Scriptables.Actions
{
    [CreateAssetMenu(menuName = "Scriptables/MonoActions/Lock On Logic")]
    public class LockOnLogic : Action
    {
        [NonSerialized] public readonly List<Transform> lockOnTargets = new List<Transform>();
        public Transform currentLockOnTarget;
        public StatesManagerVariable state;
        public InputButton lockOnButton;

        [NonSerialized] public float timer;
        public FloatVariable delta;

        public override void Execute()
        {
            if (lockOnButton.isPressed)
            {
                if (state.value.isLockedOn)
                {
                    state.value.isLockedOn = false;
                }
                else
                {
                    state.value.isLockedOn = true;
                    FindLockableTargets();
                }
            }

            if (currentLockOnTarget != null)
            {
                timer += delta.value;
                if (timer > 2)
                {
                    ValidateTargets();
                    timer = 0;
                }
            }
        }

        private void FindLockableTargets()
        {
            var colliders = Physics.OverlapSphere(state.value.myTransform.position, 10f);
            if (colliders.Length <= 0) return;

            foreach (var collider in colliders)
            {
                var lockable = collider.transform.root.GetComponent<ILockable>();
                if (lockable == null) continue;

                var dotProduct = Vector3.Dot(state.value.myTransform.forward, collider.transform.position);

                if (!(dotProduct > 0)) continue;
                var targetToAdd = lockable.LockOn();
                if (targetToAdd == null) continue;
                if (!lockOnTargets.Contains(targetToAdd))
                    lockOnTargets.Add(targetToAdd);
            }

            var minDistance = 100f;
            Transform closest = null;
            for (var i = 0; i < lockOnTargets.Count; i++)
            {
                var tempDistance = Vector3.Distance(state.value.myTransform.position,
                    lockOnTargets[i].position);

                if (tempDistance < minDistance && lockOnTargets[i] != closest)
                {
                    minDistance = tempDistance;
                    closest = lockOnTargets[i];
                }
            }

            currentLockOnTarget = closest;
            Debug.Log("LockOn Targets in range : " + lockOnTargets.Count);
        }

        private void ValidateTargets()
        {
            var dist = Vector3.Distance(currentLockOnTarget.position, state.value.myTransform.position);
            if (dist > 10)
            {
                state.value.isLockedOn = false;
            }
        }
    }
}
