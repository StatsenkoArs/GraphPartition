using GraphRepresentation;

namespace GraphOptimisation
{
    public class FiducciaMattheysesMethod : IPartitionOptimisation
    {

        private int[] rightPartition; //Лучшее разделение
        private int[] currentPartition; //Текущее разделение

        private int[] E_CountTornEdge; // Количество разрезанных связей для каждой вершины
        private int[] I_CountWholeEdge; //Количество неразрезанных связей для каждой вершины
        private int[] D; // D = E - I;
        private int countBlockingIteration; //Кол-во итераций, когда вершину двигать нельзя. 
        private int[] T_CountBlockingIterations; //Вектор кол-ва итераций для каждой вершины, когда вершину двигать нельзя
        private int[] isVertexMoved; //Была ли тронута вершина
        private int numberPartition = 0; //Номер текущего подграфа

        private int rightCriterion = 0; //Лучший минимальный критерий
        private int currentCriterion = 0; //Текущий критерий

        private float rightDisbalance = 0; //Лучший дисбаланс
        private float currentDisbalance = 0; //Текущий дисбаланс

        private int iteration = 0;
        private int indexMovedVertex = 0; //Номер перемещённой вершины
        private int countElemInFirstSubgraph = 0; //Кол-во элементов в первом подграфе


        /// <summary>
        /// Метод Федуччи-Маттеуса
        /// </summary>
        /// <param name="graph"> граф </param>
        /// <param name="partition"> изначальное разделение </param>
        /// <param name="numberOfBlockedIterations"> Кол-во итераций, когда вершину двигать нельзя</param>
        /// <returns>Вектор разделения на подграфы</returns>
        public int[] OptimisePartition(IGraph graph, int[] partition, int numberOfBlockedIterations)
        {
            // блок инициализации
            this.currentPartition = partition;
            rightPartition = new int[currentPartition.Length];
            for (int i = 0; i < rightPartition.Length; i++)
            {
                rightPartition[i] = currentPartition[i];
            }
            T_CountBlockingIterations = new int[graph.CountVertecies];
            isVertexMoved = new int[graph.CountVertecies];
            this.countBlockingIteration = numberOfBlockedIterations;
            iteration = 0;
            numberPartition = 0;
            countElemInFirstSubgraph = 0;


            do
            {
                UpdateVectorsEID(graph);
                //Первый подграф - подграф с большим кол-вом вершин.
                if (countElemInFirstSubgraph < currentPartition.Length - countElemInFirstSubgraph)
                {
                    numberPartition = 1;
                }
                else numberPartition = 0;

                if (iteration == 0)
                    CriterionAndDisbalanceCheck(graph);

                //Находим элеменет, который будет перемещён в другой подграф
                bool flag = false; //флаг на первый элменет
                int maxInD = 0; //индекс максимального элемента в D
                indexMovedVertex = -1;
                for (int i = 0; i < D.Length; i++)
                {
                    if (!flag && currentPartition[i] == numberPartition && T_CountBlockingIterations[i] == 0) //Находит первый элемент из нужного подграфа
                    {
                        maxInD = D[i];
                        indexMovedVertex = i;
                        flag = true;
                    }

                    if (D[i] > maxInD && currentPartition[i] == numberPartition && T_CountBlockingIterations[i] == 0) //Ищет максимум
                    {
                        maxInD = D[i];
                        indexMovedVertex = i;
                    }
                }

                // Меняем номер подграфа у выбранного элемента
                if (currentPartition[indexMovedVertex] == 1)
                {
                    currentPartition[indexMovedVertex] = 0;
                    countElemInFirstSubgraph++;
                }
                else
                {
                    currentPartition[indexMovedVertex] = 1;
                    countElemInFirstSubgraph--;
                }

                MovedCheck();
                CriterionAndDisbalanceCheck(graph);
                //Сравнение дисбаланса и критерия текущего с наилучшими
                if (rightCriterion >= currentCriterion && rightDisbalance >= currentDisbalance)
                {
                    rightCriterion = currentCriterion;
                    rightDisbalance = currentDisbalance;
                    for (int i = 0; i < currentPartition.Length; i++)
                    {
                        rightPartition[i] = currentPartition[i];
                    }
                }

                iteration++;

                if (countElemInFirstSubgraph < currentPartition.Length - countElemInFirstSubgraph)
                {
                    numberPartition = 1;
                }
                else numberPartition = 0;
            } while (IsVertexMoved());
            return rightPartition;
        }



