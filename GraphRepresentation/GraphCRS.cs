namespace GraphRepresentation
{
    public class GraphCRS : IGraph
    {
        protected int[] _adjacentNums;
        protected int[] _adjacentVertecies;

        public GraphCRS(int[][] graph)
        {
            List<int> nums = new List<int>();
            List<int> vertecies = new List<int>();
            for (int i = 0; i < graph.Length; i++)
            {
                nums.Add(vertecies.Count);
                for (int j = 0; j < graph[i].Length; j++)
                {
                    vertecies.Add(graph[i][j]);
                }
            }
            nums.Add(vertecies.Count);
            _adjacentNums = nums.ToArray();
            _adjacentVertecies = vertecies.ToArray();
        }
        public int this[int vertexNum, int adjacentNum] 
        { 
            get => _adjacentVertecies[_adjacentNums[vertexNum] + adjacentNum];  
            set => _adjacentVertecies[_adjacentNums[vertexNum] + adjacentNum] = value; 
        }

        public int CountVertecies => _adjacentNums.Length - 1;

        public int CountEdges => _adjacentVertecies.Length / 2;

        public int GraphWeight => CountVertecies;

        public int[] GetAdjacentVertecies(int vertexNum)
        {
            int right = _adjacentNums[vertexNum + 1];
            int left = _adjacentNums[vertexNum];
            int[] slice = new int[right - left];
            Array.Copy(_adjacentVertecies, left, slice, 0, right - left);
            return slice;
        }

        public virtual int GetEdgeWeight(int vertexNumStart, int vertexNumEnd)
        {
            if (this.GetAdjacentVertecies(vertexNumStart).Contains(vertexNumEnd))
                return 1;

            return 0;
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

        public int GetVertexDegree(int vertexNum)
        {
            return _adjacentNums[vertexNum + 1] - _adjacentNums[vertexNum];
        }

        public virtual int GetVertexWeight(int vertexNum)
        {
            return 1;
        }
    }
}
