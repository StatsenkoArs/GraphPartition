namespace ExampleGenerator
{
    public class Generator
    {
        private List<int>[] _graph;
        private int _n;
        private int _edges;
        private int _q;
        private Random _random;

        public Generator(int n, int edges, int q)
        {
            _n = n;
            _edges = edges;
            _q = q;
            _graph = new List<int>[_n];
            for (int i = 0; i < _n; i++)
            {
                _graph[i] = new List<int>();
            }
            _random = new Random();
        }

        public int[][] Generate()
        {
            int dif = _random.Next(-_n / 20, _n / 20);
            int edgesInLeft = Convert.ToInt32(Math.Ceiling((_edges - _q) / 2.0)) - dif;
            int edgesInRight = Convert.ToInt32(Math.Floor((_edges - _q) / 2.0)) + dif;
            
            List<int> leftNumbers = new List<int>();
            List<int> rightNumbers = new List<int>();
            List<int> numbers = Enumerable.Range(0, _n).ToList();
            while (numbers.Count > _n / 2)
            {
                int toLeft = _random.Next(0, numbers.Count);
                leftNumbers.Add(numbers[toLeft]);
                numbers.RemoveAt(toLeft);
            }
            foreach (int num in numbers) rightNumbers.Add(num);

            GenerateForVertexes(leftNumbers, edgesInLeft, _graph);
            GenerateForVertexes(rightNumbers, edgesInRight, _graph);

            SortAnswer(_graph);

            return ParseArrayOfLists(_graph);
        }

        private List<int>[] GenerateForVertexes(List<int> numVertexes, int quantity, List<int>[] graph)
        {
            for (int i = 0; i < quantity; i++)
            {
                bool hasGenerated = false;
                while (!hasGenerated)
                {
                    int left = numVertexes[_random.Next(0, numVertexes.Count)];
                    int right = numVertexes[_random.Next(0, numVertexes.Count)];
                    if (left != right && !graph[left].Contains(right))
                    {
                        graph[left].Add(right);
                        graph[right].Add(left);
                        hasGenerated = true;
                    }
                }
            }
            return graph;
        }

        private List<int>[] SortAnswer(List<int>[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                array[i].Sort();
            }
            return array;
        }

        private int[][] ParseArrayOfLists(List<int>[] list)
        {
            int[][] result = new int[list.Length][];
            for (int i = 0; i < list.Length; i++)
                result[i] = list[i].ToArray();
            return result;
        }
    }
}
