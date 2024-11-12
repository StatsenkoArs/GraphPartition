using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public class SimpleCompress : ICompress
    {
        private int[] vertex_mapping;
        private bool[] graph_vertex_used;  //Убрать? Учитывать это по заполненности шифра?
        private int _group;
        public SimpleCompress()
        {
            vertex_mapping = Array.Empty<int>();
            graph_vertex_used = Array.Empty<bool>();
        }
        public int[] Compress(in int[][] graph)
        {
            vertex_mapping = new int[graph.Length];
            graph_vertex_used = new bool[graph.Length];
            for (int i = 0; i < graph.Length; i++)
            {
                graph_vertex_used[i] = false;
            }
            int group = 0;
            bool flag = false;
            for (int i = 0; i < graph.Count(); i++)
            {
                if (graph_vertex_used[i] == false)
                {
                    vertex_mapping[i] = group;
                    graph_vertex_used[i] = true;
                    flag = false;
                    for (int j = 0; j < graph[i].Count(); j++)
                    {
                        if (graph_vertex_used[graph[i][j]] == false)
                        {
                            vertex_mapping[graph[i][j]] = group;
                            graph_vertex_used[graph[i][j]] = true;
                            group++;
                            flag = true;
                            break;
                        }
                    }
                    if (flag == false)
                    {
                        group++;
                    }
                }
            }
            _group = group;
            return vertex_mapping;
        }

        public int GetNumOfGroup()
        {
            return _group;
        }
    }
}
