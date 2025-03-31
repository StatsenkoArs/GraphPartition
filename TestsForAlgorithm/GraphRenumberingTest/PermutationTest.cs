using GraphRenumbering;

namespace TestsForAlgorithm.GraphRenumberingTest
{
    [TestClass]
    public class PermutationTest
    {
        private bool PermutationAssert(PermutationStructure perm, int[] expected_permutation, int[] expected_reverse_permutation)
        {
            bool assert_flag = true;
            if (perm.GetLength() != expected_permutation.Length) assert_flag = false;
            for (int i = 0; i < perm.GetLength(); i++)
            {
                if (expected_permutation[i] != perm.GetNumByPos(i) ||
                    expected_reverse_permutation[i] != perm.GetPosByNum(i))
                {
                    assert_flag = false;
                    break;
                }
            }
            return assert_flag;
        }
        [TestMethod] 
        public void PermutationStructure_CreateFromLength4_Permutation0123()
        {
            PermutationStructure perm = new PermutationStructure(4);

            int[] expected_perm     = [0, 1, 2, 3];
            int[] expected_rev_perm = [0, 1, 2, 3];

            Assert.IsTrue(PermutationAssert(perm, expected_perm, expected_rev_perm));
        }
        [TestMethod]
        public void PermutationStructure_CreateFromArrayLength4_Permutation1032()
        {
            int[] permutation_array = [1, 0, 3, 2];
            PermutationStructure perm = new PermutationStructure(permutation_array);

            int[] expected_perm = [1, 0, 3, 2];
            int[] expected_rev_perm = [1, 0, 3, 2];

            Assert.IsTrue(PermutationAssert(perm, expected_perm, expected_rev_perm));
        }
        [TestMethod]
        public void ChangeByPos_Change0and1_Permutaion1023()
        {
            PermutationStructure perm = new PermutationStructure(4);
            perm.ChangeByPos(0, 1);

            int[] expected_perm = [1, 0, 2, 3];
            int[] expected_rev_perm = [1, 0, 2, 3];

            Assert.IsTrue(PermutationAssert(perm, expected_perm, expected_rev_perm));
        }
        [TestMethod]
        public void ChangeByPos_Change0and1Change3and1_Permutaion1320()
        {
            PermutationStructure perm = new PermutationStructure(4);
            perm.ChangeByPos(0, 1);
            perm.ChangeByPos(3, 1);

            int[] expected_perm = [1, 3, 2, 0];
            int[] expected_rev_perm = [3, 0, 2, 1];

            Assert.IsTrue(PermutationAssert(perm, expected_perm, expected_rev_perm));
        }
        [TestMethod]
        public void ChangeByPos_Change0and1Change3and1Cnahge1and3_Permutaion1023()
        {
            PermutationStructure perm = new PermutationStructure(4);
            perm.ChangeByPos(0, 1);
            perm.ChangeByPos(3, 1);
            perm.ChangeByPos(1, 3);

            int[] expected_perm = [1, 0, 2, 3];
            int[] expected_rev_perm = [1, 0, 2, 3];

            Assert.IsTrue(PermutationAssert(perm, expected_perm, expected_rev_perm));
        }

        [TestMethod]
        public void GetNumByPos_NumFromPos3_Num3()
        {
            PermutationStructure perm = new PermutationStructure(4);

            int expected_num = 3;

            Assert.AreEqual(expected_num, perm.GetNumByPos(3));
        }

        [TestMethod]
        public void GetPosByNum_PosFromNum3_Pos3()
        {
            PermutationStructure perm = new PermutationStructure(4);

            int expected_pos = 3;

            Assert.AreEqual(expected_pos, perm.GetPosByNum(3));
        }

    }
}
