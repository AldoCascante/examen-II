using examen_II.Domain;

namespace examen_II.Application
{
    public interface IDrinksQuery
    {
        public List<DrinkInfoDTO>? GetAvailableDrinks();
        public bool BuyDrinks(TransaccionModel order);
        public bool ValidateOrder(TransaccionModel order);
        public bool CheckAvailability(TransaccionModel order);
    }
}
