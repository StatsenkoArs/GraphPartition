using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRepresentation
{
    public abstract class AGraphCSR : IGraph
    {
        protected int[] _adjacentNums;
        private int[] _adjacentVertecies;
        public AGraphCSR(int[][] graph)
        {
            int count_edge = 0;
            for (int vert = 0; vert < graph.Length; vert++)
            {
                count_edge += graph[vert].Length;
            }
            _adjacentNums = new int[graph.Length + 1];
            _adjacentVertecies = new int[count_edge];

            int count = 0;
            for (int i = 0; i < graph.Length; i++)
            {
                _adjacentNums[i] = count;
                for (int j = 0; j < graph[i].Length; j++)
                {
                    _adjacentVertecies[count] = graph[i][j];
                    count++;
                }
            }
            _adjacentNums[graph.Length] = count;

        }
        public int this[int vertexNum, int adjacentNum]
        {
            get => _adjacentVertecies[_adjacentNums[vertexNum] + adjacentNum];
        }

        public int CountVertecies => _adjacentNums.Length - 1;

        public int CountEdges => _adjacentVertecies.Length / 2;

        abstract public int GraphWeight { get;}

        public int[] GetAdjacentVertecies(int vertexNum)
        {
            int left = _adjacentNums[vertexNum];
            int right = _adjacentNums[vertexNum + 1];
            int[] slice = new int[right - left];
            Array.Copy(_adjacentVertecies, left, slice, 0, right - left);
            return slice;
        }

        public int GetVertexDegree(int vertexNum)
        {
            return _adjacentNums[vertexNum + 1] - _adjacentNums[vertexNum];
        }

        public int GetGraphCut(int[] partition)
        {
            int count_cut = 0;
            for (int vertex = 0; vertex < CountVertecies; vertex++)
            {
                for (int adj_v_ind = 0; adj_v_ind < GetVertexDegree(vertex); adj_v_ind++)
                {
                    if (partition[vertex] != partition[this[vertex, adj_v_ind]])
                    {
                        count_cut++;
                    }
                }
            }
            return count_cut / 2;
        }

        public abstract int GetEdgeWeight(int vertexNumStart, int vertexNumEnd);

        public abstract int GetVertexWeight(int vertexNum);
    }
}
