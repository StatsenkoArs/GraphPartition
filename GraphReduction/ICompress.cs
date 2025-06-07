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
        /// <param name="compress_ratio"?Степень сжатия для графа от 0 до 100, где 100 - 
        /// максимальное сжатие в лучшем случае в два раза</param>
        /// <returns>Сжимающее отображение</returns>
        public int[] Compress(IGraph graph, int compress_ratio);
        /// <summary>
        /// Метод, возвращающий количество групп, распределенных в последнем сжимающем отображении
        /// </summary>
        /// <returns>Количество группп</returns>
        public int GetNumOfGroup();
    }
}
