using examen_II.Domain;
using examen_II.Infrastructure;

namespace examen_II.Application
{
    public class DrinksQuery : IDrinksQuery
    {
        IDrinksRepository drinksRepository;
        int drinkOrderSize = 2;
        public DrinksQuery() {
            drinksRepository = new DrinksRepository();
            drinkOrderSize = 2;
        }

        public DrinksQuery(IDrinksRepository repository)
        {
            drinksRepository = repository;
            drinkOrderSize = 2;
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
                for (int i = 0; i < order.drinkOrders.Count; i = i + drinkOrderSize)
                {
                    drinksRepository.buyDrink(order.drinkOrders[i], int.Parse(order.drinkOrders[i + 1]));
                }
            }
            catch (Exception)
            {
                throw new Exception("Error inesperado");
            }

            return true;
        }

        public bool ValidateOrder(TransaccionModel order)
        {
            List<DrinkInfoDTO> drinksInventory = drinksRepository.GetAvailableDrinks();
            if (order.drinkOrders.Count % drinkOrderSize != 0) { return false; }

            for(int i = 0; i < order.drinkOrders.Count; i++)
            { if(order.drinkOrders[i] == null) { return false; } }

            for (int i = 0; i < order.drinkOrders.Count; i = i + drinkOrderSize)
            { if (!Contains(drinksInventory, order.drinkOrders[i])) { return false; } }

            for (int i = 1; i < order.drinkOrders.Count; i = i + drinkOrderSize)
            {
                int isValid;
                int.TryParse(order.drinkOrders[i], out isValid);
            if (isValid <= 0) { return false; } }

            return true;
        }

        public bool CheckAvailability(TransaccionModel order)
        {
            List<DrinkInfoDTO> temp = drinksRepository.GetAvailableDrinks();
            List<DrinkInfoDTO> inventory = new List<DrinkInfoDTO>(temp.Count);

            temp.ForEach((item) =>
            {
                inventory.Add(new DrinkInfoDTO
                {
                    name = item.name,
                    available = item.available,
                    price = item.price,
                });
            });


            for (int i = 0;i < order.drinkOrders.Count; i = i + drinkOrderSize)
            {
                for (int j = 0; j < inventory.Count; j++)
                {
                    if (inventory[j].name == order.drinkOrders[i])
                    {
                        inventory[j].available = inventory[j].available - int.Parse(order.drinkOrders[i + 1]);
                        if(inventory[j].available < 0) { return false; }
                        break;
                    }
                }
            }

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
