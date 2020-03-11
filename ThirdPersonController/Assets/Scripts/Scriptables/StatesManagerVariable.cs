using ANM.Managers;
using UnityEngine;

namespace ANM.Scriptables
{
    [CreateAssetMenu(menuName = "Variables/States Manager")]
    public class StatesManagerVariable : ScriptableObject
    {
        public StateManager value;
    }
}