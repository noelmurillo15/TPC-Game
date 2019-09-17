#if UNITY_EDITOR
using UnityEditor;
using System.Collections.Generic;


namespace SA.Utilities.Editor
{
    public static class EditorUtilities
    {
        public static List<T> FindAssetsByType<T>() where T : UnityEngine.Object
        {
            List<T> assets = new List<T>();

            string[] guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T)));

            for (int i = 0; i < guids.Length; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
                T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                if (asset != null) assets.Add(asset);
            }

            return assets;
        }
    }
}
#endif