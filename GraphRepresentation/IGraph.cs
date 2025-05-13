namespace GraphRepresentation
{
    public interface IGraph
    {
        int this[int vertexNum, int adjacentNum] { get; }
        int CountVertecies { get; }
        int CountEdges { get; }
        int GraphWeight { get; }
        int GetVertexDegree(int vertexNum);
        int GetVertexWeight(int vertexNum);
        int GetEdgeWeight(int vertexNumStart, int vertexNumEnd);
        int[] GetAdjacentVertecies(int vertexNum);
        int GetGraphCut(int[] partition);
    }
}
