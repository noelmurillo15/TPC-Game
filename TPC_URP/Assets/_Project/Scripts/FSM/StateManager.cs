/*
* StateManager -
* Created by : Allan N. Murillo
* Last Edited : 8/18/2020
*/

using UnityEngine;
using ANM.TPC.Behaviour;
using System.Collections.Generic;

namespace ANM.TPC.StateManagers
{
    public abstract class StateManager : MonoBehaviour
    {
        [HideInInspector] public Transform myTransform;
        private readonly Dictionary<string, State> _allStates = new Dictionary<string, State>();
        private State _currentState;


        private void Start()
        {
            Initialize();
        }

        public abstract void Initialize();


        public void Tick()
        {
            _currentState?.Tick();
        }

        public void FixedTick()
        {
            _currentState?.FixedTick();
        }

        public void LateTick()
        {
            _currentState?.LateTick();
        }

        public void ChangeState(string targetID)
        {
            if (_currentState != null)
            {
                //    Run onExit Actions
            }

            var targetState = GetState(targetID);
            _currentState = targetState;
            _currentState.onEnter?.Invoke();
        }

        private State GetState(string targetID)
        {
            _allStates.TryGetValue(targetID, out var retVal);
            return retVal;
        }

        protected void RegisterState(string stateID, State state)
        {
            if (!_allStates.ContainsKey(stateID))
                _allStates.Add(stateID, state);
        }
    }
}
