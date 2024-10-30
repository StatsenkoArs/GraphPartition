using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    internal interface IRestruct //Перестраивает граф по готовому отображению
    {
        public List<int>[] GetNewGraph();
        public void Restruct(ref List<int>[] graph, in int[] vertex_mapping, int group);
    }
}
