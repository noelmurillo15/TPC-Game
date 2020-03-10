using UnityEngine;
using ANM.Managers;

//    TODO : NOT NEEDED ANYMORE

namespace ANM.Behaviour.Conditions
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Conditions/IsDead")]
    public class IsDead : Condition
    {
        public override bool CheckCondition(StateManager stateManager)
        {
            return false;
        }
    }
}
