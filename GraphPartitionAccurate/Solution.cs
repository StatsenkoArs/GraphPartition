namespace GraphPartitionAccurate
{
    public class Solution
    {
        private int[][] _edges;
        private int _n;
        private int[] _x;
        private int _q;
        private int _allEdges;

        public Solution(int n, int[][] edges)
        {
            _n = n;
            _edges = edges; // подмассивы ДОЛЖНЫ БЫТЬ ОТСОТИРОВАНЫ по возрастанию
            _allEdges = AllEdges;
            _x = new int[_n];
            for (int i = 0; i < _n / 2; i++) _x[i] = 1;
            _q = CalculateQ(_x);
            this.FindSolution(new int[_n], _n, 0, 0, 0, 0, _allEdges);
        }

        public Tuple<int[], int> GetSolution()
        {
            return Tuple.Create(_x, _q);
        }

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

        private int CalculateQ(int[] x)
        {
            int q = 0;
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _edges[i].Length; j++)
                {
                    q += x[i] * (1 - x[_edges[i][j]]);
                }
            }
            return q;
        }

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
