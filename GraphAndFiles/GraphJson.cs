using System.Text;
using System.Text.Json;

namespace GraphAndFiles
{
    public static class GraphJson
    {
        /// <summary>
        /// Запись графа в json
        /// </summary>
        /// <param name="folderPath">путь до папки</param>
        /// <param name="graphData">данные графа</param>
        /// <param name="fileName">имя файла</param>
        public static void WriteToJson(string folderPath, GraphData graphData, string fileName = "graph")
        {
            using FileStream fs = new FileStream(folderPath + @$"\Json\{fileName}.json", FileMode.OpenOrCreate);
            string json = JsonSerializer.Serialize(graphData);
            byte[] buffer = Encoding.Default.GetBytes(json);
            fs.Write(buffer, 0, buffer.Length);
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
    }
}