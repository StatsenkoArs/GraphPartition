using GraphPartitionAccurate;
using GraphOptimisation;
using ExampleGenerator;
using GraphReduction;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        int n = 100;
        
        Generator g  = new Generator();
        var graph = g.Generate(n, n * (n - 2) / 16, n / 20);

        for (int i = 0; i < graph.Length; i++)
        {
            Console.WriteLine(i + ": " + String.Join(" ", graph[i]));
        }
        Console.WriteLine("----------------------------------");

        GraphReductAlg gra = new GraphReductAlg(new SimpleCompress(), new SimpleRestruct());

        bool enoughReduction = false;
        int t = 0;
        int count = 0;
        while (!enoughReduction)
        {
            count++;
            graph = gra.Reduct(graph);
            t = graph.Count();
            enoughReduction = t <= 20;
        }

        Solution s = new Solution();
        var result = s.Solve(graph.Length, graph);


        for (int i = 0; i < graph.Length; i++)
        {
            Console.WriteLine(i + ": " + String.Join(" ", graph[i]));
        }
        Console.WriteLine("----------------------------------");
        Console.WriteLine(String.Join(" ", result.Item1));
        Console.WriteLine(result.Item2);
        Console.WriteLine("----------------------------------");

        int[] x = new int[result.Item1.Length];
        result.Item1.CopyTo(x, 0);
        int q = result.Item2;

        for (int i = 0; i < count; i++)
        {
            x = gra.UnmappingStep(x, out graph);

            //TODO убрать как можно скорее
            for (int j = 0; j < graph.Length; j++)
            {
                for (int k = 0; k < graph[j].Length; k++)
                {
                    graph[j][k]++;
                }
            }


            GrafFeduchiMatteus gfm = new GrafFeduchiMatteus(graph, x);
            q = gfm.FeduchiMatteus(graph, ref x, n);

            //TODO убрать как можно скорее
            for (int j = 0; j < graph.Length; j++)
            {
                for (int k = 0; k < graph[j].Length; k++)
                {
                    graph[j][k]--;
                }
            }

        }


        for (int i = 0; i < graph.Length; i++)
        {
            Console.WriteLine(i + ": " + String.Join(" ", graph[i]));
        }
        Console.WriteLine("----------------------------------");
        Console.WriteLine(String.Join(" ", x));
        Console.WriteLine(q);
        Console.WriteLine("----------------------------------");

    }
}