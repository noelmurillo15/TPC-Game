/*
 * AbstractInventory -
 * Created by : Allan N. Murillo
 * Last Edited : 3/13/2020
 */

using ANM.Scriptables.Inventory;

namespace ANM.Inventory
{
    [System.Serializable]
    public class AbstractInventory
    {
        public AbstractItem rightHandItem;
        public AbstractItem leftHandItem;
    }
}
