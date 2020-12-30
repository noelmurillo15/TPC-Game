/*
* AbstractEquippableItem - Contains data necessary to create a runtime instance of a Equip Item
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Scriptables.Inventory
{
    [CreateAssetMenu(menuName = "Scriptables/Inventory/AbstractEquippableItem")]
    public class AbstractEquippableItem : AbstractItem
    {
        public GameObject modelPrefab;
        public StringVariable throwAnim;
        public StringVariable useAnim;


        public void Init()
        {

        }
    }
}
