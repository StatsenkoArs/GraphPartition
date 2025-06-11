namespace ExampleGenerator
{
    public class Generator
    {
        private List<int>[] _graph;
        private Random _random = new Random();
        private int _lowLimit;
        private int _upLimit;
        private int _edgesGenerated;
        int edgesInLeft;
        int edgesInRight;

        public Generator() 
        { 
            _graph = Array.Empty<List<int>>();
        }

        /// <summary>
        /// Генератор случайных графов
        /// </summary>
        /// <param name="n">число вершин</param>
        /// <param name="q">критерий</param>
        /// <param name="lowerAjustLimit">минимум соседей</param>
        /// <param name="upperAdjustLimit">максимум соседей</param>
        /// <param name="dif">дисбаланс</param>
        /// <returns></returns>
        public int[][] Generate(int n, int q, int lowerAjustLimit = 1, int upperAdjustLimit = 5, int dif = 0)
        {
            _lowLimit = lowerAjustLimit;
            _upLimit = upperAdjustLimit;

            _graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                _graph[i] = new List<int>();
            }

            _edgesGenerated = 0;
            int edges = _random.Next(n * _lowLimit / 4 + 1, n * _upLimit / 4);
            edgesInLeft = Convert.ToInt32(Math.Floor((edges - q) / 2.0)) - dif;
            edgesInRight = Convert.ToInt32(Math.Floor((edges - q) / 2.0)) + dif;
            
            List<int> leftNumbers = new List<int>();
            List<int> rightNumbers = new List<int>();
            List<int> numbers = Enumerable.Range(0, n).ToList();
            while (numbers.Count > n / 2)
            {
                int toLeft = _random.Next(0, numbers.Count);
                leftNumbers.Add(numbers[toLeft]);
                numbers.RemoveAt(toLeft);
            }
            foreach (int num in numbers) rightNumbers.Add(num);

            GenerateForVertexes(leftNumbers, edgesInLeft);
            GenerateForVertexes(rightNumbers, edgesInRight);

            for (int i = 0; i < q; i++)
            {
                AddEdge(leftNumbers, rightNumbers);
            }

            SortAnswer(_graph);

            return ParseArrayOfLists(_graph);
        }

        /// <summary>
        /// возвращает уже сгенерированный граф
        /// </summary>
        public int[][] Graph
        {
            get
            {
                return ParseArrayOfLists(_graph);
            }
        }

        /// <summary>
        /// добавление ребра в граф
        /// </summary>
        /// <param name="leftVertexNums">номера начала ребра</param>
        /// <param name="rightVertexNums">номера конца ребра</param>
        private void AddEdge(List<int> leftVertexNums, List<int> rightVertexNums)
        {
            bool hasGenerated = false;
            while (!hasGenerated)
            {
                int leftVertex = leftVertexNums[_random.Next(0, leftVertexNums.Count)];
                int rightVertex = rightVertexNums[_random.Next(0, rightVertexNums.Count)];
                bool v = !_graph[leftVertex].Contains(rightVertex);
                bool v1 = _graph[leftVertex].Count < _upLimit;
                bool v2 = _graph[rightVertex].Count < _upLimit;
                if (leftVertex != rightVertex && v && v1 && v2)
                {
                    _graph[leftVertex].Add(rightVertex);
                    _graph[rightVertex].Add(leftVertex);
                    hasGenerated = true;
                }
            }
            _edgesGenerated++;
        }

        /// <summary>
        /// добавление ребер для множества номеров вершин
        /// </summary>
        /// <param name="numVertexes">номера вершин</param>
        /// <param name="quantity">количество ребер</param>
        private void GenerateForVertexes(List<int> numVertexes, int quantity)
        {
            for (int i = 1; i < numVertexes.Count; i++)
            {
                _graph[numVertexes[i]].Add(numVertexes[i - 1]);
                _graph[numVertexes[i - 1]].Add(numVertexes[i]);
            }

            for (int i = 1; i < numVertexes.Count; i++)
            {
                while(_graph[numVertexes[i]].Count < _lowLimit)
                {
                    AddEdge(new List<int>{ numVertexes[i] }, numVertexes);
                }
            }

            while(_edgesGenerated < quantity)
            {
                AddEdge(numVertexes, numVertexes);
            }
        }

        /// <summary>
        /// сортирует подмассивы списка смежности
        /// </summary>
        /// <param name="array">неотсортированный список смежности</param>
        private void SortAnswer(List<int>[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                array[i].Sort();
            }
        }

        /// <summary>
        /// Парсит массив листов в массив массивов
        /// </summary>
        /// <param name="list">массив листов</param>
        /// <returns>массив массивов</returns>
        private int[][] ParseArrayOfLists(List<int>[] list)
        {
            int[][] result = new int[list.Length][];
            for (int i = 0; i < list.Length; i++)
                result[i] = list[i].ToArray();
            return result;
        }
    }
}
