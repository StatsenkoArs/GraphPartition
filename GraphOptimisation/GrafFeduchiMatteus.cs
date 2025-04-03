using GraphRepresentation;

namespace GraphOptimisation
{
    public class FiducciaMattheysesMethod : IPartitionOptimisation
    {
        /// <summary>
        /// Метод Федуччи-Маттеуса
        /// </summary>
        /// <param name="graph"> граф </param>
        /// <param name="partition"> изначальное разделение </param>
        /// <param name="numberOfBlockedIterations"> Кол-во итераций, когда вершину двигать нельзя</param>
        /// <param name="limitDisbalance"> Верхняя граница дисбаланса </param>
        /// <returns>Вектор разделения на подграфы</returns>
        public int[] OptimisePartition(IGraph graph, int[] partition, int numberOfBlockedIterations, int limitDisbalance)
        {
            // Объявление полей и инициализация
            int[] currentPartition = partition; //Текущее разделение
            int[] rightPartition = new int[currentPartition.Length]; //Лучшее разделение
            for (int i = 0; i < currentPartition.Length; i++)
            {
                rightPartition[i] = currentPartition[i];
            }
            int currentCriterion = 0; //Текущий критерий
            int[] E_CountTornEdge = new int[currentPartition.Length]; // Количество разрезанных связей для каждой вершины
            int[] I_CountWholeEdge = new int[currentPartition.Length]; //Количество неразрезанных связей для каждой вершины
            int[] D_DifferenceEdge = new int[currentPartition.Length]; ; // D = E - I;
            int countElemInBiggerSubgraph = 0; //Кол-во элементов в первом подграфе
            for (int i = 0; i < graph.CountVertecies; i++)
            {
                for (int j = 0; j < graph.GetVertexDegree(i); j++)
                {
                    if (currentPartition[i] != currentPartition[graph[i, j]])
                    {
                        currentCriterion++;
                    }
                    //Считаем вектора I и E
                    if (currentPartition[i] == currentPartition[graph[i, j]])
                    {
                        I_CountWholeEdge[i]++;
                    }
                    else
                    {
                        E_CountTornEdge[i]++;
                    }
                }
                D_DifferenceEdge[i] = E_CountTornEdge[i] - I_CountWholeEdge[i];
                //Считаем кол-во вершин в подграфе
                if (currentPartition[i] == 0) countElemInBiggerSubgraph++;
            }
            currentCriterion /= 2;
            int rightCriterion = currentCriterion; //Лучший минимальный критерий

            float rightDisbalance = 0; //Лучший дисбаланс
            float currentDisbalance = 0; //Текущий дисбаланс
            if (partition.Length - countElemInBiggerSubgraph > countElemInBiggerSubgraph)
            {
                currentDisbalance = (graph.CountVertecies / 2) - countElemInBiggerSubgraph;
            }
            else
            {
                currentDisbalance = (graph.CountVertecies / 2) - (partition.Length - countElemInBiggerSubgraph);
            }
            rightDisbalance = currentDisbalance;
            int[] T_CountBlockingIterations = new int[graph.CountVertecies]; //Вектор кол-ва итераций для каждой вершины, когда вершину двигать нельзя
            int[] isVertexMoved = new int[graph.CountVertecies]; //Была ли тронута вершина
            int numberCurrentSubgraph = 0; //Номер текущего подграфа
            int iteration = 0; //номер итерации
            int indexMovedVertex = 0; //Номер перемещённой вершины
            int countBlockingIteration = numberOfBlockedIterations; //Кол-во итераций, когда вершину двигать нельзя. 

            do
            {
                if (iteration != 0)
                {
                    GetEID(E_CountTornEdge, I_CountWholeEdge, D_DifferenceEdge, currentPartition, graph, indexMovedVertex);
                }
                GetNumberBiggerSubgraph(ref numberCurrentSubgraph, currentPartition, countElemInBiggerSubgraph);
                MoveToAnotherSubgraph(ref indexMovedVertex, currentPartition, ref countElemInBiggerSubgraph, D_DifferenceEdge, T_CountBlockingIterations, numberCurrentSubgraph, iteration);
                NegativeIndexCheck(ref countElemInBiggerSubgraph, partition, indexMovedVertex);
                GetCriterion(ref currentCriterion, graph, currentPartition, indexMovedVertex);
                GetDisbalance(ref currentDisbalance, graph, currentPartition, countElemInBiggerSubgraph);
                MovedCheck(isVertexMoved, T_CountBlockingIterations, ref countBlockingIteration, indexMovedVertex, iteration);

                //Сравнение дисбаланса и критерия текущего с наилучшими
                if ((rightCriterion >= currentCriterion && limitDisbalance >= currentDisbalance && rightDisbalance >= currentDisbalance) || iteration == 0)
                {
                    (rightCriterion, rightDisbalance) = (currentCriterion, currentDisbalance);
                    for (int i = 0; i < currentPartition.Length; i++)
                    {
                        rightPartition[i] = currentPartition[i];
                    }
                }
                GetNumberBiggerSubgraph(ref numberCurrentSubgraph, currentPartition, countElemInBiggerSubgraph);
                iteration++;
            } while (IsVertexMoved(T_CountBlockingIterations, currentPartition, numberCurrentSubgraph, iteration));
            return rightPartition;
        }

