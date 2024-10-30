using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public class GraphReductAlg      //Основной класс
    {
        private List<int[]> decoder;
        private ICompress compress;
        private IRestruct restruct;
        public GraphReductAlg(ICompress comp, IRestruct restruct)
        {
            this.compress = comp;
            this.restruct = restruct;
        }
        public void Reduct(ref List<int>[] graph, int n)   //Добавить что-то вместо 
        {
            //decoder = new int[n][];
            decoder = new List<int[]>();
            int[] local_cipher;
            int k = 0;
            int group = -1;
            while (graph.Length > n)
            {
                compress.Compress(graph);
                local_cipher = compress.GetCipher();
                decoder.Add(local_cipher);
                group = compress.GetNumOfGroup();
                restruct.Restruct(ref graph, in local_cipher, group);
                graph = restruct.GetGraph();
                k++;
            }
        }
        public int[] GetSolution(int[] partition)    //Очень страшно и ужасно, но работает, заменить как можно скорее!
                                                     //По распределению на малом графе выстраивает распределение для большого.
        {
            int[] tmp = Array.Empty<int>();
            for (int i = decoder.Count() - 1; i >= 0; i--)
            {
                tmp = new int[decoder[i].Count()];
                for (int j = 0; j < decoder[i].Count(); j++)
                {
                    for (int t = 0; t < partition.Length; t++)
                    {
                        if (decoder[i][j] == t)
                        {
                            tmp[j] = partition[t];
                        }
                    }
                }
                partition = tmp;
            }
            return tmp;
        }
    }
}
