using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphOptimisation;
using GraphPartitionAccurate;
using GraphReduction;

namespace GraphPartitionClass
{
    public interface IGraphPartition
    {
        IGraphReduction ReductionAlgorithm { set; get; }
        IAccuratePartition AccuratePartition { set; get; }
        IGraphRestoration RestorationAlgorithm { set; get; }
        IPartitionOptimisation OptimisationAlgorithm { set; get; }
        int[] GetPartition(int[][] graph);
    }
}
