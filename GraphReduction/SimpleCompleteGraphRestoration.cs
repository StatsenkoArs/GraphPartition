using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphOptimisation;

namespace GraphReduction
{
    //internal class SimpleCompleteGraphRestoration: ICompleteGraphRestoration
    //{
    //    private GrafFeduchiMatteus _FM_method;
    //    private IGraphRestoration _graphRestoration;
    //    public SimpleCompleteGraphRestoration(GrafFeduchiMatteus fm_method, IGraphRestoration graphRestoration)
    //    {
    //        _FM_method = fm_method;
    //        _graphRestoration = graphRestoration;
    //    }
    //    public int[][] GraphRestoration(List<int[][]> graphs, List<int[]> mappings, int[] start_partition)
    //    {
    //        int[] tmp_partition = start_partition;
    //        for (int i = 0; i < mappings.Count; i++)
    //        {
    //            tmp_partition = _graphRestoration.UnmappingStep(tmp_partition, mappings[i]);
    //            _FM_method.FeduchiMatteus(graphs[i], tmp_partition, 2);
    //        }

    //    }

    //}
}
