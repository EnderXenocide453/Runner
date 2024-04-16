namespace UI.Visualization
{
    /// <summary>
    /// Интерфейс хранилища максимального значения.
    /// Необходим для визуализаторов с отображением максимального и текущего значения
    /// </summary>
    public interface IMaxValueHandler
    {
        public abstract float MaxValue { get; set; }
    }
}

