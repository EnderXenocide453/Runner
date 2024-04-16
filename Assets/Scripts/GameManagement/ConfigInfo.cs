namespace GameManagement
{
    [System.Serializable]
    public struct ConfigInfo
    {
        public SoundInfo soundInfo;
        public InputInfo inputInfo;
    }

    [System.Serializable]
    public struct SoundInfo
    {
        public bool isMusicActive;
        public float musicVolume;
        public bool isSoundActive;
        public float soundVolume;
    }

    [System.Serializable]
    public struct InputInfo
    {
        public float deviationSensitivity;
    }
}
