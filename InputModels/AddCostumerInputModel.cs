﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevCarsAPI.InputModels
{
    public class AddCostumerInputModel
    {
        public string FullName { get; set; }

        public string Document { get; set; }

        public DateTime BirthDate { get; set; }

    }
}
