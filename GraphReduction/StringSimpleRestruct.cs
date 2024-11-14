using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public class StringSimpleRestruct: IRestruct
    {
        private int[][] new_graph;

        public StringSimpleRestruct()
        {
            new_graph = Array.Empty<int[]>();
        }

        public int[][] Restruct(int[][] graph, in int[] vertex_mapping, int group)
        {
            int[] restruct_string = new int[group];
            List<int> tmp_row_graph = new List<int>();
            List <int>[] tmp_new_graph = new  List<int>[group];
            for (int i = 0; i < group; i++)
            {
                tmp_new_graph[i] = new List<int>();
            }
            new_graph = new int[group][];
            for (int i = 0; i < graph.Length; i++)
            {

                for (int k = 0; k < group; k++)
                {
                    restruct_string[k] = 0;
                }
                for (int j = 0; j < graph[i].Length; j++)
                {
                    restruct_string[vertex_mapping[graph[i][j]]]++;
                }
                for (int j = 0; j < restruct_string.Length; j++)
                {
                    if (vertex_mapping[i] == j)
                    {

                    }
                    else if (restruct_string[j] != 0)
                    {
                        if (tmp_new_graph[vertex_mapping[i]].Contains(j) == false)
                        {
                            tmp_new_graph[vertex_mapping[i]].Add(j);
                        }

                    }
                }
            }
            for (int i = 0;i < tmp_new_graph.Length; i++)
            {
                new_graph[i] = tmp_new_graph[i].ToArray();
            }
            return new_graph;
        }
    }
}
