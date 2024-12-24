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
          
          private int rightCriterion; //Лучший минимальный критерий
          private int currentCriterion; //Текущий критерий
          
          private float rightDisbalance; //Лучший дисбаланс
          private float currentDisbalance; //Текущий дисбаланс
          
          private int iteration = 0; //Номер итерации
          private int indexMovedVertex; //Номер перемещённой вершины
          private int countElemInFirstSubgraph; //Кол-во элементов в первом подграфе
     
          /// <summary>
          /// Метод Федуччи-Маттеуса
          /// </summary>
          /// <param name="graph"> граф </param>
          /// <param name="partition"> изначальное разделение </param>
          /// <param name="numberOfBlockedIterations"> Кол-во итераций, когда вершину двигать нельзя</param>
          /// <returns>Вектор разделения на подграфы</returns>
          public int[] OptimisePartition(IGraph graph, int[] partition, int numberOfBlockedIterations)
          {
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
              do
              {
                  UpdateData(graph);
                  if (countElemInFirstSubgraph < currentPartition.Length - countElemInFirstSubgraph)
                  {
                      numberPartition = 1;
                  }
                  else numberPartition = 0;
          
                  bool flag = false;
                  int maxInD = 0;
                  indexMovedVertex = -1;
                  //Находим элменет, который будет перемещён в другой подграф
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
                  if (rightCriterion > currentCriterion)
                      rightCriterion = currentCriterion;
                  if (rightDisbalance > currentDisbalance)
                      rightDisbalance = currentDisbalance;
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
          /// Пересчитывает критерий
          /// </summary>
          /// <param name="graph">Граф</param>
          private void CriterionAndDisbalanceCheck(IGraph graph)
          {
              if (iteration == 0)
              {
                  currentDisbalance = 0;
                  currentCriterion = 0;
                  for (int i = 0; i < graph.CountVertecies; i++)
                  {
                      for (int j = 0; j <  graph.GetVertexDegree(i); j++)
                      {
                          //Считаем критерий
                          if (currentPartition[i] != currentPartition[graph[i,j]])
                              currentCriterion++;
                      }
                  }
                  currentCriterion /= 2;
                  rightCriterion = currentCriterion;
                  currentDisbalance = Math.Abs(1 - (float)countElemInFirstSubgraph/(float)(currentPartition.Length - countElemInFirstSubgraph));
                  rightDisbalance = currentDisbalance;
              }
              else
              {
                  for (int i = 0; i < graph.GetVertexDegree(indexMovedVertex); i++)
                  {
                      if (currentPartition[indexMovedVertex] == currentPartition[graph[indexMovedVertex,i]])
                      {
                          currentCriterion--;
                      }
                      else
                      {
                          currentCriterion++;
                      }  
                  }
                  currentDisbalance = Math.Abs(1 - (float)countElemInFirstSubgraph / (float)(currentPartition.Length - countElemInFirstSubgraph));
              }
          }
          
          /// <summary>
          /// Считаем вектора I, E, D и кол-во вершин графа
          /// </summary>
          /// <param name="graph"> Граф </param>
          private void UpdateData(IGraph graph)
          {
             //На первой итерации полный перебор графа
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
                      //Считаем кол-во вершин в графе
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
                      if (currentPartition[indexMovedVertex] == currentPartition[graph[indexMovedVertex,i]])
                      {
                          I_CountWholeEdge[graph[indexMovedVertex,i]]++;
                          E_CountTornEdge[graph[indexMovedVertex,i]]--;
                      }
                      else
                      {
                          E_CountTornEdge[graph[indexMovedVertex, i]]++;
                          I_CountWholeEdge[graph[indexMovedVertex, i]]--;
                      }
                      //Считаем Д
                      D[graph[indexMovedVertex, i]] = E_CountTornEdge[graph[indexMovedVertex, i]] - I_CountWholeEdge[graph[indexMovedVertex, i]];
                  }
                  //Считаем Д
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
