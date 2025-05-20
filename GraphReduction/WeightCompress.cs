using GraphRepresentation;
using System.Collections;
using System.Net.Http.Headers;

namespace GraphReduction
{
    class WeightCompress : ICompress
    {
        private int[] vertex_mapping;
        private bool[] graph_vertex_used;  //Убрать? Учитывать это по заполненности шифра?
        private int _group;
        private Stack<int> vertex_stack;
        public WeightCompress()
        {
            vertex_mapping = Array.Empty<int>();
            graph_vertex_used = Array.Empty<bool>();
            vertex_stack = new Stack<int>();
        }
        public int[] Compress(IGraph graph)
        {
            vertex_mapping = new int[graph.CountVertecies];
            graph_vertex_used = new bool[graph.CountVertecies];
            int group = 0;
            bool flag = true;
            int current_vertex = 0;
            for (int i = 0; i < graph.CountVertecies; i++)
            {
                if (flag == false)
                {
                    group++;
                    flag = true;
                }
                if (graph_vertex_used[i] == false)
                {
                    vertex_mapping[i] = group;
                    graph_vertex_used[i] = true;
                    flag = false;
                    for (int j = 0; j < graph.GetVertexDegree(i); j++)
                    {
                        if (graph_vertex_used[graph[i, j]] == false)
                        {
                            vertex_mapping[graph[i, j]] = group;
                            graph_vertex_used[graph[i, j]] = true;
                            group++;
                            flag = true;
                            break;
                        }
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
