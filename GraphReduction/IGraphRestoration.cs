using GraphRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    public interface IGraphRestoration
    {
        int[] Restore(int[] partition);
    }
}
