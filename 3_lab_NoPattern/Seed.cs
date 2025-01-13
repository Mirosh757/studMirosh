using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3_lab_NoPattern
{
    internal class Seed
    {
        public void Content()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                animals animals = new animals() { title = "Животные", parent_id = 0 };
                db.animals.Add(animals);
                animals = new animals() { title = "Эуметазои", parent_id = 1 };
                db.animals.Add(animals);
                animals = new animals() { title = "Прометазои", parent_id = 1 };
                db.animals.Add(animals);
                animals = new animals() { title = "Билатерии", parent_id = 2 };
                db.animals.Add(animals);
                animals = new animals() { title = "Кишечнополостые", parent_id = 2 };
                db.animals.Add(animals);
                animals = new animals() { title = "Губки", parent_id = 3 };
                db.animals.Add(animals);
                animals = new animals() { title = "Пластинчатые", parent_id = 3 };
                db.animals.Add(animals);
                animals = new animals() { title = "Первичнородные", parent_id = 4 };
                db.animals.Add(animals);
                animals = new animals() { title = "Вторичнородные", parent_id = 4 };
                db.animals.Add(animals);
                animals = new animals() { title = "Гребники", parent_id = 5 };
                db.animals.Add(animals);
                animals = new animals() { title = "Стрекающие", parent_id = 5 };
                db.animals.Add(animals);
                animals = new animals() { title = "Линяющие", parent_id = 8 };
                db.animals.Add(animals);
                animals = new animals() { title = "Спиральные", parent_id = 8 };
                db.animals.Add(animals);
                animals = new animals() { title = "Амбулакральные", parent_id = 9 };
                db.animals.Add(animals);
                animals = new animals() { title = "Хордовые", parent_id = 9 };
                db.animals.Add(animals);
                animals = new animals() { title = "Иглокожие", parent_id = 14 };
                db.animals.Add(animals);
                animals = new animals() { title = "Полухордовые", parent_id = 14 };
                db.animals.Add(animals);
                animals = new animals() { title = "Обладающие обонянием", parent_id = 15 };
                db.animals.Add(animals);
                animals = new animals() { title = "Бесчерепные", parent_id = 18 };
                db.animals.Add(animals);
                animals = new animals() { title = "Позвоночные", parent_id = 18 };
                db.animals.Add(animals);
                animals = new animals() { title = "Оболочки", parent_id = 18 };
                db.animals.Add(animals);
                db.SaveChanges();
            }
        }
    }
}
