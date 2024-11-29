using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphPartitionAccurate
{
    public interface IAccuratePartition
    {
        int[] GetPartition(IGraph graph);
    }
}
