using GraphReduction;
using GraphRepresentation;

namespace TestsForAlgorithm.GraphReductionTest
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
            int[] result = compress.Compress(new GraphCSR(graph));
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
            int[] result = compress.Compress(new GraphCSR(graph));
            Assert.AreEqual(expected, compress.GetNumOfGroup());
        }
        [TestMethod]
        public void TestSimpleCompress3()
        {
            int[] expected = [0,1,1,2,3,2,0,4,5,3];
            int[][] graph = [
                    [6],
                    [2,5],
                    [1,6,7],
                    [5,8],
                    [5,9],
                    [1,3,4,7],
                    [0,2],
                    [2,5],
                    [3,9],
                    [4,8]
                ];
            ICompress compress = new SimpleCompress();
            int[] result = compress.Compress(new GraphCSR(graph));
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
