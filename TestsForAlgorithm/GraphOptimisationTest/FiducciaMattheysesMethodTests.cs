using GraphOptimisation;
using GraphRepresentation;
using ExampleGenerator;

namespace TestsForAlgorithm.GraphOptimisationTest
{
    public class FiducciaMattheysesMethodTest_TestingPrivateMethods : FiducciaMattheysesMethod
    {
        private static int[][] graph = new int[][]
        {
            new int[]{ 1, 2 },
            new int[]{ 0, 2, 3 },
            new int[] { 0, 1, 4 },
            new int[] { 1, 4 },
            new int[] { 2, 3, 5, 6 },
            new int[] { 4, 6, 7, 8 },
            new int[] { 4, 5, 7, 9 },
            new int[] { 5, 6, 8, 9 },
            new int[] { 5, 7 },
            new int[] { 6, 7 }
        };
        //private int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 }; //равное
        private GraphCSR g = new GraphCSR(graph);


        //имя теста [Тестируемый метод]_[Сценарий]_[Ожидаемое поведение]
        [TestMethod]
        public void GetCriterion_SwapVertex_CriterionDownBy2()
        {
            int criterion = 10;
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 1, 0, 1 };
            GetCriterion(ref criterion, g, partition, 7);
            Assert.AreEqual(8, criterion);
        }
        [TestMethod]
        public void GetCriterion_SwapVertex_CriterionUpBy4()
        {
            int criterion = 0;
            int[] partition = new int[] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0 };
            GetCriterion(ref criterion, g, partition, 6);
            Assert.AreEqual(4, criterion);
        }
        [TestMethod]
        public void GetDisbalance_EmptySecondSubgraph_DisbalanceIsHalhVertex()
        {
            float disbalance = 0;
            int[] partition = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            GetDisbalance(ref disbalance, g, partition, 10);
            Assert.AreEqual(5, disbalance);
        }
        [TestMethod]
        public void GetDisbalance_TwoEqualsSubgraphs_DisbalanceZero()
        {
            float disbalance = 0;
            int[] partition = new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
            GetDisbalance(ref disbalance, g, partition, 5);
            Assert.AreEqual(0, disbalance);
        }
        [TestMethod]
        public void GetEID_SwapVertex7_E7Is1()
        {
            int[] eOld = new int[] { 2, 2, 1, 2, 3, 3, 2, 3, 1, 1 };
            int[] iOld = new int[] { 0, 1, 2, 0, 1, 1, 2, 1, 1, 1 };
            int[] dOld = new int[] { 2, 1, -1, 2, 2, 2, 0, 2, 0, 0 };
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };
            GetEID(eOld, iOld, dOld, partition, g, 7);
            Assert.AreEqual(1, eOld[7]);
        }
        [TestMethod]
        public void GetEID_SwapVertex7_I7Is3()
        {
            int[] eOld = new int[] { 2, 2, 1, 2, 3, 3, 2, 3, 1, 1 };
            int[] iOld = new int[] { 0, 1, 2, 0, 1, 1, 2, 1, 1, 1 };
            int[] dOld = new int[] { 2, 1, -1, 2, 2, 2, 0, 2, 0, 0 };
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };
            GetEID(eOld, iOld, dOld, partition, g, 7);
            Assert.AreEqual(3, iOld[7]);
        }
        [TestMethod]
        public void GetEID_SwapVertex7_D7IsMinus2()
        {
            int[] eOld = new int[] { 2, 2, 1, 2, 3, 3, 2, 3, 1, 1 };
            int[] iOld = new int[] { 0, 1, 2, 0, 1, 1, 2, 1, 1, 1 };
            int[] dOld = new int[] { 2, 1, -1, 2, 2, 2, 0, 2, 0, 0 };
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };
            GetEID(eOld, iOld, dOld, partition, g, 7);
            Assert.AreEqual(-2, dOld[7]);
        }
        [TestMethod]
        public void GetNumberBiggerSubgraph_EmptySecondSubgraph_First()
        {
            int numberPartition = 0;
            int[] partition = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int countElemInFirstSubgraph = 10;
            GetNumberBiggerSubgraph(ref numberPartition, partition, countElemInFirstSubgraph);
            Assert.AreEqual(0, numberPartition);
        }
        [TestMethod]
        public void GetNumberBiggerSubgraph_EmptyFirstSubgraph_Second()
        {
            int numberPartition = 0;
            int[] partition = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            int countElemInFirstSubgraph = 0;
            GetNumberBiggerSubgraph(ref numberPartition, partition, countElemInFirstSubgraph);
            Assert.AreEqual(1, numberPartition);
        }
        [TestMethod]
        public void GetNumberBiggerSubgraph_HalfSubgraph_First()
        {
            int numberPartition = 0;
            int[] partition = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };
            int countElemInFirstSubgraph = 5;
            GetNumberBiggerSubgraph(ref numberPartition, partition, countElemInFirstSubgraph);
            Assert.AreEqual(0, numberPartition);
        }
        [TestMethod]
        public void MoveToAnotherSubgraph_StandartScript_RightIndex()
        {
            int index = -1;
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };
            int countElemInBiggerSubgraph = 5;
            int[] D = new int[] { 2, 1, -1, 2, 2, 2, 0, 2, 0, 0 };
            int[] T_CountBlockingIterations = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            MoveToAnotherSubgraph(ref index, partition, ref countElemInBiggerSubgraph,
                D, T_CountBlockingIterations, 0, 1);
            Assert.AreEqual(4, index);
        }
        [TestMethod]
        public void MoveToAnotherSubgraph_StandartScript_SwapNumberSubgraph()
        {
            int index = -1;
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };
            int countElemInBiggerSubgraph = 5;
            int[] D = new int[] { 2, 1, -1, 2, 2, 2, 0, 2, 0, 0 };
            int[] T_CountBlockingIterations = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            MoveToAnotherSubgraph(ref index, partition, ref countElemInBiggerSubgraph,
                D, T_CountBlockingIterations, 0, 1);
            Assert.AreEqual(1, partition[index]);
        }
        [TestMethod]
        public void MoveToAnotherSubgraph_StandartScript_CountElemInSecondSubgraphBigger()
        {
            int index = -1;
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };
            int countElemInBiggerSubgraph = 5;
            int[] D = new int[] { 2, 1, -1, 2, 2, 2, 0, 2, 0, 0 };
            int[] T_CountBlockingIterations = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            MoveToAnotherSubgraph(ref index, partition, ref countElemInBiggerSubgraph,
                D, T_CountBlockingIterations, 0, 1);
            Assert.AreEqual(4, countElemInBiggerSubgraph);
        }
        [TestMethod]
        public void MoveToAnotherSubgraph_NotFoundIndex_NegativeIndex()
        {
            int index = -1;
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };
            int countElemInBiggerSubgraph = 5;
            int[] D = new int[] { 2, 1, -1, 2, 2, 2, 0, 2, 0, 0 };
            int[] T_CountBlockingIterations = new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
            MoveToAnotherSubgraph(ref index, partition, ref countElemInBiggerSubgraph,
                D, T_CountBlockingIterations, 0, 1);
            Assert.AreEqual(-1, index);
        }
        [TestMethod]
        public void MovedCheck_MoveFirstVertex_HisVertexMovedIs1()
        {
            int[] isVertexMoved = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
            int[] T_CountBlockingIterations = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int countBlockingIteration = 1;
            MovedCheck(isVertexMoved, T_CountBlockingIterations, ref countBlockingIteration, 0, 0);
            Assert.AreEqual(1, isVertexMoved[0]);
        }
        [TestMethod]
        public void MovedCheck_MoveFirstVertexIn0iter_HisTIs1()
        {
            int[] isVertexMoved = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
            int[] T_CountBlockingIterations = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int countBlockingIteration = 1;
            MovedCheck(isVertexMoved, T_CountBlockingIterations, ref countBlockingIteration, 0, 0);
            Assert.AreEqual(1, T_CountBlockingIterations[0]);
        }
        [TestMethod]
        public void MovedCheck_MoveFirstVertexIn10iter_HisTIs11()
        {
            int[] isVertexMoved = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
            int[] T_CountBlockingIterations = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int countBlockingIteration = 1;
            MovedCheck(isVertexMoved, T_CountBlockingIterations, ref countBlockingIteration, 0, 10);
            Assert.AreEqual(11, T_CountBlockingIterations[0]);
        }
        [TestMethod]
        public void MovedCheck_MoveFirstMovedVertexIn10iter_HisTIs12()
        {
            int[] isVertexMoved = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
            int[] T_CountBlockingIterations = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int countBlockingIteration = 1;
            MovedCheck(isVertexMoved, T_CountBlockingIterations, ref countBlockingIteration, 0, 10);
            Assert.AreEqual(12, T_CountBlockingIterations[0]);
        }
        [TestMethod]
        public void IsVertexMoved_ThereAreVerticesToMove_True()
        {
            int[] isVertexMoved = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, };
            int[] T_CountBlockingIterations = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };

            Assert.AreEqual(true, IsVertexMoved(T_CountBlockingIterations, partition, 0, 2));
        }
        [TestMethod]
        public void IsVertexMoved_NotAreVerticesToMove_False()
        {
            int[] isVertexMoved = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            int[] T_CountBlockingIterations = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            int[] partition = new int[] { 1, 0, 0, 1, 0, 1, 1, 0, 0, 1 };
            Assert.AreEqual(false, IsVertexMoved(T_CountBlockingIterations, partition, 0, 0));
        }

    }





    [TestClass]
    public class GraphOptimisationTest
    {
        [TestMethod]
        public void FiducciaMattheysesMethod_EqualDistribution10Vertex_EqualDistribution()
        {
            //arrange
            int[][] g = new int[][]
            {
                new int[]{1, 2 },
                new int[]{0, 2, 3},
                new int[]{0, 1, 4},
                new int[]{1, 4},
                new int[]{2, 3, 5, 6},
                new int[]{4, 6, 7, 8},
                new int[]{4, 5, 7, 9},
                new int[]{5, 6, 8, 9},
                new int[]{5, 7},
                new int[]{6, 7}
            };
            GraphCSR graph = new GraphCSR(g);
            int[] r = new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
            FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();
            int[] newR = new int[r.Length];

            //act
            newR = f.OptimisePartition(graph, r, 1, 0);

            //assert
            Assert.AreEqual(5, newR.Sum());
        }



        [TestMethod]
        public void FiducciaMattheysesMethod_NotEqualDistribution10Vertex_EqualDistribution()
        {
            //arrange
            Generator generator = new Generator();
            int[][] g = new int[][]
            {
                new int[]{1, 2 },
                new int[]{0, 2, 3},
                new int[]{0, 1, 4},
                new int[]{1, 4},
                new int[]{2, 3, 5, 6},
                new int[]{4, 6, 7, 8},
                new int[]{4, 5, 7, 9},
                new int[]{5, 6, 8, 9},
                new int[]{5, 7},
                new int[]{6, 7}
            };
            GraphCSR graph = new GraphCSR(g);
            int[] r = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();
            int[] newR = new int[r.Length];

            //act
            newR = f.OptimisePartition(graph, r, 1, 0);

            //assert
            Assert.AreEqual(5, newR.Sum());
        }

        [TestMethod]
        public void FiducciaMattheysesMethod_UnconnectedGraphAndNotEqualDistribution2Vertex_EqualDistribution()
        {
            //arrange
            int[][] g = new int[][]
            {
                new int[]{},
                new int[]{},
            };
            GraphCSR graph = new GraphCSR(g);
            int[] r = new int[] { 0, 0 };
            FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();
            int[] newR = new int[r.Length];
            //act
            newR = f.OptimisePartition(graph, r, 1, 0);
            //assert
            Assert.AreEqual(1, newR.Sum());
        }

        [TestMethod]
        public void FiducciaMattheysesMethod_UnconnectedGraphAndEqualDistribution2Vertex_EqualDistribution()
        {
            //arrange
            int[][] g = new int[][]
            {
                new int[]{},
                new int[]{},
            };
            GraphCSR graph = new GraphCSR(g);
            int[] r = new int[] { 1, 0 };
            FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();
            int[] newR = new int[r.Length];
            //act
            newR = f.OptimisePartition(graph, r, 1, 0);
            //assert
            Assert.AreEqual(1, newR.Sum());
        }
    }

}
