using GraphRepresentation;

namespace GraphReduction
{
    public interface IGraphTraversal
    {
        int[] GetGraphTraversal(IGraph graph);
    }

    public class RandGraphTraversal : IGraphTraversal
    {
        public int[] GetGraphTraversal(IGraph graph)
        {
            int[] rand_vert_traversal = new int[graph.CountVertecies];
            for (int i = 0; i < graph.CountVertecies; i++)
            {
                rand_vert_traversal[i] = i;
            }
            Random.Shared.Shuffle(rand_vert_traversal);
            return rand_vert_traversal;
        }
    }

    public class VertexWeightTraversal : IGraphTraversal
    {
        public int[] GetGraphTraversal(IGraph graph)
        {
            int[] vert_traversal = new int[graph.CountVertecies];
            int[] vert_weight = new int[graph.CountVertecies];
            for (int i = 0; i < graph.CountVertecies; i++)
            {
                vert_traversal[i] = i;
                vert_weight[i] = graph.GetVertexWeight(i);
            }
            Array.Sort(vert_weight, vert_traversal);
            return vert_traversal;
        }
    }
}
