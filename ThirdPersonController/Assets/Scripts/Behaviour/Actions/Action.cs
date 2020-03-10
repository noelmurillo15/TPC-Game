/*
* Action - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;

namespace ANM.Behaviour.Actions
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute();
    }
}