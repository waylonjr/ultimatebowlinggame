namespace ConsoleApp1
{
    public class Throw
    {
        //Note: 0-9, / (spare), X (strike)
        public string Score { get; internal set; }

        public int ScoreAsInt { get {
                var value = 0;

                int.TryParse(Score, out value);
                    return value;

            } }
    }
}
