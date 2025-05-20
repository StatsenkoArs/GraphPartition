using GraphRepresentation;
using System.Collections;
using System.Net.Http.Headers;

namespace GraphReduction
{
    public class WeightCompress : ICompress
    {
        private int[] vertex_mapping;
        private bool[] graph_vertex_used;  //Убрать? Учитывать это по заполненности шифра?
        private int _group;
        public WeightCompress()
        {
            vertex_mapping = Array.Empty<int>();
            graph_vertex_used = Array.Empty<bool>();
        }
        public int[] Compress(IGraph graph)
        {
            vertex_mapping = new int[graph.CountVertecies];
            graph_vertex_used = new bool[graph.CountVertecies];
            int group = 0;
            bool is_paring = true;
            int min_weight_vert, min_vert;
            for (int vert = 0; vert < graph.CountVertecies; vert++)
            {
                if (is_paring == false)
                {
                    group++;
                    is_paring = true;
                }
                if (graph_vertex_used[vert] == false)
                {
                    vertex_mapping[vert] = group;
                    graph_vertex_used[vert] = true;
                    is_paring = false;
                    min_vert = 0;
                    min_weight_vert = graph.GetVertexWeight(graph[vert, min_vert]);
                    //Поиск веришны с минимальным весом для стягивания
                    for (int adj_vert = 0; adj_vert < graph.GetVertexDegree(vert); adj_vert++)
                    {
                        if (graph_vertex_used[graph[vert, adj_vert]] == false &&
                            graph.GetVertexWeight(graph[vert, adj_vert]) < min_weight_vert)
                        {
                            min_weight_vert = graph.GetVertexWeight(graph[vert, adj_vert]);
                            min_vert = adj_vert;
                        }
                    }
                    if (graph_vertex_used[graph[vert, min_vert]] == false)
                    {
                        vertex_mapping[graph[vert, min_vert]] = group;
                        graph_vertex_used[graph[vert, min_vert]] = true;
                        group++;
                        is_paring = true;
                    }
                }
            }
            _group = group;
            return vertex_mapping;
        }

        public int GetNumOfGroup()
        {
            throw new NotImplementedException();
        }
    }
}
