/*
 * VideoSettings - Handles displaying / configuring video settings
 * Created by : Allan N. Murillo
 * Last Edited : 2/17/2020
 */

using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ANM.Framework.Utils;
using ANM.Framework.Managers;
using ANM.Framework.Extensions;
using UnityEngine.EventSystems;

namespace ANM.Framework.Settings
{
    public class VideoSettingsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject videoPanel = null;
        [SerializeField] private Animator videoPanelAnimator = null;
        
        [SerializeField] private Toggle vsyncToggle = null;
        [SerializeField] private TMP_Dropdown msaaDropdown = null;
        [SerializeField] private TMP_Dropdown anisotropicDropdown = null;
        [SerializeField] private Slider renderDistSlider = null;
        [SerializeField] private Slider shadowDistSlider = null;
        
        [SerializeField] private Slider masterTexSlider = null;
        [SerializeField] private Slider shadowCascadesSlider = null;
        [SerializeField] private TMP_Text presetLabel = null;
        [SerializeField] private float[] shadowDist = null;
        
        [SerializeField] private Button videoPanelSelectedObj = null;

        private Camera _mainCamera;
        private string[] _presets;


        private void Awake()
        {
            _mainCamera = Camera.main;
            _presets = QualitySettings.names;
        }

        private void Start()
        {
            TurnOffPanel();
        }
        
        public void TurnOffPanel()
        {
            videoPanel.SetActive(false);
        }
        
        public void VideoPanelIn(EventSystem eventSystem)
        {
            if (videoPanelAnimator == null) return;
            videoPanelAnimator.enabled = true;
            videoPanelAnimator.Play("Video Panel In");
            eventSystem.SetSelectedGameObject(GetSelectObject());
            videoPanelSelectedObj.OnSelect(null);
        }
        
        public IEnumerator SaveVideoSettings()
        {
            videoPanelAnimator.Play("Video Panel Out");
            SaveSettings.currentQualityLevelIni = QualitySettings.GetQualityLevel();
            SaveSettings.msaaIni = msaaDropdown.value;
            SaveSettings.anisotropicFilteringLevelIni = anisotropicDropdown.value;
            SaveSettings.renderDistIni = renderDistSlider.value;
            SaveSettings.textureLimitIni = (int)masterTexSlider.value;
            SaveSettings.shadowDistIni = shadowDistSlider.value;
            SaveSettings.shadowCascadeIni = (int)shadowCascadesSlider.value;
            SaveSettings.vsyncIni = vsyncToggle.isOn;
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
            GameManager.Instance.SaveGameSettings();
        }

        public IEnumerator RevertVideoSettings()
        {
            videoPanelAnimator.Play("Video Panel Out");
            QualitySettings.SetQualityLevel(SaveSettings.currentQualityLevelIni);
            msaaDropdown.value = SaveSettings.msaaIni;
            anisotropicDropdown.value = SaveSettings.anisotropicFilteringLevelIni;
            renderDistSlider.value = SaveSettings.renderDistIni;
            masterTexSlider.value = SaveSettings.textureLimitIni;
            shadowDistSlider.value = SaveSettings.shadowDistIni;
            shadowCascadesSlider.value = SaveSettings.shadowCascadeIni;
            vsyncToggle.isOn = SaveSettings.vsyncIni;
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
        }
        
        public void ApplyVideoSettings()
        {
            OverrideVsync();
            OverrideGraphicsPreset();
            OverrideAnisotropicFiltering();
            OverrideMasterTextureQuality();
            OverrideRenderDistance();
            OverrideShadowDistance();
            OverrideShadowCascade();
            OverrideMsaa();
        }
        
        public void UpdateRenderDistance(float renderDistance)
        {
            try
            {
                _mainCamera.farClipPlane = renderDistance;
            }
            catch
            {
                _mainCamera = Camera.main;
                _mainCamera.farClipPlane = renderDistance;
            }
        }

        public void UpdateVsync(bool toggle)
        {
            QualitySettings.vSyncCount = toggle ? 1 : 0;
        }

        public void UpdateMasterTextureQuality(float textureQuality)
        {
            var f = Mathf.RoundToInt(textureQuality);
            QualitySettings.masterTextureLimit = f;
        }

        public void UpdateShadowDistance(float shadowDistance)
        {
            QualitySettings.shadowDistance = shadowDistance;
        }
        
        public void UpdateAnisotropicFiltering(int level)
        {
            switch (level)
            {
                case 0:
                    DisableAnisotropicFilter();
                    break;
                case 1:
                    PerTextureAnisotropicFilter();
                    break;
                case 2:
                    ForceOnAnisotropicFilter();
                    break;
            }
        }

        public void UpdateShadowCascades(float cascades)
        {
            var c = Mathf.RoundToInt(cascades);
            switch (c)
            {
                case 1:
                case 3:
                    c = 2;
                    break;
            }
            QualitySettings.shadowCascades = c;
        }

        public void UpdateMsaa(int level)
        {
            switch (level)
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
        }
        
        public void NextPreset()
        {
            QualitySettings.IncreaseLevel();
            var cur = QualitySettings.GetQualityLevel();
            PresetOverride(cur);
        }

        public void LastPreset()
        {
            QualitySettings.DecreaseLevel();
            var cur = QualitySettings.GetQualityLevel();
            PresetOverride(cur);
        }

