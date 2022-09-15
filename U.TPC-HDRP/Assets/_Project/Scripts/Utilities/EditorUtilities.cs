#if UNITY_EDITOR

/*
 * EditorUtilities -
 * FindAssetsByType is used by the inventory to get all items in the game, in a List
 * Created by : Allan N. Murillo
 * Last Edited : 2/28/2020
 */

using System.Linq;
using UnityEditor;
using System.Collections.Generic;

namespace ANM.Editor
{
    public static class EditorUtilities
    {
        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            return guids.Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .Where(asset => asset != null)
                .ToList();
        }
    }
}
#endif
