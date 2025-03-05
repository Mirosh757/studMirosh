using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace _1_lab_BD_tran
{
    internal class Facade
    {
        private void AddUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                users user = new users();
                Console.Write("Для регистрации пользователя введите значения атрибутов\nЛогином является элетронная почта пользователя, содержащая в себе только один из четырех доменов, таких как: @mail.ru, @gmail.com, @yandex.ru, @bsu.ru\nlogin: ");
                user.login = Console.ReadLine().Trim();
                while(user.login == "")
                {
                    Console.Write("Не верно введены данные\nlogin: ");
                    user.login = Console.ReadLine().Trim();
                }
                Console.Write("Password: ");
                user.password = Console.ReadLine();
                while(user.password == "")
                {
                    Console.Write("Не верно введены данные\nPassword: ");
                    user.password = Console.ReadLine();
                }
                user.registration_date = DateTime.Now;
                Console.Write("Далее требуется указать личные данные пользователя\nFamily: ");
                AddAccount(user);
            }
            PrintMenu();
        }
        private void AddAccount(users user)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                accounts accounts = new accounts();
                accounts.user_id = user.id;
                accounts.family = Console.ReadLine().Trim();
                while (accounts.family == "")
                {
                    Console.Write("Не верно введены данные\nFamily: ");
                    accounts.family = Console.ReadLine().Trim();
                }
                Console.Write("Name: ");
                accounts.name = Console.ReadLine().Trim();
                while (accounts.name == "")
                {
                    Console.Write("Не верно введены данные\nName: ");
                    accounts.name = Console.ReadLine().Trim();
                }
                Console.Write("Если пользователь не имеет отчества, требуется указать прочерк в атрибуте\nPatronymic: ");
                accounts.patronymic = Console.ReadLine().Trim();
                while (accounts.patronymic == "")
                {
                    Console.Write("Не верно введены данные\nPatronymic: ");
                    accounts.patronymic = Console.ReadLine().Trim();
                }
                Console.Write("Введите дату рождения в формате ДД-ММ-ГГГГ\nBirth_date: ");
                string[] line = Console.ReadLine().Split('-');
                try
                {
                    accounts.birth_date = new DateTime(Int32.Parse(line[2]), Int32.Parse(line[1]), Int32.Parse(line[0]));
                }
                catch
                {
                    Console.WriteLine("Введена не верная дата рождения");
                }
                while (accounts.birth_date == new DateTime())
                {
                    Console.Write("Не верно введены данные\nBirth_date: ");
                    line = Console.ReadLine().Split('-');
                    try
                    {
                        accounts.birth_date = new DateTime(Int32.Parse(line[2]), Int32.Parse(line[1]), Int32.Parse(line[0]));
                    }
                    catch
                    {
                        Console.WriteLine("Введена не верная дата рождения");
                    }
                }
                try
                {
                    int forDeleteMany = db.Database.ExecuteSqlRaw($"CALL add_user('{user.login}'::VARCHAR, '{user.password}'::VARCHAR, '{accounts.family}'::VARCHAR, '{accounts.name}'::VARCHAR, '{accounts.patronymic}'::VARCHAR, '{accounts.birth_date}'::DATE)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    AddAccount(user);
                }
                Console.WriteLine("Пользователь успешно создан");
            }
        }
        private void PrintUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string id, login, password, registration_date, name, family, patronymic, birth_date;
                List<users> users = db.users.ToList();
                List<accounts> accounts = db.accounts.ToList();
                Console.WriteLine("Id  | Login             | Password      | Registration_date   | Name                 | Family               | Patronymic           | Birth_Date");
                /*
                for(int i = 0;i < users.Count;i++)
                {
                    id = " " + users[i].id.ToString();
                    for (int j = id.Length; j < 4; j++)
                        id += " ";
                    login = " " + users[i].login.ToString();
                    for (int j = login.Length; j < 27; j++)
                        login += " ";
                    password = " " + users[i].password.ToString();
                    for (int j = password.Length; j < 17; j++)
                        password += " ";
                    registration_date = " " + users[i].registration_date.ToString() + " ";
                    name = " " + accounts[i].name.ToString();
                    for (int j = name.Length; j < 22; j++)
                        name += " ";
                    family = " " + accounts[i].family.ToString();
                    for (int j = family.Length; j < 26; j++)
                        family += " ";
                    patronymic = accounts[i].patronymic.ToString();
                    if (patronymic != "")
                    {
                        patronymic = " " + patronymic;
                        for (int j = patronymic.Length; j < 29; j++)
                            patronymic += " ";
                    }
                    Console.WriteLine($"{id}|{login}|{password}|{registration_date}|{family}|{name}|{patronymic}");
                }
                */
                for (int i = 0; i < users.Count; i++)
                {
                    id = CorrFormat(users[i].id.ToString(), 3) + " ";
                    login = " " + CorrFormat(users[i].login, 17) + " ";
                    password = " " + CorrFormat(users[i].password, 13) + " ";
                    registration_date = " " + CorrFormat(users[i].registration_date.ToString(), 19) + " ";
                    name = " " + CorrFormat(accounts[i].name, 20) + " ";
                    family = " " + CorrFormat(accounts[i].family, 20) + " ";
                    patronymic = " " + CorrFormat(accounts[i].patronymic, 20) + " ";
                    birth_date = " " + CorrFormat(accounts[i].birth_date.ToShortDateString(), 10) + " ";
                    Console.WriteLine($"{id}|{login}|{password}|{registration_date}|{name}|{family}|{patronymic}|{birth_date}");
                    /*
                    id = string.Format("{0:d3}", users[i].id) + " ";
                    if (users[i].login.Length > 17)
                        login = " " + users[i].login.Substring(0, 14) + "...";
                    else
                    {
                        login = " " + users[i].login;
                        for(int j = users[i].login.Length;j < 17;j++)
                           login += " ";
                    }
                    if (users[i].password.Length > 13)
                        password = " " + users[i].password.Substring(0, 10) + "...";
                    else
                    {
                        password = " " + users[i].password;
                        for (int j = users[i].password.Length; j < 13; j++)
                            password += " ";
                    }
                    registration_date = " " + users[i].registration_date.ToString();
                    if(registration_date.Length < 19)
                    {
                        for(int j = registration_date.Length; j < 19;j++)
                            registration_date += " ";
                    }
                    name = " " + accounts[i].name;
                    if(name.Length > 20)
                        name = " " + name.Substring(0,17) + "...";
                    else
                    {
                        for (int j = name.Length; j < 20;j++)
                            name += " ";
                    }
                    family = " " + accounts[i].family;
                    if (family.Length > 20)
                        family = " " + family.Substring(0, 17) + "...";
                    else
                    {
                        for(int j = family.Length;j < 20;j++)
                            family += " ";
                    }
                    patronymic = " " + accounts[i].patronymic;
                    if (patronymic.Length > 20)
                        patronymic = " " + patronymic.Substring(0, 17) + "...";
                    else
                    {
                        for(int j = patronymic.Length;j < 20;j++)
                            patronymic += " ";
                    }
                    birth_date = " " + accounts[i].birth_date.ToString();
                    if (birth_date.Length < 19)
                    {
                        for (int j = birth_date.Length; j < 19; j++)
                            birth_date += " ";
                    }
                    Console.WriteLine($"{id}|{login}|{password}|{registration_date}|{name}|{family}|{patronymic}|{birth_date}");
                    */
                }
            }
            PrintMenu();
        }
        private string CorrFormat(string value, int count)
        {
            if(value.Length <= count)
                return value.PadRight(count, ' ');
            else
                return value.Substring(0, count - 3) + "...";
        }
        public void PrintMenu()
        {
            Console.WriteLine("0.    Регистрация пользователя\n1.    Отображение всех пользователей");
            string line = Console.ReadLine();
            if (line.Length != 1 && (line != "0" || line != "1"))
            {
                Console.WriteLine("Неверная команда");
                PrintMenu();
            }
            else
            {
                switch (line)
                {
                    case "0": AddUsers(); break;
                    case "1": PrintUsers(); break;
                }
            }
        }
    }
}
