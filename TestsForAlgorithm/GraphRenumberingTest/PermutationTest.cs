using GraphRenumbering;

namespace TestsForAlgorithm.GraphRenumberingTest
{
    [TestClass]
    public class PermutationTest
    {
        [TestMethod] 
        public void ConstructorTest()
        {
            PermutationStructure ps = new PermutationStructure(4);
            int expected = 3;
            int res = ps.GetNumByPos(3);
            Assert.AreEqual(expected, res);
        }
        [TestMethod]
        public void AddTest1()
        {
            PermutationStructure ps = new PermutationStructure(4);
            ps.ChangeByPos(0, 2);
            int expected = 0;
            int res = ps.GetNumByPos(2);
            Assert.AreEqual(expected, res);
        }
        [TestMethod]
        public void AddTest2()
        {
            PermutationStructure ps = new PermutationStructure(4);
            ps.ChangeByPos(0, 2);
            ps.ChangeByPos(3, 0);
            int expected = 2;
            int res = ps.GetNumByPos(3);
            Assert.AreEqual(expected, res);
        }
        [TestMethod]
        public void AddTest3()
        {
            PermutationStructure ps = new PermutationStructure(4);
            ps.ChangeByPos(0, 2);
            ps.ChangeByPos(3, 0);
            ps.ChangeByPos(1, 3);
            int expected = 3;
            int res = ps.GetPosByNum(1);
            Assert.AreEqual(expected, res);
        }
        [TestMethod]
        public void ConstructorArrayTest()
        {
            PermutationStructure ps = new PermutationStructure([3,2,1,0]);
            int expected = 1;
            int res = ps.GetPosByNum(2);
            Assert.AreEqual(expected, res);
        }
    }
}
