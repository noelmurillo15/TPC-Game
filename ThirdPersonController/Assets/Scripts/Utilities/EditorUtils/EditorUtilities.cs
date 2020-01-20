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
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            return guids.Select(t => AssetDatabase.GUIDToAssetPath(t)).Select(assetPath => AssetDatabase.LoadAssetAtPath<T>(assetPath)).Where(asset => asset != null).ToList();
        }
    }
}
#endif