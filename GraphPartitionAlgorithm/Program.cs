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

        int[][] graph = { [1, 4, 7],
                        [0, 2, 6],
                        [1, 3, 8],
                        [2, 4, 9],
                        [0, 3, 5],
                        [4, 6, 9],
                        [1, 5, 7],
                        [0, 6, 8],
                        [2, 7, 9],
                        [3, 5, 8]};

        int[] graphVertWeights = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        int[][] graphEdgeWeights = { [7, 18, 10],
                                    [7, 15, 5],
                                    [15, 4, 14],
                                    [4, 12, 2],
                                    [18, 12, 9],
                                    [9, 3, 13],
                                    [5, 3, 11],
                                    [10, 11, 6],
                                    [14, 6, 8],
                                    [2, 13, 8]};

        for (int i = 0; i < graph.Length; i++)
        {
            Console.Write($"{i} : {graphVertWeights[i]}\n\t");
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

        IAccuratePartition grp = new AccuratePartition();

        int[] answer = grp.GetPartition(igraph);

        Console.WriteLine(String.Join(" ", answer));

        var balance = igraph.GetGraphBalance(answer);
        Console.WriteLine($"{balance.left}:{balance.right}");


        Console.WriteLine(igraph.GetGraphCut(answer));
    }
}