using GraphRepresentation;

namespace GraphPartitionAccurate
{
    /// <summary>
    /// Алгоритм точного разбиения графа методом ветвей и границ
    /// </summary>
    public class BranchAndBoundsAlgorithm : IAccuratePartition
    {
        // Решение задачи
        private int[] _bestPartition = Array.Empty<int>(); // бинарный вектор разбиения
        private int _bestCriterion = 0; // рекорд критерия

        // Параметры задачи
        private IGraph _graph = null!; // граф
        private double _balanceCriteria = 0; // критерий баланса разбиения
        
        // Кэш параметров графа
        private int _n = 0; // число вершин в графе
        private int _criterionDelta = 0; // разница критерия при расчете разбиения
        private int _graphWeight = 0; // вес всего графа

        /// <summary>
        /// Запускает решение точным алгоритмом
        /// </summary>
        /// <param name="graph">список смености графа</param>
        /// <param name="balanceCriteria">критерий сбалансированности решения</param>
        /// <returns>бинарный вектор-решение</returns>
        public int[] GetPartition(IGraph graph, double balanceCriteria)
        {
            ValidateInput(graph, balanceCriteria);
            Init(graph, balanceCriteria);

            FindSolution(new int[_n], 0, 0, 0, 0);

            return _bestPartition;
        }

        /// <summary>
        /// Проверка корректности ввода данных
        /// </summary>
        /// <param name="graph">граф</param>
        /// <param name="balanceCriteria">критерий сбалансированности разбинеие (0; 0.5)</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ValidateInput(IGraph graph, double balanceCriteria)
        {
            ArgumentNullException.ThrowIfNull(graph);

            if (balanceCriteria < 0 || balanceCriteria > 0.5)
                throw new ArgumentOutOfRangeException(nameof(balanceCriteria),
                    "Критерий сбалансированности должен быть между 0 и 0.5");
        }

        /// <summary>
        /// Инит полей класса
        /// </summary>
        /// <param name="graph">граф для разбиения</param>
        private void Init(IGraph graph, double balanceCriteria)
        {
            _graph = graph;
            _balanceCriteria = balanceCriteria;
            _n = graph.CountVertecies;
            _bestCriterion = int.MaxValue;
            _bestPartition = new int[_n];
            _graphWeight = graph.GraphWeight;
        }

        /// <summary>
        /// Возвращает уже вычсиленное решение
        /// </summary>
        /// <returns>бинарный вектор-решение/критерий</returns>
        public (int[] partition, int criterion) GetSolution()
        {
            return (_bestPartition, _bestCriterion);
        }

        /// <summary>
        /// Считает изменение внутренней связности графа, при помещенее туда вершины под номером index
        /// complexity O(m) (m - max edges of vertex in graph)
        /// </summary>
        /// <param name="partiton">текущее решение</param>
        /// <param name="index"> (вершина) текущий шаг алгоритма</param>
        /// <returns>изменение критерия при помещении вершины в подграф</returns>
        private int QChanges(int[] partiton, int index)
        {
            int result = 0;
            for (int i = 0; i < _graph.GetVertexDegree(index); i++)
            {
                if (_graph[index, i] < index)
                    result += partiton[index] == partiton[_graph[index, i]] ? 0 : _graph.GetEdgeWeight(index, _graph[index, i]);
            }
            return Math.Abs(result);
        }

        /// <summary>
        /// Рекурсивный перебор всех возможных бинарных векторов-решений
        /// </summary>
        /// <param name="partition">бинарный вектор решение '0' для одного подграфа, '1' для другого</param>
        /// <param name="index">текущий шаг (номер обрабатываемой вершины)</param>
        /// <param name="weightLeft">вес подграфа '0'</param>
        /// <param name="currentCriterion">текущий критерий</param>
        /// <param name="accumulatedWeight">вес уже обработанного подграфа</param>
        private void FindSolution(int[] partition, int index, int weightLeft, int accumulatedWeight, int currentCriterion)
        {
            if (index == _n)
            {
                if (currentCriterion < _bestCriterion && Math.Abs((double)weightLeft / _graphWeight - 0.5) < _balanceCriteria)
                {
                    partition.CopyTo(_bestPartition, 0);
                    _bestCriterion = currentCriterion;
                }
                return;
            }

            if (currentCriterion > _bestCriterion)
                return;

            if (accumulatedWeight > (double)_graphWeight / 2 && ((double)weightLeft / _graphWeight - 0.5 > _balanceCriteria || (accumulatedWeight - (double)weightLeft) / _graphWeight - 0.5 > _balanceCriteria))
                return;


            int currentVertexWeight = _graph.GetVertexWeight(index);

            partition[index] = 0;
            _criterionDelta = QChanges(partition, index);
            FindSolution(partition, index + 1, weightLeft + currentVertexWeight, accumulatedWeight + currentVertexWeight, currentCriterion + _criterionDelta);
            partition[index] = 1;
            _criterionDelta = QChanges(partition, index);
            FindSolution(partition, index + 1, weightLeft, accumulatedWeight + currentVertexWeight, currentCriterion + _criterionDelta);
        }
    }
}