        /// <summary>
        /// Полностью перебирает граф, возвращая оценку критерия (разрез) и дисбаланс
        /// </summary>
        /// <param name="criterion">Критерий</param>
        /// <param name="disbalance">Дисбаланс</param>
        /// <param name="graph">Граф</param>
        /// <param name="partition">Разбиение</param>
        public void Statistics(ref int criterion, ref float disbalance, IGraph graph, int[] partition)
        {
            (disbalance, criterion, int countElemInBiggerSubgraph) = (0, 0, 0);
            for (int i = 0; i < graph.CountVertecies; i++)
            {
                for (int j = 0; j < graph.GetVertexDegree(i); j++)
                {
                    //Считаем критерий
                    if (partition[i] != partition[graph[i, j]])
                    {
                    criterion++;
                    }
                }
                if (partition[i] == 0)
                {
                    countElemInBiggerSubgraph++;
                }
            }
            criterion /= 2;
            GetDisbalance(ref disbalance, graph, partition, countElemInBiggerSubgraph);
        }

        /// <summary>
        /// Возвращает критерий разреза
        /// </summary>
        /// <param name="criterion">Критерий который необходимо пересчитать</param>
        /// <param name="graph">Граф</param>
        /// <param name="partition">Разбиение</param>
        /// <param name="indexMovedVertex">Индекс движимой вершины</param>
        protected static void GetCriterion(ref int criterion, IGraph graph, int[] partition, int indexMovedVertex)
        {
            for (int i = 0; i < graph.GetVertexDegree(indexMovedVertex); i++)
            {
                criterion = (partition[indexMovedVertex] == partition[graph[indexMovedVertex, i]]) ? criterion-- : criterion++;
            }
        }

        /// <summary>
        /// Возвращает дисбаланс
        /// </summary>
        /// <param name="disbalance">Значение дисбаланса</param>
        /// <param name="graph">Граф</param>
        /// <param name="partition">Разбиение</param>
        /// <param name="countElemInBiggerSubgraph">Количество элементов в первом подграфе</param>
        protected static void GetDisbalance(ref float disbalance, IGraph graph, int[] partition, int countElemInBiggerSubgraph)
        {
            if (partition.Length - countElemInBiggerSubgraph > countElemInBiggerSubgraph)
            {
                disbalance = (graph.CountVertecies / 2) - countElemInBiggerSubgraph;
            }
            else
            {
                disbalance = (graph.CountVertecies / 2) - (partition.Length - countElemInBiggerSubgraph);
            }
        }

        /// <summary>
        /// Обновляет массивы E_CountTornEdge, I_CountWholeEdge и D
        /// </summary>
        /// <param name="E_CountTornEdge">Количество разрезанных связей для каждой вершины</param>
        /// <param name="I_CountWholeEdge">Количество целых связей для каждой вершины</param>
        /// <param name="D_DifferenceEdge">D = E - I</param>
        /// <param name="partition">разбиение</param>
        /// <param name="graph">граф</param>
        /// <param name="indexMovedVertex">Индекс движимой вершины</param>
        protected static void GetEID(int[] E_CountTornEdge, int[] I_CountWholeEdge, int[] D_DifferenceEdge, int[] partition, IGraph graph, int indexMovedVertex)
        {
            //Изменяется только то, что было тронуто. Остальное остаётся прежним.
            (E_CountTornEdge[indexMovedVertex], I_CountWholeEdge[indexMovedVertex]) = (I_CountWholeEdge[indexMovedVertex], E_CountTornEdge[indexMovedVertex]);
            for (int i = 0; i < graph.GetVertexDegree(indexMovedVertex); i++)
            {
                //Считаем вектора I и E
                if (partition[indexMovedVertex] == partition[graph[indexMovedVertex, i]])
                {
                    I_CountWholeEdge[graph[indexMovedVertex, i]]++;
                    E_CountTornEdge[graph[indexMovedVertex, i]]--;
                }
                else
                {
                    E_CountTornEdge[graph[indexMovedVertex, i]]++;
                    I_CountWholeEdge[graph[indexMovedVertex, i]]--;
                }
                //Считаем Д для соседей элемента indexMovedVertex
                D_DifferenceEdge[graph[indexMovedVertex, i]] = E_CountTornEdge[graph[indexMovedVertex, i]] - I_CountWholeEdge[graph[indexMovedVertex, i]];
            }
            //Считаем Д элемента indexMovedVertex
            D_DifferenceEdge[indexMovedVertex] = E_CountTornEdge[indexMovedVertex] - I_CountWholeEdge[indexMovedVertex];
        }

