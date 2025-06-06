using GraphRepresentation;
using System.Security.AccessControl;

namespace GraphReduction
{
    public interface IPairStrategy
    {
        int PairVertex(IGraph graph, int cur_vertex, bool[] is_visited_vert);
    }

    public abstract class PairStrategy: IPairStrategy
    {
        protected int GetFirstIndFreeVert(IGraph graph, int curr_v, bool[] is_visited_vert)
        {
            for (int i_adj_v = 0; i_adj_v < graph.GetVertexDegree(curr_v); i_adj_v++)
            {
                int adj_vert = graph[curr_v, i_adj_v];
                if (is_visited_vert[adj_vert] == false)
                {
                    return i_adj_v;
                }
            }
            return -1;
        }
        public abstract int PairVertex(IGraph graph, int cur_vertex, bool[] is_visited_vert);
    }

    public class HeavyEdgePair : PairStrategy
    {
        public override int PairVertex(IGraph graph, int curr_v, bool[] is_visited_vert)
        {
            int start_v_ind = GetFirstIndFreeVert(graph, curr_v, is_visited_vert);

            if (start_v_ind == -1) return -1;

            int find_vert = graph[curr_v, start_v_ind];
            int min_vert_degree = graph.GetVertexDegree(find_vert);
            int max_edge_weight = graph.GetEdgeWeight(curr_v, find_vert);

            for (int i_adj_v = start_v_ind; i_adj_v < graph.GetVertexDegree(curr_v); i_adj_v++)
            {
                int adj_vert = graph[curr_v, i_adj_v];
                if (is_visited_vert[adj_vert] == true) continue;

                int edge_weight = graph.GetEdgeWeight(curr_v, adj_vert);
                int adj_vert_degree = graph.GetVertexDegree(adj_vert);
                //Ищем незанятую вершину с минимальной степенью и максимальным весом ребра
                if (edge_weight > max_edge_weight ||
                    edge_weight == max_edge_weight && adj_vert_degree < min_vert_degree)
                {
                    min_vert_degree = adj_vert_degree;
                    max_edge_weight = edge_weight;
                    find_vert = adj_vert;
                }
            }
            return find_vert;
        }
    }

    public class HeavyVertexPair : PairStrategy
    {
        public override int PairVertex(IGraph graph, int curr_v, bool[] is_visited_vert)
        {
            int start_v_ind = GetFirstIndFreeVert(graph, curr_v, is_visited_vert);

            if (start_v_ind == -1) return -1;

            int find_vert = graph[curr_v, start_v_ind];
            int min_vert_degree = graph.GetVertexDegree(find_vert);
            int min_vert_weight = graph.GetVertexWeight(find_vert);

            for (int i_adj_v = start_v_ind; i_adj_v < graph.GetVertexDegree(curr_v); i_adj_v++)
            {
                int adj_vert = graph[curr_v, i_adj_v];
                if (is_visited_vert[adj_vert] == true) continue;

                int vert_weight = graph.GetVertexWeight(curr_v);
                int adj_vert_degree = graph.GetVertexDegree(adj_vert);
                //Ищем незанятую вершину с минимальной степенью и минимальным весом
                if (vert_weight < min_vert_weight ||
                    vert_weight == min_vert_weight && adj_vert_degree < min_vert_degree)
                {
                    min_vert_degree = adj_vert_degree;
                    min_vert_weight = vert_weight;
                    find_vert = adj_vert;
                }
            }
            return find_vert;
        }
    }

    public class RandomVertexPair : PairStrategy
    {
        Random _rnd;
        public RandomVertexPair()
        {
            _rnd = new Random();
        }
        public override int PairVertex(IGraph graph, int cur_vertex, bool[] is_visited_vert)
        {
            int[] free_adj_vertex = new int[graph.CountVertecies];
            int count_free_vertex = 0;
            for (int i_adj_v = 0; i_adj_v < graph.GetVertexDegree(cur_vertex); i_adj_v++)
            {
                int vertex = graph[cur_vertex, i_adj_v];
                if (is_visited_vert[vertex] == false)
                {
                    free_adj_vertex[count_free_vertex] = vertex;
                    count_free_vertex++;
                }                
            }
            return free_adj_vertex[_rnd.Next(0, count_free_vertex)];
        }
    }
}
