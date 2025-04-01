using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDrawer
{
    public interface IDrawGraph
    {
        void Draw(int[][] graph, Point[] coordVertex, int[] partition, string filename);
        void Statistics(IGraph graph, int[] partition, string filename, bool isStart);
    }
}
