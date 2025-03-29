using GraphRenumbering;
using GraphRepresentation;

namespace TestsForAlgorithm
{
    [TestClass]
    public class RenumGraphTest
    {
        [TestMethod]
        public void Test1()
        {
            IGraph graph = new GraphSRC([[1], [0]]);
            PermutationStructure pstr = new PermutationStructure([1, 0]);
            IGraph new_graph = GraphRenum.RenumGraph(graph, pstr);
            Assert.AreEqual(0, new_graph[0,1]);
        }
    }
}
