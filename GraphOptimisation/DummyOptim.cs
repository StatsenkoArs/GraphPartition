using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimisation
{
    public class DummyOptim : IPartitionOptimisation
    {
        public int[] OptimisePartition(IGraph graph, int[] partition, int numberOfBlockedIterations, int limitDisbalance)
        {
            return partition;
        }
    }
}
