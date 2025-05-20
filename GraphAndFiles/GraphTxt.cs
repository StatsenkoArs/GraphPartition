using System.Text;

namespace GraphAndFiles
{
    public static class GraphTxt
    {
        /// <summary>
        /// Запись в txt
        /// </summary>
        /// <param name="folderPath">путь до папки</param>
        /// <param name="graphData">данные графа</param>
        /// <param name="fileName">название файла</param>
        public static void WriteToTxt(string folderPath, GraphData graphData, string fileName = "graph")
        {
            using FileStream fs = new FileStream(folderPath + @$"\Txt\{fileName}.txt", FileMode.OpenOrCreate);
            string txt = "";
            for (int j = 0; j < graphData.graph.Length; j++)
            {
                txt += j.ToString() + ": ";
                for (int u = 0; u < graphData.graph[j].Length; u++)
                {
                    txt += graphData.graph[j][u].ToString() + " ";
                }
                txt += "\n";
            }
            byte[] buffer = Encoding.Default.GetBytes(txt);
            fs.Write(buffer, 0, buffer.Length);
            buffer = Encoding.Default.GetBytes(graphData.q.ToString());
            fs.Write(buffer, 0, buffer.Length);
        }
    }
}