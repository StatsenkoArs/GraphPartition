using GraphPartitionAccurate;
using GraphRepresentation;

namespace TestsForAlgorithm.GraphPartitionAccurateTest
{
    [TestClass]
    public class AccurateTests
    {
        private double eps = 0.06;

        private TestContext testContext = null!;

        public TestContext TestContext
        {
            get { return testContext; }
            set { testContext = value; }
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_9Vertexes_BalancedPartition()
        {
            int[][] graphArray = [ [1, 3],
                                [0, 2, 3, 4],
                                [1, 4, 5],
                                [0, 1, 4, 6],
                                [1, 2, 3, 5, 6, 7],
                                [2, 4, 7, 8],
                                [3, 4, 7],
                                [4, 5, 6, 8],
                                [5, 7] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_10Vertexes_BalancedPartition()
        {
            int[][] graphArray = [ [ 3,  5,  6,  7 ],
                            [ 3,  4,  5,  7,  8,  9 ],
                            [ 4,  6,  9 ],
                            [ 0,  1,  4,  5 ],
                            [ 1,  2,  3,  5,  8 ],
                            [ 0,  1,  3,  4,  6,  8 ],
                            [ 0,  2,  5,  7 ],
                            [ 0,  1,  6 ],
                            [ 1,  4,  5 ],
                            [ 1,  2 ] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_10Vertexes2_BalancedPartition()
        {
            int[][] graphArray = [ [ 1,  3,  4],
                                [ 0,  2,  4],
                                [ 1,  4,  5],
                                [ 0,  4,  6],
                                [ 0,  1,  2,  3,  5,  6,  7,  8],
                                [ 2,  4,  8],
                                [ 3,  4,  7],
                                [ 4,  6,  8],
                                [ 4,  5,  7],
                                [ 6,  7] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_10Vertexes3_BalancedPartition()
        {
            int[][] graphArray = [ [ 1,  3,  4],
                                [ 0,  2,  4],
                                [ 1,  4,  5],
                                [ 0,  4,  6],
                                [ 0,  1,  2,  3,  5,  6,  7,  8],
                                [ 2,  4,  8],
                                [ 3,  4,  7, 9],
                                [ 4,  6,  8, 9],
                                [ 4,  5,  7],
                                [ 6,  7] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_15Vertexes_BalancedPartition()
        {
            int[][] graphArray = [ [ 8,  9,  13 ],
                                [ 4,  10 ],
                                [ 3,  7,  11,  12 ],
                                [ 2,  5,  6 ],
                                [ 1,  7,  8,  9,  10,  11 ],
                                [ 3,  8,  10,  14 ],
                                [ 3,  8,  11,  13,  14 ],
                                [ 2,  4,  11 ],
                                [ 0,  4,  5,  6,  12 ],
                                [ 0,  4,  11,  13,  14 ],
                                [ 1,  4,  5,  11,  13 ],
                                [ 2,  4,  6,  7,  9,  10 ],
                                [ 2,  8 ],
                                [ 0,  6,  9,  10 ],
                                [ 5,  6,  9 ] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_85Vertexes_BalancedPartition()
        {
            int[][] graphArray = [ [1, 9, 10],
                                [0, 2, 10, 11],
                                [1, 3, 11],
                                [2, 4, 11, 12, 13],
                                [3, 5, 13, 14],
                                [4, 6, 14],
                                [5, 7, 14, 15, 16],
                                [6, 8, 16, 17],
                                [7, 17],
                                [0, 10, 18],
                                [0, 1, 9, 11, 18, 19],
                                [1, 2, 3, 10, 12, 19, 20, 21],
                                [3, 11, 13, 21],
                                [3, 4, 12, 14, 21, 22],
                                [4, 5, 6, 13, 15, 22, 23],
                                [6, 14, 16, 23, 24],
                                [6, 7, 15, 17, 24, 25],
                                [7, 8, 16, 25, 26],
                                [9, 10, 19, 27],
                                [10, 11, 18, 20, 27, 28],
                                [11, 19, 21, 28, 29],
                                [11, 12, 13, 20, 22, 29, 30],
                                [13, 14, 21, 23, 30, 31],
                                [14, 15, 22, 24, 31, 32],
                                [15, 16, 23, 25, 32, 33],
                                [16, 17, 24, 26, 33, 34, 35],
                                [17, 25, 35],
                                [18, 19, 28, 36],
                                [19, 20, 27, 29, 36, 37, 38],
                                [20, 21, 28, 30, 38],
                                [21, 22, 29, 31, 38, 39],
                                [22, 23, 30, 32, 39, 40],
                                [23, 24, 31, 33, 40, 41],
                                [24, 25, 32, 34, 41, 42],
                                [25, 33, 35, 42, 43],
                                [25, 26, 34, 43, 44],
                                [27, 28, 37, 45],
                                [28, 36, 38, 45, 46, 47],
                                [28, 29, 30, 37, 39, 47, 48],
                                [30, 31, 38, 40, 48],
                                [31, 32, 39, 41, 48, 49],
                                [32, 33, 40, 42, 49, 50, 51],
                                [33, 34, 41, 43, 51],
                                [34, 35, 42, 44, 51, 52],
                                [35, 43, 52, 53],
                                [36, 37, 46, 54, 55],
                                [37, 45, 47, 55],
                                [37, 38, 46, 48, 55, 56],
                                [38, 39, 40, 47, 49, 56, 57],
                                [40, 41, 48, 50, 57, 58],
                                [41, 49, 51, 58, 59],
                                [41, 42, 43, 50, 52, 59, 60, 61],
                                [43, 44, 51, 53, 61],
                                [44, 52, 61, 62],
                                [45, 55, 63],
                                [45, 46, 47, 54, 56, 63, 64],
                                [47, 48, 55, 57, 64, 65, 66],
                                [48, 49, 56, 58, 66],
                                [49, 50, 57, 59, 66, 67],
                                [50, 51, 58, 60, 67, 68, 69],
                                [51, 59, 61, 69, 70],
                                [51, 52, 53, 60, 62, 70, 71],
                                [53, 61, 71],
                                [54, 55, 64, 72],
                                [55, 56, 63, 65, 72, 73],
                                [56, 64, 66, 73, 74, 75],
                                [56, 57, 58, 65, 67, 75],
                                [58, 59, 66, 68, 75, 76, 77],
                                [59, 67, 69, 77],
                                [59, 60, 68, 70, 77, 78],
                                [60, 61, 69, 71, 78, 79],
                                [61, 62, 70, 79, 80],
                                [63, 64, 73, 81, 82],
                                [64, 65, 72, 74, 82, 83],
                                [65, 73, 75, 83],
                                [65, 66, 67, 74, 76, 83, 84],
                                [67, 75, 77, 84],
                                [67, 68, 69, 76, 78],
                                [69, 70, 77, 79],
                                [70, 71, 78, 80],
                                [71, 79],
                                [72, 82],
                                [72, 73, 81, 83],
                                [73, 74, 75, 82, 84],
                                [75, 76, 83] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_97Vertexes_BalancedPartition()
        {
            int[][] graphArray = [  [1],
                                    [0, 2],
                                    [1, 3],
                                    [4, 2],
                                    [5, 6, 7, 3, 8],
                                    [4, 9],
                                    [10, 4, 11],
                                    [12, 4],
                                    [4, 13, 14],
                                    [5],
                                    [15, 6, 16, 17, 18, 19],
                                    [6],
                                    [20, 7],
                                    [21, 22, 8, 19],
                                    [23, 8],
                                    [10, 24, 25, 26, 27, 28],
                                    [10, 25, 29],
                                    [10, 30, 31],
                                    [10],
                                    [10, 13, 32, 31],
                                    [12, 33],
                                    [34, 13],
                                    [25, 35, 13, 36],
                                    [37, 14],
                                    [15, 38],
                                    [15, 16, 22, 39],
                                    [15, 40, 41, 42, 43, 44],
                                    [15, 46, 45],
                                    [15, 47, 48],
                                    [16, 49, 50],
                                    [51, 42, 17, 52, 53],
                                    [54, 17, 55, 19],
                                    [56, 19],
                                    [20, 38],
                                    [58, 57, 21],
                                    [51, 59, 22],
                                    [60, 22, 61],
                                    [23],
                                    [24, 33],
                                    [49, 25, 62],
                                    [44, 63, 26],
                                    [26, 62],
                                    [30, 26],
                                    [57, 26],
                                    [40, 64, 26],
                                    [46, 65, 66, 27],
                                    [45, 27],
                                    [28],
                                    [28],
                                    [29, 39],
                                    [54, 29],
                                    [67, 35, 30],
                                    [30],
                                    [30],
                                    [50, 68, 31],
                                    [31],
                                    [59, 69, 32],
                                    [60, 43, 34, 70],
                                    [34, 71],
                                    [56, 35],
                                    [57, 36],
                                    [72, 36],
                                    [41, 39],
                                    [40],
                                    [73, 44],
                                    [74, 45],
                                    [76, 45, 75],
                                    [51],
                                    [54],
                                    [56, 75],
                                    [57],
                                    [58],
                                    [61],
                                    [64],
                                    [65, 77, 78, 79],
                                    [80, 69, 66],
                                    [66],
                                    [74],
                                    [74],
                                    [74],
                                    [75],
                                    [],
                                    [],
                                    [],
                                    [],
                                    [],
                                    [],
                                    [],
                                    [],
                                    [90],
                                    [89, 91],
                                    [90],
                                    [],
                                    [],
                                    [95],
                                    [94],
                                    [] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_100Vertexes2_BalancedPartition()
        {
            int[][] graphArray = [ [],
                                [34, 86],
                                [36, 44, 55],
                                [58],
                                [42, 92],
                                [46, 48, 89],
                                [22, 27, 40, 65, 84, 87],
                                [22, 34, 66],
                                [21, 35, 42, 81],
                                [],
                                [],
                                [38, 53, 65],
                                [95],
                                [22, 40, 62],
                                [],
                                [41, 74],
                                [17, 47],
                                [16, 77],
                                [],
                                [65, 96],
                                [62, 79],
                                [8, 88],
                                [6, 7, 13, 33, 67, 98],
                                [],
                                [28, 38],
                                [76, 91],
                                [63, 73, 90],
                                [6, 30],
                                [24],
                                [99],
                                [27, 77],
                                [71, 84],
                                [55, 65],
                                [22, 55, 90],
                                [1, 7, 47, 93, 94],
                                [8, 65],
                                [2],
                                [55],
                                [11, 24, 65],
                                [],
                                [6, 13, 68, 79],
                                [15],
                                [4, 8, 60],
                                [70],
                                [2, 46, 68],
                                [49, 71],
                                [5, 44],
                                [16, 34],
                                [5, 99],
                                [45, 50, 51, 97],
                                [49],
                                [49],
                                [60, 68, 94, 98],
                                [11],
                                [],
                                [2, 32, 33, 37, 80],
                                [61],
                                [87],
                                [3, 71, 99],
                                [],
                                [42, 52],
                                [56],
                                [13, 20, 63],
                                [26, 62],
                                [],
                                [6, 11, 19, 32, 35, 38],
                                [7],
                                [22],
                                [40, 44, 52, 88],
                                [70, 94],
                                [43, 69],
                                [31, 45, 58, 84],
                                [],
                                [26],
                                [15, 93],
                                [],
                                [25],
                                [17, 30],
                                [],
                                [20, 40, 96],
                                [55],
                                [8],
                                [],
                                [87],
                                [6, 31, 71],
                                [90],
                                [1],
                                [6, 57, 83],
                                [21, 68, 95],
                                [5, 98],
                                [26, 33, 85, 98],
                                [25],
                                [4],
                                [34, 74],
                                [34, 52, 69],
                                [12, 88],
                                [19, 79],
                                [49],
                                [22, 52, 89, 90],
                                [29, 48, 58] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_101Vertexes_BalancedPartition()
        {
            int[][] graphArray = [ [1],
                                [0, 2, 12],
                                [1, 14],
                                [4, 14, 15],
                                [3, 5, 15, 16],
                                [4, 6, 16, 17],
                                [5, 7, 17, 18],
                                [6, 8, 19],
                                [7, 20],
                                [10, 20],
                                [9, 20],
                                [22],
                                [1, 22],
                                [14, 22],
                                [2, 3, 13],
                                [3, 4, 24],
                                [4, 5, 24, 25],
                                [5, 6, 25, 26],
                                [6, 19],
                                [7, 18, 20, 27, 28],
                                [8, 9, 10, 19, 28, 29],
                                [22, 31],
                                [11, 12, 13, 21, 32, 33],
                                [24, 33],
                                [15, 16, 23, 34],
                                [16, 17, 35, 36, 37],
                                [17, 37, 38],
                                [19, 38, 39],
                                [19, 20, 29, 39],
                                [20, 28, 30, 39],
                                [29, 40],
                                [21, 32, 41, 42],
                                [22, 31],
                                [22, 23, 34, 43],
                                [24, 33, 35, 44],
                                [25, 34, 36, 45, 46],
                                [25, 35, 37],
                                [25, 26, 36, 47, 55],
                                [26, 27, 39, 47],
                                [27, 28, 29, 38, 40, 47],
                                [30, 39, 48, 50],
                                [31, 52],
                                [31, 43],
                                [33, 42, 44, 52, 53],
                                [34, 43, 45],
                                [35, 44, 53],
                                [35, 54, 55],
                                [37, 38, 39, 48, 56, 57],
                                [40, 47, 49, 57],
                                [48, 50, 58, 59, 60],
                                [40, 49],
                                [52, 61],
                                [41, 43, 51, 62],
                                [43, 45, 54, 63, 64],
                                [46, 53, 55, 64, 65],
                                [37, 46, 54, 56],
                                [47, 55, 65, 68],
                                [47, 48, 58],
                                [49, 57, 59, 69],
                                [49, 58, 70],
                                [49, 70],
                                [51],
                                [52, 63, 71],
                                [53, 62, 72],
                                [53, 54, 73, 74],
                                [54, 56, 66, 75],
                                [65, 67],
                                [66, 68],
                                [56, 67, 76, 77, 79],
                                [58],
                                [59, 60, 80],
                                [62, 72, 81],
                                [63, 71, 82, 83],
                                [64, 84],
                                [64, 75, 84, 85],
                                [65, 74, 76],
                                [68, 75, 86],
                                [68, 78],
                                [77, 87, 88],
                                [68, 89],
                                [70, 90],
                                [71, 91],
                                [72, 92],
                                [72, 84],
                                [73, 74, 83, 93, 94],
                                [74, 86, 94],
                                [76, 85, 87, 95, 96],
                                [78, 86],
                                [78, 89, 97],
                                [79, 88],
                                [80, 98, 100],
                                [81],
                                [82, 93],
                                [84, 92],
                                [84, 85],
                                [86, 96],
                                [86, 95],
                                [88, 98],
                                [90, 97],
                                [100],
                                [90, 99] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_151Vertexes_BalancedPartition()
        {
            int[][] graphArray = [ [1],
                                [0, 2, 12],
                                [1, 14],
                                [4, 14, 15],
                                [3, 5, 15, 16],
                                [4, 6, 16, 17],
                                [5, 7, 17, 18],
                                [6, 8, 19],
                                [7, 20],
                                [10, 20],
                                [9, 20, 102],
                                [22],
                                [1, 22],
                                [14, 22],
                                [2, 3, 13, 23],
                                [3, 4, 24],
                                [4, 5, 24, 25],
                                [5, 6, 25, 26, 27],
                                [6, 19],
                                [7, 18, 20, 27, 28],
                                [8, 9, 10, 19, 28, 29, 102],
                                [22, 31],
                                [11, 12, 13, 21, 32, 33],
                                [14, 24, 33],
                                [15, 16, 23, 34],
                                [16, 17, 35, 36, 37],
                                [17, 37, 38],
                                [17, 19, 38, 39],
                                [19, 20, 29, 39],
                                [20, 28, 30, 39],
                                [29, 40, 50, 102, 104],
                                [21, 32, 41, 42],
                                [22, 31],
                                [22, 23, 34, 43],
                                [24, 33, 35, 44],
                                [25, 34, 36, 45, 46],
                                [25, 35, 37],
                                [25, 26, 36, 47, 55],
                                [26, 27, 39, 47],
                                [27, 28, 29, 38, 40, 47],
                                [30, 39, 48, 50],
                                [31, 52],
                                [31, 43],
                                [33, 42, 44, 52, 53],
                                [34, 43, 45],
                                [35, 44, 53],
                                [35, 54, 55],
                                [37, 38, 39, 48, 56, 57],
                                [40, 47, 49, 57],
                                [48, 50, 58, 59, 60],
                                [30, 40, 49, 104, 105],
                                [52, 61],
                                [41, 43, 51, 62],
                                [43, 45, 54, 63, 64],
                                [46, 53, 55, 64, 65],
                                [37, 46, 54, 56],
                                [47, 55, 65, 68],
                                [47, 48, 58],
                                [49, 57, 59, 69],
                                [49, 58, 70],
                                [49, 70, 105, 106],
                                [51],
                                [52, 63, 71],
                                [53, 62, 72],
                                [53, 54, 73, 74],
                                [54, 56, 66, 75],
                                [65, 67],
                                [66, 68],
                                [56, 67, 76, 77, 79],
                                [58, 70],
                                [59, 60, 69, 80],
                                [62, 72, 81],
                                [63, 71, 73, 82, 83],
                                [64, 72, 84],
                                [64, 75, 84, 85],
                                [65, 74, 76],
                                [68, 75, 86],
                                [68, 78],
                                [77, 87, 88],
                                [68, 80, 89],
                                [70, 79, 90, 106, 107],
                                [71, 91],
                                [72, 83, 92],
                                [72, 82, 84],
                                [73, 74, 83, 93, 94],
                                [74, 86, 94],
                                [76, 85, 87, 95, 96],
                                [78, 86, 96],
                                [78, 89, 97],
                                [79, 88],
                                [80, 98, 100, 108],
                                [81],
                                [82, 93],
                                [84, 92],
                                [84, 85],
                                [86, 96],
                                [86, 87, 95],
                                [88, 98],
                                [90, 97],
                                [100],
                                [90, 99, 109],
                                [102, 111],
                                [10, 20, 30, 101, 103],
                                [102, 104, 112],
                                [30, 50, 103, 113, 114],
                                [50, 60, 106, 114, 115],
                                [60, 80, 105, 107, 115, 116],
                                [80, 106, 117],
                                [90, 109, 117, 118],
                                [100, 108, 110, 118, 120],
                                [109, 120],
                                [101, 112],
                                [103, 111, 113, 122],
                                [104, 112, 114, 123],
                                [104, 105, 113, 115, 124],
                                [105, 106, 114, 116, 125],
                                [106, 115, 117, 126, 127],
                                [107, 108, 116, 127],
                                [108, 109, 128],
                                [128, 129, 130],
                                [109, 110, 130],
                                [122, 132],
                                [112, 121, 123],
                                [113, 122, 132, 133],
                                [114, 125, 133, 135],
                                [115, 124, 126],
                                [116, 125, 127, 135, 136],
                                [116, 117, 126, 136, 137],
                                [118, 119, 137, 139],
                                [119, 130, 139],
                                [119, 120, 129, 139],
                                [132],
                                [121, 123, 131, 141, 142, 143],
                                [123, 124, 143, 144],
                                [144],
                                [124, 126, 145],
                                [126, 127, 137, 145, 146],
                                [127, 128, 136, 138, 147],
                                [137, 139, 149],
                                [128, 129, 130, 138, 140],
                                [139, 149],
                                [132],
                                [132],
                                [132, 133],
                                [133, 134, 145],
                                [135, 136, 144],
                                [136, 147],
                                [137, 146, 148],
                                [147, 149],
                                [138, 140, 148, 150],
                                [149] ];
            IGraph graph = new GraphCSR(graphArray);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)ap.GetSolution().Item1.Sum() / ap.GetSolution().Item1.Length, 2));

            Assert.IsTrue(Math.Abs((double)x.Sum() / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        private int SubGraphWeight(int[] x, int[] weights)
        {
            int result = 0;
            for (int i = 0; i < x.Length; ++i)
            {
                result += x[i] * weights[i];
            }
            return result;
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_10VertexesWeighted_BalancedPartition()
        {
            int[][] graphArray = [ [ 5, 7 ],
                            [ 3, 4, 5 ],
                            [ 3, 4, 8 ],
                            [ 1, 2, 4, 6, 9 ],
                            [ 1, 2, 3 ],
                            [ 0, 1 ],
                            [ 3, 8 ],
                            [ 0, 9 ],
                            [ 2, 6 ],
                            [ 0, 3, 7 ] ];
            int[] vertexWeights = [3, 4, 3, 3, 4, 8, 1, 4, 3, 3];
            int[][] edgesWeights = [ [ 8, 1 ],
                            [ 10, 11, 3 ],
                            [ 4, 8, 6 ],
                            [ 10, 9, 4, 7, 2 ],
                            [ 11, 8, 4 ],
                            [ 8, 3 ],
                            [ 7, 3 ],
                            [ 1, 6 ],
                            [ 6, 3 ],
                            [ 7, 2, 6 ] ];
            IGraph graph = new GraphCSRWeights(graphArray, vertexWeights, edgesWeights);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)SubGraphWeight(x, vertexWeights) / graph.GraphWeight, 2));

            Assert.IsTrue(Math.Abs((double)SubGraphWeight(x, vertexWeights) / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_50VertexesWeighted_BalancedPartition()
        {
            // Список смежности - jagged array
            int[][] adjacencyList =
            [
                [1, 5, 12, 23], // вершина 0
                [0, 2, 8, 15], // вершина 1
                [1, 3, 9, 18], // вершина 2
                [2, 4, 7, 21], // вершина 3
                [3, 6, 11, 19], // вершина 4
                [0, 7, 14, 25], // вершина 5
                [4, 8, 16, 27], // вершина 6
                [3, 5, 13, 29], // вершина 7
                [1, 6, 17, 31], // вершина 8
                [2, 10, 20, 33], // вершина 9
                [9, 11, 22, 35], // вершина 10
                [4, 10, 24, 37], // вершина 11
                [0, 13, 26, 39], // вершина 12
                [7, 12, 28, 41], // вершина 13
                [5, 15, 30, 43], // вершина 14
                [1, 14, 32, 45], // вершина 15
                [6, 17, 34, 47], // вершина 16
                [8, 16, 36, 49], // вершина 17
                [2, 19, 38], // вершина 18
                [4, 18, 40], // вершина 19
                [9, 21, 42], // вершина 20
                [3, 20, 44], // вершина 21
                [10, 23, 46], // вершина 22
                [0, 22, 48], // вершина 23
                [11, 25], // вершина 24
                [5, 24], // вершина 25
                [12, 27], // вершина 26
                [6, 26], // вершина 27
                [13, 29], // вершина 28
                [7, 28], // вершина 29
                [14, 31], // вершина 30
                [8, 30], // вершина 31
                [15, 33], // вершина 32
                [9, 32], // вершина 33
                [16, 35], // вершина 34
                [10, 34], // вершина 35
                [17, 37], // вершина 36
                [11, 36], // вершина 37
                [18, 39], // вершина 38
                [12, 38], // вершина 39
                [19, 41], // вершина 40
                [13, 40], // вершина 41
                [20, 43], // вершина 42
                [14, 42], // вершина 43
                [21, 45], // вершина 44
                [15, 44], // вершина 45
                [22, 47], // вершина 46
                [16, 46], // вершина 47
                [23, 49], // вершина 48
                [17, 48] // вершина 49
            ];

            // Массив весов вершин
            int[] vertexWeights =
            [
                15, 23, 8, 31, 12, 19, 27, 5, 33, 17,
                22, 9, 28, 14, 25, 11, 29, 6, 20, 16,
                24, 13, 30, 7, 21, 18, 26, 10, 32, 4,
                35, 1, 37, 3, 39, 2, 41, 36, 43, 38,
                45, 40, 47, 42, 49, 44, 51, 46, 53, 48
            ];

            // Jagged array с весами рёбер (соответствует adjacencyList)
            int[][] edgeWeights =
            [
                [12, 18, 25, 31], // веса рёбер для вершины 0
                [12, 16, 22, 29], // веса рёбер для вершины 1
                [16, 14, 20, 27], // веса рёбер для вершины 2
                [14, 19, 23, 30], // веса рёбер для вершины 3
                [19, 17, 24, 28], // веса рёбер для вершины 4
                [18, 21, 26, 33], // веса рёбер для вершины 5
                [17, 22, 25, 32], // веса рёбер для вершины 6
                [23, 21, 24, 34], // веса рёбер для вершины 7
                [22, 17, 27, 35], // веса рёбер для вершины 8
                [20, 15, 26, 36], // веса рёбер для вершины 9
                [15, 18, 28, 37], // веса рёбер для вершины 10
                [24, 18, 29, 38], // веса рёбер для вершины 11
                [25, 20, 30, 39], // веса рёбер для вершины 12
                [24, 20, 31, 40], // веса рёбер для вершины 13
                [26, 29, 32, 41], // веса рёбер для вершины 14
                [29, 26, 33, 42], // веса рёбер для вершины 15
                [25, 27, 34, 43], // веса рёбер для вершины 16
                [27, 25, 35, 44], // веса рёбер для вершины 17
                [27, 21, 36], // веса рёбер для вершины 18
                [28, 21, 37], // веса рёбер для вершины 19
                [26, 23, 38], // веса рёбер для вершины 20
                [30, 23, 39], // веса рёбер для вершины 21
                [28, 31, 40], // веса рёбер для вершины 22
                [31, 28, 41], // веса рёбер для вершины 23
                [29, 33], // веса рёбер для вершины 24
                [33, 29], // веса рёбер для вершины 25
                [30, 32], // веса рёбер для вершины 26
                [32, 30], // веса рёбер для вершины 27
                [31, 34], // веса рёбер для вершины 28
                [34, 31], // веса рёбер для вершины 29
                [32, 35], // веса рёбер для вершины 30
                [35, 32], // веса рёбер для вершины 31
                [33, 36], // веса рёбер для вершины 32
                [36, 33], // веса рёбер для вершины 33
                [34, 37], // веса рёбер для вершины 34
                [37, 34], // веса рёбер для вершины 35
                [35, 38], // веса рёбер для вершины 36
                [38, 35], // веса рёбер для вершины 37
                [36, 39], // веса рёбер для вершины 38
                [39, 36], // веса рёбер для вершины 39
                [37, 40], // веса рёбер для вершины 40
                [40, 37], // веса рёбер для вершины 41
                [38, 41], // веса рёбер для вершины 42
                [41, 38], // веса рёбер для вершины 43
                [39, 42], // веса рёбер для вершины 44
                [42, 39], // веса рёбер для вершины 45
                [40, 43], // веса рёбер для вершины 46
                [43, 40], // веса рёбер для вершины 47
                [41, 44], // веса рёбер для вершины 48
                [44, 41] // веса рёбер для вершины 49
            ];

            IGraph graph = new GraphCSRWeights(adjacencyList, vertexWeights, edgeWeights);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)SubGraphWeight(x, vertexWeights) / graph.GraphWeight, 2));

            Assert.IsTrue(Math.Abs((double)SubGraphWeight(x, vertexWeights) / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }

        [TestMethod]
        [Timeout(600000)] // 10 мин
        public void BBAlgorithmGetPartition_100VertexesWeighted_BalancedPartition()
        {
            // Список смежности - jagged array

            int[][] adjacencyList =
            [
                [1, 5, 12, 23], // вершина 0
                [0, 2, 8, 15], // вершина 1
                [1, 3, 9, 18], // вершина 2
                [2, 4, 7, 21], // вершина 3
                [3, 6, 11, 19], // вершина 4
                [0, 7, 14, 25], // вершина 5
                [4, 8, 16, 27], // вершина 6
                [3, 5, 13, 29], // вершина 7
                [1, 6, 17, 31], // вершина 8
                [2, 10, 20, 33], // вершина 9
                [9, 11, 22, 35], // вершина 10
                [4, 10, 24, 37], // вершина 11
                [0, 13, 26, 39], // вершина 12
                [7, 12, 28, 41], // вершина 13
                [5, 15, 30, 43], // вершина 14
                [1, 14, 32, 45], // вершина 15
                [6, 17, 34, 47], // вершина 16
                [8, 16, 36, 49], // вершина 17
                [2, 19, 38, 51], // вершина 18
                [4, 18, 40, 53], // вершина 19
                [9, 21, 42, 55], // вершина 20
                [3, 20, 44, 57], // вершина 21
                [10, 23, 46, 59], // вершина 22
                [0, 22, 48, 61], // вершина 23
                [11, 25, 50, 63], // вершина 24
                [5, 24, 52, 65], // вершина 25
                [12, 27, 54, 67], // вершина 26
                [6, 26, 56, 69], // вершина 27
                [13, 29, 58, 71], // вершина 28
                [7, 28, 60, 73], // вершина 29
                [14, 31, 62, 75], // вершина 30
                [8, 30, 64, 77], // вершина 31
                [15, 33, 66, 79], // вершина 32
                [9, 32, 68, 81], // вершина 33
                [16, 35, 70, 83], // вершина 34
                [10, 34, 72, 85], // вершина 35
                [17, 37, 74, 87], // вершина 36
                [11, 36, 76, 89], // вершина 37
                [18, 39, 78, 91], // вершина 38
                [12, 38, 80, 93], // вершина 39
                [19, 41, 82, 95], // вершина 40
                [13, 40, 84, 97], // вершина 41
                [20, 43, 86, 99], // вершина 42
                [14, 42, 88], // вершина 43
                [21, 45, 90], // вершина 44
                [15, 44, 92], // вершина 45
                [22, 47, 94], // вершина 46
                [16, 46, 96], // вершина 47
                [23, 49, 98], // вершина 48
                [17, 48], // вершина 49
                [24, 51], // вершина 50
                [18, 50], // вершина 51
                [25, 53], // вершина 52
                [19, 52], // вершина 53
                [26, 55], // вершина 54
                [20, 54], // вершина 55
                [27, 57], // вершина 56
                [21, 56], // вершина 57
                [28, 59], // вершина 58
                [22, 58], // вершина 59
                [29, 61], // вершина 60
                [23, 60], // вершина 61
                [30, 63], // вершина 62
                [24, 62], // вершина 63
                [31, 65], // вершина 64
                [25, 64], // вершина 65
                [32, 67], // вершина 66
                [26, 66], // вершина 67
                [33, 69], // вершина 68
                [27, 68], // вершина 69
                [34, 71], // вершина 70
                [28, 70], // вершина 71
                [35, 73], // вершина 72
                [29, 72], // вершина 73
                [36, 75], // вершина 74
                [30, 74], // вершина 75
                [37, 77], // вершина 76
                [31, 76], // вершина 77
                [38, 79], // вершина 78
                [32, 78], // вершина 79
                [39, 81], // вершина 80
                [33, 80], // вершина 81
                [40, 83], // вершина 82
                [34, 82], // вершина 83
                [41, 85], // вершина 84
                [35, 84], // вершина 85
                [42, 87], // вершина 86
                [36, 86], // вершина 87
                [43, 89], // вершина 88
                [37, 88], // вершина 89
                [44, 91], // вершина 90
                [38, 90], // вершина 91
                [45, 93], // вершина 92
                [39, 92], // вершина 93
                [46, 95], // вершина 94
                [40, 94], // вершина 95
                [47, 97], // вершина 96
                [41, 96], // вершина 97
                [48, 99], // вершина 98
                [42, 98] // вершина 99
            ];

            // Массив весов вершин
            int[] vertexWeights =
            [
                15, 23, 8, 31, 12, 19, 27, 5, 33, 17,
                22, 9, 28, 14, 25, 11, 29, 6, 20, 16,
                24, 13, 30, 7, 21, 18, 26, 10, 32, 4,
                35, 1, 37, 3, 39, 2, 41, 36, 43, 38,
                45, 40, 47, 42, 49, 44, 51, 46, 53, 48,
                55, 50, 57, 52, 59, 54, 61, 56, 63, 58,
                65, 60, 67, 62, 69, 64, 71, 66, 73, 68,
                75, 70, 77, 72, 79, 74, 81, 76, 83, 78,
                85, 80, 87, 82, 89, 84, 91, 86, 93, 88,
                95, 90, 97, 92, 99, 94, 101, 96, 103, 98
            ];

            // Jagged array с весами рёбер (соответствует adjacencyList)
            int[][] edgeWeights =
            [
                [12, 18, 25, 31], // веса рёбер для вершины 0
                [12, 16, 22, 29], // веса рёбер для вершины 1
                [16, 14, 20, 27], // веса рёбер для вершины 2
                [14, 19, 23, 30], // веса рёбер для вершины 3
                [19, 17, 24, 28], // веса рёбер для вершины 4
                [18, 21, 26, 33], // веса рёбер для вершины 5
                [17, 22, 25, 32], // веса рёбер для вершины 6
                [23, 21, 24, 34], // веса рёбер для вершины 7
                [22, 17, 27, 35], // веса рёбер для вершины 8
                [20, 15, 26, 36], // веса рёбер для вершины 9
                [15, 18, 28, 37], // веса рёбер для вершины 10
                [24, 18, 29, 38], // веса рёбер для вершины 11
                [25, 20, 30, 39], // веса рёбер для вершины 12
                [24, 20, 31, 40], // веса рёбер для вершины 13
                [26, 29, 32, 41], // веса рёбер для вершины 14
                [29, 26, 33, 42], // веса рёбер для вершины 15
                [25, 27, 34, 43], // веса рёбер для вершины 16
                [27, 25, 35, 44], // веса рёбер для вершины 17
                [27, 21, 36, 45], // веса рёбер для вершины 18
                [28, 21, 37, 46], // веса рёбер для вершины 19
                [26, 23, 38, 47], // веса рёбер для вершины 20
                [30, 23, 39, 48], // веса рёбер для вершины 21
                [28, 31, 40, 49], // веса рёбер для вершины 22
                [31, 28, 41, 50], // веса рёбер для вершины 23
                [29, 33, 42, 51], // веса рёбер для вершины 24
                [33, 29, 43, 52], // веса рёбер для вершины 25
                [30, 32, 44, 53], // веса рёбер для вершины 26
                [32, 30, 45, 54], // веса рёбер для вершины 27
                [31, 34, 46, 55], // веса рёбер для вершины 28
                [34, 31, 47, 56], // веса рёбер для вершины 29
                [32, 35, 48, 57], // веса рёбер для вершины 30
                [35, 32, 49, 58], // веса рёбер для вершины 31
                [33, 36, 50, 59], // веса рёбер для вершины 32
                [36, 33, 51, 60], // веса рёбер для вершины 33
                [34, 37, 52, 61], // веса рёбер для вершины 34
                [37, 34, 53, 62], // веса рёбер для вершины 35
                [35, 38, 54, 63], // веса рёбер для вершины 36
                [38, 35, 55, 64], // веса рёбер для вершины 37
                [36, 39, 56, 65], // веса рёбер для вершины 38
                [39, 36, 57, 66], // веса рёбер для вершины 39
                [37, 40, 58, 67], // веса рёбер для вершины 40
                [40, 37, 59, 68], // веса рёбер для вершины 41
                [38, 41, 60, 69], // веса рёбер для вершины 42
                [41, 38, 61], // веса рёбер для вершины 43
                [39, 42, 62], // веса рёбер для вершины 44
                [42, 39, 63], // веса рёбер для вершины 45
                [40, 43, 64], // веса рёбер для вершины 46
                [43, 40, 65], // веса рёбер для вершины 47
                [41, 44, 66], // веса рёбер для вершины 48
                [44, 41], // веса рёбер для вершины 49
                [42, 45], // веса рёбер для вершины 50
                [45, 42], // веса рёбер для вершины 51
                [43, 46], // веса рёбер для вершины 52
                [46, 43], // веса рёбер для вершины 53
                [44, 47], // веса рёбер для вершины 54
                [47, 44], // веса рёбер для вершины 55
                [45, 48], // веса рёбер для вершины 56
                [48, 45], // веса рёбер для вершины 57
                [46, 49], // веса рёбер для вершины 58
                [49, 46], // веса рёбер для вершины 59
                [47, 50], // веса рёбер для вершины 60
                [50, 47], // веса рёбер для вершины 61
                [48, 51], // веса рёбер для вершины 62
                [51, 48], // веса рёбер для вершины 63
                [49, 52], // веса рёбер для вершины 64
                [52, 49], // веса рёбер для вершины 65
                [50, 53], // веса рёбер для вершины 66
                [53, 50], // веса рёбер для вершины 67
                [51, 54], // веса рёбер для вершины 68
                [54, 51], // веса рёбер для вершины 69
                [52, 55], // веса рёбер для вершины 70
                [55, 52], // веса рёбер для вершины 71
                [53, 56], // веса рёбер для вершины 72
                [56, 53], // веса рёбер для вершины 73
                [54, 57], // веса рёбер для вершины 74
                [57, 54], // веса рёбер для вершины 75
                [55, 58], // веса рёбер для вершины 76
                [58, 55], // веса рёбер для вершины 77
                [56, 59], // веса рёбер для вершины 78
                [59, 56], // веса рёбер для вершины 79
                [57, 60], // веса рёбер для вершины 80
                [60, 57], // веса рёбер для вершины 81
                [58, 61], // веса рёбер для вершины 82
                [61, 58], // веса рёбер для вершины 83
                [59, 62], // веса рёбер для вершины 84
                [62, 59], // веса рёбер для вершины 85
                [60, 63], // веса рёбер для вершины 86
                [63, 60], // веса рёбер для вершины 87
                [61, 64], // веса рёбер для вершины 88
                [64, 61], // веса рёбер для вершины 89
                [62, 65], // веса рёбер для вершины 90
                [65, 62], // веса рёбер для вершины 91
                [63, 66], // веса рёбер для вершины 92
                [66, 63], // веса рёбер для вершины 93
                [64, 67], // веса рёбер для вершины 94
                [67, 64], // веса рёбер для вершины 95
                [65, 68], // веса рёбер для вершины 96
                [68, 65], // веса рёбер для вершины 97
                [66, 69], // веса рёбер для вершины 98
                [69, 66] // веса рёбер для вершины 99
            ];

            IGraph graph = new GraphCSRWeights(adjacencyList, vertexWeights, edgeWeights);
            IAccuratePartition ap = new BranchAndBoundsAlgorithm();
            int[] x = ap.GetPartition(graph, eps);

            TestContext.WriteLine("x = [" + string.Join(", ", ap.GetSolution().Item1) + "]");
            TestContext.WriteLine("q = " + ap.GetSolution().Item2);
            TestContext.WriteLine("balance = " + Math.Round((double)SubGraphWeight(x, vertexWeights) / graph.GraphWeight, 2));

            Assert.IsTrue(Math.Abs((double)SubGraphWeight(x, vertexWeights) / graph.GraphWeight - 1.0 / 2.0) < eps);
            Assert.IsTrue(x.Sum() > 0);
            Assert.IsTrue(ap.GetSolution().Item2 < int.MaxValue);
        }
    }
}