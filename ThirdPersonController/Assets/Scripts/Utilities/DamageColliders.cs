/*
 * DamageColliders - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using ANM.Managers;
using UnityEngine;

namespace ANM.Utilities
{
    public class DamageColliders : MonoBehaviour
    {
        public delegate void OnHit(StateManager stateManager);
        public OnHit onHit;


        private void CheckCollisions()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            var stateManager = other.transform.GetComponentInChildren<StateManager>();
            if (stateManager == null) return;
            onHit?.Invoke(stateManager);
        }
    }
}