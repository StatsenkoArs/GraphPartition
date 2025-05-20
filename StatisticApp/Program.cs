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
using GraphAndFiles;
using static System.IO.Directory;

public class Program
{
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
            CreateDirectory(folderPath + @"\Json");
            CreateDirectory(folderPath + @"\Txt");
            CreateDirectory(folderPath + @"\Bin");

            for (int i = 1; i <= numberOfGraphs; i++)
            {
                int q = i % 5 + 40;
                var temp = gen.Generate(v, q);
                GraphData graphData = new GraphData(temp, q);

                // Запись TXT файла
                GraphTxt.WriteToTxt(folderPath, graphData, $"{v}.{q}.{i}");

                // Запись BIN файла
                GraphBin.WriteToBin(folderPath, graphData, $"{v}.{q}.{i}");

                Console.WriteLine($"Создан граф {v}.{q}.{i}; {DateTime.Now}");
            }
        }
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
        int[] numOfVerts = (ConfigurationManager.AppSettings["NumOfVerts"] ?? "0").Split().Select(o => int.Parse(o)).ToArray();
        int numOfGraphs = int.Parse(ConfigurationManager.AppSettings["NumOfGraphs"] ?? "0");
        if (String.IsNullOrEmpty(path))
            return;
        NukeDirectory(path);
        GenBase(path,  numOfVerts, numOfGraphs);

        List<GraphData> graphs = GraphBin.ParseBin(path + @"\Bin");

        var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("Результаты");

        int row = 1;

        worksheet.Cell(row, 1).Value = "Размер графа";
        worksheet.Cell(row, 2).Value = "Ожидаемое значение критерия";
        worksheet.Cell(row, 3).Value = "Время выполнения (секунды)";
        worksheet.Cell(row, 4).Value = "Баланс разбиения";
        worksheet.Cell(row, 5).Value = "Найденное значение критерия";
        worksheet.Cell(row, 6).Value = "Отклонение значения критерия в %";

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

            Console.WriteLine($"{row - 1}: Граф из {gr.Length} вершин, разбит за {Math.Round((double)time / 1000, 2).ToString()}c; {DateTime.Now}");

            worksheet.Cell(row, 1).Value = gr.Length;
            worksheet.Cell(row, 2).Value = q;
            worksheet.Cell(row, 3).Value = Math.Round((double) time / 1000, 2);
            worksheet.Cell(row, 4).Value = Math.Round(balance, 3);
            worksheet.Cell(row, 5).Value = qReal;
            worksheet.Cell(row, 6).Value = Math.Round((double) qReal / q, 4) * 100;
        }

        worksheet.Columns().AdjustToContents();

        workbook.SaveAs(path + @"\result.xlsx");

        var processStartInfo = new ProcessStartInfo
        {
            FileName = "explorer.exe",
            Arguments = path,
            UseShellExecute = true
        };

        Process.Start(processStartInfo);
    }
}