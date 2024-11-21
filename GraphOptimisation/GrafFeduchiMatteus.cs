namespace GraphOptimisation
{
     
public class FiducciaMattheysesMethod
{
    private int[][] graph; //Граф
    private int countVertex = 0; // Кол-во вершин графа
    private int[] E_CountTornEdge; // Количество разрезанных связей для каждой вершины
    private int[] I_CountWholeEdge; //Количество неразрезанных связей для каждой вершины
    private int[] D; // D = E - I;
    private int countBlockingIteration; //Кол-во итераций, когда вершину двигать нельзя. 
    private int[] T_CountBlockingIterations; //Вектор кол-ва итераций для каждой вершины, когда вершину двигать нельзя
    private int[] isVertexMoved; //Была ли тронута вершина
    private int numberPartition = 0; //Номер текущего подграфа
    private int[] rightPartition; //Лучшее разделение
    private int[] currentPartition; //Текущее разделение
    private int rightCriterion; //Лучший минимальный критерий
    private int currentCriterion; //Текущий критерий
    private int iteration = 0;
    private int indexMovedVertex; //Номер перемещённой вершины

    /// <summary>
    /// Конструктор, инициализирует граф, вектор разделения, вектор итогового разделения, вектор-табу и вектор проверки на сдвиг вершин
    /// </summary>
    /// <param name="graph">Граф</param>
    /// <param name="currentPartition">Вектор разделения на подграфы</param>
    public FiducciaMattheysesMethod(int[][] graph, int[] currentPartition)
    {
        this.graph = graph;
        this.currentPartition = currentPartition;
        rightPartition = new int[currentPartition.Length];
        rightPartition = currentPartition;
        T_CountBlockingIterations = new int[graph.Length];
        isVertexMoved = new int[graph.Length];
    }

    /// <summary>
    /// Метод Федуччи-Маттеуса
    /// </summary>
    /// <param name="countBlockingIteration">Пользовательское кол-во итераций, когда вершину двигать нельзя</param>
    /// <returns>Вектор разделения на подграфы</returns>
    public int[] FiducciaMattheyses(int countBlockingIteration)
    {
        this.countBlockingIteration = countBlockingIteration;
        
        while (IsVertexMoved())
        {
            UpdateData();
            int maxInD; //Максимальное число их вектора Д
            //Находим элменет, который будет перемещён в другой подграф
            if (iteration == 0)
            {
                maxInD = D[0];
                indexMovedVertex = 0;
                for (int i = 1; i < D.Length; i++)
                {
                    if (D[i] > maxInD)
                    {
                        maxInD = D[i];
                        indexMovedVertex = i;
                    }
                }
                numberPartition = currentPartition[indexMovedVertex];
            }
            else
            {
                bool flag = false;
                maxInD = 0;
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
            }
            //Меняем номер подграфа у выбранного элемента 
            if (currentPartition[indexMovedVertex] == 1)
            {
                currentPartition[indexMovedVertex] = 0;
                numberPartition = 0;
            }
            else
            {
                currentPartition[indexMovedVertex] = 1;
                numberPartition = 1;
            }

            MovedCheck();
            CriterionCheck();

            if (rightCriterion > currentCriterion)
            {
                rightCriterion = currentCriterion;
                for (int i = 0; i < currentPartition.Length; i++)
                {
                    rightPartition[i] = currentPartition[i];
                }

            }
            iteration++;
        }
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
    /// Пересчитывает критерий
    /// </summary>
    private void CriterionCheck()
    {
        if (iteration == 0)
        {
            currentCriterion = 0;
           
            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph[i].Length; j++)
                {
                    //Считаем критерий
                    if (currentPartition[i] != currentPartition[graph[i][j]])
                        currentCriterion++;
                }
            }
            currentCriterion /= 2;
            rightCriterion = currentCriterion;
        }
        else
        {
            for (int i = 0; i < graph[indexMovedVertex].Length; i++)
            {
                if (currentPartition[indexMovedVertex] == currentPartition[graph[indexMovedVertex][i]])
                {
                    currentCriterion--;
                }
                else
                {
                    currentCriterion++;
                }  
            }
        }
    }

    /// <summary>
    /// Считаем вектора I, E, D и кол-во вершин графа
    /// </summary>
    private void UpdateData()
    {
       //На первой итерии полный перебор графа
        if (iteration == 0)
        {
            E_CountTornEdge = new int[graph.Length];
            I_CountWholeEdge = new int[graph.Length];
            D = new int[graph.Length];

            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph[i].Length; j++)
                {
                    //Считаем вектора I и E
                    if (currentPartition[i] == currentPartition[graph[i][j]])
                    {
                        I_CountWholeEdge[i]++;
                    }
                    else
                    {
                        E_CountTornEdge[i]++;
                    }    
                }
                //Считаем D
                D[i] = E_CountTornEdge[i] - I_CountWholeEdge[i];
                //Считаем кол-во вершин в графе
                countVertex++;
            }
        }
        else
        {
            //Изменяется только то, что было тронуто. Остальное остаётся прежним.
            (E_CountTornEdge[indexMovedVertex], I_CountWholeEdge[indexMovedVertex]) = (I_CountWholeEdge[indexMovedVertex], E_CountTornEdge[indexMovedVertex]);
            for (int i = 0; i < graph[indexMovedVertex].Length; i++)
            {
                //Считаем вектора I и E
                if (currentPartition[indexMovedVertex] == currentPartition[graph[indexMovedVertex][i]])
                {
                    I_CountWholeEdge[graph[indexMovedVertex][i]]++;
                    E_CountTornEdge[graph[indexMovedVertex][i]]--;
                }
                else
                {
                    E_CountTornEdge[graph[indexMovedVertex][i]]++;
                    I_CountWholeEdge[graph[indexMovedVertex][i]]--;
                }

                //Считаем критерий
            }

            //Считаем Д
            foreach (var elem in graph[indexMovedVertex])
            {
                D[elem] = E_CountTornEdge[elem] - I_CountWholeEdge[elem];
            }
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

    /// <summary>
    /// Печать элементов вектора на консоль
    /// </summary>
    /// <param name="name">Имя вектора</param>
    /// <param name="vector">Вектор</param>
    private void Print(string name, int[] vector)
    {
        Console.Write(name + ": ");
        foreach (int i in vector) Console.Write(i + " ");
        Console.WriteLine();
    }


}
}
