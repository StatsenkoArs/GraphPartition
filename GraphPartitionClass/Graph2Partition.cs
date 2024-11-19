using GraphOptimisation;
using GraphPartitionAccurate;
using GraphReduction;
using GraphRepresentation;

namespace GraphPartitionClass
{
    public class Graph2Partition : IGraphPartition
    {
        public IGraphReduction ReductionAlgorithm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IAccuratePartition AccuratePartition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGraphRestoration RestorationAlgorithm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPartitionOptimisation OptimisationAlgorithm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private IGraph graph;
        private int[] partition;
        private int q;

        public Graph2Partition(IGraphReduction reductionAlgoritm, IAccuratePartition accuratePartition, IGraphRestoration restorationAlgorithm, IPartitionOptimisation optimisationAlgorithm) 
        {
            ReductionAlgorithm = reductionAlgoritm;
            AccuratePartition = accuratePartition;
            RestorationAlgorithm = restorationAlgorithm;
            OptimisationAlgorithm = optimisationAlgorithm;
        }

        public int[] GetPartition(int[][] graph)
        {
            throw new NotImplementedException();
        }
    }
}
