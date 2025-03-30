using ExampleGenerator;
using GraphRepresentation;
using System.Drawing;

namespace TestsForAlgorithm.GeneratorTest
{
    [TestClass]
    public class GridGraphGenratorTests
    {
        private static bool JaggedArrayAssert (int[][] array, int[][] expected_array)
        {
            //Да, мне пришлось писать этот код, так как MSTest не умеет
            //проверять зубачатые массивы.
            bool flag_assert = true;
            if (expected_array.Length != array.Length) flag_assert = false;
            else
            {
                for (int i = 0; i < array.Length && flag_assert; i++)
                {
                    if (expected_array[i].Length != array[i].Length)
                    {
                        flag_assert = false;
                        break;
                    }
                    for (int j = 0; j < array[i].Length; j++)
                    {
                        if (expected_array[i][j] != array[i][j])
                        {
                            flag_assert = false;
                        }
                    }
                }
            }
            return flag_assert;
        }
        [TestMethod]
        public void GetExpandedMatrix_CreateMatrixSize5to7_MatrixSize7To9()
        {
            int num_of_row = 5;
            int num_of_col = 7;
            int[,] matrix = GridGraphGenerator.GetExpandedMatrix(num_of_row, num_of_col);

            int expected_num_of_row = 7;
            int expected_num_of_col = 9;

            Assert.IsTrue(matrix.GetLength(0) == expected_num_of_row && matrix.GetLength(1) == expected_num_of_col);
        }

        [TestMethod]
        public void GetExpandedMatrix_CreateMatrixSize2to3_MatrixSize4To5FillZero()
        {
            int num_of_row = 2;
            int num_of_col = 3;
            int[,] matrix = GridGraphGenerator.GetExpandedMatrix(num_of_row, num_of_col);

            int[,] expected_matrix = {{0, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 0},
                                      {0, 0, 0, 0, 0}};

            CollectionAssert.AreEqual(matrix, expected_matrix);
        }

        [TestMethod]
        public void FillExpandedMatrixBorder_AddMinus1BorderToMatrix_MatrixWithMinus1Border()
        {
            int num_of_row = 2;
            int num_of_col = 3;
            int[,] matrix = GridGraphGenerator.GetExpandedMatrix(num_of_row, num_of_col);
            GridGraphGenerator.FillExpandedMatrixBorder(matrix);

            int[,] expected_matrix = {{-1, -1, -1, -1, -1},
                                      {-1,  0,  0,  0, -1},
                                      {-1,  0,  0,  0, -1},
                                      {-1, -1, -1, -1, -1}};

            CollectionAssert.AreEqual(matrix, expected_matrix);
        }

        [TestMethod]
        public void AddRectangleHoleMatrix_AddCenterHole_MatrixWithOneCenterHoleMinus1()
        {
            int num_of_row = 2;
            int num_of_col = 3;
            int[,] matrix = GridGraphGenerator.GetExpandedMatrix(num_of_row, num_of_col);
            GridGraphGenerator.AddRectangleHoleMatrix(matrix, [new Point(1, 1)], [new Point(2, 3)]);

            int[,] expected_matrix = {{0,  0,  0,  0,  0},
                                      {0, -1, -1, -1,  0},
                                      {0, -1, -1, -1,  0},
                                      {0,  0,  0,  0,  0}};

            CollectionAssert.AreEqual(matrix, expected_matrix);
        }

        [TestMethod]
        public void AddRectangleHoleMatrix_AddTwoHole_MatrixWithTwoHoleMinus1()
        {
            int num_of_row = 4;
            int num_of_col = 5;
            int[,] matrix = GridGraphGenerator.GetExpandedMatrix(num_of_row, num_of_col);
            GridGraphGenerator.AddRectangleHoleMatrix(matrix, [new Point(1, 1), new Point(4, 4)],
                                                              [new Point(2, 2), new Point(5, 6)]);

            int[,] expected_matrix = {{0,  0,  0,  0,  0,  0,  0},
                                      {0, -1, -1,  0,  0,  0,  0},
                                      {0, -1, -1,  0,  0,  0,  0},
                                      {0,  0,  0,  0,  0,  0,  0},
                                      {0,  0,  0,  0, -1, -1, -1},
                                      {0,  0,  0,  0, -1, -1, -1}};

            CollectionAssert.AreEqual(matrix, expected_matrix);
        }

