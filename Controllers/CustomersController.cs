using DevCarsAPI.Entities;
using DevCarsAPI.InputModels;
using DevCarsAPI.Persistence;
using DevCarsAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.Controllers
{
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly DevCarsDBContext _dbContext;
        public CustomersController(DevCarsDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]

        public IActionResult Post([FromBody] AddCostumerInputModel model)
        {
            var costumer = new Customer(model.FullName, model.Document, model.BirthDate);

            _dbContext.Customers.Add(costumer);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("{Id}/{orderId}")]

        public IActionResult GetOrder(int Id, int orderId)
        {
  
            var order = _dbContext.Orders
                .Include(o => o.ExtraItems)
                .SingleOrDefault(e => e.Id == orderId);

            if  (order == null) return NotFound();

            var extraItems = order.ExtraItems.Select(e => e.Description).ToList();

            var orderViewModel = new OrderDetailsViewModel(order.IdCar, order.IdCostumer, order.TotalCost, extraItems);

            return Ok(orderViewModel);
        }


        [HttpPost("{Id}")]

        public IActionResult PostOrder(int Id, [FromBody] AddOrderInputModel model)
        {

            var extraItems = model.ExtraItems.Select(e => new ExtraOrderItem(e.Description, e.Price)).ToList();

            var car = _dbContext.Cars.SingleOrDefault(c => c.Id == model.IdCar);
            var costumer = _dbContext.Customers.SingleOrDefault(c => c.Id == model.IdCostumer);

            if (car == null || costumer == null) return NotFound();

            var order = new Order(model.IdCar, model.IdCostumer, car.Price, extraItems);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return CreatedAtAction(
                    nameof(GetOrder),
                    new { Id = costumer.Id, orderId = order.Id },
                    model
                );
        }

        [HttpDelete("{Id}")]

        public IActionResult Delete(int Id)
        {
            var costumer = _dbContext.Customers.SingleOrDefault(c => c.Id == Id);

            if (costumer == null) return NotFound();

            _dbContext.Customers.Remove(costumer);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{Id}/{orderId}")]

        public IActionResult DeleteOrder(int Id, int orderId)
        {
            var costumer = _dbContext.Customers.SingleOrDefault(c => c.Id == Id);

            if (costumer == null) return NotFound();

            var order = costumer.Orders.SingleOrDefault(e => e.Id == orderId);

            if (order == null) return NotFound();

            costumer.Orders.Remove(order);
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
