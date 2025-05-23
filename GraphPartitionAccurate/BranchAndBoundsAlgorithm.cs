using GraphRepresentation;

namespace GraphPartitionAccurate
{
    public class BranchAndBoundsAlgorithm : IAccuratePartition
    {
        private int[] _x = Array.Empty<int>(); // бинарный вектор разбиения
        private int _q = 0; // рекорд критерия
        private IGraph _graph; // граф
        private int _n = 0; // число вершин в графе
        private int _allEdges = 0; // всего ребер в графе
        private int _dif = 0; // разница критерия при расчете разбиения
        private int _graphWeight = 0;
        private double _balanceCriteria = 0;

        /// <summary>
        /// Запускает решение точным алгоритмом
        /// </summary>
        /// <param name="graph">список смености графа</param>
        /// <param name="balanceCriteria">критерий сбалансированности решения</param>
        /// <returns>бинарный вектор-решение</returns>
        public int[] GetPartition(IGraph graph, double balanceCriteria)
        {
            _balanceCriteria = balanceCriteria;

            Init(graph);

            FindSolution(new int[_n], 0, 0, 0, 0);

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
            _q = int.MaxValue;
            _x = new int[_n];
            _graphWeight = graph.GraphWeight;
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
                    result += x[step] == x[_graph[step, i]] ? 0 : _graph.GetEdgeWeight(step, _graph[step, i]);
            }
            return Math.Abs(result);
        }

        /// <summary>
        /// Рекурсивный перебор всех возможных бинарных векторов-решений
        /// </summary>
        /// <param name="x">вектор-решение</param>
        /// <param name="step">текущий шаг (номер обрабатываемой вершины)</param>
        /// <param name="weightLeft">вес подграфа '0'</param>
        /// <param name="currentQ">текущий критерий</param>
        /// <param name="weight">вес уже обработанного подграфа</param>
        private void FindSolution(int[] x, int step, int weightLeft, int currentQ, int weight)
        {
            if (step == _n)
            {
                if (currentQ < _q && Math.Abs((double)weightLeft / _graphWeight - 1.0 / 2.0) < _balanceCriteria)
                {
                    x.CopyTo(_x, 0);
                    _q = currentQ;
                }
                return;
            }

            if (currentQ > _q)
                return;

            if (weight > (double)_graphWeight / 2 && ((double)weightLeft / _graphWeight - 1.0 / 2.0 > _balanceCriteria || (weight - (double)weightLeft) / _graphWeight - 1.0 / 2.0 > _balanceCriteria))
                return;


            x[step] = 0;
            _dif = QChanges(x, step);
            FindSolution(x, step + 1, weightLeft + _graph.GetVertexWeight(step), currentQ + _dif, weight + _graph.GetVertexWeight(step));
            x[step] = 1;
            _dif = QChanges(x, step);
            FindSolution(x, step + 1, weightLeft, currentQ + _dif, weight + _graph.GetVertexWeight(step));
        }
    }
}