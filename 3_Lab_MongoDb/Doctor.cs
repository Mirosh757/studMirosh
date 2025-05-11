using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;
using System.Data.Common;
using System.Xml;
using System.Globalization;

namespace _2_Lab_MongoDb
{
    internal class Doctor
    {
        public Doctor() { }
        public ObjectId Id { get; set; }
        private string _name = "";
        public string name
        {
            set
            {
                _name = Validation("Имя", value.Trim(), 2, 30);
            }
            get => _name;
        }
        private string _family = "";
        public string family
        {
            set
            {
                _family = Validation("Фамилия", value.Trim(), 2, 50);
            }
            get => _family;
        }
        private string _patronymic = "";
        public string patronymic
        {
            set
            {
                if(value != "-")
                    _patronymic = Validation("Отчество", value.Trim(), 4, 50);
                else
                    _patronymic = value;
            }
            get => _patronymic;
        }
        private string _passportDetails = "";
        public string passport_details
        {
            set
            {
                value = value.Trim();
                if (!Regex.IsMatch(value, @"^\d{10}$"))
                    Console.WriteLine("Паспортные данные должны состоять из 10 цифр");
                else
                    _passportDetails = value;
            }
            get => _passportDetails;
        }
        private string _address = "";
        public string address // не может состоять только из букв или цифр, длина не меньше 14 символов
        {
            set
            {
                value = value.Trim();
                if (value.Length < 14)
                    Console.WriteLine("Адрес должен иметь не меньше 14 симвоолв");
                else if (!Regex.IsMatch(value, @"^[\p{IsCyrillic}0-9\s.,;/-]+$"))
                    Console.WriteLine("Адрес содержит недопустимые символы");
                else if (Regex.IsMatch(value, @"^\d+$"))
                    Console.WriteLine("Адрес не может состоять только из цифр");
                else
                    _address = value;
            }
            get => _address;
        }
        private DateTime _dateBirtch = new DateTime();
        public DateTime date_birth
        {
            set
            {
                if (value == default(DateTime))
                    Console.WriteLine("Дата рождения не введена");
                else if (value > DateTime.Now)
                    Console.WriteLine("Дата рождения не может быть больше текущей даты");
                else if (value < new DateTime(1903, 01, 01) || value > new DateTime(2013, 01, 01))
                    Console.WriteLine("Возраст пользователя не может быть больше 123 и не моложе 12 лет");
                else
                    _dateBirtch = value;
            }
            get => _dateBirtch;
        }
        public int? __v {  get; set; }

        private string Validation(string title, string value, int minLength, int maxLength)
        {
            if (value.Length < minLength || value.Length > maxLength)
                Console.WriteLine($"{title} должно содержать от 2 до 30 символов");
            else if (Regex.IsMatch(value, @"\d"))
                Console.WriteLine($"{title} не может содержать цифры");
            else if (!Regex.IsMatch(value, @"^[\p{IsCyrillic}-]+$"))
                Console.WriteLine($"{title} может содержать только кириллицу и дефис");
            else if (value.StartsWith("-") || value.EndsWith("-"))
                Console.WriteLine("Дефис не может быть в начале или конце");
            else
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            return "";
        }
    }
}
