﻿using examen_II.Domain;
using examen_II.Infrastructure;

namespace examen_II.Application
{
    public class DrinksQuery : IDrinksQuery
    {
        IDrinksRepository drinksRepository;
        int drinkOrderSize;
        int totalCost;
        int totalCashInput;
        public DrinksQuery() {
            drinksRepository = new DrinksRepository();
            drinkOrderSize = 2;
            totalCost = 0;
            totalCashInput = 0;
        }

        public DrinksQuery(IDrinksRepository repository, int newDrinkOrderSize)
        {
            drinksRepository = repository;
            drinkOrderSize = newDrinkOrderSize;
            totalCost = 0;
            totalCashInput = 0;
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
            try
            {
                List<DrinkInfoDTO> drinksInventory = drinksRepository.GetAvailableDrinks();
                if (order.drinkOrders.Count % drinkOrderSize != 0) { return false; }

                for (int i = 0; i < order.drinkOrders.Count; i++)
                { if (order.drinkOrders[i] == null) { return false; } }

                for (int i = 0; i < order.drinkOrders.Count; i = i + drinkOrderSize)
                { if (!Contains(drinksInventory, order.drinkOrders[i])) { return false; } }

                for (int i = 1; i < order.drinkOrders.Count; i = i + drinkOrderSize)
                {
                    int isValid;
                    int.TryParse(order.drinkOrders[i], out isValid);
                    if (isValid <= 0) { return false; }
                }

                if (order.thousandBills < 0) { return false; }
                if (order.fiveHundredCoins < 0) { return false; }
                if (order.oneHundredCoins < 0) { return false; }
                if (order.fiftyCoins < 0) { return false; }
                if (order.twentyFiveCoins < 0) { return false; }
            }
            catch (Exception)
            {
                throw new Exception("Error inesperado");
            }

            return true;
        }

        public TransaccionModel CheckAvailability(TransaccionModel order)
        {
            TransaccionModel change = new TransaccionModel();
            change.drinkOrders = new List<string>();
            change.fiveHundredCoins = 0;
            change.oneHundredCoins = 0;
            change.fiftyCoins = 0;
            change.twentyFiveCoins = 0;

            int currentFiveHundredCoins = DrinkInfoDataBase.fiveHundredCoins;
            int currentOneHundredCoins = DrinkInfoDataBase.oneHundredCoins;
            int currentFiftyCoins = DrinkInfoDataBase.fiftyCoins;
            int currentTwentyFiveCoins = DrinkInfoDataBase.twentyFiveCoins;

            try
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


                for (int i = 0; i < order.drinkOrders.Count; i = i + drinkOrderSize)
                {
                    for (int j = 0; j < inventory.Count; j++)
                    {
                        if (inventory[j].name == order.drinkOrders[i])
                        {
                            int drinkQuanity = int.Parse(order.drinkOrders[i + 1]);
                            inventory[j].available = inventory[j].available - drinkQuanity;
                            totalCost += inventory[j].price * drinkQuanity;
                            if (inventory[j].available < 0) { throw new Exception("Bebidas insuficientes en la maquina"); }
                            break;
                        }
                    }
                }

                // es un numero magico si es el valor explicito de la moneda a la que se refiere la variable?
                totalCashInput += order.thousandBills * 1000;
                totalCashInput += order.fiveHundredCoins * 500;
                totalCashInput += order.oneHundredCoins * 100;
                totalCashInput += order.fiftyCoins * 50;
                totalCashInput += order.twentyFiveCoins * 25;
                if (totalCashInput < totalCost) { throw new Exception("Monto insuficiente"); }

                currentFiveHundredCoins += order.fiveHundredCoins;
                currentOneHundredCoins += order.oneHundredCoins;
                currentFiftyCoins += order.fiftyCoins;
                currentTwentyFiveCoins += order.twentyFiveCoins;

                int vuelto = totalCashInput - totalCost;

                while (vuelto > 0 && currentFiveHundredCoins > 0)
                {
                    if ((vuelto - 500) >= 0)
                    {
                        vuelto -= 500;
                        currentFiveHundredCoins--;
                        change.fiveHundredCoins++;
                    }
                    else { break; }
                }

                while (vuelto > 0 && currentOneHundredCoins > 0)
                {
                    if ((vuelto - 100) >= 0)
                    {
                        vuelto -= 100;
                        currentOneHundredCoins--;
                        change.oneHundredCoins++;
                    }
                    else { break; }
                }

                while (vuelto > 0 && currentFiftyCoins > 0)
                {
                    if ((vuelto - 50) >= 0)
                    {
                        vuelto -= 50;
                        currentFiftyCoins--;
                        change.fiftyCoins++;
                    }
                    else { break; }
                }

                while (vuelto > 0 && currentTwentyFiveCoins > 0)
                {
                    if ((vuelto - 25) >= 0)
                    {
                        vuelto -= 25;
                        currentTwentyFiveCoins--;
                        change.twentyFiveCoins++;
                    }
                    else { break; }
                }

                if(vuelto != 0) { throw new Exception("Fallo al realizar la compra"); }

                DrinkInfoDataBase.fiveHundredCoins = currentFiveHundredCoins;
                DrinkInfoDataBase.oneHundredCoins = currentOneHundredCoins;
                DrinkInfoDataBase.fiftyCoins = currentFiftyCoins;
                DrinkInfoDataBase.twentyFiveCoins = currentTwentyFiveCoins;
            }
            catch (Exception)
            {
                throw;
            }
            return change;
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
