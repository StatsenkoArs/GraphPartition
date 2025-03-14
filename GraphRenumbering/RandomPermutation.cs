namespace GraphRenumbering
{
    public class RandomPermutation
    {
        /// <summary>
        /// Генерирует случайную перестановку для перенумерации графа
        /// </summary>
        /// <param name="graph_size">Размер перестановки для графа</param>
        /// <returns>Перестановка для графа</returns>
        public static PermutationStructure GetPermutation(int graph_size)
        {
            int[] perm = new int[graph_size];
            for (int i = 0; i < graph_size; i++)
            {
                perm[i] = i;
            }
            Random.Shared.Shuffle(perm);
            PermutationStructure permutation = new PermutationStructure(perm);
            return permutation;
        }
    }
}
