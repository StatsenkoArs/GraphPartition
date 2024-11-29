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
        /// <param name="partition"></param>
        /// <returns></returns>
        int[] Restore(int[] partition);
        void SetMappingStorage(Stack<int[]> mappings);
    }
}
