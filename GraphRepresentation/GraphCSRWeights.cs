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

        override public int GetGraphCut(int[] partition)
        {
            int count_cut = 0;
            for (int vertex = 0; vertex < CountVertecies; vertex++)
            {
                for (int adj_v_ind = 0; adj_v_ind < GetVertexDegree(vertex); adj_v_ind++)
                {
                    if (partition[vertex] != partition[this[vertex, adj_v_ind]])
                    {
                        count_cut += GetEdgeWeight(vertex, this[vertex, adj_v_ind]);
                    }
                }
            }
            return count_cut / 2;
        }

        override public (int left, int right) GetGraphBalance(int[] partition)
        {
            int left = 0;
            int right = 0;
            for (int i = 0; i < partition.Length; ++i)
            {
                if (partition[i] == 0) left += _vertexWeights[i];
            }
            right = GraphWeight - left;
            return (left, right);
        }


    }
}
