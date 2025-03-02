using ExampleGenerator;
using GraphOptimisation;
using GraphPartitionAccurate;
using GraphReduction;
using GraphPartitionClass;
using System.Diagnostics;

namespace TestsForAlgorithm
{
    [TestClass]
    public class EndToEndTest
    {
        private TestContext testContext;

        public TestContext TestContext
        {
            get { return testContext; }
            set { testContext = value; }
        }

        [TestMethod] 
        public void EndToEnd_10000Vertex_Balance5000()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            int num_of_vetex = 10000;
            int balance = 5000;
            int cut = 2;

            Generator g = new Generator();
            var graph = g.Generate(num_of_vetex, num_of_vetex * (num_of_vetex - 2) / 16, cut);


            IGraphPartition grp = new Graph2Partition(new SimpleGraphReduction(new SimpleRestruct(), new SimpleCompress()),
                                                    new BranchAndBoundsAlgorithm(),
                                                    new SimpleGraphRestoration(new FiducciaMattheysesMethod()),
                                                    new FiducciaMattheysesMethod());

            int[] partition = grp.GetPartition(graph);
            timer.Stop();
            Assert.AreEqual(balance, partition.Sum());
            testContext.WriteLine(Convert.ToString(timer.Elapsed.TotalSeconds));
        }

        [TestMethod]
        public void EndToEnd_5000Vertex_Balance2500()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            int num_of_vetex = 5000;
            int balance = 2500;
            int cut = 2;

            Generator g = new Generator();
            var graph = g.Generate(num_of_vetex, num_of_vetex * (num_of_vetex - 2) / 16, cut);


            IGraphPartition grp = new Graph2Partition(new SimpleGraphReduction(new SimpleRestruct(), new SimpleCompress()),
                                                    new BranchAndBoundsAlgorithm(),
                                                    new SimpleGraphRestoration(new FiducciaMattheysesMethod()),
                                                    new FiducciaMattheysesMethod());

            int[] partition = grp.GetPartition(graph);
            timer.Stop();
            Assert.AreEqual(balance, partition.Sum());
            testContext.WriteLine(Convert.ToString(timer.Elapsed.TotalSeconds));
        }
    }
}
