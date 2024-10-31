namespace ExampleGenerator
{
    public class Generator
    {
        private List<int>[] _graph;
        private Random _random = new Random();

        public Generator() 
        { 
            _graph = Array.Empty<List<int>>(); 
        }

        public int[][] Generate(int n, int edges, int q, int dif = 0)
        {

            if (edges > Enumerable.Range(1, n / 2 - dif).Sum() / 2) throw new Exception("Too many edges in graph generation");

            _graph = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                _graph[i] = new List<int>();
            }

            int edgesInLeft = Convert.ToInt32(Math.Ceiling((edges - q) / 2.0)) - dif;
            int edgesInRight = Convert.ToInt32(Math.Floor((edges - q) / 2.0)) + dif;
            
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

        public int[][] Graph
        {
            get
            {
                return ParseArrayOfLists(_graph);
            }
        }

        private void AddEdge(List<int> leftVertexNums, List<int> rightVertexNums)
        {
            bool hasGenerated = false;
            while (!hasGenerated)
            {
                int leftVertex = leftVertexNums[_random.Next(0, leftVertexNums.Count)];
                int rightVertex = rightVertexNums[_random.Next(0, rightVertexNums.Count)];
                if (leftVertex != rightVertex && !_graph[leftVertex].Contains(rightVertex))
                {
                    _graph[leftVertex].Add(rightVertex);
                    _graph[rightVertex].Add(leftVertex);
                    hasGenerated = true;
                }
            }
        }

        private void GenerateForVertexes(List<int> numVertexes, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                AddEdge(numVertexes, numVertexes);
            }
        }

        private void SortAnswer(List<int>[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                array[i].Sort();
            }
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