        /// <summary>
        /// Возвращает номер подграфа с наибольшим количеством элементов в нём
        /// </summary>
        /// <param name="numberPartition">номер подграфа</param>
        /// <param name="partition">разбиение</param>
        /// <param name="count">количество вершин в одном из подграфов</param>
        protected static void GetNumberBiggerSubgraph(ref int numberPartition, int[] partition, int count)
        {
            numberPartition = (count < partition.Length - count) ? 1 : 0;
        }

        /// <summary>
        /// Переносит одну вершину из одного подграфа в другой
        /// </summary>
        /// <param name="index">индекс движимой вершины</param>
        /// <param name="partition">разбиение</param>
        /// <param name="countElemInBiggerSubgraph">кол-во элементов в первом подграфе</param>
        /// <param name="D_DifferenceEdge">D = E - I</param>
        /// <param name="T_CountBlockingIterations">кол-во итераций для каждой вершины, когда вершину двигать нельзя</param>
        /// <param name="numberCurrentSubgraph">номер текущего подграфа</param>
        /// <param name="iteration">номер итерации</param>
        protected static void MoveToAnotherSubgraph(ref int index, int[] partition, ref int countElemInBiggerSubgraph, int[] D_DifferenceEdge, int[] T_CountBlockingIterations, int numberCurrentSubgraph, int iteration)
        {
            //Находим элеменет, который будет перемещён в другой подграф
            bool flag = false; //флаг на первый элменет
            int maxInD = 0; //индекс максимального элемента в D
            index = -1;
            for (int i = 0; i < D_DifferenceEdge.Length; i++)
            {
                if (!flag && partition[i] == numberCurrentSubgraph && T_CountBlockingIterations[i] <= iteration) //Находит первый элемент из нужного подграфа
                {
                    (maxInD, index, flag) = (D_DifferenceEdge[i], i, true);
                }
                if (D_DifferenceEdge[i] > maxInD && partition[i] == numberCurrentSubgraph && T_CountBlockingIterations[i] <= iteration) //Ищет максимум
                {
                    (maxInD, index) = (D_DifferenceEdge[i], i);
                }
            }
        }
        protected static void NegativeIndexCheck(ref int countElemInBiggerSubgraph, int[] partition, int index)
        {
            try
            {
                // Меняем номер подграфа у выбранного элемента
                if (partition[index] == 1)
                {
                    partition[index] = 0;
                    countElemInBiggerSubgraph++;
                }
                else
                {
                    partition[index] = 1;
                    countElemInBiggerSubgraph--;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Помечает двинутую вершину и по необходимости изменяет номер итерации, на которой вершина становится открытой для пермещения
        /// </summary>
        /// <param name="isVertexMoved">Массив тронутых вершин</param>
        /// <param name="T_CountBlockingIterations">Массив итераций для передвижения</param>
        /// <param name="countBlockingIteration">Число итераций, когда вершину двигать нельзя </param>
        /// <param name="indexMovedVertex">индекс движимой вершины</param>
        /// <param name="iteration">номер итерации</param>
        protected static void MovedCheck(int[] isVertexMoved, int[] T_CountBlockingIterations, ref int countBlockingIteration, int indexMovedVertex, int iteration)
        {
            if (isVertexMoved[indexMovedVertex] != 0)
            {
                countBlockingIteration++;
                T_CountBlockingIterations[indexMovedVertex] = iteration + countBlockingIteration;
            }
            else
            {
                T_CountBlockingIterations[indexMovedVertex] = iteration + countBlockingIteration;
                isVertexMoved[indexMovedVertex]++;
            }
        }

        /// <summary>
        /// Проверка на то, возможно ли ещё взаимодействовать с вершинами графа
        /// </summary>
        /// <param name="isVertexMoved">индекс движимой вершины</param>
        /// <param name="T_CountBlockingIterations">Массив итераций для передвижения</param>
        /// <param name="partition">разбиение</param>
        /// <param name="numberCurrentSubgraph">номер текущего подграфа</param>
        /// <returns>Да, если ещё можно взаимодейстовать. Нет, иначе</returns>
        protected static bool IsVertexMoved(int[] T_CountBlockingIterations, int[] partition, int numberCurrentSubgraph, int iteration)
        {
            for (int i = 0; i < T_CountBlockingIterations.Length; i++)
            {
                if (T_CountBlockingIterations[i] <= iteration && partition[i] == numberCurrentSubgraph)
                return true;
            }
            return false;
        }
    }
}
