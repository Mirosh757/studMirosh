using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _1_lab_No_Pattern
{
    internal class Doctor
    {
        Facade facade = new Facade();
        public string NameTable => "doctors";
        private DateTime _created_at;
        protected DateTime CreatedAt
        {
            set
            {
                if (value > DateTime.Now)
                {
                    Console.WriteLine("Дата создания не может быть больше текущей даты");
                }
                else
                    _created_at = value;
            }
            get => _created_at;
        }
        private DateTime _updated_at;
        protected DateTime UpdatedAt
        {
            set
            {
                if (value > DateTime.Now)
                    Console.WriteLine("Дата обновления не может быть больше текущей даты");
                else if (value < CreatedAt)
                    Console.WriteLine("Дата обновленя не может быть меньше даты создания");
                else
                    _updated_at = value;
            }
            get => _updated_at;
        }
        private string _name = null;
        public string Name
        {
            set
            {
                if (Regex.IsMatch(value, "[qazwsxedcrfvtgbyhnujmik,ol.p0123456789]"))
                    Console.WriteLine("Имя может содержать только русские буквы и пробелы");
                else if (value.Trim() == "")
                    Console.WriteLine("Имя не может содержать только пробелы");
                else if (value.Length > 100 || value.Length < 7)
                    Console.WriteLine("Имя не может содержать больше 100 и меньше 7 букв");
                else
                    _name = value;
                if(_name == null)
                {
                    Console.WriteLine("Введено не верное имя");
                    facade.PrintMenu();
                }
            }
            get => _name;
        }
        private string _address = null;
        public string Address
        {
            set
            {
                if (Regex.IsMatch(value, "[qazwsxedcrfvtgbyhnujmikolp]"))
                    Console.WriteLine("Адрес может содержать только русские буквы и пробелы и некоторые символы пунктуации");
                else if (value.Trim() == "")
                    Console.WriteLine("Адрес не может содержать только пробелы");
                else if (value.Length < 7)
                    Console.WriteLine("Адрес не может содержать меньше 7 букв");
                else
                    _address = value;
                if (_address == null)
                {
                    Console.WriteLine("Введен не верный адрес");
                    facade.PrintMenu();
                }
            }
            get => _address;
        }
        private string _passport_details = null;
        public string PassportDetails
        {
            set
            {
                if (!Regex.IsMatch(value, "[0123456789]"))
                    Console.WriteLine("Паспортные данные может содержать только цифры");
                else if (value.Length > 10 || value.Length < 10)
                    Console.WriteLine("Паспортные данные не могут содержать только 10 цифр");
                else
                    _passport_details = value;
                if (_passport_details == null )
                {
                    Console.WriteLine("Введены не верные паспортные данные");
                    facade.PrintMenu();
                }
            }
            get => _passport_details;
        }
        private DateTime _date_birth = new DateTime(111,01, 01);
        public DateTime DateBirth
        {
            set
            {
                DateTime dateTimeMax = new DateTime(2001, 01, 01), dateTimeMin = new DateTime(1880, 01, 01);
                if (value > dateTimeMax || value < dateTimeMin)
                    Console.WriteLine("Дата рождения не может быть больше 2001-01-01 и меньше 1880-01-01");
                else
                    _date_birth = value;
                if (_date_birth == new DateTime(111, 01, 01))
                {
                    Console.WriteLine("Введена не верная дата рождения");
                    facade.PrintMenu();
                }
            }
            get => _date_birth;
        }
        public Doctor(int id, DateTime created_at, DateTime updated_at, string name, string address, string passport_details, DateTime date_birth)
        {
            CreatedAt = created_at;
            UpdatedAt = updated_at;
            Name = name;
            Address = address;
            PassportDetails = passport_details;
            DateBirth = date_birth;
        }
        public Doctor(DateTime created_at, DateTime updated_at, string name, string address, string passport_details, DateTime date_birth)
        {
            CreatedAt = created_at;
            UpdatedAt = updated_at;
            Name = name;
            Address = address;
            PassportDetails = passport_details;
            DateBirth = date_birth;
        }
        public Doctor(string name, string address, string passport_details, DateTime date_birth)
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Name = name;
            Address = address;
            PassportDetails = passport_details;
            DateBirth = date_birth;
        }
        public Doctor() { }
        public string GetInfo()
        {
            return $"'{_created_at}','{_updated_at}','{_name}','{_address}','{_passport_details}','{_date_birth}'";
        }
        public string GetTable()
        {
            return "doctors(created_at, updated_at, name, address, passport_details, date_birth)";
        }
    }
}
