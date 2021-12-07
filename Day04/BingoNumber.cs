namespace Day04
{
    public class BingoNumber
    {
        public int Value { get; private set; }
        public bool IsMarked { get; set; }

        public BingoNumber(int value)
        {
            Value = value;
            IsMarked = false;
        }
    }
}
