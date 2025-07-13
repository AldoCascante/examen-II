using examen_II.Domain;

namespace examen_II.Application
{
    public interface IDrinksQuery
    {
        public List<DrinkInfoDTO>? GetAvailableDrinks();
    }
}
