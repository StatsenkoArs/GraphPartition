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
            _random = new Random();
        }

        public int[][] Generate()
        {
            int dif = _random.Next(-_n / 20, _n / 20);
            int edgesInLeft = Convert.ToInt32(Math.Ceiling((_edges - _q) / 2.0)) - dif;
            int edgesInRight = Convert.ToInt32(Math.Ceiling((_edges - _q) / 2.0)) + dif;
            
            List<int> leftNumbers = new List<int>();
            List<int> rightNumbers = new List<int>();
            List<int> numbers = Enumerable.Range(0, _n - 1).ToList();
            while (numbers.Count > _n / 2)
            {
                int toLeft = _random.Next(0, numbers.Count);
                leftNumbers.Add(numbers[toLeft]);
                numbers.RemoveAt(toLeft);
            }
            foreach (int num in numbers) rightNumbers.Add(num);

            for (int i = 0; i < edgesInLeft; i++)
            {
                bool hasGenerated = false;
                while (!hasGenerated)
                {
                    int left = leftNumbers[_random.Next(0, leftNumbers.Count)];
                    int right = leftNumbers[_random.Next(0, leftNumbers.Count)];
                    if (!_graph[left].Contains(right))
                    {
                        _graph[left].Add(right);
                        _graph[right].Add(left);
                        hasGenerated = true;
                    }
                }
            }

            for (int i = 0; i < edgesInRight; i++)
            {
                bool hasGenerated = false;
                while (!hasGenerated)
                {
                    int left = rightNumbers[_random.Next(0, rightNumbers.Count)];
                    int right = rightNumbers[_random.Next(0, rightNumbers.Count)];
                    if (!_graph[left].Contains(right))
                    {
                        _graph[left].Add(right);
                        _graph[right].Add(left);
                        hasGenerated = true;
                    }
                }
            }

            return ParseArrayOfLists(_graph);
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
