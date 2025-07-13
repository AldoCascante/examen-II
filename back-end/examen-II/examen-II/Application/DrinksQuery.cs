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

        public DrinksQuery(IDrinksRepository repository)
        {
            drinksRepository = repository;
        }

        public List<DrinkInfoDTO>? GetAvailableDrinks()
        {
            List<DrinkInfoDTO> info = drinksRepository.GetAvailableDrinks();
            return info;
        }

        public bool BuyDrinks(TransaccionModel order)
        {
            try
            {
                if (!ValidateOrder(order)) { return false; }
                if (!ProcessOrder(order)) { return false; }
            }
            catch (Exception)
            {
                throw new Exception("Solicitud de bebidas invalida");
            }

            return true;
        }

        public bool ValidateOrder(TransaccionModel order)
        {
            List<DrinkInfoDTO> drinksInventory = drinksRepository.GetAvailableDrinks();
            if (order.drinkOrders.Count % 2 != 0) { return false; }

            for(int i = 0; i < order.drinkOrders.Count; i++)
            { if(order.drinkOrders[i] == null) { return false; } }

            for (int i = 0; i < order.drinkOrders.Count; i = i + 2)
            { if (!Contains(drinksInventory, order.drinkOrders[i])) { return false; } }

            for (int i = 1; i < order.drinkOrders.Count; i = i + 2)
            {
                int isValid;
                int.TryParse(order.drinkOrders[i], out isValid);
            if (isValid <= 0) { return false; } }

            return true;
        }

        public bool ProcessOrder(TransaccionModel order)
        {
            

            return true;
        }

        private bool Contains(List<DrinkInfoDTO> inventory, string drinkName)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].name == drinkName) { return true; }
            }

            return false;
        }
    }
}
