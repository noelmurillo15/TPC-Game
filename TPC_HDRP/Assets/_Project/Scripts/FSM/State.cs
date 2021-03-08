/*
* State -
* Created by : Allan N. Murillo
* Last Edited : 8/18/2020
*/

using System.Collections.Generic;

namespace ANM.TPC.Behaviour
{
    public class State
    {
        public delegate void OnEnter();
        public OnEnter onEnter;

        private bool _forceExit;
        private readonly List<StateAction> _updateActions;
        private readonly List<StateAction> _fixedUpdateActions;

        public State(List<StateAction> fixedActions, List<StateAction> updateActions)
        {
            _fixedUpdateActions = fixedActions;
            _updateActions = updateActions;
        }


        public void FixedTick()
        {
            ExecuteStateActions(_fixedUpdateActions);
        }

        public void Tick()
        {
            ExecuteStateActions(_updateActions);
        }

        public void LateTick()
        {
            _forceExit = false;
        }

        private void ExecuteStateActions(IReadOnlyList<StateAction> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (_forceExit)
                {
                    return;
                }
                _forceExit = list[i].Execute();
            }
        }
    }
}
