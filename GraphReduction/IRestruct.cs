using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    /// <summary>
    /// Интерфейс для перестройки графа по готовому отображению
    /// </summary>
    public interface IRestruct
    {
        /// <summary>
        /// Метод, возвращающий последний сжатый граф
        /// </summary>
        /// <returns>Последний сжатый граф</returns>
        public int[][] GetGraph();
        /// <summary>
        /// По графу, сжиамющему отображению и кол-ву групп в отображении строит новый граф
        /// </summary>
        /// <param name="graph">Исходный граф</param>
        /// <param name="vertex_mapping">Сжиамющее отображение</param>
        /// <param name="group">Кол-во групп в сжимающем отображении</param>
        /// <returns>Новый граф</returns>
        public int[][] Restruct(int[][] graph, in int[] vertex_mapping, int group);
    }
}