        /// <summary>
        /// Помечает двинутую вершину и по необходимости изменяет кол-во итераций, которое вершина не может быть тронута
        /// </summary>
        private void MovedCheck()
        {
            for (int i = 0; i < T_CountBlockingIterations.Length; i++)
            {
                if (T_CountBlockingIterations[i] != 0) T_CountBlockingIterations[i]--;
            }

            if (isVertexMoved[indexMovedVertex] != 0)
            {
                countBlockingIteration++;
                T_CountBlockingIterations[indexMovedVertex] = countBlockingIteration;
            }
            else
            {
                T_CountBlockingIterations[indexMovedVertex] = countBlockingIteration;
                isVertexMoved[indexMovedVertex]++;
            }

        }

        /// <summary>
        /// Считает значение критерия и дисбаланса
        /// </summary>
        /// <param name="graph"></param>
        private void CriterionAndDisbalanceCheck(IGraph graph)
        {
            if (iteration == 0)
            {
                currentDisbalance = 0;
                currentCriterion = 0;

                for (int i = 0; i < graph.CountVertecies; i++)
                {
                    for (int j = 0; j < graph.GetVertexDegree(i); j++)
                    {
                        //Считаем критерий
                        if (currentPartition[i] != currentPartition[graph[i, j]])
                            currentCriterion++;
                    }
                }
                currentCriterion /= 2;
                rightCriterion = currentCriterion;

                if (currentPartition.Length - countElemInFirstSubgraph > countElemInFirstSubgraph)
                    currentDisbalance = graph.CountVertecies / 2 - countElemInFirstSubgraph;
                else
                    currentDisbalance = graph.CountVertecies / 2 - (currentPartition.Length - countElemInFirstSubgraph);
                rightDisbalance = currentDisbalance;
            }
            else
            {
                for (int i = 0; i < graph.GetVertexDegree(indexMovedVertex); i++)
                {
                    if (currentPartition[indexMovedVertex] == currentPartition[graph[indexMovedVertex, i]])
                    {
                        currentCriterion--;
                    }
                    else
                    {
                        currentCriterion++;
                    }
                }

                if (currentPartition.Length - countElemInFirstSubgraph > countElemInFirstSubgraph)
                    currentDisbalance = graph.CountVertecies / 2 - countElemInFirstSubgraph;
                else
                    currentDisbalance = graph.CountVertecies / 2 - (currentPartition.Length - countElemInFirstSubgraph);

            }
        }



        /// <summary>
        /// Обновление векторов E, I, D 
        /// </summary>
        /// <param name="graph"></param>
        private void UpdateVectorsEID(IGraph graph)
        {
            //На первой итерии полный перебор графа, мощность О(n)
            if (iteration == 0)
            {
                E_CountTornEdge = new int[graph.CountVertecies];
                I_CountWholeEdge = new int[graph.CountVertecies];
                D = new int[graph.CountVertecies];
                for (int i = 0; i < graph.CountVertecies; i++)
                {
                    for (int j = 0; j < graph.GetVertexDegree(i); j++)
                    {
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


                    D[i] = E_CountTornEdge[i] - I_CountWholeEdge[i];

                    //Считаем кол-во вершин в подграфе
                    if (currentPartition[i] == 0) countElemInFirstSubgraph++;
                }
            }
            else
            {
                //Изменяется только то, что было тронуто. Остальное остаётся прежним.
                (E_CountTornEdge[indexMovedVertex], I_CountWholeEdge[indexMovedVertex]) = (I_CountWholeEdge[indexMovedVertex], E_CountTornEdge[indexMovedVertex]);
                for (int i = 0; i < graph.GetVertexDegree(indexMovedVertex); i++)
                {
                    //Считаем вектора I и E
                    if (currentPartition[indexMovedVertex] == currentPartition[graph[indexMovedVertex, i]])
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
                    D[graph[indexMovedVertex, i]] = E_CountTornEdge[graph[indexMovedVertex, i]] - I_CountWholeEdge[graph[indexMovedVertex, i]];
                }

                //Считаем Д элемента indexMovedVertex
                D[indexMovedVertex] = E_CountTornEdge[indexMovedVertex] - I_CountWholeEdge[indexMovedVertex];
            }
        }

        /// <summary>
        /// Проверка на то, возможно ли ещё взаимодействовать с вершинами графа
        /// </summary>
        /// <returns>Да, если ещё можно взаимодейстовать. Нет, иначе.</returns>
        private bool IsVertexMoved()
        {
            for (int i = 0; i < isVertexMoved.Length; i++)
            {
                if (T_CountBlockingIterations[i] == 0 && currentPartition[i] == numberPartition)
                    return true;
            }
            return false;
        }

    }
}
