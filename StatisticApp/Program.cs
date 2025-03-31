using ClosedXML.Excel;
using ExampleGenerator;
using GraphOptimisation;
using GraphPartitionAccurate;
using GraphPartitionClass;
using GraphReduction;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

public class Program
{
    public class GraphData
    {
        /// <summary>
        /// Граф задан в виде матрицы смежности.
        /// </summary>
        public int[][] graph { get; set; }

        /// <summary>
        /// Критерий Q для данного графа и его разбиения.
        /// </summary>
        public int q { get; set; }

        public GraphData(int[][] graph, int q)
        {
            this.graph = graph;
            this.q = q;
        }
    }

    /// <summary>
    /// Сгенерировать файлы данных для разного размера графов.
    /// </summary>
    /// <param name="folderPath">Путь к папке, в которую будут сохранены файлы.</param>
    /// <param name="numbersOfVerts">Массив размеров графов (количество вершин).</param>
    /// <param name="numberOfGraphs">Количество генерируемых графов для каждого размера.</param>
    public static void GenBase(string folderPath, int[] numbersOfVerts, int numberOfGraphs)
    {
        Generator gen = new Generator();

        foreach (var v in numbersOfVerts)
        {
            System.IO.Directory.CreateDirectory(folderPath + @"\Json");
            System.IO.Directory.CreateDirectory(folderPath + @"\Txt");
            System.IO.Directory.CreateDirectory(folderPath + @"\Bin");

            for (int i = 1; i <= numberOfGraphs; i++)
            {
                int q = i + 1;
                var t = gen.Generate(v, q);
                GraphData d = new GraphData(t, q);

                // Запись JSON файла
                using (FileStream fs = new FileStream(folderPath + @$"\Json\{v}.{i}.json", FileMode.OpenOrCreate))
                {
                    string json = JsonSerializer.Serialize(d);
                    byte[] buffer = Encoding.Default.GetBytes(json);
                    fs.Write(buffer, 0, buffer.Length);
                }

                // Запись TXT файла
                using (FileStream fs = new FileStream(folderPath + @$"\Txt\{v}.{i}.txt", FileMode.OpenOrCreate))
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
                    buffer = Encoding.Default.GetBytes(q.ToString());
                    fs.Write(buffer, 0, buffer.Length);
                }

                // Запись BIN файла
                using (FileStream fs = new FileStream(folderPath + @$"\Bin\{v}.{i}.bin", FileMode.OpenOrCreate))
                {
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(d.graph.Length);
                    foreach (var row in d.graph)
                    {
                        bw.Write(row.Length);
                        foreach (var elem in row)
                        {
                            bw.Write(elem);
                        }
                    }
                    bw.Write(d.q);
                }
            }
        }
    }

    /// <summary>
    /// Парсить все BIN файлы из указанной папки и возвращать список данных графов.
    /// </summary>
    /// <param name="path">Путь к папке, содержащей BIN файлы.</param>
    /// <returns>Список объектов GraphData с данными из BIN файлов.</returns>
    public static List<GraphData> ParseBin(string path)
    {
        List<string> files = new List<string>(Directory.GetFiles(path, "*.bin"));
        List<GraphData> result = new List<GraphData>();
        foreach (var file in files)
        {
            using (FileStream fs = new FileStream(file, FileMode.Open))
            {
                BinaryReader br = new BinaryReader(fs);
                int[][] graph = new int[br.ReadInt32()][];
                for (int j = 0; j < graph.Length; j++)
                {
                    graph[j] = new int[br.ReadInt32()];
                    for (int u = 0; u < graph[j].Length; u++)
                    {
                        graph[j][u] = br.ReadInt32();
                    }
                }
                int q = br.ReadInt32();
                GraphData d = new GraphData(graph, q);
                if (d != null)
                {
                    result.Add(d);
                }
            }
        }
        return result;
    }

    /// <summary>
    /// Парсить все JSON файлы из указанной папки и возвращать список данных графов.
    /// </summary>
    /// <param name="path">Путь к папке, содержащей JSON файлы.</param>
    /// <returns>Список объектов GraphData с данными из JSON файлов.</returns>
    public static List<GraphData> ParseJson(string path)
    {
        List<string> files = new List<string>(Directory.GetFiles(path, "*.json"));
        List<GraphData> result = new List<GraphData>();
        foreach (var file in files)
        {
            string jsonContent = File.ReadAllText(file);
            GraphData data = JsonSerializer.Deserialize<GraphData>(jsonContent);
            if (data != null)
            {
                result.Add(data);
            }
        }
        return result;
    }

    /// <summary>
    /// Вычислить критерий Q для заданного графа и раскраски.
    /// </summary>
    /// <param name="graph">Матрица смежности графа.</param>
    /// <param name="x">Разбиение графа.</param>
    /// <returns>Критерий Q.</returns>
    public static int Q(int[][] graph, int[] x)
    {
        int q = 0;
        for (int i = 0; i < graph.Length; ++i)
        {
            for (int j = 0; j < graph[i].Length; ++j)
            {
                q += x[i] != x[graph[i][j]] ? 1 : 0;
            }
        }
        return q / 2;
    }

    /// <summary>
    /// Удалить все файлы из указанной папки и её подпапок.
    /// </summary>
    /// <param name="path">Путь к папке, из которой нужно удалить.</param>
    public static void NukeDirectory(string path)
    {
        System.IO.DirectoryInfo di = new DirectoryInfo(path);

        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete();
        }
        foreach (DirectoryInfo dir in di.GetDirectories())
        {
            dir.Delete(true);
        }
    }

    public static void Main()
    {
        string path = ConfigurationManager.AppSettings["DBPath"] ?? "";
        if (String.IsNullOrEmpty(path))
            return;
        //NukeDirectory(path);
        //GenBase(path,  new int[]{ 20, 200, 2000, 20000, 50000 }, 2);

        List<GraphData> graphs = ParseBin(path + @"\Bin");

        var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("Результаты");

        int row = 1;

        worksheet.Cell(row, 1).Value = "Размер графа";
        worksheet.Cell(row, 2).Value = "Ожидаемый критерий";
        worksheet.Cell(row, 3).Value = "Время выполнения (секунды)";
        worksheet.Cell(row, 4).Value = "Баланс разбиения";
        worksheet.Cell(row, 5).Value = "Найденный критерий";

        foreach (var graph in graphs)
        {
            row++;
            var gr = graph.graph;
            var q = graph.q;

            var watch = Stopwatch.StartNew();

            IGraphPartition grp = new Graph2Partition(new SimpleGraphReduction(new SimpleRestruct(), new SimpleCompress()),
                                                new BranchAndBoundsAlgorithm(),
                                                new SimpleGraphRestoration(new FiducciaMattheysesMethod()),
                                                new FiducciaMattheysesMethod());

            int[] answer = grp.GetPartition(gr);

            watch.Stop();

            var time = watch.ElapsedMilliseconds;
            var balance = (double)answer.Sum() / gr.Length;
            var qReal = Q(gr, answer);

            worksheet.Cell(row, 1).Value = gr.Length;
            worksheet.Cell(row, 2).Value = q;
            worksheet.Cell(row, 3).Value = Math.Round((double)time / 1000, 2).ToString() + "s";
            worksheet.Cell(row, 4).Value = Math.Round(balance, 3);
            worksheet.Cell(row, 5).Value = qReal;
        }

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(path + @"\result.xlsx");
    }
}