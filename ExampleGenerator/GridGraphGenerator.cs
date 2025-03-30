using System.Drawing;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.ExceptionServices;

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
            int[] tmp_adj_vertex = new int[4];
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
                    if (c + 1 != num_of_col)
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

        public static int[,] GetExpandedMatrix(int num_of_row, int num_of_col)
        {
            return new int[num_of_row + 2, num_of_col + 2];
        }
        public static void FillExpandedMatrixBorder(int[,] matrix)
        {
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                matrix[r, 0] = -1;
                matrix[r, matrix.GetLength(1) - 1] = -1;
            }
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                matrix[0, c] = -1;
                matrix[matrix.GetLength(0) - 1, c] = -1;
            }
        }
        public static void AddRectangleHoleMatrix(int[,] matrix, 
                                                  Point[] start_rect_point, 
                                                  Point[] end_rect_point)
        {
            if (start_rect_point.Length != end_rect_point.Length) throw new Exception("start_rect_point.Length != end_rect_point.Length");
            for (int i = 0; i < start_rect_point.Length && i < end_rect_point.Length; i++)
            {
                for (int r = start_rect_point[i].X; r <= end_rect_point[i].X; r++)
                {
                    for (int c = start_rect_point[i].Y; c <= end_rect_point[i].Y; c++)
                    {
                        matrix[r, c] = -1;
                    }
                } 
            }
        }
        public static void NumberExpandedMatrixWithHole(int [,] matrix)
        {
            int count = 0;
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] != -1) matrix[r, c] = count++;
                }
            }
        }

        public static int[][] GetGraphFromMatrix(int [,] matrix)
        {
            Point[] directions = {new(-1, 0), new(0, -1), new(0, 1), new(1, 0)};
            List<int> vertexes = new List<int>();
            
            int last_number_vetex = matrix[matrix.GetLength(0) - 2, matrix.GetLength(1) - 2];
            int[][] graph = new int[last_number_vetex + 1][];
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] == -1) continue;
                    foreach (Point d in directions)
                    {
                        int adj_vertexes = matrix[r + d.X, c + d.Y];
                        if (adj_vertexes != -1) vertexes.Add(adj_vertexes);
                    }
                    graph[matrix[r, c]] = new int[vertexes.Count];
                    for (int i = 0; i < vertexes.Count; i++)
                    {
                        graph[matrix[r, c]][i] = vertexes[i];
                    }
                    vertexes.Clear();   
                }
            }
            return graph;
        }

        public static Point[] GetVertexCoordFromMatrix(int[,] matrix)
        {
            Point[] vertexes_coord = new Point[matrix[matrix.GetLength(0) - 2, matrix.GetLength(1) - 2] + 1];
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] == -1) continue;
                    vertexes_coord[matrix[r, c]] = new Point(r - 1, c - 1);
                }
            }
            return vertexes_coord;
        }
        public static int[][] GenerateWithHole(int num_of_row, int num_of_col, Point[] start_rect_point,
                                                                               Point[] end_rect_point,
                                                                               out Point[] vertexes_coord)
        {
            int[,] matrix_graph = GetExpandedMatrix(num_of_row, num_of_col);
            FillExpandedMatrixBorder(matrix_graph);
            AddRectangleHoleMatrix(matrix_graph, start_rect_point, end_rect_point);
            NumberExpandedMatrixWithHole(matrix_graph);
            vertexes_coord = GetVertexCoordFromMatrix(matrix_graph);
            return GetGraphFromMatrix(matrix_graph);
        }
    }
}
