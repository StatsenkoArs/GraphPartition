using GraphPartitionAccurate;
using GraphOptimisation;
using ExampleGenerator;
using GraphReduction;
using System.Runtime.CompilerServices;
using GraphPartitionClass;
using GraphRepresentation;

class Program
{
    static void Main(string[] args)
    {
        //int n = 1000;
        
        //Generator g  = new Generator();
        //var graph = g.Generate(n, 3);

        int[][] graph = { [1, 3],
                        [0, 2, 3, 4],
                        [1, 4, 5],
                        [0, 1, 4, 6],
                        [1, 2, 3, 5, 6, 7],
                        [2, 4, 7, 8],
                        [3, 4, 7],
                        [4, 5, 6, 8],
                        [5, 7] };

        int[] graphVertWeights = {1, 2, 3, 4, 5, 6, 7, 8, 9 };

        int[][] graphEdgeWeights = { [4, 4],
                        [2, 2, 2, 2],
                        [5, 4, 5],
                        [7, 1, 4, 6],
                        [1, 2, 3, 5, 6, 7],
                        [2, 3, 7, 8],
                        [3, 6, 7],
                        [4, 6, 6, 8],
                        [5, 7] };

        for (int i = 0; i < graph.Length; i++)
        {
            Console.Write($"{i} : {graphVertWeights[i]} - ");
            for (int j = 0; j < graph[i].Length; j++)
            {
                Console.Write($"{graph[i][j]} : {graphEdgeWeights[i][j]}; ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("----------------------------------");

        //IGraphPartition grp = new Graph2Partition(new SimpleGraphReduction(new SimpleRestruct(), new SimpleCompress()), 
        //                                        new BranchAndBoundsAlgorithm(), 
        //                                        new SimpleGraphRestoration(new FiducciaMattheysesMethod()), 
        //                                        new FiducciaMattheysesMethod());

        IGraph igraph = new GraphCSRWeights(graph, graphVertWeights, graphEdgeWeights);

        IAccuratePartition grp = new BranchAndBoundsAlgorithm();

        int[] answer = grp.GetPartition(igraph);

        Console.WriteLine(String.Join(" ", answer));

        var balance = igraph.GetGraphBalance(answer);
        Console.WriteLine($"{balance.left}:{balance.right}");


        Console.WriteLine(igraph.GetGraphCut(answer));
    }
}