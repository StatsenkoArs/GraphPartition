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
            Stack<int[]> mapping = new Stack<int[]>();
            Stack<IGraph> graphCache = new Stack<IGraph>();
            //TODO: remove magic number '20' (add some kind of config maybe)
            while (srcGraph.CountVertecies > 20)
            {
                srcGraph = ReductionAlgorithm.Reduct(srcGraph);
                graphCache.Push(srcGraph);
                mapping.Push(ReductionAlgorithm.GetLastMapping());
            }
            int[] partition = AccuratePartition.GetPartition(srcGraph);
            RestorationAlgorithm.SetGraphStorage(graphCache);
            RestorationAlgorithm.SetMappingStorage(mapping);
            while (srcGraph.CountVertecies < n)
            {
                partition = RestorationAlgorithm.Restore(partition);
            }
            return partition;
        }
    }
}
