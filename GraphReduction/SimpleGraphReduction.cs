using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public class SimpleGraphReduction : IGraphReduction
    {
        IRestruct restruct;
        ICompress compress;
        int[] _mapping;
        public SimpleGraphReduction(IRestruct restruct, ICompress compress)
        {
            this.restruct = restruct;
            this.compress = compress;
        }
        public IGraph Reduct(IGraph graph)
        {
            _mapping = compress.Compress(graph);
            return restruct.Restruct(graph, _mapping, compress.GetNumOfGroup());
        }
        public int[] GetLastMapping()
        {
            return _mapping;
        }
    }
}
