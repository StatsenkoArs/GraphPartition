using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    /// <summary>
    /// Интерфейс для создания сжмающих отображений
    /// </summary>
    public interface ICompress
    {
        public void Compress(in List<int>[] graph);
        public int[] GetMapping();
        public int GetNumOfGroup();
    }
}
