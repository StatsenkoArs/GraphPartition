using GraphRepresentation;

namespace GraphReduction
{
    public class SimpleRestruct : IRestruct
    {
        public IGraph Restruct(IGraph graph, in int[] vertex_mapping, int group)
        {
            int[][] new_graph;
            List<int> tmp_row_graph = new List<int>();
            new_graph = new int[group + 1][];
            int[,] new_graph_matrix = GetTransitionMatrix(graph, in vertex_mapping, group);
            for (int i = 0; i < new_graph_matrix.GetLength(0); i++)
            {
                tmp_row_graph.Clear();
                for (int j = 0; j < new_graph_matrix.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        //Тоже нужно будет учитывать потом
                    }
                    else if (new_graph_matrix[i, j] != 0)
                    {
                        tmp_row_graph.Add(j);
                    }
                    new_graph[i] = tmp_row_graph.ToArray();
                }
            }
            return new GraphSRC(new_graph); //Не лучший момент, много переконвертаций.
        }
        private int[,] GetTransitionMatrix(IGraph graph, in int[] vertex_mapping, int group)
        {
            int[,] new_graph_matrix = new int[group + 1, group + 1];
            for (int i = 0; i < graph.CountVertecies; i++)
            {
                for (int j = 0; j < graph.GetVertexDegree(i); j++)
                {
                    int m1 = vertex_mapping[i];
                    int m2 = vertex_mapping[graph[i,j]];
                    new_graph_matrix[m1, m2]++;
                }
            }
            return new_graph_matrix;
        }
    }
}
