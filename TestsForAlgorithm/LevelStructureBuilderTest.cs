using GraphReduction;
using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsForAlgorithm
{
    [TestClass]
    public class LevelStructureBuilderTest
    {
        [TestMethod]
        public void Test1()
        {
            LevelStructureBuilder builder = new LevelStructureBuilder();
            IGraph graph = new GraphSRC([[1], [0]]);
            int[] res = builder.BuildLevelStructure(graph, 0);
            int[] expected = [0, 1];
            CollectionAssert.AreEqual(expected, res);
        }
        [TestMethod]
        public void Test2()
        {
            LevelStructureBuilder builder = new LevelStructureBuilder();
            IGraph graph = new GraphSRC([[1], [0,2], [1]]);
            int[] res = builder.BuildLevelStructure(graph, 0);
            int[] expected = [0, 1, 2];
            CollectionAssert.AreEqual(expected, res);
        }

        [TestMethod]
        public void Test3()
        {
            LevelStructureBuilder builder = new LevelStructureBuilder();
            IGraph graph = new GraphSRC([[1], [0, 2,3], [1], [1]]);
            int[] res = builder.BuildLevelStructure(graph, 0);
            int[] expected = [0, 1, 2, 2];
            CollectionAssert.AreEqual(expected, res);
        }
        [TestMethod]
        public void Test4()
        {
            LevelStructureBuilder builder = new LevelStructureBuilder();
            IGraph graph = new GraphSRC([[1], [0, 2, 3], [1], [1,4], [3]]);
            int[] res = builder.BuildLevelStructure(graph, 0);
            int[] expected = [0, 1, 2, 2, 3];
            CollectionAssert.AreEqual(expected, res);
        }

        [TestMethod]
        public void Test5()
        {
            LevelStructureBuilder builder = new LevelStructureBuilder();
            IGraph graph = new GraphSRC([[1], [0, 2, 3], [1, 5], [1, 4], [3], [2]]);
            int[] res = builder.BuildLevelStructure(graph, 0);
            int[] expected = [0, 1, 2, 2, 3, 3];
            CollectionAssert.AreEqual(expected, res);
        }

        [TestMethod]
        public void Test6()
        {
            LevelStructureBuilder builder = new LevelStructureBuilder();
            IGraph graph = new GraphSRC([[1], [0, 2, 3], [1, 5], [1, 4], [3,5], [2,4]]);
            int[] res = builder.BuildLevelStructure(graph, 0);
            int[] expected = [0, 1, 2, 2, 3, 3];
            CollectionAssert.AreEqual(expected, res);
        }

        [TestMethod]
        public void Test7()
        {
            LevelStructureBuilder builder = new LevelStructureBuilder();
            IGraph graph = new GraphSRC([[1, 2], [0, 3], [0, 4], [1, 4], [2, 3, 5], [4]]);
            int[] res = builder.BuildLevelStructure(graph, 5);
            int[] expected = [3, 3, 2, 2, 1, 0];
            CollectionAssert.AreEqual(expected, res);
        }
    }
}
