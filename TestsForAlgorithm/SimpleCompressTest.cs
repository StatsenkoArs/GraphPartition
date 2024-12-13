using GraphReduction;
using GraphRepresentation;

namespace TestsForAlgorithm
{
    [TestClass]
    public class SimpleCompressTest
    {
        [TestMethod]
        public void TestSimpleCompress1()
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
            int[] result = compress.Compress(new GraphSRC(graph));
            CollectionAssert.AreEqual(expected, result);
        }
        [TestMethod]
        public void TestSimpleCompress2()
        {
            int expected = 3;
            int[][] graph = [
                    [2,3,4],
                    [2],
                    [0,1,3],
                    [0,4,5],
                    [0,3],
                    [3]
                ];
            ICompress compress = new SimpleCompress();
            int[] result = compress.Compress(new GraphSRC(graph));
            Assert.AreEqual(expected, compress.GetNumOfGroup());
        }
    }
}
