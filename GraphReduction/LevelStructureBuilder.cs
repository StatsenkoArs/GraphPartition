using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public class LevelStructureBuilder
    {
        public LevelStructureBuilder() { }
        public int[] BuildLevelStructure(IGraph graph, int start_vertex)
        {
            bool[] is_use = new bool[graph.CountVertecies]; //костыль?
            int[] level_structure = new int[graph.CountVertecies]; 
            int[] max_vertex_in_level = new int[graph.CountVertecies];
            PermutationStructure vertex_perm = new PermutationStructure(graph.CountVertecies);
            Queue<int> vertex_queue = new Queue<int>();
            int divide_count = 0, level = 0, count_in_level = 0;

            vertex_queue.Enqueue(start_vertex); //как лучше выбирать первую вершину?
            max_vertex_in_level[level] = 1; 
            while (vertex_queue.Count != 0)
            {
                int cur_vertex = vertex_queue.Dequeue();
                level_structure[cur_vertex] = level;
                vertex_perm.Change(cur_vertex, divide_count);

                count_in_level++;
                if (count_in_level == max_vertex_in_level[level])
                {
                    level++;
                    count_in_level = 0;
                }
                divide_count++;

                for (int i = 0; i < graph.GetVertexDegree(cur_vertex); i++)
                {
                    int vertex = graph[cur_vertex, i];
                    if (vertex_perm.GetPosByNum(vertex) >= divide_count && is_use[vertex] == false)
                    {
                        vertex_queue.Enqueue(vertex);
                        is_use[vertex] = true;
                        if (count_in_level == 0)
                        {
                            max_vertex_in_level[level]++;
                        }
                        else
                        {
                            max_vertex_in_level[level + 1]++;
                        }
                        
                    }
                }
            }
            return level_structure;
        }
    }
}
