/*
 * SaveSettings - Save/Loads game settings to/from a JSON file
 * Created by : Allan N. Murillo
 */

using System.IO;
using UnityEngine;

namespace ANM.Framework
{
    public class SaveSettings
    {
        private static string _jsonString;
        private const string FileName = "/GameSettings.json";

        public float MasterVolume;
        public float EffectVolume;
        public float BackgroundVolume;
        public int CurrentQualityLevel;
        public bool Vsync;
        public int Msaa;
        public float RenderDist;
        public float ShadowDist;
        public int TextureLimit;
        public int AnisoFilterLevel;
        public int ShadowCascade;

        internal static float MasterVolumeIni;
        internal static float EffectVolumeIni;
        internal static float BackgroundVolumeIni;
        internal static int CurrentQualityLevelIni;
        internal static bool VsyncIni;
        internal static int MsaaIni;
        internal static float RenderDistIni;
        internal static float ShadowDistIni;
        internal static int TextureLimitIni;
        internal static int AnisoFilterLevelIni;
        internal static int ShadowCascadeIni;
        internal static bool SettingsLoadedIni;


        public void Initialize() {  SettingsLoadedIni = LoadGameSettings();  }
        
        private static object CreateJsonObj(string jsonString)
        {
            return JsonUtility.FromJson<SaveSettings>(jsonString);
        }

        public bool LoadGameSettings()
        {
            var path = Application.persistentDataPath + FileName;
            if (!VerifyDirectory(path)) return false;
            OverwriteGameSettings(File.ReadAllText(path));
            return true;
        }

        public void SaveGameSettings()
        {
            var path = Application.persistentDataPath + FileName;
            if (VerifyDirectory(path)) { File.Delete(path); }
            
            MasterVolume = MasterVolumeIni;
            EffectVolume = EffectVolumeIni;
            BackgroundVolume = BackgroundVolumeIni;
            RenderDist = RenderDistIni;
            ShadowDist = ShadowDistIni;
            Msaa = MsaaIni;
            Vsync = VsyncIni;
            TextureLimit = TextureLimitIni;
            CurrentQualityLevel = CurrentQualityLevelIni;
            ShadowCascade = ShadowCascadeIni;
            AnisoFilterLevel = AnisoFilterLevelIni;
            
            _jsonString = JsonUtility.ToJson(this);
            File.WriteAllText(path, _jsonString);
        }

        private void OverwriteGameSettings(string jsonString)
        {
            try
            {
                SaveSettings read = (SaveSettings)CreateJsonObj(jsonString);
                MasterVolumeIni = read.MasterVolume;
                EffectVolumeIni = read.EffectVolume;
                BackgroundVolumeIni = read.BackgroundVolume;
                RenderDistIni = read.RenderDist;
                ShadowDistIni = read.ShadowDist;
                MsaaIni = read.Msaa;
                VsyncIni = read.Vsync;
                TextureLimitIni = read.TextureLimit;
                CurrentQualityLevelIni = read.CurrentQualityLevel;
                ShadowCascadeIni = read.ShadowCascade;
                AnisoFilterLevelIni = read.AnisoFilterLevel;
            }
            catch (FileLoadException)
            {
                Debug.LogError("Could not read game settings from json file");
            }
        }

        private bool VerifyDirectory(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
