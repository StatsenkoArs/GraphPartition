using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphRepresentation
{
    public class GraphCRSWeights : GraphCRS
    {
        protected int[] _vertexWeights;
        protected int[] _edgesWeights;
        public GraphCRSWeights(int[][] graph, int[] vertexWeights, int[][] edgesWeights) : base(graph)
        {
            this._vertexWeights = vertexWeights;

            List<int> vertecies = new List<int>();
            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph[i].Length; j++)
                {
                    vertecies.Add(graph[i][j]);
                }
            }
            _edgesWeights = vertecies.ToArray();
        }

        public int GraphWeight
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
            int index = Array.IndexOf(this.GetAdjacentVertecies(vertexNumStart), vertexNumEnd);
            if (index != -1)
                return _edgesWeights[_adjacentNums[vertexNumStart] + index];

            return 0;
        }
        public override int GetVertexWeight(int vertexNum)
        {
            return _vertexWeights[vertexNum];
        }
    }
}
