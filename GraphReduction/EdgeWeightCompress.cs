using GraphRepresentation;
using System.Numerics;

namespace GraphReduction
{
    public class EdgeWeightCompress : ICompress
    {
        int _group;
        public int[] Compress(IGraph graph, int ads)
        {
            int[] rand_vert_traversal = new int[graph.CountVertecies];
            bool[] is_visited_vert = new bool[graph.CountVertecies];
            int[] vert_mapping = new int[graph.CountVertecies];

            for (int i = 0; i < graph.CountVertecies; i++)
            {
                rand_vert_traversal[i] = i;
            }
            Random.Shared.Shuffle(rand_vert_traversal);

            int group_count = 0;
            for (int i = 0; i < rand_vert_traversal.Length; i++)
            {
                int curr_v = rand_vert_traversal[i];
                if (is_visited_vert[curr_v] == true) continue;

                is_visited_vert[curr_v] = true;
                vert_mapping[curr_v] = group_count;

                int find_vert = -1;
                int min_vert_degree = 0;
                int max_edge_weight = 0;
                int count_start = -1;
                for (int i_adj_v = 0; i_adj_v < graph.GetVertexDegree(curr_v); i_adj_v++)
                {
                    int adj_vert = graph[curr_v, i_adj_v];
                    if (is_visited_vert[adj_vert] == false)
                    {
                        find_vert = adj_vert;
                        min_vert_degree = graph.GetVertexDegree(find_vert);
                        max_edge_weight = graph.GetEdgeWeight(curr_v, find_vert);
                        count_start = i_adj_v;
                        break;
                    }
                }

                if (count_start != -1)
                {
                    for (int i_adj_v = count_start; i_adj_v < graph.GetVertexDegree(curr_v); i_adj_v++)
                    {
                        int adj_vert = graph[curr_v, i_adj_v];
                        int edge_weight = graph.GetEdgeWeight(curr_v, adj_vert);
                        int adj_vert_degree = graph.GetVertexDegree(adj_vert);
                        //Ищем незанятую вершину с минимальной степенью и максимальным весом ребра
                        if (is_visited_vert[adj_vert] == false)
                        {
                            if (edge_weight > max_edge_weight ||
                                edge_weight == max_edge_weight && adj_vert_degree < min_vert_degree ||
                                adj_vert == graph[curr_v, 0])
                            {
                                min_vert_degree = adj_vert_degree;
                                max_edge_weight = edge_weight;
                                find_vert = adj_vert;
                            }
                        }
                    }
                }
                
                if (find_vert != -1)
                {
                    is_visited_vert[find_vert] = true;
                    vert_mapping[find_vert] = group_count;
                }
                group_count++;
            }
            _group = group_count - 1;
            return vert_mapping;
        }

        public int GetNumOfGroup()
        {
            return _group;
        }
    }
}