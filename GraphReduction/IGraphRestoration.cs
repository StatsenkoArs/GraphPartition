using GraphRepresentation;

namespace GraphReduction
{
    /// <summary>
    /// Интерфейс для восстановаления разбиения на весь граф
    /// </summary>
    public interface IGraphRestoration
    {
        /// <summary>
        /// Растягивает разбиение на большую размерность графа
        /// </summary>
        /// <param name="partition">Исходное разбиение</param>
        /// <returns>Разбиение большей размерности</returns>
        int[] Restore(int[] partition);
        /// <summary>
        /// Установить хранилище для графов
        /// </summary>
        /// <param name="graph">Хранилище графов</param>
        public void SetGraphStorage(Stack<IGraph> graphs);
        /// <summary>
        /// Установить хранилище для отображений
        /// </summary>
        /// <param name="mappings">Хранилище отображений</param>
        void SetMappingStorage(Stack<int[]> mappings);
    }
}
