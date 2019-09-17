using UnityEngine;
using SA.Managers;


namespace SA
{
    public abstract class StateActions : ScriptableObject
    {
        public abstract void Execute(StateManager states);
    }
}