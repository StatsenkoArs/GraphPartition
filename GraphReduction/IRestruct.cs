using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public interface IRestruct //Перестраивает граф по готовому отображению
    {
        public List<int>[] GetGraph();
        public void Restruct(ref List<int>[] graph, in int[] vertex_mapping, int group);
    }
}
