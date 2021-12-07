using System;
using System.Collections.Generic;
using System.Linq;

namespace Day04
{
    public class BingoBoard
    {
        public BingoNumber[,] BoardNumbers { get; set; }
        public int Score { get; set; }

        private Dictionary<int, BingoNumber> numberSet = new Dictionary<int, BingoNumber>();

        public BingoBoard(string newBoard)
        {
            BoardNumbers = new BingoNumber[5, 5];
            var rows = newBoard.Split('\n');

            int i = 0;
            foreach (var row in rows)
            {
                var items = row.Split(new[] { " ", "  " }, StringSplitOptions.RemoveEmptyEntries);

                for (int col = 0; col < items.Length; col++)
                {
                    BoardNumbers[i, col] = new BingoNumber(int.Parse(items[col]));
                }

                i++;
            }

            numberSet = BoardNumbers.Flatten().ToDictionary(b => b.Value);
        }

        public int MarkNumber(int number)
        {
            if (numberSet.ContainsKey(number))
                numberSet[number].IsMarked = true;

            // Check if the board has bingo and return the unmarked total
            return HasBingo() ? SumUnmarked() : -1;
        }

        public bool HasBingo()
        {
            // Are any rows or columns fully marked?
            var rowHasBingo = Enumerable.Range(0, 5).Any(i => BoardNumbers.SliceRow(i).All(b => b.IsMarked));
            var colHasBingo = Enumerable.Range(0, 5).Any(i => BoardNumbers.SliceColumn(i).All(b => b.IsMarked));

            return rowHasBingo || colHasBingo;
        }

        public int SumUnmarked()
        {
            return BoardNumbers.Flatten().Where(b => !b.IsMarked).Sum(b => b.Value);
        }
    }
}
