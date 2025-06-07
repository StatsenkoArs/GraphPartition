using GraphRepresentation;

namespace GraphReduction
{
    public class SimpleGraphReduction : IGraphReduction
    {
        IRestruct restruct;
        ICompress compress;
        int _compress_ratio;
        int[] _mapping;
        public SimpleGraphReduction(IRestruct restruct, ICompress compress, int compress_ratio)
        {
            this.restruct = restruct;
            this.compress = compress;
            _compress_ratio = compress_ratio;
        }
        public IGraph Reduct(IGraph graph)
        {
            _mapping = compress.Compress(graph, _compress_ratio);
            return restruct.Restruct(graph, _mapping, compress.GetNumOfGroup());
        }
        public int[] GetLastMapping()
        {
            return _mapping;
        }
    }
}
