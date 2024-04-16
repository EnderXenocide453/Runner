namespace GameManagement
{
    /// <summary>
    /// Информация для сохранения настроек
    /// </summary>
    [System.Serializable]
    public struct ConfigInfo
    {
        public SoundInfo soundInfo;
        public InputInfo inputInfo;
    }

    /// <summary>
    /// Информация о настройках звука
    /// </summary>
    [System.Serializable]
    public struct SoundInfo
    {
        public bool isMusicActive;
        public float musicVolume;
        public bool isSoundActive;
        public float soundVolume;
    }

    /// <summary>
    /// Информация о настройках ввода
    /// </summary>
    [System.Serializable]
    public struct InputInfo
    {
        public float deviationSensitivity;
    }
}
