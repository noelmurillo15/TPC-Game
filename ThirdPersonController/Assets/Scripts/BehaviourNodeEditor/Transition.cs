/*
 * Transition -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

namespace ANM.BehaviourNodeEditor
{
    [System.Serializable]
    public class Transition
    {
        public Condition condition;
        public State targetState;
        public bool disable;
    }
}
