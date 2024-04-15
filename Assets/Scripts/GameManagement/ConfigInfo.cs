namespace GameManagement
{
    [System.Serializable]
    public struct ConfigInfo
    {
        public SoundInfo soundInfo;
    }

    [System.Serializable]
    public struct SoundInfo
    {
        public bool isMusicActive;
        public float musicVolume;
        public bool isSoundActive;
        public float soundVolume;
    }
}
