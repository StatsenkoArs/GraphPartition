using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphOptimisation;

namespace GraphReduction
{
    internal class SimpleCompleteGraphRestoration: ICompleteGraphRestoration
    {
        private GrafFeduchiMatteus _FM_method;
        private IGraphRestoration _graphRestoration;
        public SimpleCompleteGraphRestoration(GrafFeduchiMatteus fm_method, IGraphRestoration graphRestoration)
        {
            _FM_method = fm_method;
            _graphRestoration = graphRestoration;
        }
        public int[][] GraphRestoration(List<int[][]> graphs, List<int[]> mappings, int[] strar_partition)
        {

        }

    }
}
