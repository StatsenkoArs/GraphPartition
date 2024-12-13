using GraphRepresentation;
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
        /// Строит новый граф по старому графу, отображению вершин и кол-ву групп.
        /// </summary>
        /// <param name="graph">Старый граф</param>
        /// <param name="vertex_mapping">Отображение вершин</param>
        /// <param name="group">Кол-во групп отображения</param>
        /// <returns>Новый граф</returns>
        public IGraph Restruct(IGraph graph, in int[] vertex_mapping, int group);
    }
}
