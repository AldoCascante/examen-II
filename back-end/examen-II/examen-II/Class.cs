using examen_II.Domain;

namespace examen_II
{
    public static class DrinkInfoDataBase
    {
        public static List<DrinkInfoDTO> drinksTable = new List<DrinkInfoDTO>();
        public static int fiveHundredCoins = 20;
        public static int oneHundredCoins = 30;
        public static int fiftyCoins = 50;
        public static int twentyFiveCoins = 25;
        public static int TotalDisponible => fiveHundredCoins + oneHundredCoins + fiftyCoins + twentyFiveCoins;
    }
}
