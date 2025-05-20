using ExampleGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsForAlgorithm.GeneratorTest
{
    [TestClass]
    public class RandGraphGeneratorTests
    {
        [TestMethod]
        public void Generate_StressTest10of10000_CorrectlyGenerate100Graphs()
        {
            Generator generator = new();
            int numOfVerts = 10000;
            int cut = 5;

            for (int i = 1; i <= 100; ++i)
            {
                int[][] graph = generator.Generate(numOfVerts, cut, i % 5, 5 + i % 5);
                Assert.AreEqual(numOfVerts, graph.Length);
            }
        }
    }
}
