/*
 * UniqueId -
 * Created by : Allan N. Murillo
 * Last Edited : 3/4/2020
 */

using UnityEngine;
using System.Collections.Generic;

namespace ANM
{
    public class CollectibleItemSet : MonoBehaviour
    {
        public HashSet<string> CollectedItems { get; } = new HashSet<string>();
    }
}