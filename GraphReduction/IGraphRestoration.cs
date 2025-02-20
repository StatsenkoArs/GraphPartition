using GraphRepresentation;

namespace GraphReduction
{
    public interface IGraphRestoration
    {
        int[] Restore(int[] partition);
        public void SetGraphStorage(Stack<IGraph> graph);
        void SetMappingStorage(Stack<int[]> mappings);
    }
}
