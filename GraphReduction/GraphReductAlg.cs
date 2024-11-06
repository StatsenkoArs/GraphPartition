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
        //private int count_decoder;
        public GraphReductAlg(ICompress comp, IRestruct restruct)
        {
            this.compress = comp;
            this.restruct = restruct;
            decoder = new List<int[]>();

        }
        /// <summary>
        /// Осуществляет один шаг сжатия графа
        /// </summary>
        /// <param name="graph">Изначальный граф</param>
        /// <returns>Новый граф</returns>
        public List<int>[] Reduct(in List<int>[] graph)
        {
            List<int>[] new_graph = new List<int>[graph.Length];
            int[] tmp_mapping;
            int group;
            tmp_mapping = compress.Compress(graph);
            group = compress.GetNumOfGroup();
            new_graph = restruct.Restruct(graph, in tmp_mapping, group);
            decoder.Add(tmp_mapping);
            return new_graph;
        }
        /// <summary>
        /// По распределению на малом графе выстраивает распределение для большого.
        /// </summary>
        /// <param name="partition">Разбиение графа</param>
        /// <returns>Разбиение для всего большого графа</returns>
        public int[] GetSolution(int[] partition)    //Очень страшно и ужасно, но работает, заменить как можно скорее!
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
        /// <summary>
        /// Один шаг экстраполяции разбиения графа
        /// </summary>
        /// <param name="partition">Разбиение графа нужной размерности</param>
        /// <returns>Соедующее разбиение графа</returns>
        public int[] UnmappingStep(int[] partition)
        {
            int[] tmp = Array.Empty<int>();
            int count = decoder.Count() - 1;
            tmp = new int[decoder[count].Count()];
            for (int j = 0; j < decoder[count].Count(); j++)
            {
                for (int t = 0; t < partition.Length; t++)
                {
                    if (decoder[count][j] == t)
                    {
                        tmp[j] = partition[t];
                    }
                }
            }
            partition = tmp;
            return tmp;
        }
    }
}
