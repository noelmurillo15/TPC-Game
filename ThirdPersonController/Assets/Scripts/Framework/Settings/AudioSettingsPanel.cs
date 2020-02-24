﻿/*
 * AudioSettings - Handles displaying / configuring audio settings
 * Created by : Allan N. Murillo
 * Last Edited : 2/17/2020
 */

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
    public class AudioSettingsPanel : MonoBehaviour
    {
        [SerializeField] private GameObject audioPanel = null;
        [SerializeField] private Animator audioPanelAnimator = null;
        
        [SerializeField] private Slider audioMasterVolumeSlider = null;
        [SerializeField] private Slider effectsVolumeSlider = null;
        [SerializeField] private Slider backgroundVolumeSlider = null;
        
        [SerializeField] private Button audioPanelSelectedObj = null;
        
        [SerializeField] private AudioSource bgMusic = null;
        [SerializeField] private AudioSource[] sfx = null;

        
        private void Start()
        {
            TurnOffPanel();
        }

        public void TurnOffPanel()
        {
            audioPanel.SetActive(false);
        }
        
        public void AudioPanelIn(EventSystem eventSystem)
        {
            if (audioPanelAnimator == null) return;
            audioPanelAnimator.enabled = true;
            audioPanelAnimator.Play("Audio Panel In");
            eventSystem.SetSelectedGameObject(GetSelectObject());
            audioPanelSelectedObj.OnSelect(null);
        }
        
        public IEnumerator SaveAudioSettings()
        {
            audioPanelAnimator.Play("Audio Panel Out");
            SaveSettings.masterVolumeIni = audioMasterVolumeSlider.value;
            SaveSettings.effectVolumeIni = effectsVolumeSlider.value;
            SaveSettings.backgroundVolumeIni = backgroundVolumeSlider.value;
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
            GameManager.Instance.SaveGameSettings();
        }

        public IEnumerator RevertAudioSettings()
        {
            audioPanelAnimator.Play("Audio Panel Out");
            audioMasterVolumeSlider.value = SaveSettings.masterVolumeIni;
            effectsVolumeSlider.value = SaveSettings.effectVolumeIni;
            backgroundVolumeSlider.value = SaveSettings.backgroundVolumeIni;
            yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(0.5f));
        }

        public void ApplyAudioSettings()
        {
            OverrideMasterVolume();
            OverrideBackgroundVolume();
            OverrideEffectsVolume();
        }
        
        public void UpdateMasterVolume(float amount)
        {
            AudioListener.volume = amount;
        }
        
        public void UpdateEffectsVolume(float amount)
        {
            foreach (var effect in sfx)
            {
                effect.volume = amount;
            }
        }
        
        public void UpdateBackgroundVolume(float amount)
        {
            if(bgMusic != null)
                bgMusic.volume = amount;
        }
        
        private void OverrideMasterVolume()
        {
            if (Math.Abs(AudioListener.volume - SaveSettings.masterVolumeIni) > 0f)
                AudioListener.volume = SaveSettings.masterVolumeIni;            

            if (!(Math.Abs(audioMasterVolumeSlider.value - SaveSettings.masterVolumeIni) > 0f)) return;
            EventExtension.MuteEventListener(audioMasterVolumeSlider.onValueChanged);
            audioMasterVolumeSlider.value = SaveSettings.masterVolumeIni;
            EventExtension.UnMuteEventListener(audioMasterVolumeSlider.onValueChanged);
        }
        
        private void OverrideBackgroundVolume()
        {
            if (bgMusic != null && Math.Abs(bgMusic.volume - SaveSettings.backgroundVolumeIni) > 0f)
                bgMusic.volume = SaveSettings.backgroundVolumeIni;            

            if (!(Math.Abs(backgroundVolumeSlider.value - SaveSettings.backgroundVolumeIni) > 0f)) return;
            EventExtension.MuteEventListener(backgroundVolumeSlider.onValueChanged);
            backgroundVolumeSlider.value = SaveSettings.backgroundVolumeIni;
            EventExtension.UnMuteEventListener(backgroundVolumeSlider.onValueChanged);
        }
        
        private void OverrideEffectsVolume()
        {
            if (sfx.Length > 0 && Math.Abs(sfx[0].volume - SaveSettings.effectVolumeIni) > 0f)
                foreach (var effect in sfx)                
                    effect.volume = SaveSettings.effectVolumeIni;
            
            if (!(Math.Abs(effectsVolumeSlider.value - SaveSettings.effectVolumeIni) > 0f)) return;
            EventExtension.MuteEventListener(effectsVolumeSlider.onValueChanged);
            effectsVolumeSlider.value = SaveSettings.effectVolumeIni;
            EventExtension.UnMuteEventListener(effectsVolumeSlider.onValueChanged);
        }
        
        private GameObject GetSelectObject()
        {
            return audioPanelSelectedObj.gameObject;
        }
    }
}