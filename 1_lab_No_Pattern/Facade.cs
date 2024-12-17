using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_lab_No_Pattern
{
    internal class Facade
    {
        DoctorMapper mapper = new DoctorMapper();
        public void PrintMenu()
        {
            Console.WriteLine("0.   Вставить объект в базу\n1.   Вывод всех экземпляров модели\n2.   Вывод конкретного экземпляра модели по id\n3.    Изменение значений атрибутов модели по id\n4.    Удаление модели из базы по id\n5.    Удаление моделей из базы по списку значений id");
            string line = Console.ReadLine();
            if (line.Length != 1 && (line != "1" || line != "2" || line != "3" || line != "4" || line != "5" || line != "0"))
            {
                Console.WriteLine("Неверная команда");
                PrintMenu();
            }
            else
            {
                switch (line)
                {
                    case "0": mapper.DoctorCreate(); PrintMenu(); break;
                    case "1": mapper.DoctorRetrieveAll(); PrintMenu(); break;
                    case "2": mapper.DoctorRetrieve(); PrintMenu(); break;
                    case "3": mapper.DoctorUpdate(); PrintMenu(); break;
                    case "4": mapper.DoctorDelete(); PrintMenu(); break;
                    case "5": mapper.DoctorDeleteMany(); PrintMenu(); break;
                }
            }
        }
    }
}
