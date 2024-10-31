namespace GraphPartitionAccurate
{
    public class Solution
    {
        private int[] _x = Array.Empty<int>();
        private int _q = 0;
        private int[][] _edges = Array.Empty<int[]>();
        private int _n = 0;
        private int _allEdges = 0;

        public Solution()
        {
        }
        /// <summary>
        /// Запускает решение точным алгоритмом
        /// </summary>
        /// <param name="n">число вершин графа</param>
        /// <param name="edges">список смености графа</param>
        /// <returns>бинарный вектор-решение/критерий</returns>
        public Tuple<int[], int> Solve(int n, int[][] edges) // подмассивы ДОЛЖНЫ БЫТЬ ОТСОТИРОВАНЫ по возрастанию
        {
            _edges = edges;
            _n = n;
            _allEdges = AllEdges;
            _q = _allEdges;
            this.FindSolution(new int[n], n, 0, 0, 0, 0, _allEdges);
            return Tuple.Create(_x, _q);
        }
        /// <summary>
        /// Возвращает уже вычсиленное решение
        /// </summary>
        /// <returns>бинарный вектор-решение/критерий</returns>
        public Tuple<int[], int> GetSolution()
        {
            return Tuple.Create(_x, _q);
        }
        /// <summary>
        /// Считает общее число ребер в графе
        /// complexity O(n)
        /// </summary>
        private int AllEdges
        {
            get
            {
                int sum = 0;
                for (int i = 0; i < _n; i++)
                {
                    sum += _edges[i].Length;
                }
                return sum / 2;
            }
        }
        /// <summary>
        /// Считает изменение внутренней связности графа, при помещенее туда вершины под номером step
        /// complexity O(m) (m - max edges of vertex in graph)
        /// </summary>
        /// <param name="x">текущее решение</param>
        /// <param name="step">текущий шаг (вершина) алгоритма</param>
        /// <returns></returns>
        private int EdgesChange(int[] x, int step)
        {
            int koef = x[step] == 0 ? 1 : 0;
            int result = 0;
            for (int i = 0; i < _edges[step].Length; i++)
            {
                if (_edges[step][i] > step) break;
                result += (x[step] - koef) * (x[_edges[step][i]] - koef);
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
        /// <param name="edgesFirst">внутрення связность первого подграфа('0')</param>
        /// <param name="edgesSecond">внутренная связность второго подграфа('1')</param>
        /// <param name="edgesLeft">ребра, смежные с нераспределенными вершинами</param>
        private void FindSolution(int[] x, int n, int step, int sum, int edgesFirst, int edgesSecond, int edgesLeft)
        {
            if (step > n / 2)
                if (n % 2 == 0)
                {
                    if (sum > n / 2 || step - sum > n / 2) return;
                }
                else if (sum > n / 2 + 1 || step - sum > n / 2 + 1) return;

            if (_allEdges - edgesFirst - edgesSecond - edgesLeft > _q) return;

            if (step == n)
            {
                if (_allEdges - edgesFirst - edgesSecond < _q)
                {
                    x.CopyTo(_x, 0);
                    _q = _allEdges - edgesFirst - edgesSecond;
                }
                return;
            }
            x[step] = 0;
            int dif = EdgesChange(x, step);
            this.FindSolution(x, n, step + 1, sum, edgesFirst + dif, edgesSecond, edgesLeft - dif);
            x[step] = 1;
            dif = EdgesChange(x, step);
            this.FindSolution(x, n, step + 1, sum + 1, edgesFirst, edgesSecond + dif, edgesLeft - dif);
        }
    }
}
