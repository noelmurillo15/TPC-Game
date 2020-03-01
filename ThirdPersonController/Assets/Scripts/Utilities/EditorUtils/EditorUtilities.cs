/*
 * EditorUtilities -
 * FindAssetsByType is used by the inventory to get all items in the game, in a List
 * Created by : Allan N. Murillo
 * Last Edited : 2/28/2020
 */

#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;
using System.Linq;


namespace SA.Utilities.Editor
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