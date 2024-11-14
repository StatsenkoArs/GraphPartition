using GraphReduction;

namespace TestsForAlgorithm
{
    [TestClass]
    public class SimpleCompressTest
    {
        [TestMethod]
        public void SimpleTest()
        {
            int[] expected = { 0, 1, 0, 2, 2, 3 };
            int[][] graph = [
                    [2,3,4],
                    [2],
                    [0,1,3],
                    [0,4,5],
                    [0,3],
                    [3]
                ];
            ICompress compress = new SimpleCompress();
            int[] result = compress.Compress(graph);
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
