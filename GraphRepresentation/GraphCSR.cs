namespace GraphRepresentation
{
    public class GraphCSR: AGraphCSR
    {
        public GraphCSR(int[][] graph) : base(graph)
        {
        }

        
        override public int GetEdgeWeight(int vertexNumStart, int vertexNumEnd)
        {
            if (GetAdjacentVertecies(vertexNumStart).Contains(vertexNumEnd))
                return 1;
            return 0;
        }

        override public int GetVertexWeight(int vertexNum)
        {
            return 1;
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
                        count_cut++;
                    }
                }
            }
            return count_cut / 2;
        }

        override public int GraphWeight => CountVertecies;

        override public (int left, int right) GetGraphBalance(int[] partition)
        {
            int left = 0;
            int right = 0;
            for (int i = 0; i < partition.Length; ++i)
            {
                if (partition[i] == 0) left++;
            }
            right = GraphWeight - left;
            return (left, right);
        }
    }
}
