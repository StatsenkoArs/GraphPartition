using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphReduction
{
    internal interface ICompleteGraphRestoration
    {
        public void GraphRestoration(List<int[][]> graphs, List<int[]> mappings, int[] strar_partition);
    }
}
