using GraphPartitionAccurate;
using GraphOptimisation;
using ExampleGenerator;
using GraphReduction;
using System.Runtime.CompilerServices;
using GraphPartitionClass;

class Program
{
    static void Main(string[] args)
    {
        int n = 1000;
        
        Generator g  = new Generator();
        var graph = g.Generate(n, 3);

        for (int i = 0; i < graph.Length; i++)
        {
           Console.WriteLine(i + ": " + String.Join(" ", graph[i]));
        }
        Console.WriteLine("----------------------------------");

        IGraphPartition grp = new Graph2Partition(new SimpleGraphReduction(new SimpleRestruct(), new SimpleCompress()), 
                                                new BranchAndBoundsAlgorithm(), 
                                                new SimpleGraphRestoration(new FiducciaMattheysesMethod()), 
                                                new FiducciaMattheysesMethod());

        int[] answer = grp.GetPartition(graph);

        Console.WriteLine(String.Join(" ", answer));
        Console.WriteLine(answer.Sum());
    }
}