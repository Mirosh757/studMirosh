using System;
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
    [Index("title", IsUnique = true, Name = "title_Index")]
    internal class tags
    {
        private int _id = 0;
        public int id
        {
            set 
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    tags tag = db.tags.Where(t => t.id == (value)).DefaultIfEmpty().First();
                    if (tag != null)
                        _id = value;
                    else
                        Console.WriteLine("Данного id не существует в таблице tags");
                }
            }
            get { return _id; }
        }
        private string _title = "";
        public string title
        {
            set
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    List<tags> tPrint = db.tags.OrderBy(t => t.id).ToList();
                    for (int i = 0; i < tPrint.Count; i++)
                    {
                        if (value == i.ToString())
                        {
                            _title = tPrint[i].title;
                            break;
                        }
                    }
                    if (_title == "")
                    {
                        Console.WriteLine("Номер названия тега лежит от 0 до 4");
                    }
                }
            }
            get { return _title; }
        }
    }
}
