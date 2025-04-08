namespace GraphAndFiles
{
    /// <summary>
    /// Класс для записи данных о графе в файл
    /// </summary>
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
}