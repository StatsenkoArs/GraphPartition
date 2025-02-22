using GraphRepresentation;

namespace GraphRenumbering
{
    /// <summary>
    /// Класс для перенумерации графа
    /// </summary>
    public class GraphRenum
    {
        /// <summary>
        /// По графу и перестановке вершин строит новый перенумерованный граф
        /// </summary>
        /// <param name="graph">Исходный граф</param>
        /// <param name="permutation">Перестановка вершин графа</param>
        /// <returns>Граф с новой нумераией</returns>
        public IGraph RenumGraph(IGraph graph, PermutationStructure permutation)
        {
            int[][] renum_graph = new int[graph.CountVertecies][];
            for (int i = 0; i < graph.CountVertecies; i++)
            {
                int new_vert_i = permutation.GetNumByPos(i);
                renum_graph[new_vert_i] = new int[graph.GetVertexDegree(i)];
                for (int j = 0; j < graph.GetVertexDegree(i); j++)
                {
                    renum_graph[new_vert_i][j] = permutation.GetNumByPos(graph[i,j]); 
                }
            }
            return new GraphSRC(renum_graph);
        }
    }
}
