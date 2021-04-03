﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.Entities
{
    public class Order
    {
        protected Order() { }
        public Order(int idCar, int idCostumer, decimal Price ,List<ExtraOrderItem> items)
        {
            IdCar = idCar;
            IdCostumer = idCostumer;
            TotalCost = items.Sum(i => i.Price) + Price;
            ExtraItems = items;
        }

        public int Id { get; private set; }
        public int IdCar { get; private set; }
        public Car Car { get; private set; }
        public int IdCostumer { get; private set; }
        public Customer Customer { get; private set; }
        public decimal TotalCost { get; private set; }
        public List<ExtraOrderItem> ExtraItems { get; private set; }
    }

    public class ExtraOrderItem
    {
        public ExtraOrderItem(string description, decimal price)
        {
            Description = description;
            Price = price;
        }

        public int Id { get; private set; }

        public string Description { get; private set; }

        public decimal Price { get; private set; }

        public int IdOrder { get; private set; }
    }
}
