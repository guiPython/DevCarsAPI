using DevCarsAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.Persistence
{
    public class DevCarsDBContext
    {
        public DevCarsDBContext()
        {
            Cars = new List<Car>
            {
                new Car(1,"123ABC","Honda","CIVIC",2019,120000,"Cinza",new DateTime(2019,1,1)),
                new Car(2,"456DEF","Toyota","Corolla",2020,140000,"Preto",new DateTime(2020,7,1)),
                new Car(3,"789FGH","Chevrolet","Onix",2021,75000,"Branco",new DateTime(2021,1,13)),
                new Car(4,"101IJK","Hyunday","Hb20",2018,65000,"Prata",new DateTime(2016,8,26))
            };

            Costumers = new List<Costumer>
            {
                new Costumer(1,"Guilherme Rocha Muzi Franco","123456789",new DateTime(1992,5,6)),
                new Costumer(2,"Gabriel Meneses Love Youu guy","123456789",new DateTime(1998,9,25)),
                new Costumer(3,"Thales Paixão vai pegar a @Becks","XXXTENTATION",new DateTime(2001,8,26))
            };
        }
        public List<Car> Cars { get; set; }

        public List<Costumer> Costumers { get; set; }
    }
}
