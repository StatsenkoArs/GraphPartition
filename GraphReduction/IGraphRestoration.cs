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
        /// Растягивает разбиение (partition) на очередное отображение (mapping)
        /// </summary>
        /// <param name="partition">Разбиение</param>
        /// <param name="mapping">Очередное отображение</param>
        /// <returns>Новое разбиение больше размерности</returns>
        public int[] UnmappingStep(int[] partition, int[] mapping);
    }
}
