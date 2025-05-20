namespace GraphAndFiles
{
    public static class GraphBin
    {
        /// <summary>
        /// Запись в бинарник
        /// </summary>
        /// <param name="folderPath">путь до папки записи</param>
        /// <param name="graphData">данные графа</param>
        /// <param name="vertexes">вершин в </param>
        /// <param name="indexOfGraph"></param>
        public static void WriteToBin(string folderPath, GraphData graphData, string fileName = "graph")
        {
            using FileStream fs = new FileStream(folderPath + @$"\Bin\{fileName}.bin", FileMode.OpenOrCreate);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(graphData.graph.Length);
            foreach (var row in graphData.graph)
            {
                bw.Write(row.Length);
                foreach (var elem in row)
                {
                    bw.Write(elem);
                }
            }
            bw.Write(graphData.q);
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
                using FileStream fs = new FileStream(file, FileMode.Open);
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
            return result;
        }
    }
}