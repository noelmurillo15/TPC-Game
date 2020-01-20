/*
    *   GameSettingsManager - Manages GameSettings UI & Functionality
    *   Created by : Allan N. Murillo
 */
using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Serialization;


namespace GameFramework.Managers
{
    public class GameSettingsManager : MonoBehaviour
    {
        #region Class Members
        //  Panels
        [SerializeField] private GameObject mask = null;
        [SerializeField] private GameObject mainPanel = null;
        [SerializeField] private GameObject vidPanel = null;
        [SerializeField] private GameObject audioPanel = null;
        [SerializeField] private GameObject quitPanel = null;
        [FormerlySerializedAs("TitleTexts")] [SerializeField] private GameObject titleTexts = null;

        //  Animators
        [SerializeField] private Animator audioPanelAnimator = null;
        [SerializeField] private Animator quitPanelAnimator = null;
        [SerializeField] private Animator vidPanelAnimator = null;

        //  Misc
        [SerializeField] private Camera mainCam = null;
        [SerializeField] private String mainMenuSceneName = string.Empty;
        [SerializeField] private float timeScale = 1f;

        //  Settings UI
        [SerializeField] private TMP_Text pauseMenuTitleText = null;

        [SerializeField] private Dropdown aaCombo = null;
        [SerializeField] private Dropdown afCombo = null;

        [SerializeField] private Slider renderDistSlider = null;
        [SerializeField] private Slider shadowDistSlider = null;
        [SerializeField] private Slider audioMasterVolumeSlider = null;
        [SerializeField] private Slider masterTexSlider = null;
        [SerializeField] private Slider shadowCascadesSlider = null;
        [SerializeField] private Toggle vSyncToggle = null;

        [SerializeField] private Text presetLabel = null;
        private String[] _presets;

        //  Hardcoded Shadow Distance
        [SerializeField] private float[] shadowDist = null;

        //  Audio Sources
        [SerializeField] private AudioSource bgMusic = null;
        [SerializeField] private AudioSource[] sfx = null;

        public bool paused = false;
        private GameManager _gameManager = null;

        //  INI Settings
        internal static float MasterVolumeIni;
        internal static int CurrentQualityLevelIni;
        internal static bool VsyncIni;
        internal static int MsaaIni;
        internal static float RenderDistIni;
        internal static float ShadowDistIni;
        internal static int TextureLimitIni;
        internal static int AnisoFilterLevelIni;
        internal static int ShadowCascadeIni;
        internal static bool SettingsLoadedIni;
        #endregion


        //  TODO : updated the graphics preset changes other graphics settings but their UI is not updated =(
        public void Awake()
        {   //  Default Values
            if (titleTexts != null)
            {
                titleTexts.transform.GetChild(0).GetComponent<TMP_Text>().text = Application.productName;
            }

            paused = false;
            Time.timeScale = timeScale;
            _presets = QualitySettings.names;
            pauseMenuTitleText.text = "Main Menu";

            _gameManager = GameManager.Instance;
            _gameManager.InitializeManager(this);

            //  TODO : Create an audio setting for BG music
            if (bgMusic != null) { bgMusic.volume = 0.8f; }

            if (SceneLoader.GetCurrentSceneName() == mainMenuSceneName)
            {   //  Loaded Main Menu Scene                
                _gameManager.IsMenuUIActive = true;
                _gameManager.IsGameOver = true;
                mask.SetActive(true);
                mainPanel.SetActive(true);
                titleTexts.SetActive(true);
                audioPanel.SetActive(false);
                vidPanel.SetActive(false);
            }
            else
            {   //  Loaded a Level Scene
                _gameManager.IsMenuUIActive = false;
                _gameManager.IsGameOver = false;
                mask.SetActive(false);
                mainPanel.SetActive(false);
                titleTexts.SetActive(false);
                audioPanel.SetActive(false);
                vidPanel.SetActive(false);
            }

            if (!SettingsLoadedIni) { DefaultSettings(); }
            ApplyIniSettings();
        }

