using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    internal class SimpleRestruct : IRestruct
    {
        private List<int>[] new_graph;

        public SimpleRestruct()
        {
            new_graph = Array.Empty<List<int>>();
        }
        public List<int>[] GetNewGraph()
        {
            if (new_graph.Length != 0) return new_graph;
            else throw new Exception("GraphIsEmptyException");
        }

        public void Restruct(ref List<int>[] graph, in int[] vertex_mapping, int group)
        {
            new_graph = new List<int>[group];
            for (int i = 0; i < group; i++)
            {
                new_graph[i] = new List<int>();
            }
            int[,] new_graph_matrix = GetTransitionMatrix(in graph, in vertex_mapping, group);
            for (int i = 0; i < new_graph_matrix.GetLength(0); i++)
            {
                for (int j = 0; j < new_graph_matrix.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        //Тоже нужно будет учитывать потом
                    }
                    else if (new_graph_matrix[i, j] != 0)
                    {
                        new_graph[i].Add(j);
                    }
                }
            }
        }
        private int[,] GetTransitionMatrix(in List<int>[] graph, in int[] vertex_mapping, int group)
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
