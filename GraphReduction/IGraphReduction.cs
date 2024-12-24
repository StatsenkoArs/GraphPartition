using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public interface IGraphReduction
    {
        /// <summary>
        /// По графу строит граф меньшего размера.
        /// </summary>
        /// <param name="graph">Старый граф</param>
        /// <returns>Новый граф меньшего размера</returns>
        IGraph Reduct(IGraph graph);
        /// <summary>
        /// Возвращает последнее отображения. Костыль?
        /// </summary>
        /// <returns>Последнее отображение</returns>
        public int[] GetLastMapping();

    }
}
