using GraphReduction;

namespace TestsForAlgorithm
{
    [TestClass]
    public class RestorationTest
    {
        [TestMethod]
        public void TestSimpleRestore1()
        {
            Stack<int[]> mapping_stack = new Stack<int[]>();
            mapping_stack.Push(new int[] {0, 0, 1, 1});
            int[] partition = {1, 0};
            int[] expected = { 1, 1, 0, 0 };
            IGraphRestoration restore = new SimpleGraphRestoration();
            restore.SetMappingStorage(mapping_stack);
            partition = restore.Restore(partition);
            CollectionAssert.AreEqual(expected, partition);
        }
    }
}
