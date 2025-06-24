using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _4_Lab_MongoDb
{
    internal class Medical_instituction
    {
        public Medical_instituction() { }
        public ObjectId Id { get; set; }
        private string _title = "";
        public string title
        {
            set
            {
                if (value.Length < 10 || value.Length > 250)
                    Console.WriteLine("Длина названия больницы не может быть меньше 10 и больше 250 символов");
                else if (!Regex.IsMatch(value, @"^[\p{IsCyrillic}\s\d.,()'<>\-№]+$"))
                    Console.WriteLine("Название больницы может содержать только русские символы и некоторые спец. символы");
                else
                    _title = value;
            }
            get => _title;
        }
        private string _website = "";
        public string website
        {
            set
            {
                if (value.Length < 4 || value.Length > 60)
                    Console.WriteLine("Длина веб сайта не может быть меньше 4 и больше 60 символов");
                else if (Regex.IsMatch(value, @"^\d+$"))
                    Console.WriteLine("Веб сайт не может состоять только из цифр");
                else if (value.Split('.').Length < 2)
                    Console.WriteLine("Веб сайт должен содержать точку");
                else if (value.StartsWith(".") || value.EndsWith("."))
                    Console.WriteLine("Точка не может быть в начале или конце");
                else
                {
                    string[] splitweb = value.Split(".");
                    bool flag = true;
                    for(int i = 0;i < splitweb.Length;i++)
                    {
                        if (splitweb[i].Length < 2)
                            flag = false;
                    }
                    if (flag)
                        _website = value;
                    else
                        Console.WriteLine("Не верно указан веб-сайт");
                }
            }
            get => _website;
        }
        private string _address = "";
        public string address
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
        private ObjectId _region_id = new ObjectId();
        public ObjectId region_id
        {
            set => _region_id = value;
            get => _region_id;
        }
        private ObjectId[] _departments = new ObjectId[0];
        public ObjectId[] departments
        {
            set
            {
                _departments = value;
            }
            get => _departments;
        }
        private string _type = "";
        [BsonElement("type")]
        public string type
        {
            set
            {
                if (value.ToLower() == "hospital" || value.ToLower() == "department")
                    _type = value;
                else
                    Console.WriteLine("Введено не верное значение");
            }
            get => _type;
        }
        //private Requisites? _requisites = new Requisites();
        public Requisites? requisites { get; set; }
        public int? __v { get; set; }
    }
}
/*

    Для свойства department_id: 
        1)Подумать что лучше, получать прям ObjectId или число, являющийся id в списке
        2)Сделать проверку чтобы больница и отделение находились в одном отделении
        (или лучше в facade давать выбрать отделения только в том же регионе, в котором находится больница)
        3)Похоже что лучше чтобы пользователь выбирал сначала с чем хочет работать(больница или отделение)
        4)по любоиу есть еще что-то, так что стоит подумать

    Для свойств requisites и region:
        1)Надо посмотреть как драйвер вставит название региона и реквизиты
*/