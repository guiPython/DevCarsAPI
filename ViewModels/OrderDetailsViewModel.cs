using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDetailsViewModel(int idCar, int idCostumer, decimal totalCost, List<string> extraItems)
        {
            IdCar = idCar;
            IdCostumer = idCostumer;
            TotalCost = totalCost;
            ExtraItems = extraItems;
        }

        public int IdCar { get; set; }

        public int IdCostumer { get; set; }

        public decimal TotalCost { get; set; }

        public List<string> ExtraItems { get; set; }
    }
}
