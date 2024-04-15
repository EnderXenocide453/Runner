using System.IO;
using UnityEngine;

namespace GameManagement
{
    public static class SaveManager
    {
        public static string SavePath => Application.persistentDataPath + "/SaveData.sav";
        public static string ConfigPath => Application.persistentDataPath + "/Config.cfg";

        public static void SaveGame(SaveInfo saveInfo) => SaveFile(SavePath, saveInfo);

        public static SaveInfo LoadGame()
        {
            if (LoadFile<SaveInfo>(SavePath, out var data)) {
                return data;
            }
            return new SaveInfo();
        }

        public static void SaveConfig(ConfigInfo config) => SaveFile(ConfigPath, config);

        public static ConfigInfo LoadConfig()
        {
            if (LoadFile<ConfigInfo>(ConfigPath, out var data)) {
                return data;
            }

            return new ConfigInfo()
            {
                soundInfo = new SoundInfo
                {
                    isMusicActive = true,
                    musicVolume = 1,
                    isSoundActive = true,
                    soundVolume = 1
                }
            };
        }

        public static void SaveFile(string filePath, object data)
        {
            string serialized = JsonUtility.ToJson(data);

            try {
                File.WriteAllText(filePath, serialized);
            }
            catch (System.Exception) {
                Debug.LogError($"Ошибка записи файла {filePath}");
            }
        }

        public static bool LoadFile<T>(string filePath, out T data) where T : struct
        {
            data = new();

            if (!File.Exists(filePath))
                return false;

            try {
                data = JsonUtility.FromJson<T>(File.ReadAllText(filePath));
            }
            catch (System.Exception) {
                Debug.LogError($"Ошибка чтения файла {filePath}");
                return false;
            }

            return true;
        }
    }
}
