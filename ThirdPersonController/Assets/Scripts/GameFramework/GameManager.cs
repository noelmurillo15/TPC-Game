/*
    *   GameManager - Backbone of the game application
    *   Created by : Allan N. Murillo
 */
using System;
using UnityEngine;
using System.Collections;
using GameFramework.Events;
using GameFramework.External;

//    TODO : DO NOT REFACTOR - doing so will cause an error with containers

namespace GameFramework.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region GameManager Class Members
        //  Singleton Design Pattern
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null) { _instance = GameObject.FindObjectOfType<GameManager>(); }
                return _instance;
            }
        }

        //  Persistant References
        [HideInInspector] public GameSettingsManager gameSettingsManager = null;
        [HideInInspector] public ScreenFader screenFader = null;
        [HideInInspector] public SceneLoader sceneLoader = null;
        [HideInInspector] private SaveSettings gameSettings = null;

        public UI_InputHandler uiInputHandler = null;
        public GameEvent OnApplicationQuitEvent = null;

        //  FPS tracker
        private float deltaTime = 0.0f;

        //  Flags
        private bool displayFPS = false;
        private bool isGameOver = false;
        private bool isInventoryActive = false;
        private bool isMenuActive = false;
        private bool isPauseActive = false;

        public bool IsInventoryUIActive { get { return isInventoryActive; } set { isInventoryActive = value; } }
        public bool IsPauseUIActive { get { return isPauseActive; } set { isPauseActive = value; } }
        public bool IsMenuUIActive { get { return isMenuActive; } set { isMenuActive = value; } }
        public bool IsGameOver { get { return isGameOver; } set { isGameOver = value; } }
        #endregion


        //  TODO : Singletons are great for Audio Manager, Input Manager, SceneLoader
        protected virtual void Awake()
        {
            if (_instance != null && _instance != this) { Destroy(this.gameObject); return; }
            string startSceneName = string.Format("__________ Start Screen : {0} __________", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            Debug.Log(startSceneName);
            Debug.Log("GM::Awake()");

            //  Persist throughout scenes
            DontDestroyOnLoad(this.gameObject);
            _instance = this;

            // Game Settings
            GameSettingsManager.SettingsLoadedIni = false;
            gameSettings = new SaveSettings();
            gameSettings.Initialize();

            //  Get Components
            sceneLoader = gameObject.GetComponent<SceneLoader>();
            screenFader = gameObject.GetComponentInChildren<ScreenFader>();

            //  Toggle Flags
            displayFPS = true;
            isInventoryActive = false;
            isPauseActive = false;
            isMenuActive = false;
            isGameOver = false;

            //  Scene Loader
            if (sceneLoader != null) { sceneLoader.Initialize(); }
        }

        private void Update()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        private void OnGUI()
        {
            if (displayFPS)
            {
                int w = Screen.width, h = Screen.height;

                GUIStyle style = new GUIStyle();

                Rect rect = new Rect(w - 180, 0, w, h * 2 / 100);
                style.alignment = TextAnchor.UpperLeft;
                style.fontSize = h * 2 / 100;
                style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                float msec = deltaTime * 1000.0f;
                float fps = 1.0f / deltaTime;
                string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
                GUI.Label(rect, text, style);
            }
        }

        #region Game Settings
        public void LoadSettingsFromIndexedDB()
        {
            gameSettings.LoadGameSettings();
            if (sceneLoader != null) { sceneLoader.Initialize(); }
        }

        public void InitializeManager(GameSettingsManager gsm)
        {
            gameSettingsManager = gsm;
            uiInputHandler.AssignGameSettings(gsm);
        }
        #endregion

        #region Game Events
        public void StartGameEvent()
        {
            isGameOver = false;
            isMenuActive = false;
            sceneLoader.LoadLevel("Level 1");
        }

        public void LoadMainMenuEvent()
        {
            isGameOver = true;
            isMenuActive = true;
            sceneLoader.LoadMainMenu();
        }

        public void QuitApplicationEvent()
        {
            isGameOver = true;
            StartCoroutine(QuitSequence());
        }

        private IEnumerator QuitSequence()
        {
            gameSettings.SaveGameSettings();
            yield return screenFader.FadeOut(3f);
            if (OnApplicationQuitEvent != null) { OnApplicationQuitEvent.Raise(); }
            sceneLoader.LoadCreditsScene();
        }

        private void OnDestroy()
        {
            if (GameManager.Instance == this)
            {
                if (OnApplicationQuitEvent != null) { OnApplicationQuitEvent.UnregisterAllListeners(); }
                Resources.UnloadUnusedAssets();
                GC.Collect();
            }
        }
        #endregion                
    }
}