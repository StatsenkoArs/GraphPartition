using System.Text;
using System.Text.Json;

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

        /// <summary>
        /// метод, генерирующий граф
        /// </summary>
        /// <param name="n">число вершин графа</param>
        /// <param name="edges">число ребер графа</param>
        /// <param name="q">критерий задачи разбиения</param>
        /// <param name="dif">разница между числом ребер в правом и левом подграфах</param>
        /// <returns>сгенерированный граф</returns>
        /// <exception cref="Exception">число ребер больше чем число возможных ребер для данного графа</exception>
        public int[][] Generate(int n, int edges, int q, int dif = 0)
        {

            if (n % 2 == 0 && edges > n * (n - 2) / 4.0 + q) throw new Exception("Too many edges in graph generation");
            else if (n % 2 != 0 && edges > Math.Pow(n / 2.0, 2) + q) throw new Exception("Too many edges in graph generation");

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
        /// Генерация графа в json и txt
        /// useful info:
        /// n * (n - 2) / 16 - нормальная завсисимость чсила ребер от числа вершин
        /// </summary>
        /// <param name="numVertexes">число вершин</param>
        /// <param name="numEdges">число ребер</param>
        /// <param name="q">критерий</param>
        /// <param name="numGraphs">число генерируемых графов</param>
        /// <param name="folderPath">папка для результатов</param>
        /// <param name="genJson">создавать Json или нет</param>
        /// <param name="genTxt">создавать Txt или нет</param>
        public void GenDataBase(int numVertexes, int numEdges, int q, int numGraphs, string folderPath, bool genJson = true, bool genTxt = true) 
        {
            if (genJson) System.IO.Directory.CreateDirectory(folderPath + @"\Json");
            if (genTxt) System.IO.Directory.CreateDirectory(folderPath + @"\Txt");
            for (int i = 1; i <= numGraphs; i++)
            {
                var t = this.Generate(numVertexes, numEdges, q);
                if (genJson)
                {
                    using (FileStream fs = new FileStream(folderPath + @$"\Json\{numVertexes}.{i}.json", FileMode.OpenOrCreate))
                    {
                        JsonSerializer.Serialize<int[][]>(fs, t);
                    }
                }
                if (genTxt)
                {
                    using (FileStream fs = new FileStream(folderPath + @$"\Txt\{numVertexes}.{i}.txt", FileMode.OpenOrCreate))
                    {
                        string txt = "";
                        for (int j = 0; j < t.Length; j++)
                        {
                            txt += j.ToString() + ": ";
                            for (int u = 0; u < t[j].Length; u++)
                            {
                                txt += t[j][u].ToString() + " ";
                            }
                            txt += "\n";
                        }
                        byte[] buffer = Encoding.Default.GetBytes(txt);
                        fs.Write(buffer, 0, buffer.Length);
                    }
                }
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
                if (leftVertex != rightVertex && !_graph[leftVertex].Contains(rightVertex))
                {
                    _graph[leftVertex].Add(rightVertex);
                    _graph[rightVertex].Add(leftVertex);
                    hasGenerated = true;
                }
            }
        }

        /// <summary>
        /// добавление ребер для множества номеров вершин
        /// </summary>
        /// <param name="numVertexes">номера вершин</param>
        /// <param name="quantity">количество ребер</param>
        private void GenerateForVertexes(List<int> numVertexes, int quantity)
        {
            for (int i = 0; i < quantity; i++)
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
