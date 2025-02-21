namespace GraphRepresentation
{
    public class GraphSRC : IGraph
    {
        private int[] _adjacentNums;
        private int[] _adjacentVertecies;

        public GraphSRC(int[][] graph)
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

        public int[] GetAdjacentVertecies(int vertexNum)
        {
            int right = _adjacentNums[vertexNum + 1];
            int left = _adjacentNums[vertexNum];
            int[] slice = new int[right - left];
            Array.Copy(_adjacentVertecies, left, slice, 0, right - left);
            return slice;
        }

        public int GetVertexDegree(int vertexNum)
        {
            return _adjacentNums[vertexNum + 1] - _adjacentNums[vertexNum];
        }
    }
}
