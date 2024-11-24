using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace _1_lab_DB
{
    internal class Doctor : DomainObject
    {
        public override string NameTable => "doctors";
        private DateTime _created_at;
        public DateTime CreatedAt 
        {
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Дана создания не может быть больше текущей даты");
                _created_at = value;
            }
            get => _created_at;
        }
        private DateTime _updated_at;
        public DateTime UpdatedAt
        {
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Дана обновления не может быть больше текущей даты");
                else if (value < CreatedAt)
                    throw new ArgumentException("Дана обновленя не может быть меньше даты создания");
                _updated_at = value;
            }
            get => _updated_at;
        }
        private string _name;
        public string Name
        {
            set
            {
                if (Regex.IsMatch(value, "^[^qazwsxedcrfvtgbyhnujmik,ol.p;.['/]]$"))
                    throw new ArgumentException("Имя может содержать только русские буквы и пробелы");
                else if (value.Trim() == "")
                    throw new ArgumentException("Имя не может содержать только пробелы");
                else if (value.Length > 100 || value.Length < 7)
                    throw new ArgumentException("Имя не может содержать больше 100 и меньше 7 букв");
                _name = value;
            }
            get => _name;
        }
        private string _address;
        public string Address
        {
            set
            {
                if (Regex.IsMatch(value, "^[^qazwsxedcrfvtgbyhnujmik,olp]]$"))
                    throw new ArgumentException("Адрес может содержать только русские буквы и пробелы и некоторые символы пунктуации");
                else if (value.Trim() == "")
                    throw new ArgumentException("Адрес не может содержать только пробелы");
                else if (value.Length < 7)
                    throw new ArgumentException("Адрес не может содержать меньше 7 букв");
                _address = value;
            }
            get => _address;
        }
        private string _passport_details;
        public string PassportDetails
        {
            set
            {
                if (Regex.IsMatch(value, "^[^qazwsxedcrfvtgbyhnujmik,olp;./'<>:{}|?йфяцычувскамепинртгоьшлбщдюзжхэъ ]]$"))
                    throw new ArgumentException("Паспортные данные может содержать только цифры");
                else if (value.Length > 10)
                    throw new ArgumentException("Паспортные данные не могут содержать больше 10 цифр");
                _passport_details = value;
            }
            get => _passport_details;
        }
        private DateTime _date_birth;
        public DateTime DateBirth
        {
            set
            {
                DateTime dateTime = new DateTime(2001, 01, 01);
                if (value > dateTime)
                    throw new ArgumentException("Дана рождения не может быть больше 2001-01-01");
                _date_birth = dateTime;
            }
            get => _date_birth;
        }
        public Doctor(int id, DateTime created_at, DateTime updated_at, string name, string address, string passport_details, DateTime date_birth) : base(id)
        {
            CreatedAt = created_at;
            UpdatedAt = updated_at;
            _name = name;
            _address = address;
            _passport_details = passport_details;
            DateBirth = date_birth;
        }
        public Doctor(DateTime created_at, DateTime updated_at, string name, string address, string passport_details, DateTime date_birth) : base(0)
        {
            CreatedAt = created_at;
            UpdatedAt = updated_at;
            _name = name;
            _address = address;
            _passport_details = passport_details;
            DateBirth = date_birth;
        }
        public Doctor() : base(0) { }
        public override string GetInfo()
        {
            return $"'{_created_at}','{_updated_at}','{_name}','{_address}','{_passport_details}','{_date_birth}'";
        }
        public override string GetTable() 
        {
            return "doctors(created_at, updated_at, name, address, passport_details, date_birth)";
        }
        public override string GetInfoForInsertOrUpdate()
        {
            return $"'{_created_at}','{DateTime.Now}','{_name}','{_address}','{_passport_details}','{_date_birth}'"; 
        }
    }
}
