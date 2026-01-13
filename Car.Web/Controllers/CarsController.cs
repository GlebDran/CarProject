using Car.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Car.Core.Domain;

namespace Car.Web.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarServices _carServices;

        public CarsController(ICarServices carServices)
        {
            _carServices = carServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _carServices.GetAll();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carServices.Get(id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Car.Core.Domain.Car car)
        {
            if (ModelState.IsValid)
            {
                await _carServices.Create(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var car = await _carServices.Get(id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Car.Core.Domain.Car car)
        {
            if (ModelState.IsValid)
            {
                await _carServices.Update(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carServices.Get(id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _carServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}