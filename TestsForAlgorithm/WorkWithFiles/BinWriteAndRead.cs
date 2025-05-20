using GraphAndFiles;

namespace TestsForAlgorithm.WorkWithFiles;

[TestClass]
public class GraphBinTests
{
    private string _testFolderPath;
    private string _binFolderPath;

    [TestInitialize]
    public void Setup()
    {
        // Создаем временную папку для тестов
        _testFolderPath = Path.Combine(Path.GetTempPath(), "GraphBinTests", Guid.NewGuid().ToString());
        _binFolderPath = Path.Combine(_testFolderPath, "Bin");
        Directory.CreateDirectory(_binFolderPath);
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Удаляем временную папку после теста
        if (Directory.Exists(_testFolderPath))
        {
            Directory.Delete(_testFolderPath, true);
        }
    }

    [TestMethod]
    public void WriteToBin_ValidData_WritesCorrectly()
    {
        // Arrange
        var graphData = new GraphData(new int[][] { new int[] { 1, 2 }, new int[] { 3, 4 } }, 5);
        string fileName = "testGraph";
        string expectedFilePath = Path.Combine(_binFolderPath, fileName + ".bin");

        // Act
        GraphBin.WriteToBin(_testFolderPath, graphData, fileName);

        // Assert
        Assert.IsTrue(File.Exists(expectedFilePath));

        using (var fs = new FileStream(expectedFilePath, FileMode.Open))
        using (var br = new BinaryReader(fs))
        {
            int rowCount = br.ReadInt32();
            Assert.AreEqual(2, rowCount);

            for (int i = 0; i < rowCount; i++)
            {
                int colCount = br.ReadInt32();
                Assert.AreEqual(2, colCount);

                for (int j = 0; j < colCount; j++)
                {
                    int value = br.ReadInt32();
                    Assert.AreEqual(graphData.graph[i][j], value);
                }
            }

            int q = br.ReadInt32();
            Assert.AreEqual(5, q);
        }
    }

    [TestMethod]
    public void ParseBin_ValidFiles_ReturnsCorrectGraphDataList()
    {
        // Arrange
        var graphData1 = new GraphData(new int[][] { new int[] { 1, 2 } }, 10);
        var graphData2 = new GraphData(new int[][] { new int[] { 3, 4 }, new int[] { 5 } }, 20);

        // Записываем два тестовых файла
        GraphBin.WriteToBin(_testFolderPath, graphData1, "graph1");
        GraphBin.WriteToBin(_testFolderPath, graphData2, "graph2");

        // Act
        var result = GraphBin.ParseBin(_binFolderPath);

        // Assert
        Assert.AreEqual(2, result.Count);

        // Проверка первого графа
        Assert.AreEqual(1, result[0].graph.Length);
        Assert.AreEqual(2, result[0].graph[0].Length);
        Assert.AreEqual(1, result[0].graph[0][0]);
        Assert.AreEqual(2, result[0].graph[0][1]);
        Assert.AreEqual(10, result[0].q);

        // Проверка второго графа
        Assert.AreEqual(2, result[1].graph.Length);
        Assert.AreEqual(2, result[1].graph[0].Length);
        Assert.AreEqual(3, result[1].graph[0][0]);
        Assert.AreEqual(4, result[1].graph[0][1]);
        Assert.AreEqual(1, result[1].graph[1].Length);
        Assert.AreEqual(5, result[1].graph[1][0]);
        Assert.AreEqual(20, result[1].q);
    }

    [TestMethod]
    public void ParseBin_NoFiles_ReturnsEmptyList()
    {
        // Arrange
        string emptyBinPath = Path.Combine(_testFolderPath, "EmptyBin.bin");
        Directory.CreateDirectory(emptyBinPath);

        // Act
        var result = GraphBin.ParseBin(emptyBinPath);

        // Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void ParseBin_InvalidFile_ThrowsException()
    {
        // Arrange
        string invalidFilePath = Path.Combine(_binFolderPath, "invalid.bin");
        File.WriteAllBytes(invalidFilePath, new byte[] { 0x01, 0x02 }); // Некорректные данные

        // Act & Assert
        Assert.ThrowsException<EndOfStreamException>(() =>
            GraphBin.ParseBin(_binFolderPath)
        );
    }
}