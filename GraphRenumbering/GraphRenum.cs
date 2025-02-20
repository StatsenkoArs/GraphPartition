using GraphRepresentation;

namespace GraphRenumbering
{
    public class GraphRenum
    {
        public IGraph RenumGraph(IGraph graph, PermutationStructure permutation)
        {
            int[][] renum_graph = new int[graph.CountVertecies][];
            for (int i = 0; i < graph.CountVertecies; i++)
            {
                int new_vert_i = permutation.GetNumByPos(i);
                renum_graph[new_vert_i] = new int[graph.GetVertexDegree(i)];
                for (int j = 0; j < graph.GetVertexDegree(i); j++)
                {
                    renum_graph[new_vert_i][j] = permutation.GetNumByPos(graph[i,j]); 
                }
            }
            return new GraphSRC(renum_graph);
        }
    }
}
