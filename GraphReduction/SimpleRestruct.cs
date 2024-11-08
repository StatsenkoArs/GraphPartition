using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public class SimpleRestruct : IRestruct
    {
        private int[][] new_graph;

        public SimpleRestruct()
        {
            new_graph = Array.Empty<int[]>();
        }
        public int[][] GetGraph()
        {
            if (new_graph.Length != 0) return new_graph;
            else throw new Exception("GraphIsEmptyException");
        }

        public int[][] Restruct(int[][] graph, in int[] vertex_mapping, int group)
        {
            List<int> tmp_row_graph = new List<int>();
            new_graph = new int[group][];
            int[,] new_graph_matrix = GetTransitionMatrix(in graph, in vertex_mapping, group);
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
            return new_graph;
        }
        private int[,] GetTransitionMatrix(in int[][] graph, in int[] vertex_mapping, int group)
        {
            int[,] new_graph_matrix = new int[group, group];
            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph[i].Count(); j++)
                {
                    int m1 = vertex_mapping[i];
                    int m2 = vertex_mapping[graph[i][j]];
                    new_graph_matrix[m1, m2]++;
                }
            }
            return new_graph_matrix;
        }
    }
}
