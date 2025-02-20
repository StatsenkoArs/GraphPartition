namespace GraphRenumbering
{
    class CuthillMcKeeAlg
    {
        public PermutationStructure GetPermutation(int[][] graph, int s_v)
        {
            PermutationStructure vertex_perm = new PermutationStructure(graph.Length);
            var comp = Comparer<int>.Create((int x, int y) => Convert.ToInt32(graph[x].Length < graph[y].Length));
            int perm_pos = 0, perm_limit = 0, current_vertex;
            int[] tmp_vertexes = new int[graph.Length];
            //Добавляем первую вершину
            vertex_perm.ChangeByPos(FindRoot(graph, s_v), perm_limit);
            perm_limit++;

            while (perm_pos < graph.Length)
            {
                current_vertex = vertex_perm.GetNumByPos(perm_pos);
                perm_pos++;
                //Сортируем смежные вершины по возрастанию степеней
                graph[current_vertex].CopyTo(tmp_vertexes, 0);
                Array.Sort(tmp_vertexes, 0, graph[current_vertex].Length, comp);
                //Перебор смежных отсортировнных вершин
                for (int i = 0; i < graph[current_vertex].Length; i++)
                {
                    if (vertex_perm.GetPosByNum(tmp_vertexes[i]) >= perm_limit)
                    {
                        vertex_perm.ChangeByPos(vertex_perm.GetPosByNum(tmp_vertexes[i]), perm_limit);
                        perm_limit++;
                    }
                }
                //Если связная компонента графа закончилась
                if ((perm_pos == perm_limit || graph[current_vertex].Length == 0) && perm_limit < graph.Length)
                {
                    vertex_perm.ChangeByPos(vertex_perm.GetPosByNum(FindRoot(graph, vertex_perm.GetNumByPos(perm_pos))), perm_limit);
                    perm_limit++;
                }
            }
            return vertex_perm;
        }
        public int FindRoot(int[][] graph, int start_vertex) //Модифицированный алгоритм Гиббса
        {
            LevelStructCRS root_level_struct = BuildGraphLevel(graph, start_vertex);
            int root, x = start_vertex, max_level = 0, min_degree;

            while (root_level_struct.CountLevels - 1 > max_level)
            {
                root = x;
                max_level = root_level_struct.CountLevels - 1;
                min_degree = graph.Length;
                for (int i = 0; i < root_level_struct.GetNumVertexOnLevel(max_level); i++)
                {
                    if (graph[root_level_struct[max_level, i]].Length < min_degree)
                    {
                        min_degree = graph[root_level_struct[max_level, i]].Length;
                        x = root_level_struct[max_level, i];
                    }
                }
                root_level_struct = BuildGraphLevel(graph, x);
            }
            return x;
        }
        public LevelStructCRS BuildGraphLevel(int[][] graph, int root_vertex)
        {
            int[] level = new int[graph.Length];
            List<int> level_pos = new List<int>();
            bool[] visited = new bool[graph.Length];
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
                foreach (int vertex in graph[current_vertex])
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
