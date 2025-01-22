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
        [Obsolete]
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
                    LANGUAGE plpgsql
                    AS $$
                    BEGIN
                        value1 := TRIM(value1);
                        IF LENGTH(value1) < 2 OR LENGTH(value1) > 50 THEN 
                            RAISE EXCEPTION 'Длина названия города не может быть меньше двух и больше 50';
                        END IF;
	                    IF NOT value1 ~ '^[А-Яа-я -]+$' THEN
		                    RAISE EXCEPTION 'Неверное написание города';
	                    END IF;
	                    IF EXISTS (SELECT 1 FROM regions WHERE LOWER(region_name) = LOWER(value1)) THEN
		                    RAISE EXCEPTION 'Город с таким названием уже существует';
	                    END IF;
	                    INSERT INTO regions(region_name) VALUES (value1);
                    END
                    $$;

                    CREATE OR REPLACE PROCEDURE delete_region(value1 integer)
                    LANGUAGE SQL
                    AS $$
                    DELETE FROM regions WHERE id = value1;
                    $$;

                    CREATE OR REPLACE PROCEDURE update_region(IN value1 integer,IN value2 VARCHAR(50))
                    LANGUAGE plpgsql
                    AS $$
                    BEGIN
                        value2 := TRIM(value2);
                        IF LENGTH(value2) < 2 OR LENGTH(value2) > 50 THEN 
                            RAISE EXCEPTION 'Длина названия города не может быть меньше двух и больше 50';
                        END IF;
                        IF EXISTS (SELECT 1 FROM regions WHERE id <> value1) THEN 
                            RAISE EXCEPTION 'Данного id не существует в таблице';
                        END IF;
	                    IF NOT value2 ~ '^[А-Яа-я -]+$' THEN
		                    RAISE EXCEPTION 'Неверное написание города';
	                    END IF;
	                    IF EXISTS (SELECT 1 FROM regions WHERE LOWER(region_name) = LOWER(value2)) THEN
		                    RAISE EXCEPTION 'Город с таким названием уже существует';
	                    END IF;
                        UPDATE regions SET region_name = value2 WHERE id = value1;
                    END
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
