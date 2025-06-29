using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_SRS_DB
{
    internal class Truck : Vehicle
    {
        private decimal loadCapacity = 0;
        public decimal LoadCapacity
        {
            set
            {
                if (value <= 0 || value > 100) // 100 тонн — пример максимума
                    Console.WriteLine("Грузоподъёмность должна быть от 0.1 до 100 тонн");
                else
                    loadCapacity = value;
            }
            get => loadCapacity;
        }
        private int axleCount = 0; 
        public int AxleCount
        {
            set
            {
                if (value < 2 || value > 10)
                    Console.WriteLine("Количество осей(пар колес) должно быть от 2 до 10");
                else
                    axleCount = value;
            }
            get => axleCount;
        }
    }
}
