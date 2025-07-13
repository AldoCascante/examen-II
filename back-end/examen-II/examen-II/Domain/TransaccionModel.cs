namespace examen_II.Domain
{
    public class TransaccionModel
    {
        public int thousandBills { get; set; }
        public int fiveHundredCoins { get; set; }
        public int oneHundredCoins { get; set; }
        public int fiftyCoins { get; set; }
        public int twentyFiveCoins { get; set; }
        public List<string> drinkOrders { get; set; }
    }
}
