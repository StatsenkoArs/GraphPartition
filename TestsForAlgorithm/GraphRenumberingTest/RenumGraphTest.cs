using ExampleGenerator;
using GraphRenumbering;
using GraphRepresentation;

namespace TestsForAlgorithm.GraphRenumberingTest
{
    [TestClass]
    public class RenumGraphTest
    {
        private bool RenumGraphAssert(IGraph graph, IGraph renum_graph, PermutationStructure perm)
        {
            if (graph.CountVertecies != renum_graph.CountVertecies) return false;
            if (graph.CountEdges != renum_graph.CountEdges) return false;

            for (int vert = 0; vert < graph.CountVertecies; vert++)
            {
                if (graph.GetVertexDegree(vert) != renum_graph.GetVertexDegree(perm.GetNumByPos(vert))) return false; 
                for (int i = 0; i < graph.GetVertexDegree(vert); i++)
                {
                    if (graph[vert, i] != perm.GetPosByNum(renum_graph[perm.GetNumByPos(vert), i])) return false;
                }
            }

            for (int vert = 0; vert < renum_graph.CountVertecies; vert++)
            {
                if (renum_graph.GetVertexDegree(vert) != graph.GetVertexDegree(perm.GetPosByNum(vert))) return false;
                for (int i = 0; i < renum_graph.GetVertexDegree(vert); i++)
                {
                    if (renum_graph[vert, i] != perm.GetNumByPos(graph[perm.GetPosByNum(vert), i])) return false;
                }
            }
            return true;
        }
        [TestMethod]
        public void RenumGraph_SimpleRenumGraph_RenumGraph()
        {
            IGraph graph = new GraphCRS([[2], [2], [0, 1]]);
            PermutationStructure perm = new PermutationStructure([2, 1, 0]);
            IGraph renum_graph = GraphRenum.RenumGraph(graph, perm);

            Assert.IsTrue(RenumGraphAssert(graph, renum_graph, perm));
        }


        [TestMethod]
        public void RenumGraph_GridGraph9to11RndomRenum_Graph9to11WithNewNumber()
        {
            IGraph graph = new GraphCRS(GridGraphGenerator.Generate(9, 11));
            PermutationStructure perm = RandomPermutation.GetPermutation(graph.CountVertecies);
            IGraph renum_graph = GraphRenum.RenumGraph(graph, perm);

            Assert.IsTrue(RenumGraphAssert(graph, renum_graph, perm));
        }

        [TestMethod]
        public void RenumGraph_GridGraph9to11IdenticalPermutation_Graph9to11WithSameNumber()
        {
            IGraph graph = new GraphCRS(GridGraphGenerator.Generate(9, 11));
            PermutationStructure perm = new PermutationStructure(graph.CountVertecies);
            IGraph renum_graph = GraphRenum.RenumGraph(graph, perm);

            Assert.IsTrue(RenumGraphAssert(graph, renum_graph, perm));
        }

    }
}