        private static void ForceOnAnisotropicFilter()
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        }

        private static void PerTextureAnisotropicFilter()
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
        }

        private static void DisableAnisotropicFilter()
        {
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
        }
        
        private static void DisableMsaa()
        {
            QualitySettings.antiAliasing = 0;
        }

        private static void TwoMsaa()
        {
            QualitySettings.antiAliasing = 2;
        }

        private static void FourMsaa()
        {
            QualitySettings.antiAliasing = 4;
        }

        private static void EightMsaa()
        {
            QualitySettings.antiAliasing = 8;
        }
        
        private void OverrideGraphicsPreset()
        {
            if (QualitySettings.GetQualityLevel() != SaveSettings.currentQualityLevelIni)
                QualitySettings.SetQualityLevel(SaveSettings.currentQualityLevelIni);
            
            if (!presetLabel.text.Contains(_presets[SaveSettings.currentQualityLevelIni]))
                presetLabel.text = _presets[SaveSettings.currentQualityLevelIni];
        }
        
        private void OverrideMsaa()
        {
            switch (SaveSettings.msaaIni)
            {
                case 0 when QualitySettings.antiAliasing != 0:
                    DisableMsaa(); break;
                case 1 when QualitySettings.antiAliasing != 2:
                    TwoMsaa(); break;
                case 2 when QualitySettings.antiAliasing != 4:
                    FourMsaa(); break;
                case 3 when QualitySettings.antiAliasing < 8:
                    EightMsaa(); break;
            }

            if (msaaDropdown.value == SaveSettings.msaaIni) return;
            EventExtension.MuteEventListener(msaaDropdown.onValueChanged);
            msaaDropdown.value = SaveSettings.msaaIni;
            EventExtension.UnMuteEventListener(msaaDropdown.onValueChanged);
        }
        
        private void OverrideAnisotropicFiltering()
        {
            if ((int)QualitySettings.anisotropicFiltering != SaveSettings.anisotropicFilteringLevelIni)
                QualitySettings.anisotropicFiltering = (AnisotropicFiltering)SaveSettings.anisotropicFilteringLevelIni;

            if (anisotropicDropdown.value == SaveSettings.anisotropicFilteringLevelIni) return;
            EventExtension.MuteEventListener(anisotropicDropdown.onValueChanged);
            anisotropicDropdown.value = SaveSettings.anisotropicFilteringLevelIni;
            EventExtension.UnMuteEventListener(anisotropicDropdown.onValueChanged);
        }

        private void OverrideRenderDistance()
        {
            if (Math.Abs(_mainCamera.farClipPlane - SaveSettings.renderDistIni) > 0f)
                _mainCamera.farClipPlane = SaveSettings.renderDistIni;

            if (!(Math.Abs(renderDistSlider.value - SaveSettings.renderDistIni) > 0f)) return;
            EventExtension.MuteEventListener(renderDistSlider.onValueChanged);
            renderDistSlider.value = SaveSettings.renderDistIni;
            EventExtension.UnMuteEventListener(renderDistSlider.onValueChanged);
        }
        
        private void OverrideShadowDistance()
        {
            if (Math.Abs(QualitySettings.shadowDistance - SaveSettings.shadowDistIni) > 0f)
                QualitySettings.shadowDistance = SaveSettings.shadowDistIni;

            if (!(Math.Abs(shadowDistSlider.value - SaveSettings.shadowDistIni) > 0f)) return;
            EventExtension.MuteEventListener(shadowDistSlider.onValueChanged);
            shadowDistSlider.value = SaveSettings.shadowDistIni;
            EventExtension.UnMuteEventListener(shadowDistSlider.onValueChanged);
        }
        
        private void OverrideShadowCascade()
        {
            if (QualitySettings.shadowCascades != SaveSettings.shadowCascadeIni)
                QualitySettings.shadowCascades = SaveSettings.shadowCascadeIni;

            if (!(Math.Abs(shadowCascadesSlider.value - SaveSettings.shadowCascadeIni) > 0f)) return;
            EventExtension.MuteEventListener(shadowCascadesSlider.onValueChanged);
            shadowCascadesSlider.value = SaveSettings.shadowCascadeIni;
            EventExtension.UnMuteEventListener(shadowCascadesSlider.onValueChanged);
        }

        private void OverrideMasterTextureQuality()
        {
            if (QualitySettings.masterTextureLimit != SaveSettings.textureLimitIni)
                QualitySettings.masterTextureLimit = SaveSettings.textureLimitIni;

            if (!(Math.Abs(masterTexSlider.value - SaveSettings.textureLimitIni) > 0f)) return;
            EventExtension.MuteEventListener(masterTexSlider.onValueChanged);
            masterTexSlider.value = SaveSettings.textureLimitIni;
            EventExtension.UnMuteEventListener(masterTexSlider.onValueChanged);
        }

        private void OverrideVsync()
        {
            var intendedVsync = SaveSettings.vsyncIni;
            EventExtension.MuteEventListener(vsyncToggle.onValueChanged);
            vsyncToggle.isOn = intendedVsync;
            EventExtension.UnMuteEventListener(vsyncToggle.onValueChanged);
            QualitySettings.vSyncCount = intendedVsync ? 1 : 0;
        }

        private void PresetOverride(int currentValue)
        {
            presetLabel.text = _presets[currentValue];
            shadowDistSlider.value = shadowDist[currentValue];
            msaaDropdown.value = currentValue;
        }
        
        private GameObject GetSelectObject()
        {
            return videoPanelSelectedObj.gameObject;
        }
    }
}