        [TestMethod]
        public void NumberExpandedMatrixWithHole_NumberWithTwoHole_MatrixWithNumner()
        {
            int num_of_row = 6;
            int num_of_col = 7;
            int[,] matrix = GridGraphGenerator.GetExpandedMatrix(num_of_row, num_of_col);
            GridGraphGenerator.FillExpandedMatrixBorder(matrix);
            GridGraphGenerator.AddRectangleHoleMatrix(matrix, [new Point(2, 2), new Point(5, 5)],
                                                              [new Point(3, 3), new Point(6, 7)]);
            GridGraphGenerator.NumberExpandedMatrixWithHole(matrix);

            int[,] expected_matrix = {{-1, -1, -1, -1, -1, -1, -1, -1, -1},
                                      {-1,  0,  1,  2,  3,  4,  5,  6, -1},
                                      {-1,  7, -1, -1,  8,  9, 10, 11, -1},
                                      {-1, 12, -1, -1, 13, 14, 15, 16, -1},
                                      {-1, 17, 18, 19, 20, 21, 22, 23, -1},
                                      {-1, 24, 25, 26, 27, -1, -1, -1, -1},
                                      {-1, 28, 29, 30, 31, -1, -1, -1, -1},
                                      {-1, -1, -1, -1, -1, -1, -1, -1, -1}};

            CollectionAssert.AreEqual(matrix, expected_matrix);
        }

        [TestMethod]
        public void GetGraphFromMatrix_GenerateGrphWith8Vertexes_GrphWith8Vertexes()
        {
            int num_of_row = 3;
            int num_of_col = 3;
            int[,] matrix = GridGraphGenerator.GetExpandedMatrix(num_of_row, num_of_col);
            GridGraphGenerator.FillExpandedMatrixBorder(matrix);
            GridGraphGenerator.AddRectangleHoleMatrix(matrix, [new Point(2, 2)],
                                                              [new Point(2, 2)]);
            GridGraphGenerator.NumberExpandedMatrixWithHole(matrix);
            int[][] graph = GridGraphGenerator.GetGraphFromMatrix(matrix);

            int[][] expected_graph = {[1,3],
                                      [0,2],
                                      [1,4],
                                      [0,5],
                                      [2,7],
                                      [3,6],
                                      [5,7],
                                      [4,6]};

            Assert.IsTrue(JaggedArrayAssert(graph, expected_graph));
        }

        [TestMethod]
        public void GenerateWithHole_GenerateGrphWith8Vertexes_GrphWith8Vertexes()
        {
            int num_of_row = 3;
            int num_of_col = 3;
            Point[] vertexes_coord;
            int[][] graph = GridGraphGenerator.GenerateWithHole(num_of_row, num_of_col, [new Point(2, 2)],
                                                                                        [new Point(2, 2)],
                                                                                        out vertexes_coord);

            int[][] expected_graph = {[1,3],
                                      [0,2],
                                      [1,4],
                                      [0,5],
                                      [2,7],
                                      [3,6],
                                      [5,7],
                                      [4,6]};

            Assert.IsTrue(JaggedArrayAssert(graph, expected_graph));
        }

        [TestMethod]
        public void GenerateWithHole_GenerateGrphWith8Vertexes_CoordForVertexes()
        {
            int num_of_row = 3;
            int num_of_col = 3;
            Point[] vertexes_coord;
            int[][] graph = GridGraphGenerator.GenerateWithHole(num_of_row, num_of_col, [new Point(2, 2)],
                                                                                        [new Point(2, 2)],
                                                                                        out vertexes_coord);

            Point[] expected_coord = {new(0,0),
                                      new(0,1),
                                      new(0,2),
                                      new(1,0),
                                      new(1,2),
                                      new(2,0),
                                      new(2,1),
                                      new(2,2)};

            CollectionAssert.AreEqual(vertexes_coord, expected_coord);
        }
    }
}
