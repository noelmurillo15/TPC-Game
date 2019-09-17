using UnityEngine;


namespace SA.MonoActions
{
    public class ActionHook : MonoBehaviour
    {
        public Action[] updateActions;
        
        void Update()
        {
            for (int i = 0; i < updateActions.Length; i++)
            {
                updateActions[i].Execute();
            }
        }
    }
}