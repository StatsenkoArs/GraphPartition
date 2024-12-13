using GraphOptimisation;
using GraphPartitionAccurate;
using GraphReduction;
using GraphRepresentation;

namespace GraphPartitionClass
{
    public class Graph2Partition : IGraphPartition
    {
        public IGraphReduction ReductionAlgorithm { get; set; }
        public IAccuratePartition AccuratePartition { get; set; }
        public IGraphRestoration RestorationAlgorithm { get; set    ; }
        public IPartitionOptimisation OptimisationAlgorithm { get; set; }

        public Graph2Partition(IGraphReduction reductionAlgoritm, IAccuratePartition accuratePartition, IGraphRestoration restorationAlgorithm, IPartitionOptimisation optimisationAlgorithm) 
        {
            ReductionAlgorithm = reductionAlgoritm;
            AccuratePartition = accuratePartition;
            RestorationAlgorithm = restorationAlgorithm;
            OptimisationAlgorithm = optimisationAlgorithm;
        }

        public int[] GetPartition(int[][] graph)
        {
            IGraph srcGraph = new GraphSRC(graph);
            int n = srcGraph.CountVertecies;
            //TODO: remove magic number '30' (add some kind of config maybe)
            while (srcGraph.CountVertecies > 30)
            {
                srcGraph = ReductionAlgorithm.Reduct(srcGraph);
            }
            int[] partition = AccuratePartition.GetPartition(srcGraph);
            while (srcGraph.CountVertecies < n)
            {
                partition = RestorationAlgorithm.Restore(partition);
            }
            return partition;
        }
    }
}
