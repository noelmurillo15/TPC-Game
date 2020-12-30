/*
* MonitorInteractingAnimation -
* Created by : Allan N. Murillo
* Last Edited : 8/18/2020
*/

using ANM.TPC.Behaviour;
using ANM.TPC.StateManagers;

namespace ANM.TPC.StateActions
{
    public class MonitorInteractingAnimation : StateAction
    {
        private readonly CharacterStateManager _csm;
        private string _targetStateId;
        private string _targetBool;

        public MonitorInteractingAnimation(CharacterStateManager stateManager, string targetBool, string targetStateId)
        {
            _csm = stateManager;
            _targetBool = targetBool;
            _targetStateId = targetStateId;
        }


        public override bool Execute()
        {
            var isInteracting = _csm.myAnimator.GetBool(_targetBool);
            if (isInteracting) return false;

            _csm.ChangeState(_targetStateId);
            return true;
        }
    }
}
