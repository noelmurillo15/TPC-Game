/*
* DamageCollider - Used on weapon instances to apply damage via an OnHit Event to anything this collides into
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Inventory
{
    [RequireComponent(typeof(Collider))]
    public class DamageCollider : MonoBehaviour
    {
        public delegate void OnHit(StateManager stateManager);

        public new Collider collider;
        public OnHit onHit;


        public void Start()
        {
            gameObject.layer = 10;
            collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var stateManager = other.transform.GetComponentInChildren<StateManager>();
            if (stateManager == null) return;
            onHit?.Invoke(stateManager);
        }
    }
}