/*
 * Transition -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

namespace ANM.Editor
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
