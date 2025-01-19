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
                animals animals = new animals() { title = "Животные", path = "1/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Эуметазои", path = "1/2/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Прометазои", path = "1/3/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Билатерии", path = "1/2/4/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Кишечнополостые", path = "1/2/5/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Губки", path = "1/3/6/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Пластинчатые", path = "1/3/7/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Первичнородные", path = "1/2/4/8/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Вторичнородные", path = "1/2/4/9/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Гребники", path = "1/2/5/10/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Стрекающие", path = "1/2/5/11/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Линяющие", path = "1/2/4/8/12/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Спиральные", path = "1/2/4/8/13/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Амбулакральные", path = "1/2/4/9/14/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Хордовые", path = "1/2/4/9/15/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Иглокожие", path = "1/2/4/9/14/16/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Полухордовые", path = "1/2/4/9/14/17/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Обладающие обонянием", path = "1/2/4/9/15/18/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Бесчерепные", path = "1/2/4/9/15/19/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Позвоночные", path = "1/2/4/9/15/18/20/" };
                db.animals.Add(animals);
                animals = new animals() { title = "Оболочки", path = "1/2/4/9/15/18/21/" };
                db.animals.Add(animals);
                db.SaveChanges();
            }
        }
    }
}
