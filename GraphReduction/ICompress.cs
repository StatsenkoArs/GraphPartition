using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphReduction
{
    internal interface ICompress   //Создаёт сжимающее отображение
    {
        public void Compress(in List<int>[] graph);
        public int[] GetCipher();
        public int GetNumOfGroup();
    }
}
