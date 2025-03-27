using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace _1_lab_BD_tran
{
    [Index("login", IsUnique = true, Name = "login_Index")]
    internal class users
    {
        private int _id = 0;
        public int id
        {
            set
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    users user = db.users.Where(t => t.id == (value)).DefaultIfEmpty().First();
                    if (user != null)
                        _id = value;
                    else
                        Console.WriteLine("Данного id не существует в таблице users");
                }
            }
            get { return _id; }
        }
        private string[] emailDomains = new string[]
        {
            "@mail.ru",
            "@gmail.com",
            "@yandex.ru",
            "@bsu.ru"
        };
        private string _login = "";
        public string login
        {
            set
            {
                if (Regex.IsMatch(value, @"\s"))
                    Console.WriteLine("Логин не должен содержать пробелы");
                else
                {
                    if (Regex.IsMatch(value, @"\p{IsCyrillic}"))
                        Console.WriteLine("Логин должен содержать буквы латинского алфавита");
                    else
                    {
                        if (value.Length < 6 || value.Length > 30 || emailDomains.Contains(value) == true)
                            Console.WriteLine("Длина логина должна быть от 6 до 30 символов");
                        else
                        {
                            string[] helpCheck = value.Split("@");
                            if ((helpCheck[0].Any(p => !char.IsLetterOrDigit(p)) || helpCheck.Length > 2 ) && Regex.IsMatch(value, "[.]") == false)
                                Console.WriteLine("Логин не может содержать спец символы");
                            else
                            {
                                using (ApplicationContext db = new ApplicationContext())
                                {
                                    if (db.users.FromSqlRaw($"SELECT * FROM users WHERE login = '{value}'").Count() != 0)
                                        Console.WriteLine("Данный логин уже используется");
                                    else
                                    {
                                        for (int i = 0; i < emailDomains.Length; i++)
                                        {
                                            if (value.EndsWith(emailDomains[i]))
                                            {
                                                _login = value.ToLower();
                                                break;
                                            }
                                        }
                                        if (_login == "")
                                            Console.WriteLine("Введен не верный домен элетронной почты");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            get { return _login; }
        }
        private string _password = "";
        public string password
        {
            set
            {
                if (value.Length < 8 || value.Length > 100)
                    Console.WriteLine("Длина пароля должна быть от 8 до 100");
                else
                {
                    if(Regex.IsMatch(value, @"\s"))
                    {
                        Console.WriteLine("Пароль не может содержать пробелы");
                    }
                    else
                    {
                        if (Regex.IsMatch(value, @"\p{IsCyrillic}"))
                            Console.WriteLine("Пароль должен содержать буквы латинского алфавита");
                        else
                        {
                            if (value.Any(p => !char.IsLetterOrDigit(p)) && Regex.IsMatch(value, @"\p{P}") == false || Regex.IsMatch(value, @$"[#@$%^&*_=+\|'/><`~]") == true)
                                Console.WriteLine("Пароль не может содержать спец символы, кроме знаков препинания");
                            else
                            {
                                if (Regex.IsMatch(value, @"\d") || Regex.IsMatch(value, @"\p{L}"))
                                    _password = value;
                                else
                                    Console.WriteLine("Пароль должен содержать буквы или цифры");
                            }
                        }
                    }
                }
            }
            get { return _password; }
        }
        private DateTime _registration_date = new DateTime();
        public DateTime registration_date
        {
            set
            {
                if (value == new DateTime())
                    Console.WriteLine("Дата регистрации не введена");
                else
                {
                    if (value > DateTime.Now)
                        Console.WriteLine("Дата регистрации не может быть больше текущей даты");
                    else
                        _registration_date = value;
                }
            }
            get { return _registration_date; }
        }
        public virtual accounts Accounts { get; set; } = null!;
    }
}
