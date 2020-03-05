/*
 * UniqueId -
 * Created by : Allan N. Murillo
 * Last Edited : 3/4/2020
 */

using UnityEngine;

namespace ANM
{
    public class UniqueId : MonoBehaviour
    {
        public string Id { get; private set; }


        private void Awake()
        {
            Id = transform.position.sqrMagnitude + "-" + name + "-" + transform.GetSiblingIndex();
            Debug.Log("ID for " + name + " is : " + Id);
        }
    }
}