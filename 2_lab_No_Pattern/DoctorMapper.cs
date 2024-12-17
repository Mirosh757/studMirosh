using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _1_lab_No_Pattern
{
    internal class DoctorMapper
    {
        static int column = 7;
        static string tName = "doctors";
        Dbconnection dBConnection = new Dbconnection();
        //Facade facade = new Facade();
        public void DoctorCreate()
        {
            Doctor doctor = new Doctor();
            Console.Write("Введите значения атрибутов через запятую\nName: ");
            doctor.Name = Console.ReadLine();
            Console.Write("Address: ");
            doctor.Address = Console.ReadLine();
            Console.Write("Passport_details: ");
            doctor.PassportDetails = Console.ReadLine();
            Console.Write("Date_birth(P.S. формат ввода: ГГГГ-ММ-ДД): ");
            string s = Console.ReadLine();
            doctor.DateBirth = StringToDateFromDataBase(s);
            //Doctor doctor = new Doctor(data[0], data[1], data[2], StringToDateFromDataBase(data[3]));
            string query = $"INSERT INTO {doctor.GetTable()} VALUES ({doctor.GetInfo()}) RETURNING id;";
            Console.Write(query);
            int k = dBConnection.InsertQuery(query);
            Console.WriteLine($"Вставлен объект в базу doctors с id = {k}");
        }
        public void DoctorRetrieveAll()
        {
            string query = $"SELECT id FROM {tName}";
            List<int> _data = dBConnection.SelectAll(query);
            foreach(int a in _data)
            {
                string queryOne = $"SELECT * FROM {tName} WHERE id = {a}";
                string _dataOne = dBConnection.SelectQuery(queryOne, column);
                string[] _getData = _dataOne.Split('_');
                Doctor doctor = new Doctor(a, StringToDateTime(_getData[1]), StringToDateTime(_getData[2]), _getData[3], _getData[4], _getData[5], StringToDate(_getData[6]));
                Console.WriteLine($"{a}, {doctor.GetInfo()}");
            }
        }
        public void DoctorRetrieve()
        {
            Console.WriteLine("Введите id модели");
            string s = Console.ReadLine();
            if(!Regex.IsMatch(s, "[0123456789]"))
            {
                Console.WriteLine("Id не может содержать строку");
                DoctorRetrieve();
            }
            int id = Int32.Parse(s);
            string query = $"SELECT * FROM {tName} WHERE id = {id}";
            string _data = dBConnection.SelectQuery(query, column);
            if (_data != "")
            {
                string[] _getData = _data.Split('_');
                Doctor doctor = new Doctor(id, StringToDateTime(_getData[1]), StringToDateTime(_getData[2]), _getData[3], _getData[4], _getData[5], StringToDate(_getData[6]));
                Console.WriteLine($"{id}, {doctor.GetInfo()}");
            }
            else
                Console.WriteLine($"id = {id} не найден");
        }
        public void DoctorUpdate()
        {
            Console.WriteLine("Введите id модели для изменения значений атрибутов");
            string s = Console.ReadLine();
            if (!Regex.IsMatch(s, "[0123456789]"))
            {
                Console.WriteLine("Id не может содержать строку");
                DoctorUpdate();
            }
            int id = Int32.Parse(s);
            string query = $"SELECT * FROM {tName} WHERE id = {id}";
            string _data = dBConnection.SelectQuery(query, column);
            if (_data != "")
            {
                string[] _getData = _data.Split('_');
                Doctor doctor1 = new Doctor(id, StringToDateTime(_getData[1]), StringToDateTime(_getData[2]), _getData[3], _getData[4], _getData[5], StringToDate(_getData[6]));
                Doctor doctor = new Doctor();
                Console.WriteLine($"Далее будет представлен список названий атрибутов модели по одному, впишите значения в соответствующий атрибут для изменения, а тот атрибут, который не хотите менять оставить прочерк(_)");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                if (name != "_")
                    doctor.Name = name;
                Console.Write("Address: ");
                string address = Console.ReadLine();
                if (address != "_")
                    doctor.Address = address;
                Console.Write("Passport_details: ");
                string passportDetails = Console.ReadLine();
                if (passportDetails != "_")
                    doctor.PassportDetails = passportDetails;
                Console.Write("Date_birth(P.S. формат ввода: ГГГГ-ММ-ДД): ");
                s = Console.ReadLine();
                if (s != "_")
                    doctor.DateBirth = StringToDateFromDataBase(s);
                if (doctor.Name == null)
                    doctor.Name = doctor1.Name;
                if(doctor.Address == null)
                    doctor.Address = doctor1.Address;
                if (doctor.PassportDetails == null) 
                    doctor.PassportDetails = doctor1.PassportDetails;
                if(doctor.DateBirth == new DateTime(111, 01, 01))
                    doctor.DateBirth = doctor1.DateBirth;
                query = $"UPDATE {doctor.NameTable} SET updated_at = '{DateTime.Now}', name = '{doctor.Name}', address = '{doctor.Address}', passport_details = '{doctor.PassportDetails}', date_birth = '{doctor.DateBirth}' WHERE Id = {id}";
                int k = dBConnection.UpdateOrDeleteQuery(query);
                Console.WriteLine($"Объект с id = {id} изменился с кодом: {k}");
            }
            else
                Console.WriteLine($"id = {id} не найден");
        }
        public void DoctorDelete()
        {
            Console.WriteLine("Введите id модели для удаления");
            string s = Console.ReadLine();
            
            if (!Regex.IsMatch(s, "[0123456789]"))
            {
                Console.WriteLine("Id не может содержать строку");
                DoctorDelete();
            }
            int id = Int32.Parse(s);
            string query = $"SELECT * FROM {tName} WHERE id = {id}";
            string _data = dBConnection.SelectQuery(query, column);
            if (_data != "")
            {
                query = $"DELETE FROM {tName} WHERE id = {id}";
                int k = dBConnection.UpdateOrDeleteQuery(query);
                Console.WriteLine($"Объект с id = {id} удален с кодом: {k}");
            }
            else
                Console.WriteLine($"id = {id} не найден");
        }
        public void DoctorDeleteMany()
        {
            Console.WriteLine("Введите список id модели для удаления через пробел");
            string s = Console.ReadLine();
            string[] data = s.Split(' ');
            string query;
            int k = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (!Regex.IsMatch(data[i], "[0123456789]"))
                {
                    Console.WriteLine("Id не может содержать строку");
                    DoctorDeleteMany();
                }
                else
                {
                    query = $"DELETE FROM {tName} WHERE id = {data[i]}";
                    k += dBConnection.UpdateOrDeleteQuery(query);
                }
            }
            Console.WriteLine($"Удаленно записей - {k}");
        }
        public void DoctorSearch()
        {
            Console.WriteLine("Далее будет представлен список названий атрибутов модели по одному, впишите значения в соответствующий атрибут для поиска, а тот атрибут, который не хотите искать оставить прочерк(_)");
            Console.Write("Name: ");
            string[] line = new string[5];
            line[0] = Console.ReadLine();
            Console.Write("Address: ");
            line[1] = Console.ReadLine();
            Console.Write("Passport_details: ");
            line[2] = Console.ReadLine();
            Console.Write("Далее нужно ввести интервал для поиска даты рождения\nDate_birth_1: ");
            line[3] = Console.ReadLine();
            if (line[3] == null || line[3].Length != 10)
            {
                Console.WriteLine("Введена не коректная дата рождения 1");
                DoctorSearch();
            }
            Console.Write("Date_birth_2: ");
            line[4] = Console.ReadLine();
            if (line[4] == null || line[4].Length != 10)
            {
                Console.WriteLine("Введена не коректная дата рождения 2");
                DoctorSearch();
            }
            Console.WriteLine("Введите параметры количества выдаваемых результатов(INT) и смещение(INT) через пробел");
            string s = Console.ReadLine();
            string[] pagin = s.Split(' ');
            /*
            for(int i = 0; i < 3;i++)
            {
                if (line[i].IndexOf(' ') == -1)
                    line[i] = line[i].ToLower();
            }
            */
            List<int> result = new List<int>();
            string lineQuery = "1=1 ";
            for (int i = 0; i < 4; i++)
            {
                if (line[i] != "_")
                {
                    switch (i)
                    {
                        case 0: lineQuery += $"AND name LIKE '%{line[0]}%'"; break;
                        case 1: lineQuery += $"AND address LIKE '%{line[1]}%'"; break;
                        case 2: lineQuery += $"AND passport_details LIKE '%{line[2]}%'"; break;
                        case 3: lineQuery += $"AND '{line[3]}' < date_birth AND '{line[4]}' > date_birth"; break;
                    }
                }
            }
            string query = $"SELECT id FROM {tName} WHERE {lineQuery} LIMIT {pagin[0]}";
            int k = Int32.Parse(pagin[1]);
            result = dBConnection.SelectAll(query);
            foreach (int a in result)
            {
                string queryOne = $"SELECT * FROM {tName} WHERE id = {a}";
                string _dataOne = dBConnection.SelectQuery(queryOne, column);
                string[] _getData = _dataOne.Split('_');
                Doctor doctor = new Doctor(a, StringToDateTime(_getData[1]), StringToDateTime(_getData[2]), _getData[3], _getData[4], _getData[5], StringToDate(_getData[6]));
                Console.WriteLine($"{k++} | {a}, {doctor.GetInfo()}");
            }
        }
        private DateTime StringToDateTime(string value)
        {
            string[] data = value.Split('.', ' ', ':');
            try
            {
                return new DateTime(Int32.Parse(data[2]), Int32.Parse(data[1]), Int32.Parse(data[0]), Int32.Parse(data[3]), Int32.Parse(data[4]), Int32.Parse(data[5]));
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"При вводе даты и времени произошла ошибка: {ex}\nВероятней всего были введены не верные данные({value})"); 
                return new DateTime();
            }
        }
        public DateTime StringToDate(string value)
        {
            string[] data = value.Split('.', ' ', ':');
            try
            {
                return new DateTime(Int32.Parse(data[2]), Int32.Parse(data[1]), Int32.Parse(data[0]));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При вводе даты произошла ошибка: {ex}\nВероятней всего были введены не верные данные({value})");
                return new DateTime();
            }
        }
        public DateTime StringToDateFromDataBase(string value)
        {

            string[] data = value.Split('.', ' ', ':', '-');
            try
            {
                return new DateTime(Int32.Parse(data[0]), Int32.Parse(data[1]), Int32.Parse(data[2]));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"При вводе даты и произошла ошибка: {ex}\nВероятней всего были введены не верные данные({value})");
                return new DateTime();
            }
        }
    }
}
