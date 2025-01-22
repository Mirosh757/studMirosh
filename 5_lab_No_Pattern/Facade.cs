using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _5_lab_No_Pattern
{
    internal class Facade
    {
        private void RegionCreate()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите название региона\nregion_name: ");
                string line = Console.ReadLine();
                int forCreate = 0;
                try
                {
                    forCreate = db.Database.ExecuteSqlRaw($"CALL insert_region('{line}')");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    RegionCreate();
                    return;
                }
                if (forCreate != 0)
                {
                    int iD = db.regions.ToList().Last().id;
                    Console.WriteLine($"Вставка прошла успешно\nОбъект вставлен с id = {iD}");
                }
                else
                    Console.WriteLine("Вставка не прошла успешно");

            }
            PrintMenu();
        }
        private void RegionRetrieveAll()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Region> regions = db.regions.ToList();
                foreach (Region region in regions)
                {
                    Console.WriteLine($"{region.id}   {region.region_name}");
                }
            }
            PrintMenu();
        }
        private void RegionRetrieve()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите id объекта\nid: ");
                string id = Console.ReadLine().Trim();
                while(!Regex.IsMatch(id, @"\G[0-9]"))
                {
                    Console.Write("id может состоять только из цифр\nid: ");
                    id = Console.ReadLine().Trim();
                }
                var regions = db.regions.FromSqlRaw($"SELECT * FROM regions WHERE id = {id}").ToList().FirstOrDefault();
                if (regions != null)
                    Console.WriteLine($"{regions.id}   {regions.region_name}");
                else
                    Console.WriteLine($"Объекта с id = {id} нет в базе");
            }
            PrintMenu();
        }
        private void RegionUpdate()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите id объекта\nid: ");
                string id = Console.ReadLine();
                while (!Regex.IsMatch(id, @"\G[0-9]"))
                {
                    Console.Write("id может состоять только из цифр\nid: ");
                    id = Console.ReadLine();
                }
                Console.Write("Введите название региона\n region_name: ");
                string line = Console.ReadLine();
                int forUpdate = 0;
                try
                {
                    forUpdate = db.Database.ExecuteSqlRaw($"CALL update_region({id} , '{line}')");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    RegionUpdate();
                    return;
                }
                if (forUpdate != 0)
                    Console.WriteLine("Обновление прошло успешно");
                else
                    Console.WriteLine("Обновление не прошло успешно");
            }
            PrintMenu();
        }
        private void RegionDelete()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int kol = db.regions.Count();
                Console.Write("Введите id объекта\nid: ");
                string id = Console.ReadLine().Trim();
                while (!Regex.IsMatch(id, @"\G[0-9]"))
                {
                    Console.Write("id может состоять только из цифр\nid: ");
                    id = Console.ReadLine().Trim();
                }
                int forDelete = db.Database.ExecuteSqlRaw($"CALL delete_region({id})");
                if (forDelete != 0 && (kol - db.regions.Count()) != 0)
                    Console.WriteLine("удаление прошло успешно");
                else
                    Console.WriteLine("удаление не прошло успешно");
            }
            PrintMenu();
        }
        private void RegionDeleteMany()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int kol = db.regions.Count();
                Console.Write("Введите id объекта через пробел\nid: ");
                string id = Console.ReadLine().Trim();
                while (!Regex.IsMatch(id, @"\G[0-9  ]"))
                {
                    Console.Write("id может состоять только из цифр\nid: ");
                    id = Console.ReadLine().Trim();
                }
                int forDeleteMany = db.Database.ExecuteSqlRaw($"CALL delete_many_region('/{id.Replace(' ', '/')}/')");
                if(forDeleteMany != 0)
                {
                    Console.WriteLine($"Удалено {kol - db.regions.Count()} записей");
                }
            }
            PrintMenu();
        }
        public void PrintMenu()
        {
            Console.WriteLine("0.    Вставить объект в базу\n1.    Вывод всех экземпляров модели\n2.    Вывод конкретного экземпляра модели по id\n3.    Изменение значений атрибутов модели по id\n4.    Удаление модели из базы по id\n5.    Удаление моделей из базы по списку значений id");
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
                    case "0": RegionCreate(); PrintMenu(); break;
                    case "1": RegionRetrieveAll(); PrintMenu(); break;
                    case "2": RegionRetrieve(); PrintMenu(); break;
                    case "3": RegionUpdate(); PrintMenu(); break;
                    case "4": RegionDelete(); PrintMenu(); break;
                    case "5": RegionDeleteMany(); PrintMenu(); break;
                }
            }
        }
    }
}
