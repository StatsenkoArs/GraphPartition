namespace GraphRenumbering
{
    /// <summary>
    /// Класс для хранения перстановок (прямой и обратной)
    /// </summary>
    public class PermutationStructure
    {
        private int[] permutation;
        private int[] reverse_permutation;
        private int _length;
        /// <summary>
        /// Задаёт прямую и обратную тождественную перестановку нужной длины
        /// </summary>
        /// <param name="length">Длина перестановки</param>
        public PermutationStructure(int length)
        {
            _length = length;
            permutation = new int[length];
            reverse_permutation = new int[length];
            for (int i = 0; i < _length; i++)
            {
                permutation[i] = i;
                reverse_permutation[i] = i;
            }
        }
        /// <summary>
        /// По входному массиву прямой перестановки 
        /// задаёт прямую и обратную перестановку
        /// </summary>
        /// <param name="array">Массив прямой перестановки</param>
        public PermutationStructure(int[] array)
        {
            _length = array.Length;
            permutation = new int[_length];
            reverse_permutation = new int[_length];
            for (int i = 0; i < _length; i++)
            {
                permutation[i] = array[i];
                reverse_permutation[array[i]] = i;
            }
        }
        /// <summary>
        /// Меняет местами два числа стоящие по заданным индексам
        /// </summary>
        /// <param name="from">Первый индекс</param>
        /// <param name="to">Второй индекс</param>
        /// <exception cref="ArgumentException">Возникает, если from 
        /// или to больше длины перестановки</exception>
        public void ChangeByPos(int from, int to)
        {
            if (from >= _length) { throw new ArgumentException("from >= perm length", "from"); }
            if (to >= _length) { throw new ArgumentException("to >= perm length", "to"); }

            int tmp_to = permutation[to];
            int tmp_from = permutation[from];

            permutation[to] = permutation[from];
            permutation[from] = tmp_to;

            int tmp_reverse_to = reverse_permutation[tmp_to];

            reverse_permutation[tmp_to] = reverse_permutation[tmp_from];
            reverse_permutation[tmp_from] = tmp_reverse_to;
        }
        /// <summary>
        /// Возвращает значение прямой перестановки
        /// </summary>
        /// <param name="pos">Позиция в перестановке</param>
        /// <returns>Число на этой позиции</returns>
        public int GetNumByPos(int pos)
        {
            return permutation[pos];
        }
        /// <summary>
        /// Возвращает значение обратной перестановки
        /// </summary>
        /// <param name="num">Позиция в обратной перестановке</param>
        /// <returns>Число на этой позиции</returns>
        public int GetPosByNum(int num)
        {
            return reverse_permutation[num];
        }
        public int GetLength() { return _length; }
    }
}
