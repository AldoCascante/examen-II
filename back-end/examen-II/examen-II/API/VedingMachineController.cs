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

        [HttpGet]
        public IActionResult BuyDrinks(TransaccionModel order)
        {
            try
            {
                bool success = drinksQuery.BuyDrinks(order);
                if (!success) { throw new Exception("Transaccion invalida"); }
                return Ok();
            }
            catch (Exception exeption)
            {
                return StatusCode(500, $"Error en compra de bebidas: {exeption.Message}");
            }
        }
    }
}
