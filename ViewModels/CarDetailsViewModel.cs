using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.ViewModels
{
    public class CarDetailsViewModel
    {
        public CarDetailsViewModel(int id, string brand, string model, string vinCode, string color, int year, decimal price, DateTime productionDate)
        {
            Id = id;
            Brand = brand;
            Model = model;
            VinCode = vinCode;
            Color = color;
            Year = year;
            Price = price;
            ProductionDate = productionDate;
        }

        public int Id { get; set; }
        public string Brand { get; set; }

        public string Model { get; set; }

        public string VinCode { get; set; }

        public string Color { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public DateTime ProductionDate { get; set; }
    }
}
