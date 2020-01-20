/// <summary>
/// 9/17/19
/// Allan Murillo
/// FramesPerSecond Display
/// </summary>
using UnityEngine;


namespace ANM.System
{
    public class FpsDisplay : MonoBehaviour
    {
        private float _deltaTime = 0.0f;

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        }

        private void OnGUI()
        {
            int w = Screen.width, h = Screen.height;
            GUIStyle style = new GUIStyle();
            Rect rect = new Rect(w - 150, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = Color.white;
            float msec = _deltaTime * 1000.0f;
            float fps = 1.0f / _deltaTime;
            string text = $"{msec:0.0} ms ({fps:0.} fps)";
            GUI.Label(rect, text, style);
        }
    }
}