using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _3_lab_NoPattern
{
    internal class animals
    {
        private long _id;
        public long id 
        {
            set { _id = value; }
            get { return _id; }
        }
        private string? _title;
        public string? title
        {
            set
            {
                if (!Regex.IsMatch(value, @"\P{IsCyrillic}") && !Regex.IsMatch(value, "[0-9]") || Regex.IsMatch(value, " "))
                {
                    _title = value;
                }
                else
                    Console.WriteLine("Ввод названия только в кириллице и без использования цифр");
            }
            get { return _title; }
        }
        public string path { get; set; }
    }
}
