using examen_II.Domain;

namespace examen_II.Infrastructure
{
    public class DrinksRepository: IDrinksRepository
    {
        List<DrinkInfoDTO> drinksAvailable;
        public DrinksRepository() {
            drinksAvailable = new List<DrinkInfoDTO>();
            drinksAvailable.Add(new DrinkInfoDTO {
                name = "Coca Cola",
                available = 10,
                price = 800,
            });

            drinksAvailable.Add(new DrinkInfoDTO
            {
                name = "Pepsi",
                available = 8,
                price = 750,
            });

            drinksAvailable.Add(new DrinkInfoDTO
            {
                name = "Fanta",
                available = 10,
                price = 950,
            });

            drinksAvailable.Add(new DrinkInfoDTO
            {
                name = "Sprite",
                available = 15,
                price = 975,
            });
        }
        public List<DrinkInfoDTO> GetAvailableDrinks()
        {
            return drinksAvailable;
        }
    }
}
