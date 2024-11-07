using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int[] Compress(in int[][] graph);
        /// <summary>
        /// Метод, возвращающий последнее сжимающее отображение
        /// </summary>
        /// <returns>Последнее сжимающее отображение</returns>
        public int[] GetMapping();
        /// <summary>
        /// Метод, возвращающий количество групп, распределенных в последнем сжимающем отображении
        /// </summary>
        /// <returns>Количество группп</returns>
        public int GetNumOfGroup();
    }
}
