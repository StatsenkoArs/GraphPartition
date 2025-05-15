using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRepresentation
{
    public class GraphCSRWeights : AGraphCSR
    {
        protected int[] _vertexWeights;
        protected int[] _edgesWeights;
        public GraphCSRWeights(int[][] graph, int[] vertexWeights, int[][] edgesWeights) : base(graph)
        {
            int count_edge = 0;
            for (int vert = 0; vert < edgesWeights.Length; vert++)
            {
                count_edge += edgesWeights[vert].Length;
            }
            _edgesWeights = new int[count_edge];

            int count = 0;
            for (int i = 0; i < edgesWeights.Length; i++)
            {
                for (int j = 0; j < edgesWeights[i].Length; j++)
                {
                    _edgesWeights[count] = edgesWeights[i][j];
                    count++;
                }
            }

            _vertexWeights = vertexWeights;
        }

        override public int GraphWeight
        { 
            get 
            {
                int result = 0;
                foreach (var w in _vertexWeights)
                {
                    result += w;
                }
                return result;
            } 
        }

        public override int GetEdgeWeight(int vertexNumStart, int vertexNumEnd)
        {
            int index = Array.IndexOf(GetAdjacentVertecies(vertexNumStart), vertexNumEnd);
            if (index == -1)
                return 0;

            return _edgesWeights[_adjacentNums[vertexNumStart] + index];
        }

        public override int GetVertexWeight(int vertexNum)
        {
            return _vertexWeights[vertexNum];
        }
    }
}
