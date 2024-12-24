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
        /// <summary>
        /// ��������������� ���� �����-�� �������.
        /// </summary>
        /// <param name="partition">�������� ��������</param>
        /// <returns>��������� ������� �����������</returns>
        int[] Restore(int[] partition);

        /// <summary>
        /// ����������� ��������� ��� ������.
        /// </summary>
        /// <param name="graph">��������� ������</param>
        public void SetGraphStorage(Stack<IGraph> graph);
        /// <summary>
        /// ���������� ��������� �����������.
        /// </summary>
        /// <param name="mappings">��������� �����������</param>
        void SetMappingStorage(Stack<int[]> mappings);
    }
}
