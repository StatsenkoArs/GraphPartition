using GraphPartitionAccurate;
class Program
{
    static void Main(string[] args)
    {
        int n = 8;
        int[][] edges =
            [
            [5, 6],
            [2, 5, 6, 7],
            [1, 3, 4, 5],
            [2, 7],
            [2, 5],
            [0, 1, 2, 4],
            [0, 1],
            [1, 3]
            ];

        int[] X = new int[n];

        Solution sln = new Solution(n, edges);
        var result = sln.GetSolution();

        Console.WriteLine(result.Item2);
        Console.WriteLine(String.Join(" ", result.Item1));
        Console.WriteLine(result.Item1.Sum());
    }
}