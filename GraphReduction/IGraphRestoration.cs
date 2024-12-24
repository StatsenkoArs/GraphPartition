using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public interface IGraphRestoration
    {
        /// <summary>
        /// Восстанавливает граф каким-то образом.
        /// </summary>
        /// <param name="partition">Исходное разбиеие</param>
        /// <returns>Разбиение большей размерности</returns>
        int[] Restore(int[] partition);

        /// <summary>
        /// Уставновить хранилище для графов.
        /// </summary>
        /// <param name="graph">Хранилище графов</param>
        public void SetGraphStorage(Stack<IGraph> graph);
        /// <summary>
        /// Утсановать хранилище отображений.
        /// </summary>
        /// <param name="mappings">Хранилище отображений</param>
        void SetMappingStorage(Stack<int[]> mappings);
    }
}
