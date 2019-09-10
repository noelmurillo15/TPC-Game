using UnityEngine;


namespace SA.Scriptable
{
    [CreateAssetMenu(menuName = "Variables/String")]
    public class StringVariable : ScriptableObject
    {
        public string value;
    }
}