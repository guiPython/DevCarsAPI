using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.Entities
{
    public class Costumer
    {
        public Costumer(int id, string fullName, string document, DateTime birthDate)
        {
            Id = id;
            FullName = fullName;
            Document = document;
            BirthDate = birthDate;
            Orders = new List<Order>();
        }

        public int Id { get; private set; }

        public string FullName { get; private set; }

        public string Document { get; private set; }

        public DateTime BirthDate { get; private set; }

        public List<Order> Orders { get; private set; }

        public void Purchase(Order order) => Orders.Add(order);
    }
}
