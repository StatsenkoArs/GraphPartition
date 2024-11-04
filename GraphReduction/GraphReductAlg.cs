using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    /// <summary>
    /// Основной класс для редукции графа
    /// </summary>
    public class GraphReductAlg
    {
        private List<int[]> decoder;
        private ICompress compress;
        private IRestruct restruct;
        public GraphReductAlg(ICompress comp, IRestruct restruct)
        {
            this.compress = comp;
            this.restruct = restruct;
            decoder = new List<int[]>();
        }
        public List<int>[] Reduct(in List<int>[] graph, int n)   //Добавить что-то вместо 
        {
            List<int>[] new_graph = new List<int>[graph.Length];
            graph.CopyTo(new_graph,0);
            int[] tmp_mapping;
            int group, k = 0;
            while (new_graph.Length > n)
            {
                compress.Compress(new_graph);
                tmp_mapping = compress.GetMapping();
                decoder.Add(tmp_mapping);
                group = compress.GetNumOfGroup();
                restruct.Restruct(new_graph, in tmp_mapping, group);
                new_graph = restruct.GetGraph();
                k++;
            }
            return new_graph;
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
