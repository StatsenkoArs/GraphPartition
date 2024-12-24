using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
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

        public void Change(int from, int to)
        {
            if (from >= _length) { throw new ArgumentException("from >= perm length", "from");}
            if (to   >= _length) { throw new ArgumentException("to >= perm length", "to");}

            int tmp_perm    = permutation[from];
            int tmp_reverse = reverse_permutation[to];
            permutation[from] = to;
            permutation[tmp_reverse] = tmp_perm;

            reverse_permutation[to] = from;
            reverse_permutation[tmp_perm] = tmp_reverse;
        }

        public int GetNumByPos(int pos)
        {
            return permutation[pos];
        } 
        public int GetPosByNum(int num)
        {
            return reverse_permutation[num];
        }


    }
}
