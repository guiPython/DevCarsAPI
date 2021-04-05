using Dapper;
using DevCarsAPI.Entities;
using DevCarsAPI.InputModels;
using DevCarsAPI.Persistence;
using DevCarsAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;


namespace DevCarsAPI.Controllers
{
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly DevCarsDBContext _dbContext;
        private readonly string _connectionString;
        public CarsController(DevCarsDBContext dBContext, IConfiguration configuration)
        {
            _dbContext = dBContext;
            _connectionString = configuration.GetConnectionString("DevCarsCs");
            // _connectionString = _dbContext.Database.GetDbConnection().ConnectionString;
        }


        [HttpGet]
        public IActionResult Get()
        {
            /*var cars = _dbContext.Cars;

            var carsViewModel = cars.Select(c => new CarItemViewModel(c.Id, c.Brand, c.Model, c.Price)).ToList();*/

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "SELECT Id, Brand, Model, Price FROM Cars Where Status = 0";

                var carsViewModel = sqlConnection.Query<CarItemViewModel>(query);

                return Ok(carsViewModel);
            }
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


        /// <summary>
        /// Cadastrar Carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "brand": "Toyota",
        ///     "model": "Corolla",
        ///     "VinCode": "abc123",
        ///     "year": 2021,
        ///     "color": "Cinza",
        ///     "productionDate": "2021-04-05"
        /// }
        /// </remarks>
        /// <param name="model">Dados de um novo Carro</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Obejto criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] AddCarInputModel model)
        {
            if (model.Model.Length > 50) return BadRequest("Modelo não pode ter mais de 50 caracteres");

            var car = new Car(model.VinCode, model.Brand, model.Model, model.Year, model.Price, model.Color, model.ProductionDate);

            _dbContext.Cars.Add(car);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                nameof(GetById),
                new { id = car.Id },
                model
            );
        }



        /// <summary>
        /// Atualizar dados de um Carro
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo:
        /// {
        ///     "price": "50000",
        ///     "color": "Cinza",
        /// }
        /// </remarks>
        /// <param name="Id">Identificador de um Carro</param>
        /// <param name="model">Dados de alteração</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="204">Atualização bem-sucedida</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="404">Carro não encontrado</response>

        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int Id, [FromBody] UpdateCarInputModel model)
        {
            if (model.Price < 0) return BadRequest("Preço não pode ser menor que 0");

            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == Id);

            if (car == null) return NotFound();

            //car.Update(model.Color, model.Price);

            using( var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "UPDATE Cars SET Color = @color, Price = @price WHERE Id = @id";

                sqlConnection.Execute(query, new { car.Color, car.Price, car.Id });
            }

            return NoContent();
        }

        [HttpDelete("{Id}")]

        public IActionResult Delete(int Id)
        {
            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == Id);

            if (car == null) return NotFound();

            car.SetAsSuspended();
            _dbContext.SaveChanges();

            return NoContent();
        }

    }
}
