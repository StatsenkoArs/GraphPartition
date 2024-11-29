using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public class SimpleGraphRestoration : IGraphRestoration
    {
        private Stack<int[]> mappings;
        public SimpleGraphRestoration()
        {
            mappings = new Stack<int[]>();
        }

        public int[] Restore(int[] partition)
        {
            if (mappings.Count == 0)
            {
                return partition;
            }
            int[] mapping = mappings.Pop();
            int[] tmp = new int[mapping.Length];
            for (int i = 0; i < mapping.Length; i++)
            {
                for (int j = 0; j < partition.Length; j++)
                {
                    if (mapping[i] == j)
                    {
                        tmp[i] = partition[j];
                    }
                }
            }
            return tmp;
        }
        public void SetMappingStorage(Stack<int[]> mappings)
        {
            this.mappings = mappings;
        }
    }
}
