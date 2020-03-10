/*
 * Transition -
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

namespace ANM.Behaviour
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
