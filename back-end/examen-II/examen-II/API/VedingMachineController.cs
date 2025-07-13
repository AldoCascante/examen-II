using examen_II;
using examen_II.Application;
using examen_II.Domain;
using Microsoft.AspNetCore.Mvc;

namespace backend_planilla.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VedingMachineController : ControllerBase
    {
        private readonly IDrinksQuery drinksQuery;

        public VedingMachineController()
        {
            drinksQuery = new DrinksQuery();
        }

        [HttpGet]
        public IActionResult GetAvailableDrinks()
        {
            try
            {
                List<DrinkInfoDTO> availableDrinks = drinksQuery.GetAvailableDrinks();
                return Ok(availableDrinks);
            }
            catch (Exception exeption)
            {
                return StatusCode(500, $"Error recuperando las bebidas disponibles: {exeption.Message}");
            }
        }

        [HttpPost]
        public IActionResult BuyDrinks(TransaccionModel order)
        {
            try
            {
                if (!drinksQuery.ValidateOrder(order)) { throw new Exception("Orden invalida"); }
                drinksQuery.CheckAvailability(order);
                drinksQuery.BuyDrinks(order);
                return Ok(true);
            }
            catch (Exception exeption)
            {
                return StatusCode(500, exeption.Message);
            }
        }
    }
}
