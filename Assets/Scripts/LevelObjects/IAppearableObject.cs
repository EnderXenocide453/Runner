namespace LevelObjects
{
    /// <summary>
    /// Интерфейс объекта, способного появиться и исчезнуть
    /// </summary>
    public interface IAppearableObject
    {
        void Appear();
        void Disappear();
    }
}