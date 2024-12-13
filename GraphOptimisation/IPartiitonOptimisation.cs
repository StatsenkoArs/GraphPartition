using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphOptimisation
{
    public interface IPartitionOptimisation
    {
        int[] OptimisePartition(IGraph graph, int[] partition, int numberOfBlockedIterations);
            // Начальное кол-во итераций, когда вершину двигать нельзя (Обычно =1)
    }
}
