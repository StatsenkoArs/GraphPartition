namespace GraphRenumbering
{
    public class LevelStructCRS
    {
        private int[] _adjacentNums;
        private int[] _adjacentLevels;
        public LevelStructCRS(int[] adjacentNums, int[] adjacentLevels)
        {
            _adjacentLevels = adjacentLevels;
            _adjacentNums = adjacentNums;
        }
        public int this[int levelNum, int adjacentNum]
        {
            get => _adjacentLevels[_adjacentNums[levelNum] + adjacentNum];
        }

        public int CountLevels => _adjacentNums.Length - 1;

        public int Length => _adjacentLevels.Length;

        public int GetNumVertexOnLevel(int levelNum)
        {
            return _adjacentNums[levelNum + 1] - _adjacentNums[levelNum];
        }
    }
}
