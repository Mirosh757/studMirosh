using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_SRS_DB
{
    internal class Car : Vehicle
    {
        private int passengerCapacity = 0;
        public int PassengerCapacity 
        { 
            set
            {
                if (value < 1 || value > 12)
                    Console.WriteLine("Вместительность машины не может быть меньше 1 и больше 12");
                else
                    passengerCapacity = value;
            }
            get => passengerCapacity;
        }
        private string bodyType = "";
        public string BodyType 
        {
            set
            {
                var validTypes = new[] { "Sedan", "Hatchback", "SUV", "Coupe", "StationWagon", "Convertible", "Minivan", "Pickup" };
                if (!validTypes.Contains(value))
                    Console.WriteLine("Введенный тип кузова не существует");
                else
                    bodyType = value;
            }
            get => bodyType;
        }
    }
}
