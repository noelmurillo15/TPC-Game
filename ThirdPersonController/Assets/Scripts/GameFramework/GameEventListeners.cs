﻿using UnityEngine;
using UnityEngine.Events;

namespace GameFramework.Events
{
    public class GameEventListeners : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent Response;


        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}
