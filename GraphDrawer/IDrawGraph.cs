using GraphRepresentation;
using System.Drawing;

namespace GraphDrawer
{
    public interface IDrawGraph
    {
        void Draw(int[][] graph, Point[] coordVertex, int[] partition, string path);
        void Statistics(IGraph graph, int[] partition, string filename, bool isStart);
    }
}
