using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarInventory
{
    class Car
    {
        public string year, make, colour, mileage;

        public Car (string _year, string _make, string _colour, string _mileage)
        {
                year = _year;
            make = _make;
            colour = _colour;
            mileage = _mileage;
        }
    }
}
