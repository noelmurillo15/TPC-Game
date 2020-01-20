using UnityEngine;


namespace GameFramework.Managers
{
    public class GM_Cleanup : MonoBehaviour
    {
        private void Start()
        {
            if (GameManager.Instance != null)
            {
                Destroy(GameManager.Instance.gameObject);
            }
            Invoke(nameof(End), 3f);
        }

        private void End()
        {    //    Does not work if made static
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
