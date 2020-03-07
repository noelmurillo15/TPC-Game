/*
 * BehaviourStateManager - StateManager Being used for testing the Behaviour Node Editor
 * Created by : Allan N. Murillo
 * Last Edited : 3/7/2020
 */

using UnityEngine;

namespace ANM.Behaviour
{
    public class BehaviourStateManager : MonoBehaviour
    {
        public float health;
        public State currentState;
        [HideInInspector] public float delta;
        [HideInInspector] public Transform myTransform;


        private void Start()
        {
            myTransform = transform;
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Tick(this);
            }
        }
    }
}
