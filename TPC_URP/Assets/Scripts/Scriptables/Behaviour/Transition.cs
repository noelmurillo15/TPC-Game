/*
 * Transition - Used by Behaviour Node Editor to change to a different state when a condition is met
 * Created by : Allan N. Murillo
 * Last Edited : 3/12/2020
 */

namespace ANM.Scriptables.Behaviour
{
    [System.Serializable]
    public class Transition
    {
        public int id;
        public Condition condition;
        public State targetState;
        public bool disable;
    }
}
