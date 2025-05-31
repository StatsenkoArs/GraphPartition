 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using GraphRepresentation;
using GraphOptimisation;

namespace GraphDrawer
{
    public class Drawer : IDrawGraph
{
    /// <summary>
    /// Рисует граф
    /// </summary>
    /// <param name="graph">Граф в виде двумерного массива</param>
    /// <param name="coordVertex">Массив координат вершин</param>
    /// <param name="partition">Разделение</param>
    /// <param name="filename">полный путь куда будет сохранён файл</param>
    public void Draw(int[][] graph, Point[] coordVertex, int[] partition, string path)
    {
        foreach (var i in coordVertex)
        {
            if (i.X < 100 || i.Y < 100)
            {
                Scaling(coordVertex);
                continue;
            }
        }
        int rad = FindOptimumRadius(coordVertex);
        int[] size = FrameSize(coordVertex);
        Bitmap bitmap = new Bitmap(size[0] + rad*2, size[1] + rad*2);
        Graphics graphics = Graphics.FromImage(bitmap);

        graphics.Clear(Color.White);

        // Рисуются рёбра
        // Разрез - красный цвет
        for (int i = 0; i < graph.Length; i++)
        {
            for (int j = 0; j < graph[i].Length; j++)
            {
                if (partition[i] == partition[graph[i][j]])
                {
                    graphics.DrawLine(new Pen(Color.Black, rad/5), coordVertex[i].Y, coordVertex[i].X, coordVertex[graph[i][j]].Y, coordVertex[graph[i][j]].X);
                }
                else
                {
                    graphics.DrawLine(new Pen(Color.Red, rad/5), coordVertex[i].Y, coordVertex[i].X, coordVertex[graph[i][j]].Y, coordVertex[graph[i][j]].X);
                }
            }
        }

        //Рисуются вершины
        for (int i = 0; i < graph.Length; i++)
        {
            graphics.DrawEllipse(new Pen(Color.Black, rad/5), coordVertex[i].Y - rad, coordVertex[i].X - rad, rad * 2, rad * 2);
            if (partition[i] == 0)
            {
                graphics.FillEllipse(Brushes.White, coordVertex[i].Y - rad, coordVertex[i].X - rad, rad * 2, rad * 2);
                graphics.DrawString(i.ToString(), new Font("Arial", rad), Brushes.Black, coordVertex[i].Y - rad, coordVertex[i].X - rad);
            }
            if (partition[i] == 1)
            {
                graphics.FillEllipse(Brushes.Black, coordVertex[i].Y - rad, coordVertex[i].X - rad, rad * 2, rad * 2);
                graphics.DrawString(i.ToString(), new Font("Arial", rad), Brushes.White, coordVertex[i].Y - rad, coordVertex[i].X - rad);
            }
        }
        bitmap.Save(path);
    }

    /// <summary>
    /// Записывает в текстовый файл значения разреза и дисбаланса.
    /// </summary>
    /// <param name="graph">граф</param>
    /// <param name="partition">разбиение</param>
    /// <param name="filename">путь до файла</param>
    /// <param name="isStart">true - файл перезаписывается, иначе данные дозаписываются в старый файл</param>
    public void Statistics(IGraph graph, int[] partition, string path, bool isStart)
    {
        StreamWriter writer;
        if (isStart)
            writer = new StreamWriter(path, false);
        else
            writer = new StreamWriter(path, true);
        int criterion = 0;
        float disbalans = 0;
        FiducciaMattheysesMethod f = new FiducciaMattheysesMethod();
        f.Statistics(ref criterion, ref disbalans, graph, partition);
        writer.WriteLine(DateTime.Now);
        writer.WriteLine("Разрез: " +  criterion);
        writer.WriteLine("Дисбаланс: " + disbalans);
        writer.WriteLine();
        writer.Close();
    }

    /// <summary>
    /// Высчитывает оптимальный размер изображения
    /// </summary>
    /// <param name="coordVertex">Массив координат вершин</param>
    /// <returns>Массив размеров по ширине и высоте соответственно</returns>
    private int[] FrameSize(Point[] coordVertex)
    {
        int[] size = new int[2] { 0, 0 };
        foreach (var p in coordVertex)
        {
            if (p.X > size[0]) size[0] = p.X;
            if (p.Y > size[1]) size[1] = p.Y;
        }
        return size;
    }
    /// <summary>
    /// Высчитывает оптимальный радиус рисуемой вершины
    /// </summary>
    /// <param name="coordVertex">Массив координат вершин</param>
    /// <returns>Оптимальный радиус вершины</returns>
    private int FindOptimumRadius(Point[] coordVertex)
    {
        int[] min = [coordVertex[0].X, coordVertex[0].Y];
        foreach (var p in coordVertex)
        {
            if (p.X < min[0]) min[0] = p.X;
            if (p.Y < min[1]) min[1] = p.Y;
        }
        return min[0] < min[1] ? min[0]/4 : min[1]/4;
    }
    /// <summary>
    /// Подгоняет размер под пиксели для отрисовки
    /// </summary>
    /// <param name="old">Массив координат</param>
    private void Scaling(Point[] old)
    {
        for (int i = 0; i < old.Length; i++)
        {
            old[i].X = (old[i].X + 1) * 100;
            old[i].Y = (old[i].Y + 1) * 100;
        }
    }
}
}
