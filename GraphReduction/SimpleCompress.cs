using GraphRepresentation;

namespace GraphReduction
{
    public class SimpleCompress : ICompress
    {
        private int _count_group;
        public int[] Compress(IGraph graph, int ratio)
        {
            int[] vertex_mapping = new int[graph.CountVertecies];
            bool[] graph_vertex_used = new bool[graph.CountVertecies];

            int[] rand_vert_traversal = new int[graph.CountVertecies];
            for (int i = 0; i < graph.CountVertecies; i++)
            {
                rand_vert_traversal[i] = i;
            }
            Random.Shared.Shuffle(rand_vert_traversal);
            int count_group = 0;
            for (int i = 0; i < rand_vert_traversal.Length; i++)
            {
                int vert = rand_vert_traversal[i];
                if (graph_vertex_used[vert] == true) continue;
                graph_vertex_used[vert] = true;
                vertex_mapping[vert] = count_group;
                int find_vert = -1;
                for (int i_adj_v = 0; i_adj_v < graph.GetVertexDegree(vert); i_adj_v++)
                {
                    int adj_vert = graph[vert, i_adj_v];
                    if (graph_vertex_used[adj_vert] == false)
                    {
                        find_vert = adj_vert;
                        break;
                    }
                }
                if (find_vert != -1)
                {
                    vertex_mapping[find_vert] = count_group;
                    graph_vertex_used[find_vert] = true;
                }
                count_group++;
            }
            _count_group = count_group - 1;
            return vertex_mapping;
        }

        public int GetNumOfGroup()
        {
            return _count_group;
        }
    }
}
