using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _5_lab_No_Pattern
{
    internal class Region
    {
        private int _id;
        public int id 
        { 
            set { _id = value; }
            get { return _id; }
        }
        private string _region_name;
        [MaxLength(50)]
        public string region_name
        {
            set
            {
                if (!Regex.IsMatch(value, @"[0-9 ]"))
                {
                    if (Regex.IsMatch(value, @"\G[А-Яа-я -]"))
                    {
                        if (value.Length < 51)
                            _region_name = value;
                        else
                            Console.WriteLine("Длина названия города макс 50 символов");
                    }
                    else
                        Console.WriteLine("Название города должно использовать только русские символы");
                }
                else
                    Console.WriteLine("Название города не должен содержать в себе цифры и пробелы");
            }
            get { return _region_name; }
        }
        public void CreateProcedure()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                int regionProcedure = db.Database.ExecuteSqlRaw(@"
                    CREATE OR REPLACE PROCEDURE insert_region(value1 VARCHAR(50))
                    LANGUAGE SQL
                    AS $$
                    INSERT INTO regions(region_name) VALUES (value1);
                    $$;

                    CREATE OR REPLACE PROCEDURE delete_region(value1 integer)
                    LANGUAGE SQL
                    AS $$
                    DELETE FROM regions WHERE id = value1;
                    $$;

                    CREATE OR REPLACE PROCEDURE update_region(IN value1 integer,IN value2 VARCHAR(50))
                    LANGUAGE SQL
                    AS $$
                    UPDATE regions SET region_name = value2 WHERE id = value1
                    $$;

                    CREATE OR REPLACE PROCEDURE delete_many_region(line TEXT)
                    LANGUAGE SQL
                    AS $$
                    DELETE FROM regions WHERE line LIKE CONCAT('%/', id, '/%');
                    $$;
                    ");
            }
        }
    }
}
