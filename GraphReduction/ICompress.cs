using GraphRepresentation;

namespace GraphReduction
{
    /// <summary>
    /// Интерфейс для создания сжимающих отображений
    /// </summary>
    public interface ICompress
    {
        /// <summary>
        /// Метод для создания сжимающего отображения
        /// </summary>
        /// <param name="graph">Граф, по которому создаётся сжимающее отображение</param>
        /// <returns>Сжимающее отображение</returns>
        public int[] Compress(IGraph graph);
        /// <summary>
        /// Метод, возвращающий количество групп, распределенных в последнем сжимающем отображении
        /// </summary>
        /// <returns>Количество группп</returns>
        public int GetNumOfGroup();
    }
}
