﻿using System;
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
    [Index("login", IsUnique = true, Name = "login_Index")]
    internal class users
    {
        private int _id;
        public int id
        {
            set { _id = value; }
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
                    if (Regex.IsMatch(value, @"\G\p{IsCyrillic}"))
                        Console.WriteLine("Логин должен содержать буквы латинского алфавита");
                    else
                    {
                        if (value.Length < 10 || value.Length > 30)
                            Console.WriteLine("Длина логина должна быть от 10 до 30 символов");
                        else
                        {
                            string[] helpCheck = value.Split("@");
                            if (helpCheck[0].Any(p => !char.IsLetterOrDigit(p)) || helpCheck.Length > 2)
                                Console.WriteLine("Логин не может содержать спец символы");
                            else
                            {
                                bool flag = true;
                                string help = helpCheck[0];
                                for(int i = 0; i < help.Length - 1;i++)
                                {
                                    if (help[i] != help[i + 1])
                                        flag = false;
                                }
                                if (!flag)
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
                                else
                                    Console.WriteLine("Логин не может состоять из одного символа");
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
                if (value.Length < 8 || value.Length > 32)
                    Console.WriteLine("Длина пароля должна быть от 8 до 32");
                else
                {
                    if(value.Trim().Length == 0)
                    {
                        Console.WriteLine("Пароль не может состоять только пробелов");
                    }
                    else
                        _password = value;
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
