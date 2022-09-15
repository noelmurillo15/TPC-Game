/*
* Action - Used by the ActionHook to add logic (action) to a MonoBehaviour
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;

namespace ANM.Scriptables.Actions
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute();
    }
}