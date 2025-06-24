using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;
using _4_Lab_MongoDb;
using MongoDB.Driver;
Facade facade = new Facade();
facade.PrintMenu();
/*
while (true)
{
    string fio = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Console.ReadLine().ToLower());
    Console.WriteLine(fio);
    bool check = Regex.IsMatch(fio, @"^[А-ЯЁ][а-яё']{1,49}(?:-[А-ЯЁ][а-яё']{1,49})? [А-ЯЁ]\.[ ]?(?:[А-ЯЁ]\.?)?$");
    Console.WriteLine(check);
}
*/
/*
кучу проверок надо

23.06 сука, проверь теперь вообще все
*/