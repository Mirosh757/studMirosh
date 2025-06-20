using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace _4_Lab_MongoDb
{
    internal class Region
    {
        public Region() { }
        public ObjectId Id { get; set; }
        private string _region_name = "";
        public string region_name
        {
            set
            {
                if (value.Length < 2 || value.Length > 50)
                    Console.WriteLine("Назавание города должно содержать от 2 до 50 символов");
                else if (Regex.IsMatch(value, @"\d"))
                    Console.WriteLine($"Назавание города не может содержать цифры");
                else if (!Regex.IsMatch(value, @"^[\p{IsCyrillic}-]+$"))
                    Console.WriteLine($"Назавание города может содержать только кириллицу, дефис, пробел, точка, цифры латинского алфавита, апостроф, запятая, открывающая и закрывающая скобка ");
                else if (value.StartsWith("-") || value.EndsWith("-"))
                    Console.WriteLine("Дефис не может быть в начале или конце");
                else
                    _region_name = value;
            }
            get => _region_name;
        }
        public int? __v { get; set; }
    }
}
