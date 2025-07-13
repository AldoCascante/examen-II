using examen_II.Domain;

namespace examen_II.Infrastructure
{
    public interface IDrinksRepository
    {
        public List<DrinkInfoDTO> GetAvailableDrinks();
        public bool buyDrink(string drink, int quantity);
    }
}
