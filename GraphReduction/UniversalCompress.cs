using GraphRepresentation;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GraphReduction
{

    public class UniversalCompress : ICompress
    {
        IPairStrategy _strateg;
        IGraphTraversal _graph_traversal;
        int _group_count;
        public UniversalCompress(IPairStrategy strateg, IGraphTraversal traversal)
        {
            _strateg = strateg;
            _graph_traversal = traversal;
        }

        public int[] Compress(IGraph graph, int compress_ratio) 
        {
            int[] traversal = _graph_traversal.GetGraphTraversal(graph);
            int[] vert_mapping = new int[graph.CountVertecies];
            bool[] is_visited_vert = new bool[graph.CountVertecies];

            int vert_pair_needed = (int)Math.Ceiling(graph.CountVertecies*((double)compress_ratio/(100 + compress_ratio)));
            int group_count = 0;
            for (int i = 0; i < traversal.Length; i++)
            {
                int curr_v = traversal[i];
                if (is_visited_vert[curr_v] == true) continue;

                is_visited_vert[curr_v] = true;
                vert_mapping[curr_v] = group_count;

                int find_vert = _strateg.PairVertex(graph, curr_v, is_visited_vert);
                if (find_vert != -1 && vert_pair_needed > 0)
                {
                    is_visited_vert[find_vert] = true;
                    vert_mapping[find_vert] = group_count;
                    vert_pair_needed--;
                }
                group_count++;
            }
            _group_count = group_count - 1;
            return vert_mapping;
        }

        public int GetNumOfGroup()
        {
            return _group_count;
        }
    }
}
