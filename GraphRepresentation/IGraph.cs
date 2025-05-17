namespace GraphRepresentation
{
    public interface IGraph
    {
        /// <summary>
        /// Метод для получения номера вершины по индексу для перебора
        /// </summary>
        /// <param name="vertexNum">Номер вершины</param>
        /// <param name="adjacentNum">Индекс смежной веришины</param>
        /// <returns></returns>
        int this[int vertexNum, int adjacentNum] { get; }

        /// <summary>
        /// Возвращает кол-во вершин
        /// </summary>
        int CountVertecies { get; }

        /// <summary>
        /// Возвращает кол-во рёбер
        /// </summary>
        int CountEdges { get; }

        /// <summary>
        /// Возвращает вес всего графа
        /// </summary>
        int GraphWeight { get; }

        /// <summary>
        /// Метод для получения степени веришны
        /// </summary>
        /// <param name="vertexNum">Номер вершины</param>
        /// <returns>Степень вершины</returns>
        int GetVertexDegree(int vertexNum);

        /// <summary>
        /// Метод для получения веса вершины
        /// </summary>
        /// <param name="vertexNum">Номер вершины</param>
        /// <returns>Вес вершины</returns>
        int GetVertexWeight(int vertexNum);

        /// <summary>
        /// Метод для получения веса ребра, если оно есть
        /// </summary>
        /// <param name="vertexNumStart">Начальная веришна ребра</param>
        /// <param name="vertexNumEnd">Конечная вершина ребра</param>
        /// <returns>Вес ребра</returns>
        int GetEdgeWeight(int vertexNumStart, int vertexNumEnd);

        /// <summary>
        /// Метод для получения массива всех вершин смежных с данной
        /// </summary>
        /// <param name="vertexNum">Номер нужной вершины</param>
        /// <returns>Массив смежных вершин</returns>
        int[] GetAdjacentVertecies(int vertexNum);

        /// <summary>
        /// Метод для получения разреза графа по разбиению
        /// </summary>
        /// <param name="partition">Разбиение графа</param>
        /// <returns>Разрез графа</returns>
        int GetGraphCut(int[] partition);

        /// <summary>
        /// Метод для получения баланса графа по разбиению
        /// </summary>
        /// <param name="partition">Разбиение графа</param>
        /// <returns>Кортеж из правой и левой части баланса</returns>
        (int left, int right) GetGraphBalance(int[] partition);
    }
}
