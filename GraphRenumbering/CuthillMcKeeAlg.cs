using GraphRepresentation;

namespace GraphRenumbering
{
    /// <summary>
    /// Класс, который реализует работу алгоиртма 
    /// Катхилл - Макки и поиска начального узла
    /// </summary>
    public class CuthillMcKeeAlg
    {
        /// <summary>
        /// По алгоритму Катхилл - Макки находи необходимую перенумерацию графа
        /// </summary>
        /// <param name="graph">Граф, для которого ищется новая нумерация</param>
        /// <param name="s_v">Вершина, с которой алгоритм начинает свою работу</param>
        /// <returns>Перестановка для перенумерации графа</returns>
        public PermutationStructure GetPermutation(IGraph graph, int s_v)
        {
            PermutationStructure vertex_perm = new PermutationStructure(graph.CountVertecies);
            var comp = Comparer<int>.Create((int x, int y) => Convert.ToInt32(graph.GetVertexDegree(x) < graph.GetVertexDegree(y)));
            int perm_pos = 0, perm_limit = 0, current_vertex;
            int[] tmp_vertexes = new int[graph.CountVertecies];
            //Добавляем первую вершину
            vertex_perm.ChangeByPos(FindRoot(graph, s_v), perm_limit);
            perm_limit++;

            while (perm_pos < graph.CountVertecies)
            {
                current_vertex = vertex_perm.GetNumByPos(perm_pos);
                perm_pos++;
                //Сортируем смежные вершины по возрастанию степеней
                graph.GetAdjacentVertecies(current_vertex).CopyTo(tmp_vertexes, 0);
                Array.Sort(tmp_vertexes, 0, graph.GetVertexDegree(current_vertex), comp);
                //Перебор смежных отсортировнных вершин
                for (int i = 0; i < graph.GetVertexDegree(current_vertex); i++)
                {
                    if (vertex_perm.GetPosByNum(tmp_vertexes[i]) >= perm_limit)
                    {
                        vertex_perm.ChangeByPos(vertex_perm.GetPosByNum(tmp_vertexes[i]), perm_limit);
                        perm_limit++;
                    }
                }
                //Если связная компонента графа закончилась
                if ((perm_pos == perm_limit || graph.GetVertexDegree(current_vertex) == 0) && perm_limit < graph.CountVertecies)
                {
                    vertex_perm.ChangeByPos(vertex_perm.GetPosByNum(FindRoot(graph, vertex_perm.GetNumByPos(perm_pos))), perm_limit);
                    perm_limit++;
                }
            }
            return vertex_perm;
        }
        /// <summary>
        /// Находит псевдопериферийную вершину-корень для связной компоненты графа, 
        /// в которую входит вершина start_vertex
        /// </summary>
        /// <param name="graph">Граф, для которого нужно найти псевдопериферийную вершину</param>
        /// <param name="start_vertex">Вершина, с которой начинается поиск</param>
        /// <returns>Номер псевдопериферийной вершины</returns>
        public int FindRoot(IGraph graph, int start_vertex) //Модифицированный алгоритм Гиббса
        {
            LevelStructCRS root_level_struct = BuildGraphLevel(graph, start_vertex);
            int root, x = start_vertex, max_level = 0, min_degree;

            while (root_level_struct.CountLevels - 1 > max_level)
            {
                root = x;
                max_level = root_level_struct.CountLevels - 1;
                min_degree = graph.CountVertecies;
                for (int i = 0; i < root_level_struct.GetNumVertexOnLevel(max_level); i++)
                {
                    if (graph.GetVertexDegree(root_level_struct[max_level, i]) < min_degree)
                    {
                        min_degree = graph.GetVertexDegree(root_level_struct[max_level, i]);
                        x = root_level_struct[max_level, i];
                    }
                }
                root_level_struct = BuildGraphLevel(graph, x);
            }
            return x;
        }
        /// <summary>
        /// Строит структуру уровней смежности графа для стартовой вершины
        /// </summary>
        /// <param name="graph">Граф, для которого нужно построить структуру
        /// уровней смежности
        /// </param>
        /// <param name="root_vertex">Корневая вершина для построения</param>
        /// <returns>Стурктура уровней смежности графа в формате CRS</returns>
        public LevelStructCRS BuildGraphLevel(IGraph graph, int root_vertex)
        {
            int[] level = new int[graph.CountVertecies];
            List<int> level_pos = new List<int>();
            bool[] visited = new bool[graph.CountVertecies];
            int max_pos = 0, pos = 0, current_vertex;

            visited[root_vertex] = true;
            level[max_pos] = root_vertex;
            level_pos.Add(max_pos);
            max_pos++;

            while (pos != max_pos)
            {
                if (pos == level_pos.Last())
                {
                    level_pos.Add(max_pos);
                }
                current_vertex = level[pos];
                pos++;
                foreach (int vertex in graph.GetAdjacentVertecies(current_vertex)) //Насколько оптимально тут использовать это функцию?
                {
                    if (visited[vertex] == false)
                    {
                        visited[vertex] = true;
                        level[max_pos] = vertex;
                        max_pos++;
                    }
                }
            }
            return new LevelStructCRS(level_pos.ToArray(), level);
        }
    }
}
