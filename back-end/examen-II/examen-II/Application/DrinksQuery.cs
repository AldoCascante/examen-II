using examen_II.Domain;
using examen_II.Infrastructure;

namespace examen_II.Application
{
    public class DrinksQuery : IDrinksQuery
    {
        IDrinksRepository drinksRepository;
        public DrinksQuery() {
            drinksRepository = new DrinksRepository();
        }

        public List<DrinkInfoDTO>? GetAvailableDrinks()
        {
            List<DrinkInfoDTO> info = drinksRepository.GetAvailableDrinks();
            return info;
        }
    }
}
