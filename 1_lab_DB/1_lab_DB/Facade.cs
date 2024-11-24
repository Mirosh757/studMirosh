using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_lab_DB
{
    internal class Facade
    {
        Doctor _doctor = null;
        DoctorMapper _mapper = new DoctorMapper();
        bool flag = false;
        private void CreateObject()
        {
            Console.WriteLine("Значения атрибутов писать через запятую\nname\t address\t passport_details\t date_birth");
            string line = Console.ReadLine();
            string[] data = line.Split(',');
            Doctor doctor1 = new Doctor(DateTime.Now, DateTime.Now, data[0], data[1], data[2], _mapper.StringToDate(data[3]));
            if (_doctor != doctor1)
                _doctor = doctor1;
            else 
                Console.WriteLine("Объект уже создан"); 
            flag = true;
            PrintMenu();
        }
        private void PrintObject()
        {
            if (flag)
                Console.WriteLine(_doctor.GetInfo());
            else Console.WriteLine("Объект ещё не создан");
            PrintMenu();
        }
        private void EraseObject()
        {
            if (flag)
            {
                _doctor = null;
                flag = false;
            }
            PrintMenu();
        }
        private void GetObjectFromDataBase()
        {
            Console.WriteLine("Введите id объекта");
            string line = Console.ReadLine();
            Doctor doctor1 = (Doctor)_mapper.Find(Int32.Parse(line));
            if (_doctor != doctor1)
                _doctor = doctor1;
            else 
                Console.WriteLine("Объект уже создан"); 
            flag = true;
            PrintMenu();
        }
        private void InsertObjectFromDataBase()
        {
            if (!flag)
                Console.WriteLine("Объект еще не создан");
            else
            {
                int k = _mapper.Insert(_doctor);
                Console.WriteLine($"Кол-во добавленных записей - {k}");
            }
            PrintMenu();
        }
        private void UpdateObjectFromDataBase()
        {
            if (!flag)
                Console.WriteLine("Объект еще не создан");
            else
            {
                Console.WriteLine("Выберите один или несколько атрибутов, которые желаете изменить\n0.   name\t1.   address\t2.    passport_details\t3.    date_birth");
                string line = Console.ReadLine();
                string[] data = line.Split(' ');
                for(int i = 0;i < data.Length;i++)
                {
                    if (data[i] != "0" || data[i] == "1" || data[i] == "2" || data[i] == "3")
                    {
                        Console.WriteLine("Введенно не верное число");
                        PrintMenu();
                    }
                    else
                    {
                        switch (data[i])
                        {
                            case "0": _doctor.Name = Console.ReadLine(); break;
                            case "1": _doctor.Address = Console.ReadLine(); break;
                            case "2": _doctor.PassportDetails = Console.ReadLine(); break;
                            case "3": _doctor.DateBirth = _mapper.StringToDate(Console.ReadLine()); break;
                        }
                    }
                }
                _mapper.Update(_doctor);
                
            }
            PrintMenu();
        }
        private void DeleteObjectFromDataBase()
        {
            if (!flag)
                Console.WriteLine("Объект еще не создан\n");
            else
            {
                Console.WriteLine("1.   Для удаления 1 объекта\t2.  Для удаления мн-ва объектов\n");
                string line = Console.ReadLine();
                try
                {
                    int k;
                    if (line == "1")
                        k = _mapper.Delete(1);
                    else
                    {
                        if (line == "2")
                        {
                            Console.WriteLine("Введите через пробел все id, которые желаете удалить\n");
                            string l = Console.ReadLine();
                            string[] data = l.Split(' ');
                            int[] ints = new int[data.Length];
                            for(int i = 0;i < ints.Length;i++)
                                ints[i] = Convert.ToInt32(data[i]);
                            k = _mapper.DeleteMany(ints);
                            Console.WriteLine($"Кол-во удаленных записей - {k}");
                        }
                        else
                        {
                            Console.WriteLine("Введена не верная команда");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Введено не верное id");
                }
            }
            PrintMenu();

        }
        public void PrintMenu()
        {
            Console.WriteLine("0.   Создать объект класса\n1.   Вывести объект класса\n2.   Получить объект из базы\n3.   Вставить объект в базу\n4.   Обновить объект из базы\n5.   Удалить объект(ы) из базы\n6.    Удалить объект\n");
            string line = Console.ReadLine();
            if (line.Length != 1 && (line != "1" || line != "2" || line != "3" || line != "4" || line != "5" || line != "6"))
            {
                Console.WriteLine("Неверная команда");
                PrintMenu();
            }
            else
            {
                switch (line)
                {
                    case "0": CreateObject(); break;
                    case "1": PrintObject(); break;
                    case "2": GetObjectFromDataBase(); break;
                    case "3": InsertObjectFromDataBase(); break;
                    case "4": UpdateObjectFromDataBase(); break;
                    case "5": DeleteObjectFromDataBase(); break;
                    case "6": EraseObject(); break;
                }
            }
        }
    }
}
