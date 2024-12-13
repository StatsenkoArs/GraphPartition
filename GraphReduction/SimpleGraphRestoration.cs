using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using GraphOptimisation;
using GraphRepresentation;

namespace GraphReduction
{
    public class SimpleGraphRestoration : IGraphRestoration
    {
        private Stack<int[]> mappings;
        private Stack<IGraph> graphs;
        private IPartitionOptimisation optimisator;
        public SimpleGraphRestoration(IPartitionOptimisation optimisator)
        {
            mappings = new Stack<int[]>();
        }

        public int[] Restore(int[] partition)
        {
            if (mappings.Count == 0)
            {
                return partition;
            }
            int[] tmp = StretchPartition(partition);
            //TODO: remove magic number '5' (maybe add confid to graphpartition class & optimisation algorithm)
            tmp = optimisator.OptimisePartition(graphs.Pop(), tmp, 5);
            return tmp;
        }

        public int[] StretchPartition(int[] partition)
        {
            int[] mapping = mappings.Pop();
            int[] tmp = new int[mapping.Length];
            for (int i = 0; i < mapping.Length; i++)
            {
                for (int j = 0; j < partition.Length; j++)
                {
                    if (mapping[i] == j)
                    {
                        tmp[i] = partition[j];
                    }
                }
            }
            return tmp;
        }
        public void SetMappingStorage(Stack<int[]> mappings)
        {
            this.mappings = mappings;
        }
        public void SetGraphStorage(Stack<IGraph> graph)
        {
            this.graphs = graph;
        }
    }
}
