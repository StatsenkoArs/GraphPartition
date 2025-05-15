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

        override public int GraphWeight => CountVertecies;
    }
}
