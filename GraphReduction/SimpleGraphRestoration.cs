using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public class SimpleGraphRestoration : IGraphRestoration
    {
        public int[] Restore(int[] partition, int[] mapping) //Как избавиться тут от второго массива int
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
