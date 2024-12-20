using GraphReduction;
using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

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
            GraphRenum rgraph = new GraphRenum();
            IGraph new_graph = rgraph.RenumGraph(graph, pstr);
            Assert.AreEqual(0, new_graph[0,1]);
        }
    }
}
