using GraphRepresentation;

namespace GraphPartitionAccurate
{
    public class BranchAndBoundsAlgorithm : IAccuratePartition
    {
        private int[] _x = Array.Empty<int>();
        private int _q = 0;
        private IGraph _graph;
        private int _n = 0;
        private int _allEdges = 0;
        private int _dif = 0;

        /// <summary>
        /// Запускает решение точным алгоритмом
        /// </summary>
        /// <param name="n">число вершин графа</param>
        /// <param name="graph">список смености графа</param>
        /// <returns>бинарный вектор-решение/критерий</returns>
        public int[] GetPartition(IGraph graph)
        {
            Init(graph);

            FindSolution(new int[_n], _n, 0, 0, 0);

            return _x;
        }

        /// <summary>
        /// Инит полей класса
        /// </summary>
        /// <param name="graph">граф для разбиения</param>
        private void Init(IGraph graph)
        {
            _graph = graph;
            _n = graph.CountVertecies;
            _allEdges = graph.CountEdges;
            _q = _allEdges;
            _x = new int[_n];
        }

        /// <summary>
        /// Возвращает уже вычсиленное решение
        /// </summary>
        /// <returns>бинарный вектор-решение/критерий</returns>
        public (int[], int) GetSolution()
        {
            return (_x, _q);
        }

        /// <summary>
        /// Считает изменение внутренней связности графа, при помещенее туда вершины под номером step
        /// complexity O(m) (m - max edges of vertex in graph)
        /// </summary>
        /// <param name="x">текущее решение</param>
        /// <param name="step"> (вершина) текущий шаг алгоритма</param>
        /// <returns>изменение критерия при помещении вершины в подграф</returns>
        private int QChanges(int[] x, int step)
        {
            int result = 0;
            for (int i = 0; i < _graph.GetVertexDegree(step); i++)
            {
                if (_graph[step, i] < step)
                    result += x[step] == x[_graph[step, i]] ? 0 : 1;
            }
            return Math.Abs(result);
        }

        /// <summary>
        /// Рекурсивный перебор всех возможных бинарных векторов-решений
        /// </summary>
        /// <param name="x">вектор-решение</param>
        /// <param name="n">количестов вершин в графе</param>
        /// <param name="step">текущий шаг (номер обрабатываемой вершины)</param>
        /// <param name="sum">сумма элементов решения (количество вершин в одном из подграфов('1'))</param>
        /// <param name="currentQ">текущий критерий</param>
        private void FindSolution(int[] x, int n, int step, int sum, int currentQ)
        {
            if (step == n)
            {
                if (currentQ < _q)
                {
                    if (n % 2 == 0)
                    {
                        if (sum == n / 2)
                        {
                            x.CopyTo(_x, 0);
                            _q = currentQ;
                        }
                    }
                    else if (sum == n / 2 + 1 || step - sum == n / 2 + 1)
                    {
                        x.CopyTo(_x, 0);
                        _q = currentQ;
                    }
                }
                return;
            }

            if (step > n / 2)
                if (n % 2 == 0)
                {
                    if (sum > n / 2 || step - sum > n / 2) return;
                }
                else if (sum > n / 2 + 1 || step - sum > n / 2 + 1) return;

            if (currentQ > _q) return;

            x[step] = 0;
            _dif = QChanges(x, step);
            FindSolution(x, n, step + 1, sum, currentQ + _dif);
            x[step] = 1;
            _dif = QChanges(x, step);
            FindSolution(x, n, step + 1, sum + 1, currentQ + _dif);
        }
    }
}