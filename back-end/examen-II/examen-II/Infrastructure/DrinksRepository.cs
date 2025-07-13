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
    }
}
