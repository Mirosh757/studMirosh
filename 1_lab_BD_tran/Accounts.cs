using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1_lab_BD_tran
{
    internal class accounts
    {
        public int user_id;
        [ForeignKey("user_id")]
        public users Users { get; set; } = null!;
        private string _family = "";
        public string family // фамилия
        {
            set
            {
                if (value.Trim().Length < 2 || value.Trim().Length > 50)
                    Console.WriteLine("Фамилия должно иметь от 2 до 50 симвоолв");
                else
                {
                    if (Regex.IsMatch(value, @"\d"))
                        Console.WriteLine("Фамилия не может содержать цифры");
                    else
                    {
                        if (Regex.IsMatch(value, @"\P{IsCyrillic}"))
                            Console.WriteLine("Фамилия не может содержать символы латинского алфавита");
                        else
                            _family = value.Trim().ToUpper()[0] + value.Trim().ToLower().Substring(1);
                    }
                }

            }
            get { return _family; }
        }
        private string _name = "";
        public string name // имя
        {
            set
            {
                if (value.Trim().Length < 2 || value.Trim().Length > 30)
                    Console.WriteLine("Имя должно иметь от 2 до 30 симвоолв");
                else
                {
                    if (Regex.IsMatch(value, @"\d"))
                        Console.WriteLine("Имя не может содержать цифры");
                    else
                    {
                        if (Regex.IsMatch(value, @"\P{IsCyrillic}"))
                            Console.WriteLine("Имя не может содержать символы латинского алфавита");
                        else
                            _name = value.Trim().ToUpper()[0] + value.Trim().ToLower().Substring(1);
                    }
                }

            }
            get { return _name; }   
        }
        private string _patronymic = "";
        public string patronymic
        {
            set
            {
                if (value != "_")
                {
                    if (value.Trim().Length < 4 || value.Trim().Length > 50)
                        Console.WriteLine("Отчество должно иметь от 4 до 50 симвоолв");
                    else
                    {
                        if (Regex.IsMatch(value, @"\d"))
                            Console.WriteLine("Отчество не может содержать цифры");
                        else
                        {
                            if (Regex.IsMatch(value, @"\P{IsCyrillic}"))
                                Console.WriteLine("Отчество не может содержать символы латинского алфавита");
                            else
                                _patronymic = value.Trim().ToUpper()[0] + value.Trim().ToLower().Substring(1);
                        }
                    }
                }
                else
                    _patronymic = "NULL";
            }
            get { return _patronymic; }
        }
        private DateTime _birth_date = new DateTime();
        public DateTime birth_date
        {
            set
            {
                if (value == new DateTime())
                    Console.WriteLine("Дата рождения не введена");
                else
                {
                    if (value > DateTime.Now)
                        Console.WriteLine("Дата рождения не может быть больше текущей даты");
                    else
                        _birth_date = value;
                }
            }
            get { return _birth_date; }
        }
    }
}
