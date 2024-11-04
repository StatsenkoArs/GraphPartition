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
        public List<int>[] GetGraph();
        public List<int>[] Restruct(List<int>[] graph, in int[] vertex_mapping, int group);
    }
}
