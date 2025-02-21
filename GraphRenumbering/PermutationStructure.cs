namespace GraphRenumbering
{
    public class PermutationStructure
    {
        private int[] permutation;
        private int[] reverse_permutation;
        private int _length;
        public PermutationStructure(int length)
        {
            _length = length;
            permutation = new int[length];
            reverse_permutation = new int[length];
            for (int i = 0; i < _length; i++)
            {
                permutation[i] = i;
                reverse_permutation[i] = i;
            }
        }
        public PermutationStructure(int[] array)
        {
            _length = array.Length;
            permutation = new int[_length];
            reverse_permutation = new int[_length];
            for (int i = 0; i < _length; i++)
            {
                permutation[i] = array[i];
                reverse_permutation[array[i]] = i;
            }
        }

        public void ChangeByPos(int from, int to)
        {
            if (from >= _length) { throw new ArgumentException("from >= perm length", "from"); }
            if (to >= _length) { throw new ArgumentException("to >= perm length", "to"); }

            int tmp_to = permutation[to];
            int tmp_from = permutation[from];

            permutation[to] = permutation[from];
            permutation[from] = tmp_to;

            int tmp_reverse_to = reverse_permutation[tmp_to];

            reverse_permutation[tmp_to] = reverse_permutation[tmp_from];
            reverse_permutation[tmp_from] = tmp_reverse_to;
        }

        public int GetNumByPos(int pos)
        {
            return permutation[pos];
        } 
        public int GetPosByNum(int num)
        {
            return reverse_permutation[num];
        }
        public int GetLength() { return _length; }
    }
}
