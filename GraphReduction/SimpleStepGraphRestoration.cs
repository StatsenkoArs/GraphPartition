using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public class SimpleStepGraphRestoration : IGraphRestoration

    {
        public int[] UnmappingStep(int[] partition, int[] mapping)
        {
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
    }
}
