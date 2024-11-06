namespace GraphOptimisation
{
    public class GrafFeduchiMatteus
{
    private int grafV = 0; // Кол-во вершин графа
    private int grafE = 0; // Количество ребер графа
    private int Q = 0; // Критерий, кол-во ребер между двумя подграфами
    private int minQ = 0;  // Лучший критерий
    private int B = 0; //Дисбаланс разделения
    private int minB = 0; //Минимальный дисбаланс
    private int[] E; // Количество разрезанных связей для каждой вершины
    private int[] I; //Количество неразрезанных связей для каждой вершины
    private int[] D; // D = E - I;
    private int[] T; //Вектор кол-ва итераций, когда вершину двигать нельзя
    private int[] tCheck; //Была ли тронута вершина
    private int numberPodgrafNow = 0; //Номер текущего подграфа
    private int[] rightRazdelenie; //Лучшее разделение
    
    /// <summary>
    /// Конструктор. Считает критерий Q, высчитывает кол-во ребер и вершин графа
    /// </summary>
    /// <param name="graf"> Изначальный граф</param>
    /// <param name="razdelenie"> Изначальное разделение </param>
    public GrafFeduchiMatteus(int[][] graf, int[] razdelenie)
    {
        // Инициализирует кол-во ребер и вершин графа, считает текущий Критерий Q
        for (int i = 0; i < graf.Length; i++)
        {
            grafV++;
            for (int j = 0; j < graf[i].Length; j++)
            {
                if (razdelenie[i] != razdelenie[graf[i][j] - 1])
                    Q++;
                grafE++;
            }
        }
        grafE /= 2;
        Q /= 2;
    }

    /// <summary>
    /// Основной метод перераспределения пографов
    /// </summary>
    /// <param name="graf">Изначальный граф</param>
    /// <param name="razdelenie"> Изначальное разделение </param>
    /// <param name="t">Изначальное кол-во итераций, на которое вершина блокируется для перемещения между подграфами</param>
    /// <returns>Критерий Q</returns>
    public int FeduchiMatteus(int[][] graf, ref int[] razdelenie, int t)
    {
        T = new int[graf.Length];
        tCheck = new int[graf.Length];
        rightRazdelenie = new int[razdelenie.Length];
        int iteracia = 0;
        minQ = grafE;

        while (!IsFull())
        {
            //Console.WriteLine("---------------------" + iteracia);
            //Print("razdelenie old", razdelenie);

            EandICheck(graf, razdelenie);
            DCheck();
            int index;
            int max;
            if (iteracia == 0)
            {
                max = D[0];
                index = 0;
                for (int i = 1; i < D.Length; i++)
                {
                    if (D[i] > max)
                    {
                        max = D[i];
                        index = i;
                    }
                }
                numberPodgrafNow = razdelenie[index];
            }
            else
            {
                bool flag = false;
                max = 0;
                index = -1;
                for (int i = 0; i < D.Length; i++)
                {
                    if (!flag && razdelenie[i] == numberPodgrafNow && T[i] == 0) //Находит первый элемент из нужного подграфа
                    {
                        max = D[i];
                        index = i;
                        flag = true;
                    }

                    if (D[i] > max && razdelenie[i] == numberPodgrafNow && T[i] == 0) //Ищет максимум
                    {
                        max = D[i];
                        index = i;
                    }
                }
            }
            //Console.WriteLine("======= Меняем класс у элемента под номером " + (index + 1));
            //Console.WriteLine("Подграф: " + numberPodgrafNow);
            if (razdelenie[index] == 1)
            {
                razdelenie[index] = 0;
                numberPodgrafNow = 0;
            }
            else
            {
                razdelenie[index] = 1;
                numberPodgrafNow = 1;
            }



            TCheck(ref t, index);


            //Print("razdelenie new", razdelenie);
            //Print("T", T);
            //Print("TCheck", tCheck);

            QCheck(graf, razdelenie);
            BCheck(razdelenie);

            //Сохранение лучшего разделения
            if (minB > B) { minB = B; }
            if (minQ > Q) { minQ = Q; }
            if (minQ >= Q && minB >= B)
            {
                minB = B;
                minQ = Q;
                for (int i = 0; i < razdelenie.Length; i++)
                {
                    rightRazdelenie[i] = razdelenie[i];
                }
            }
            //Console.WriteLine("Q = " + Q + "| min = " + minQ);
            //Console.WriteLine("B = " + B + "| min = " + minB);

            iteracia++;
        }
        for (int i = 0; i < razdelenie.Length; i++)
        {
            razdelenie[i] = rightRazdelenie[i];
        }
        return minQ;
    }
    /// <summary>
    /// Пересчитывает критерий Q
    /// </summary>
    /// <param name="graf">Изначальный граф</param>
    /// <param name="razdelenie">Разделине на пографы</param>
    private void QCheck(int[][] graf, int[] razdelenie)
    {
        //Считает критерий Q
        Q = 0;
        for (int i = 0; i < graf.Length; i++)
        {
            for (int j = 0; j < graf[i].Length; j++)
            {
                if (razdelenie[i] != razdelenie[graf[i][j] - 1])
                    Q++;
            }
        }
        //Console.WriteLine("QCheck = " + Q);
        Q /= 2;
    }
    /// <summary>
    /// Пересчитывает ветора Е и I
    /// </summary>
    /// <param name="graf">граф</param>
    /// <param name="razdelenie">разделение на подграфы</param>
    private void EandICheck(int[][] graf, int[] razdelenie)
    {
        //Считает вектора E и I
        E = new int[graf.Length];
        I = new int[graf.Length];
        for (int i = 0; i < graf.Length; i++)
        {
            for (int j = 0; j < graf[i].Length; j++)
            {
                if (razdelenie[i] == razdelenie[graf[i][j] - 1])
                {
                    I[i]++;
                }
                else
                {
                    E[i]++;
                }
            }
        }
        //Print("E", E);
        //Print("I", I);
    }
    /// <summary>
    /// Пересчитывает вектор D
    /// </summary>
    private void DCheck()
    {
        // Считает D
        D = new int[E.Length];
        for (int i = 0; i < E.Length; i++)
        {
            D[i] = E[i] - I[i];
        }

        //Print("D", D);
    }
    /// <summary>
    /// Пересчитывает параметр В
    /// </summary>
    /// <param name="razdelenie"> Текущее разделение </param>
    private void BCheck(int[] razdelenie)
    {
        B = 0;
        for (int i = 0; i < razdelenie.Length; i++)
        {
            if (razdelenie[i] == 1) B++;
        }
        B = Math.Abs(grafV / 2 - B);
    }

    /// <summary>
    /// Проверяет, были ли задействованы все вершины графа
    /// </summary>
    /// <returns></returns>
    private bool IsFull()
    {
        // Если все вершины тронуты, то верни Тру.
        for (int i = 0; i < tCheck.Length; i++)
        {
            if (tCheck[i] == 0) return false;
        }
        return true;
    }

    /// <summary>
    /// Переписывает вектор Т и меняет параметр t, когда вершина уже была задействована
    /// </summary>
    /// <param name="t">Изначальное кол-во итераций, на которое вершина блокируется для перемещения между подграфами</param>
    /// <param name="index">Индекс текущего элемента</param>
    private void TCheck(ref int t, int index)
    {
        for (int i = 0; i < T.Length; i++)
        {
            if (T[i] != 0) T[i]--;
        }

        if (tCheck[index] != 0)
        {
            t++;
            T[index] = t;
        }
        else
        {
            T[index] = t;
            tCheck[index]++;
        }
    }

    /// <summary>
    /// Печать элементов вектора на консоль
    /// </summary>
    /// <param name="name">Имя вектора</param>
    /// <param name="v">Вектор</param>
    private void Print(string name, int[] v)
    {
        Console.Write(name + ": ");
        foreach (int i in v) Console.Write(i + " ");
        Console.WriteLine();
    }
}
}
