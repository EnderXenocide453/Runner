using Palmmedia.ReportGenerator.Core.Common;
using System.IO;
using UnityEngine;

namespace GameManagement
{
    public static class SaveManager
    {
        public static string Path => Application.persistentDataPath + "/SaveData.sav";

        public static void Save(SaveInfo saveInfo)
        {
            if (!File.Exists(Path))
                File.Create(Path);

            string serialized = JsonUtility.ToJson(saveInfo);

            try {
                File.WriteAllText(Path, serialized);
            }
            catch (System.Exception) {
                Debug.LogError("Ошибка записи сохранения");
            }
        }

        public static SaveInfo Load()
        {
            SaveInfo saveInfo = new SaveInfo();

            if (!File.Exists(Path))
                return saveInfo;

            try {
                saveInfo = JsonUtility.FromJson<SaveInfo>(File.ReadAllText(Path));
            }
            catch (System.Exception) {
                Debug.LogError("Ошибка чтения файла");
            }

            return saveInfo;
        }
    }
}
