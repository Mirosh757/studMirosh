using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_SRS_DB
{
    internal class Motorcycle : Vehicle
    {
        private decimal engineVolume = 0;
        public decimal EngineVolume
        {
            set
            {
                if (value < 50m || value > 3500m)
                    Console.WriteLine("Объём двигателя должен быть 50–3500 см^3");
                else
                    engineVolume = value;
            }
            get => engineVolume;
        }
        private bool isRacing = false;
        public bool IsRacing
        {
            get => isRacing;
            set
            {
                isRacing = value;
            }
        }
    }
}
