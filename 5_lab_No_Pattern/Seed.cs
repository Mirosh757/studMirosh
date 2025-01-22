using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_lab_No_Pattern
{
    internal class Seed
    {
        public void Content()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Region region = new Region() { region_name = "Улан-Удэ"};
                db.regions.Add(region);
                region = new Region() { region_name = "Иркутск" };
                db.regions.Add(region);
                region = new Region() { region_name = "Владивосток" };
                db.regions.Add(region);
                region = new Region() { region_name = "Москва" };
                db.regions.Add(region);
                region = new Region() { region_name = "Санкт-Питербург" };
                db.regions.Add(region);
                region = new Region() { region_name = "Кызыл" };
                db.regions.Add(region);
                region = new Region() { region_name = "Томск" };
                db.regions.Add(region);
                region = new Region() { region_name = "Омск" };
                db.regions.Add(region);
                region = new Region() { region_name = "Чита" };
                db.regions.Add(region);
                region = new Region() { region_name = "Сочи" };
                db.regions.Add(region);
                region = new Region() { region_name = "Красноярск" };
                db.regions.Add(region);
                region = new Region() { region_name = "Новосибирск" };
                db.regions.Add(region);
                db.SaveChanges();
            }
        }
    }
}
