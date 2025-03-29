namespace ExampleGenerator
{
    /// <summary>
    /// Класс для генератора графа на сетке
    /// </summary>
    public class GridGraphGenerator
    {
        /// <summary>
        /// Создаёт граф на сетке
        /// </summary>
        /// <param name="num_of_row">Кол-во строк</param>
        /// <param name="num_of_col">Кол-во столбцов</param>
        /// <returns>Готовый граф</returns>
        public static int[][] Generate(int num_of_row, int num_of_col)
        {
            int[][] graph = new int[num_of_row * num_of_col][];
            int[] tmp_adj_vertex =  new int[4];
            int vertex_num, vertex_degree;
            for (int r = 0; r < num_of_row; r++)
            {
                for (int c = 0; c < num_of_col; c++)
                {
                    //Вычисляем номер очередной вершины
                    vertex_num = r * num_of_col + c;
                    vertex_degree = 0;
                    //Связь с вершиной сверху
                    if (r != 0)
                    {
                        tmp_adj_vertex[vertex_degree] = vertex_num - num_of_col;
                        vertex_degree++;
                    }
                    //Связь с вершиной слева
                    if (c != 0)
                    {
                        tmp_adj_vertex[vertex_degree] = vertex_num - 1;
                        vertex_degree++;
                    }
                    //Связь с вершиной справа
                    if (c  + 1 != num_of_col) 
                    {
                        tmp_adj_vertex[vertex_degree] = vertex_num + 1;
                        vertex_degree++;
                    }
                    //Связь с вершиной снизу
                    if (r + 1 != num_of_row)
                    {
                        tmp_adj_vertex[vertex_degree] = vertex_num + num_of_col;
                        vertex_degree++;
                    }
                    //Переносим все нужные рёбра
                    graph[vertex_num] = new int[vertex_degree];
                    for (int i = 0; i < vertex_degree; i++)
                    {
                        graph[vertex_num][i] = tmp_adj_vertex[i];
                    }
                }
            }
            return graph;
        }
    }
}
