namespace GraphRepresentation
{
    public interface IGraph
    {
        int this[int vertexNum, int adjacentNum] { get; set; }
        int CountVertecies { get; }
        int CountEdges { get; }
        int GetVertexDegree(int vertexNum);
        int[] GetAdjacentVertecies(int vertexNum);
        int GetGraphCut(int[] partition);
    }
}
