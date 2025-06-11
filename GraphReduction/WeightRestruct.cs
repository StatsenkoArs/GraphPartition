using GraphRepresentation;

namespace GraphReduction
{
    public class WeightRestruct : IRestruct
    {
        public IGraph Restruct(IGraph graph, in int[] vertex_mapping, int group)
        {
            int[][] new_graph = new int[group + 1][];
            List<int> tmp_row_graph = new List<int>();
            int[][] new_edge_weight = new int[group + 1][];
            List<int> tmp_row_edge_weight = new List<int>();
            int[] new_vert_weight = new int[group + 1];

            int[,] new_graph_matrix = GetTransitionMatrix(graph, in vertex_mapping, group);
            for (int i = 0; i < new_graph_matrix.GetLength(0); i++)
            {
                tmp_row_graph.Clear();
                tmp_row_edge_weight.Clear();
                for (int j = 0; j < new_graph_matrix.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        new_vert_weight[i] = new_graph_matrix[i, j];
                    }
                    else if (new_graph_matrix[i, j] != 0)
                    {
                        tmp_row_graph.Add(j);
                        tmp_row_edge_weight.Add(new_graph_matrix[i, j]);
                    }
                }
                new_graph[i] = tmp_row_graph.ToArray();
                new_edge_weight[i] = tmp_row_edge_weight.ToArray();
            }
            return new GraphCSRWeights(new_graph, new_vert_weight, new_edge_weight);
        }
        private int[,] GetTransitionMatrix(IGraph graph, in int[] vertex_mapping, int group)
        {
            int[,] new_graph_matrix = new int[group + 1, group + 1];
            for (int vert = 0; vert < graph.CountVertecies; vert++)
            {
                int m1 = vertex_mapping[vert];
                new_graph_matrix[m1, m1] += graph.GetVertexWeight(vert);
                for (int i_adj_v = 0; i_adj_v < graph.GetVertexDegree(vert); i_adj_v++)
                {
                    int adj_vert = graph[vert, i_adj_v];
                    int m2 = vertex_mapping[adj_vert];
                    if (m1!=m2)
                    {
                        new_graph_matrix[m1, m2] += graph.GetEdgeWeight(vert, adj_vert);
                    }
                }
            }
            return new_graph_matrix;
        }
    }
}
