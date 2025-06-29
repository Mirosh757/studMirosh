using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2_SRS_DB
{
    internal class Vehicle
    {
        public int Id { get; set; }
        private string brand = "";
        public string Brand 
        {
            set
            {
                if (value.Length < 1 || value.Length > 50)
                    Console.WriteLine("Название бренда автомобиля не может быть меньше одного и больше 50 символов");
                else if (!Regex.IsMatch(value, @"^[A-Za-z][A-Za-z0-9\s\-']{0,49}$"))
                    Console.WriteLine("Название бренда автомобиля может содержать только символы латинского алфавита, пробелы, дефисы, апостроф");
                else if (value.StartsWith(@"-") || value.EndsWith(@"-"))
                    Console.WriteLine("Дефис не может находится в начале или в конце названия бренда автомобиля");
                else if (value.StartsWith(@"'") || value.EndsWith(@"'"))
                    Console.WriteLine("Апостроф не может находится в начале или в конце названия бренда автомобиля");
                else
                    brand = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
            get => brand;
        }
        private string model = "";
        public string Model 
        { 
            set
            {
                if (value.Length < 1 || value.Length > 50)
                    Console.WriteLine("Название модели автомобиля не может быть меньше одного и больше 50 символов");
                else if (!Regex.IsMatch(value, @"^[A-Za-zА-Яа-я0-9][A-Za-zА-Яа-я0-9\s\-'./+()]{0,49}$"))
                    Console.WriteLine("Название модели автомобиля может содержать только символы латинского алфавита, пробелы, дефисы, апостроф");
                else if (value.StartsWith(@"-") || value.EndsWith(@"-"))
                    Console.WriteLine("Дефис не может находится в начале или в конце названия модели автомобиля");
                else if (value.StartsWith(@"'") || value.EndsWith(@"'"))
                    Console.WriteLine("Апостроф не может находится в начале или в конце названия модели автомобиля");
                else if (value.StartsWith(@"/") || value.EndsWith(@"/"))
                    Console.WriteLine("Косая черта не может находится в начале или в конце названия модели автомобиля");
                else if (value.StartsWith(@"+") || value.EndsWith(@"+"))
                    Console.WriteLine("Знак + не может находится в начале или в конце названия модели автомобиля");
                else if (value.EndsWith(@"(") || value.StartsWith(@")"))
                    Console.WriteLine("Закрывающая скобка не может находится в начале и открывающая скобка не может находиться в конце названия модели автомобиля");
                else if (value.ToLower() == brand.ToLower())
                    Console.WriteLine("Название модели и бренда не могут совпадать");
                else
                    model = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
            get => model;
        }
        private int year = 0;
        public int Year 
        { 
            set
            {
                if (value < 1886 || value > DateTime.Now.Year)
                    Console.WriteLine("Год выпуска автомобиля не может быть меньше 1886 и больше текущего года");
                else
                    year = value;
            }
            get => year;
        }
        private DateTime createdAt = DateTime.Now;
        public DateTime CreatedAt 
        {
            set => createdAt = value;
            get => createdAt;
        }
    }
}
