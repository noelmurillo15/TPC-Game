using UnityEngine;

namespace ANM.Utilities
{
    public class ActionHook : MonoBehaviour
    {
        public StrategyPatternAction[] updateActions;

        private void Update()
        {
            foreach (var action in updateActions)
            {
                action.Execute();
            }
        }
    }
}
