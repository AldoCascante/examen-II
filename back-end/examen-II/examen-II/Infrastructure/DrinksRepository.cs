using examen_II.Domain;

namespace examen_II.Infrastructure
{
    public class DrinksRepository: IDrinksRepository
    {
        public List<DrinkInfoDTO> GetAvailableDrinks()
        {
            List<DrinkInfoDTO> drinksAvailable = DrinkInfoDataBase.drinksTable;
            return drinksAvailable;
        }

        public bool buyDrink(string drink, int quantity)
        {
            for (int i = 0; i < DrinkInfoDataBase.drinksTable.Count; i++)
            {
                if (DrinkInfoDataBase.drinksTable[i].name == drink)
                {
                    DrinkInfoDataBase.drinksTable[i].available = DrinkInfoDataBase.drinksTable[i].available - quantity;
                    break;
                }
            }
            return true;
        }
    }
}
