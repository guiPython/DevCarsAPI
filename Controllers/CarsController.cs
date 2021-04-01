using DevCarsAPI.Entities;
using DevCarsAPI.InputModels;
using DevCarsAPI.Persistence;
using DevCarsAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDBContext _dbContext;
        public CarsController(DevCarsDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet]

        public IActionResult Get()
        {
            var cars = _dbContext.Cars;

            var carsViewModel = cars.Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price)).ToList();

            return Ok(carsViewModel);
        }

        [HttpGet("{Id}")]

        public IActionResult GetById(int Id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == Id);

            if (car == null) return NotFound();

            var carViewModel = new CarDetailsViewModel(
                car.Id,
                car.Brand,
                car.Model,
                car.VinCode,
                car.Color,
                car.Year,
                car.Price,
                car.ProductionDate
                );

            return Ok(carViewModel);
        }

        [HttpPost]

        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            if (model.Model.Length > 50) return BadRequest("Modelo não pode ter mais de 50 caracteres");

            var car = new Car(4, model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);

            _dbContext.Cars.Add(car);

            return CreatedAtAction(
                nameof(GetById),
                new { id = car.Id },
                model
            );
        }

        [HttpPut("{Id}")]

        public IActionResult Put(int Id, [FromBody] UpdateCarInputModel model)
        {
            if (model.Price < 0) return BadRequest("Preço não pode ser menor que 0");

            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == Id);

            if (car == null) return NotFound();

            car.Update(model.Color, model.Price);

            return NoContent();
        }

        [HttpDelete("{Id}")]

        public IActionResult Delete(int Id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == Id);

            if (car == null) return NotFound();

            car.SetAsSuspended();

            return NoContent();
        }

    }
}
