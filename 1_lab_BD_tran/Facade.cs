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
                user.login = Console.ReadLine();
                while(user.login == "")
                {
                    Console.Write("Не верно введены данные\nlogin: ");
                    user.login = Console.ReadLine();
                }
                Console.Write("Passport: ");
                user.password = Console.ReadLine();
                while(user.password == "")
                {
                    Console.Write("Не верно введены данные\nPassport: ");
                    user.password = Console.ReadLine();
                }
                user.registration_date = DateTime.Now;
                try
                {
                    int forDeleteMany = db.Database.ExecuteSqlRaw($"CALL add_user('{user.login}'::VARCHAR, '{user.password}'::VARCHAR)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    AddUsers();
                }
                Console.Write("Далее требуется указать личные данные пользователя\nFamily: ");
                AddAccount();
            }
            PrintMenu();
        }
        private void AddAccount()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                accounts accounts = new accounts();
                List<users> users = db.users.ToList();
                accounts.user_id = users[users.Count - 1].id;
                accounts.family = Console.ReadLine();
                while (accounts.family == "")
                {
                    Console.Write("Не верно введены данные\nFamily: ");
                    accounts.family = Console.ReadLine();
                }
                Console.Write("Name: ");
                accounts.name = Console.ReadLine();
                while (accounts.name == "")
                {
                    Console.Write("Не верно введены данные\nName: ");
                    accounts.name = Console.ReadLine();
                }
                Console.Write("Если пользователь не имеет отчества, требуется указать прочерк в атрибуте\nPatronymic: ");
                accounts.patronymic = Console.ReadLine();
                while (accounts.patronymic == "")
                {
                    Console.Write("Не верно введены данные\nPatronymic: ");
                    accounts.patronymic = Console.ReadLine();
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
                    int forDeleteMany = db.Database.ExecuteSqlRaw($"CALL add_account({accounts.user_id}, '{accounts.family}'::VARCHAR, '{accounts.name}'::VARCHAR, '{accounts.patronymic}'::VARCHAR, '{accounts.birth_date}'::DATE)");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    AddAccount();
                }
                Console.WriteLine("Пользователь успешно создан");
            }
        }
        private void PrintUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string id, login, password, registration_date, name, family, patronymic;
                List<users> users = db.users.ToList();
                List<accounts> accounts = db.accounts.ToList();
                Console.WriteLine("Id  | Login                     | Password        | Registration_date   | Name                 | Family                   | Patronymic                    ");
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
                    Console.WriteLine($"{id}|{login}|{password}|{registration_date}|{name}|{family}|{patronymic}");
                }
            }
            PrintMenu();
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