        //  TODO : use new input system
        public void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape) || _gameManager.IsMenuUIActive) return;
            if (mainPanel.activeSelf == false) { Pause(); }
            else { Resume(); }
        }

        #region Game Settings Panel Buttons
        public void StartGame()
        {
            _gameManager.StartGameEvent();
        }

        public void Pause()
        {
            paused = true;
            Time.timeScale = 0f;
            _gameManager.IsPauseUIActive = true;
            mainPanel.SetActive(true);
            vidPanel.SetActive(false);
            audioPanel.SetActive(false);
            titleTexts.SetActive(true);
            mask.SetActive(true);
        }

        public void Resume()
        {
            paused = false;
            Time.timeScale = timeScale;
            _gameManager.IsPauseUIActive = false;
            mainPanel.SetActive(false);
            vidPanel.SetActive(false);
            audioPanel.SetActive(false);
            titleTexts.SetActive(false);
            mask.SetActive(false);
        }

        public void QuitOptions()
        {
            vidPanel.SetActive(false);
            audioPanel.SetActive(false);
            if (quitPanelAnimator != null)
            {
                quitPanelAnimator.enabled = true;
                quitPanelAnimator.Play("QuitPanelIn");
            }
            else
            {
                quitPanel.SetActive(true);
            }
        }

        public void QuitCancel()
        {
            if (quitPanelAnimator != null)
            {
                quitPanelAnimator.Play("QuitPanelOut");
            }
            else
            {
                quitPanel.SetActive(false);
            }
        }

        public void ReturnToMenu()
        {
            paused = false;
            Time.timeScale = timeScale;
            _gameManager.LoadMainMenuEvent();
        }

        public void QuitGame()
        {
            paused = false;
            Time.timeScale = timeScale;
            _gameManager.QuitApplicationEvent();
        }
        #endregion

        #region Audio Panel
        public void Audio()
        {
            vidPanel.SetActive(false);
            audioPanel.SetActive(true);
            if (!_gameManager.IsMenuUIActive) { mainPanel.SetActive(false); }
            AudioPanelIn();
        }

        public void AudioPanelIn()
        {
            pauseMenuTitleText.text = "Audio Menu";
            if (audioPanelAnimator == null) return;
            audioPanelAnimator.enabled = true;
            audioPanelAnimator.Play("Audio Panel In");
        }

        public void ApplyAudio()
        {
            StartCoroutine(ApplyAudioSettings());
        }

        public void CancelAudio()
        {
            StartCoroutine(RevertAudioSettings());
        }

        private IEnumerator ApplyAudioSettings()
        {
            if (audioPanelAnimator != null) { audioPanelAnimator.Play("Audio Panel Out"); }

            MasterVolumeIni = audioMasterVolumeSlider.value;

            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
            pauseMenuTitleText.text = "Main Menu";
            vidPanel.SetActive(false);
            audioPanel.SetActive(false);
            mainPanel.SetActive(true);
        }

        private IEnumerator RevertAudioSettings()
        {
            if (audioPanelAnimator != null) { audioPanelAnimator.Play("Audio Panel Out"); }

            audioMasterVolumeSlider.value = MasterVolumeIni;

            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
            pauseMenuTitleText.text = "Main Menu";
            vidPanel.SetActive(false);
            audioPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        #endregion

        #region Video Panel
        public void Video()
        {   //  Turn On/Off Panels
            vidPanel.SetActive(true);
            audioPanel.SetActive(false);
            if (!_gameManager.IsMenuUIActive) { mainPanel.SetActive(false); }
            VideoPanelIn();
        }

        public void VideoPanelIn()
        {
            pauseMenuTitleText.text = "Video Menu";
            if (vidPanelAnimator == null) return;
            vidPanelAnimator.enabled = true;
            vidPanelAnimator.Play("Video Panel In");
        }

        public void Apply()
        {
            StartCoroutine(ApplyVideoSettings());
        }

        public void CancelVideo()
        {
            StartCoroutine(RevertVideoSettings());
        }

        private IEnumerator ApplyVideoSettings()
        {
            if (vidPanelAnimator != null) { vidPanelAnimator.Play("Video Panel Out"); }

            CurrentQualityLevelIni = QualitySettings.GetQualityLevel();
            VsyncIni = vSyncToggle.isOn;
            MsaaIni = aaCombo.value;
            AnisoFilterLevelIni = afCombo.value;
            RenderDistIni = renderDistSlider.value;
            TextureLimitIni = (int)masterTexSlider.value;
            ShadowDistIni = shadowDistSlider.value;
            ShadowCascadeIni = (int)shadowCascadesSlider.value;

            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
            pauseMenuTitleText.text = "Main Menu";
            vidPanel.SetActive(false);
            audioPanel.SetActive(false);
            mainPanel.SetActive(true);
        }

        private IEnumerator RevertVideoSettings()
        {
            if (vidPanelAnimator != null) { vidPanelAnimator.Play("Video Panel Out"); }

            QualitySettings.SetQualityLevel(CurrentQualityLevelIni);
            vSyncToggle.isOn = VsyncIni;
            aaCombo.value = MsaaIni;
            afCombo.value = AnisoFilterLevelIni;
            renderDistSlider.value = RenderDistIni;
            masterTexSlider.value = TextureLimitIni;
            shadowDistSlider.value = ShadowDistIni;
            shadowCascadesSlider.value = ShadowCascadeIni;

            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
            pauseMenuTitleText.text = "Main Menu";
            vidPanel.SetActive(false);
            audioPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
        #endregion

        #region Game Setting Events
        public void UpdateMasterVol(float f)
        {
            AudioListener.volume = f;
            // Debug.Log("GameSettingsManager::updateMasterVol " + AudioListener.volume);
        }

        public void ToggleVSync(Boolean b)
        {
            QualitySettings.vSyncCount = b == true ? 1 : 0;
            // Debug.Log("GameSettingsManager::toggleVSync : " + QualitySettings.vSyncCount);
        }

        public void UpdateRenderDist(float f)
        {
            try
            {
                mainCam.farClipPlane = f;
            }
            catch
            {
                // Debug.Log("Camera was not assigned, using Camera.Main");
                mainCam = Camera.main;
                mainCam.farClipPlane = f;
            }
            // Debug.Log("GameSettingsManager::updateRenderDist() : " + Camera.main.farClipPlane);
        }

        public void UpdateTex(float qual)
        {
            int f = Mathf.RoundToInt(qual);
            QualitySettings.masterTextureLimit = f;
            // Debug.Log("GameSettingsManager::updateTex() : " + QualitySettings.masterTextureLimit);
        }

        public void UpdateShadowDistance(float dist)
        {
            QualitySettings.shadowDistance = dist;
            // Debug.Log("GameSettingsManager::updateShadowDistance() : " + QualitySettings.shadowDistance);
        }

        public static void forceOnANISO()
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        }

        public static void PerTexAniso()
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        }

        public static void DisableAniso()
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        }

        public void UpdateAniso(int anisoSetting)
        {
            switch (anisoSetting)
            {
                case 0:
                    DisableAniso();
                    break;
                case 1:
                    PerTexAniso();
                    break;
                case 2:
                    forceOnANISO();
                    break;
            }

            // Debug.Log("GameSettingsManager::updateANISO() : " + (int)QualitySettings.anisotropicFiltering);
        }

        public void UpdateCascades(float cascades)
        {
            int c = Mathf.RoundToInt(cascades);
            switch (c)
            {
                case 1:
                case 3:
                    c = 2;
                    break;
            }
            QualitySettings.shadowCascades = c;
            // Debug.Log("GameSettingsManager::updateCascades() : " + QualitySettings.shadowCascades);
        }

        public void UpdateMsaa(int msaaAmount)
        {
            switch (msaaAmount)
            {
                case 0:
                    DisableMsaa();
                    break;
                case 1:
                    TwoMsaa();
                    break;
                case 2:
                    FourMsaa();
                    break;
                case 3:
                    EightMsaa();
                    break;
            }

            // Debug.Log("MSAA has been set to " + QualitySettings.antiAliasing + "x");
        }

        public static void DisableMsaa()
        {
            QualitySettings.antiAliasing = 0;
        }

        public static void TwoMsaa()
        {
            QualitySettings.antiAliasing = 2;
        }

        public static void FourMsaa()
        {
            QualitySettings.antiAliasing = 4;
        }

        public static void EightMsaa()
        {
            QualitySettings.antiAliasing = 8;
        }

        public void NextPreset()
        {
            QualitySettings.IncreaseLevel();
            int cur = QualitySettings.GetQualityLevel();
            presetLabel.text = _presets[cur];
            shadowDistSlider.value = shadowDist[cur];
            aaCombo.value = cur;
            // Debug.Log("Graphics Preset has been set to " + presets[cur]);
        }

        public void LastPreset()
        {
            QualitySettings.DecreaseLevel();
            int cur = QualitySettings.GetQualityLevel();
            presetLabel.text = _presets[cur];
            shadowDistSlider.value = shadowDist[cur];
            aaCombo.value = cur;
            // Debug.Log("Graphics Preset has been set to " + presets[cur]);
        }
        #endregion

        #region INI Settings

        private void DefaultSettings()
        {
            Debug.Log("GameSettingsManager::DefaultSettings()");

            //  Default Audio
            MasterVolumeIni = 0.75f;

            //  Default Graphics Preset
            CurrentQualityLevelIni = _presets.Length - 1;

            //  Default Graphics Settings
            MsaaIni = 2;
            VsyncIni = true;
            AnisoFilterLevelIni = 1;
            RenderDistIni = 800.0f;
            ShadowDistIni = shadowDist[CurrentQualityLevelIni];
            ShadowCascadeIni = 4;
            TextureLimitIni = 0;
        }

        private void ApplyIniSettings()
        {
            // Debug.Log("INI Settings being applied : " + "Vol : " + masterVolumeINI + ", vsync : " + vsyncINI +
            //     ", Preset " + currentQualityLevelINI + ", RenderDist : " + renderDistINI + ", ShadowDist : " + shadowDistINI +
            //     ", cascade " + shadowCascadeINI + ", MSAA : " + msaaINI + ", aniso : " + anisoFilterLevelINI + ", texture limit : " + textureLimitINI);

            if (AudioListener.volume != MasterVolumeIni)
            {
                AudioListener.volume = MasterVolumeIni;
            }
            if (audioMasterVolumeSlider.value != MasterVolumeIni)
            {
                MuteEventListener(audioMasterVolumeSlider.onValueChanged);
                audioMasterVolumeSlider.value = MasterVolumeIni;
                UnMuteEventListener(audioMasterVolumeSlider.onValueChanged);
            }

            if (QualitySettings.GetQualityLevel() != CurrentQualityLevelIni)
            {
                QualitySettings.SetQualityLevel(CurrentQualityLevelIni);
            }
            if (!presetLabel.text.Contains(_presets[CurrentQualityLevelIni]))
            {
                presetLabel.text = _presets[CurrentQualityLevelIni];
            }

            if (VsyncIni && QualitySettings.vSyncCount == 0)
            {
                QualitySettings.vSyncCount = 1;
            }
            else if (!VsyncIni && QualitySettings.vSyncCount > 0)
            {
                QualitySettings.vSyncCount = 0;
            }
            if (vSyncToggle.isOn != VsyncIni)
            {
                MuteEventListener(vSyncToggle.onValueChanged);
                vSyncToggle.isOn = VsyncIni;
                UnMuteEventListener(vSyncToggle.onValueChanged);
            }

            switch (MsaaIni)
            {
                case 0 when QualitySettings.antiAliasing != 0:
                    DisableMsaa();
                    break;
                case 1 when QualitySettings.antiAliasing != 2:
                    TwoMsaa();
                    break;
                default:
                {
                    if (MsaaIni > 2 && QualitySettings.antiAliasing < 8)
                    {
                        EightMsaa();
                    }
                    else
                    {
                        FourMsaa();
                    }
                    break;
                }
            }
            if (aaCombo.value != MsaaIni)
            {
                MuteEventListener(aaCombo.onValueChanged);
                aaCombo.value = MsaaIni;
                UnMuteEventListener(aaCombo.onValueChanged);
            }

            //  Anisotropic Texture Filtering
            if ((int)QualitySettings.anisotropicFiltering != AnisoFilterLevelIni)
            {
                QualitySettings.anisotropicFiltering = (AnisotropicFiltering)AnisoFilterLevelIni;
            }
            if (afCombo.value != AnisoFilterLevelIni)
            {
                MuteEventListener(afCombo.onValueChanged);
                afCombo.value = AnisoFilterLevelIni;
                UnMuteEventListener(afCombo.onValueChanged);
            }

            //  Render Distance
            if (Camera.main.farClipPlane != RenderDistIni)
            {
                Camera.main.farClipPlane = RenderDistIni;
            }
            if (renderDistSlider.value != RenderDistIni)
            {
                MuteEventListener(renderDistSlider.onValueChanged);
                renderDistSlider.value = RenderDistIni;
                UnMuteEventListener(renderDistSlider.onValueChanged);
            }

            //  Shadow Distance
            if (QualitySettings.shadowDistance != ShadowDistIni)
            {
                QualitySettings.shadowDistance = ShadowDistIni;
            }
            if (shadowDistSlider.value != ShadowDistIni)
            {
                MuteEventListener(shadowDistSlider.onValueChanged);
                shadowDistSlider.value = ShadowDistIni;
                UnMuteEventListener(shadowDistSlider.onValueChanged);
            }

            //  Master Texture Limit
            if (QualitySettings.masterTextureLimit != TextureLimitIni)
            {
                QualitySettings.masterTextureLimit = TextureLimitIni;
            }
            if (masterTexSlider.value != TextureLimitIni)
            {
                MuteEventListener(masterTexSlider.onValueChanged);
                masterTexSlider.value = TextureLimitIni;
                UnMuteEventListener(masterTexSlider.onValueChanged);
            }

            //  Shadow Cascade
            if (QualitySettings.shadowCascades != ShadowCascadeIni)
            {
                QualitySettings.shadowCascades = ShadowCascadeIni;
            }
            if (shadowCascadesSlider.value != ShadowCascadeIni)
            {
                MuteEventListener(shadowCascadesSlider.onValueChanged);
                shadowCascadesSlider.value = ShadowCascadeIni;
                UnMuteEventListener(shadowCascadesSlider.onValueChanged);
            }

            // Debug.Log("Applied Settings : " + "Vol : " + AudioListener.volume + ", vsync : " + QualitySettings.vSyncCount +
            //     ", Preset " + QualitySettings.GetQualityLevel() + ", RenderDist : " + Camera.main.farClipPlane +
            //     ", Shadow Dist : " + QualitySettings.shadowDistance + ", cascade " + QualitySettings.shadowCascades +
            //     ", MSAA : " + QualitySettings.antiAliasing + ", aniso : " + (int)QualitySettings.anisotropicFiltering +
            //     ", texture limit : " + QualitySettings.masterTextureLimit
            // );

            SettingsLoadedIni = true;
        }
        #endregion

        #region Toggle OnValueChanged Listeners

        private void MuteEventListener(UnityEngine.Events.UnityEventBase eventBase)
        {
            int count = eventBase.GetPersistentEventCount();
            for (int x = 0; x < count; x++)
            {
                eventBase.SetPersistentListenerState(x, UnityEngine.Events.UnityEventCallState.Off);
            }
        }

        private void UnMuteEventListener(UnityEngine.Events.UnityEventBase eventBase)
        {
            int count = eventBase.GetPersistentEventCount();
            for (int x = 0; x < count; x++)
            {
                eventBase.SetPersistentListenerState(x, UnityEngine.Events.UnityEventCallState.RuntimeOnly);
            }
        }
        #endregion
    }
}