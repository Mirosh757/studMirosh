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
                if (value.Length < 2 || value.Length > 50)
                    Console.WriteLine("Фамилия должно иметь от 2 до 50 симвоолв");
                else
                {
                    if (Regex.IsMatch(value, @"\d"))
                        Console.WriteLine("Фамилия не может содержать цифры");
                    else
                    {
                        if (Regex.IsMatch(value, @"\P{IsCyrillic}") && !Regex.IsMatch(value, @"-"))
                            Console.WriteLine("Фамилия не может содержать символы латинского алфавита");
                        else
                        {
                            if (!Regex.IsMatch(value, "-"))
                                _family = value.ToUpper()[0] + value.ToLower().Substring(1);
                            else
                            {
                                string[] strings = value.Split("-");
                                bool flag = false;
                                for (int i = 0; i < strings.Length; i++)
                                {
                                    if (strings[i] == "" || strings[i].Length < 2)
                                    {
                                        Console.WriteLine("Первая и последняя часть фамилии должно иметь от 2 до 50 символов");
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag)
                                {
                                    for (int i = 0; i < strings.Length; i++)
                                        strings[i] = strings[i].ToUpper()[0] + strings[i].ToLower().Substring(1);
                                    for (int i = 0; i < strings.Length; i++)
                                        _family += strings[i] + '-';
                                    _family = _family.Substring(0, _family.Length - 1);
                                }
                            }
                        }
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
                if (value.Length < 2 || value.Length > 30)
                    Console.WriteLine("Имя должно иметь от 2 до 30 симвоолв");
                else
                {
                    if (Regex.IsMatch(value, @"\d"))
                        Console.WriteLine("Имя не может содержать цифры");
                    else
                    {
                        if (Regex.IsMatch(value, @"\P{IsCyrillic}") && !Regex.IsMatch(value, @"-"))
                            Console.WriteLine("Имя не может содержать символы латинского алфавита");
                        else
                        {
                            if(!Regex.IsMatch(value, "-"))
                                _name = value.ToUpper()[0] + value.ToLower().Substring(1);
                            else
                            {
                                string[] strings = value.Split("-");
                                bool flag = false;
                                for (int i = 0; i < strings.Length; i++)
                                {
                                    if (strings[i] == "" || strings[i].Length < 2)
                                    {
                                        Console.WriteLine("Первая и последняя часть имени должно иметь от 2 до 30 символов");
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag)
                                {
                                    for (int i = 0; i < strings.Length; i++)
                                        strings[i] = strings[i].ToUpper()[0] + strings[i].ToLower().Substring(1);
                                    for (int i = 0; i < strings.Length; i++)
                                        _name += strings[i] + '-';
                                    _name = _name.Substring(0, _name.Length - 1);
                                }
                            }
                        }
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
                if (value != "-")
                {
                    if (value.Length < 4 || value.Length > 50)
                        Console.WriteLine("Отчество должно иметь от 4 до 50 симвоолв");
                    else
                    {
                        if (Regex.IsMatch(value, @"\d"))
                            Console.WriteLine("Отчество не может содержать цифры");
                        else
                        {
                            if (Regex.IsMatch(value, @"\P{IsCyrillic}") && !Regex.IsMatch(value, @"-"))
                                Console.WriteLine("Отчество не может содержать символы латинского алфавита");
                            else
                            {
                                if (!Regex.IsMatch(value, "-"))
                                    _patronymic = value.ToUpper()[0] + value.ToLower().Substring(1);
                                else
                                {
                                    string[] strings = value.Split("-");
                                    bool flag = false;
                                    for (int i = 0; i < strings.Length; i++)
                                    {
                                        if (strings[i] == "")
                                        {
                                            Console.WriteLine("Все части отчества кроме последней должны иметь от 2 до 30 символов, а последняя от 4 до 50");
                                            flag = true;
                                            break;
                                        }
                                        else
                                        {
                                            if(i != strings.Length - 1)
                                            {
                                                if (strings[i].Length < 2)
                                                {
                                                    Console.WriteLine("Все части отчества кроме последней должны иметь от 2 до 30 символов, а последняя от 4 до 50");
                                                    flag = true;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                if (strings[i].Length < 4)
                                                {
                                                    Console.WriteLine("Отчество должно иметь от 4 до 50 симвоолв");
                                                    flag = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    if (!flag)
                                    {
                                        for (int i = 0; i < strings.Length; i++)
                                            strings[i] = strings[i].ToUpper()[0] + strings[i].ToLower().Substring(1);
                                        for (int i = 0; i < strings.Length; i++)
                                            _patronymic += strings[i] + '-';
                                        _patronymic = _patronymic.Substring(0, _patronymic.Length - 1);
                                    }
                                }
                            }
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
                    {
                        if (value < new DateTime(1903, 01, 01) || value > new DateTime(2013,01,01))
                            Console.WriteLine("Возраст пользователя не может быть больше 123 и не моложе 12 лет");
                        else
                            _birth_date = value;
                    }
                }
            }
            get { return _birth_date; }
        }
        private int[]? _tags = null!;
        public int[]? tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
    }
}
