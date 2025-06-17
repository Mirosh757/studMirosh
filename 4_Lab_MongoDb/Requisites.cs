using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _4_Lab_MongoDb
{
    internal class Requisites
    {
        public Requisites() { }
        public ObjectId Id { get; set; }
        private string _registration_date = "";
        public string registration_date
        {
            set
            {
                DateTime _date = new DateTime();
                try { _date = DateTime.Parse(value); }
                catch { }
                if (_date == default(DateTime))
                    Console.WriteLine("Введена не верная дата");
                else if (_date > DateTime.Now)
                    Console.WriteLine("Дата рождения не может быть больше текущей даты");
                else
                {
                    _registration_date = _date.ToShortDateString();
                }
            }
            get => _registration_date;
        }
        private string _hospital_reduce_name = "";
        public string hospital_reduce_name
        {
            set
            {
                if (value.Length < 4 || value.Length > 75)
                    Console.WriteLine("Длина сокращенного названия больницы не может быть меньше 4 и больше 75 символов");
                else if (!Regex.IsMatch(value, @"^[\p{IsCyrillic}\s\d.,()'<>\-№]+$"))
                    Console.WriteLine("Сокращенное название больницы может содержать только русские символы и некоторые спец. символы");
                else
                    _hospital_reduce_name = value;
            }
            get => _hospital_reduce_name;
        }
        private string _name_legal_faces = "";
        public string name_legal_faces
        {
            set
            {
                value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                if (!Regex.IsMatch(value, @"^[А-ЯЁ][а-яё']{1,49}(?:-[А-ЯЁ][а-яё']{1,49})? [А-ЯЁ]\.[ ]?(?:[А-ЯЁ]\.?)?$"))
                    Console.WriteLine("ФИО ведено не верно. Принимается только сокращенное ФИО, причем длина фамилии от 2 до 50 символов");
                else
                    _name_legal_faces = value;
            }
            get => _name_legal_faces;
        }
        private string _ogrn = "";
        public string ogrn // 13 символов
        {
            set
            {
                _ogrn = CheckCode("ОГРН", value, 13);
                    
            }
            get => _ogrn;
        }
        private string _inn = "";
        public string inn // 12 символов
        {
            set
            {
                _inn = CheckCode("ИНН", value, 12);
            }
            get => _inn;
        }
        private string _kpp = "";
        public string kpp // 9 символов
        {
            set
            {
                _kpp = CheckCode("КПП", value, 9);
            }
            get => _kpp;
        }
        private string CheckCode(string title, string value, int Length)
        {
            if(value.Length != Length)
                Console.WriteLine($"{title} должно содержать {Length} цифр");
            else if(!Regex.IsMatch(value, @"^\d+$"))
                Console.WriteLine($"{title} состоит только из цифр");
            else
                return value;
            return "";
        }
    }
}
