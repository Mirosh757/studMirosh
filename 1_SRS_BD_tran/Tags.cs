using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace _1_lab_BD_tran
{
    [Index("title", IsUnique = true, Name = "title_Index")]
    internal class tags
    {
        private int _id = 0;
        public int id
        {
            set 
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    tags tag = db.tags.Where(t => t.id == (value)).DefaultIfEmpty().First();
                    if (tag != null)
                        _id = value;
                    else
                        Console.WriteLine("Данного id не существует в таблице tags");
                }
            }
            get { return _id; }
        }
        private string _title = "";
        public string title
        {
            set
            {
                if (value.Length < 2 || value.Length > 20)
                    Console.WriteLine("Название тэга должно иметь от 2 до 20 симвоолв");
                else
                {
                    if (Regex.IsMatch(value, @"\P{IsCyrillic}") && !Regex.IsMatch(value, @"\d"))
                        Console.WriteLine("Название тэга не может содержать символы латинского алфавита");
                    else
                    {
                        if (value.Any(p => !char.IsLetterOrDigit(p)))
                            Console.WriteLine("Название тэга не должен содержать спец символы");
                        else
                        {
                            _title = value;
                        }
                    }
                }
            }
            get { return _title; }
        }
    }
}
