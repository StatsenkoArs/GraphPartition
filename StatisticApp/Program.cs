using ExampleGenerator;
using GraphOptimisation;
using GraphPartitionAccurate;
using GraphPartitionClass;
using GraphReduction;
using Spire.Xls;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

public class Program
{
    public class GraphData
    {
        public int[][] graph { get; set; }
        public int q { get; set; }

        public GraphData(int[][] graph, int q)
        {
            this.graph = graph;
            this.q = q;
        }
    }

    public static void GenBase(string folderPath)
    {
        int[] vert = { 20, 200, 1000, 2000, 20000 };
        Generator gen = new Generator();

        foreach (var v in vert)
        {
            System.IO.Directory.CreateDirectory(folderPath + @"\Json");
            System.IO.Directory.CreateDirectory(folderPath + @"\Txt");
            for (int i = 1; i <= 2; i++)
            {
                int q = i + 1;
                var t = gen.Generate(v, 2 * v, q);
                using (FileStream fs = new FileStream(folderPath + @$"\Json\{v}.{i}.json", FileMode.OpenOrCreate))
                {
                    GraphData d = new GraphData(t, q);
                    string json = JsonSerializer.Serialize(d);
                    byte[] buffer = Encoding.Default.GetBytes(json);
                    fs.Write(buffer, 0, buffer.Length);
                }
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
            }
        }
    }

    public static List<GraphData> Parse(string path)
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

    public static void Main()
    {
        string path = @"C:\Users\Глеб\source\repos\GraphPartition\GraphDB";
        //GenBase(path);
        List<GraphData> graphs = Parse(path + @"\Json");

        Workbook workbook = new Workbook();

        //Получение первой рабочей страницы
        Worksheet worksheet = workbook.Worksheets[0];

        int row = 1;

        worksheet.Range[row, 1].Value = "Размер графа";
        worksheet.Range[row, 2].Value = "Ожидаемый критерий";
        worksheet.Range[row, 3].Value = "Время выполнения";
        worksheet.Range[row, 4].Value = "Баланс";
        worksheet.Range[row, 5].Value = "Найденный критерий";
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

            worksheet.Range[row, 1].Value = gr.Length.ToString();
            worksheet.Range[row, 2].Value = q.ToString();
            worksheet.Range[row, 3].Value = time.ToString() + "ms";
            worksheet.Range[row, 4].Value = Math.Round(balance, 3).ToString();
            worksheet.Range[row, 5].Value = qReal.ToString();
        }
        worksheet.AllocatedRange.AutoFitColumns();

        CellStyle style = workbook.Styles.Add("newStyle");
        style.Font.IsBold = true;
        worksheet.Range[1, 1, 1, 4].Style = style;

        workbook.SaveToFile(path + @"\Results.xlsx", ExcelVersion.Version2016);
    }
}