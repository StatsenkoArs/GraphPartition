using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPartitionAccurate
{
    public interface IAccuratePartition
    {
        /// <summary>
        /// Получить разбиение
        /// </summary>
        /// <param name="graph">Граф</param>
        /// <param name="balanceCriteria">Баланс разбиения</param>
        /// <returns>вектор x - разбиение графа</returns>
        int[] GetPartition(IGraph graph, double balanceCriteria = 0);

        /// <summary>
        /// Получить уже вычисленное разбиение
        /// </summary>
        /// <returns>пара разбиение-критерий</returns>
        (int[], int) GetSolution();
    }
}
