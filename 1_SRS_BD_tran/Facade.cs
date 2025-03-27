using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
                Console.Write("Пароль может содержать только латинские буквы, цифры и обычные знаки препинания.\nPassword: ");
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
        private void PrintUsersOfAccounts()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string id, login, password, registration_date, name, family, patronymic, birth_date;
                List<users> users = db.users.ToList();
                List<accounts> accounts = db.accounts.ToList();
                Console.WriteLine("Id  | Login             | Password      | Registration_date   | Name                 | Family               | Patronymic           | Birth_Date");
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
                }
            }
        }
        private void PrintUsersTags()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string id, login, name, family;
                List<users> users = db.users.ToList();
                List<accounts> accounts = db.accounts.ToList();
                bool flag = false;
                foreach (var a in accounts)
                {
                    if (a.tags != null)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    tags tag = new tags();
                    Console.WriteLine("\n\n\nОтображение тэгов пользователя");
                    Console.WriteLine("Id  | Login             | Name                 | Family               | Tag                                                                ");
                    string titleTag = "";
                    for (int i = 0; i < users.Count; i++)
                    {
                        id = CorrFormat(users[i].id.ToString(), 3) + " ";
                        login = " " + CorrFormat(users[i].login, 17) + " ";
                        name = " " + CorrFormat(accounts[i].name, 20) + " ";
                        family = " " + CorrFormat(accounts[i].family, 20) + " ";
                        int[] index = accounts[i].tags.ToArray();
                        for (int j = 0; j < index.Length; j++)
                        {
                            if (index[j] != -1)
                            {
                                tag = db.tags.Where(t => t.id == index[j]).First();
                                titleTag += " " + CorrFormat(tag.title, 8);
                            }
                        }
                        Console.WriteLine($"{id}|{login}|{name}|{family}|{titleTag}");
                        titleTag = "";
                    }
                }
                else
                    Console.WriteLine("У пользователей нету тэгов");
            }
        }
        private void PrintUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                string id, login, password, registration_date, name, family, patronymic, birth_date;
                List<users> users = db.users.ToList();
                List<accounts> accounts = db.accounts.ToList();
                if (accounts.Count != 0)
                {
                    if (accounts[0] != null)
                    {
                        Console.WriteLine("Id  | Login             | Password      | Registration_date   | Name                 | Family               | Patronymic           | Birth_Date");
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
                        }
                        bool flag = false;
                        foreach (var a in accounts)
                        {
                            if (a.tags != new int[1] { -1 })
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag)
                        {
                            tags tag = new tags();
                            Console.WriteLine("\n\n\nОтображение тэгов пользователя");
                            Console.WriteLine("Id  | Login             | Name                 | Family               | Tag                                                                ");
                            string titleTag = "";
                            for (int i = 0; i < users.Count; i++)
                            {
                                id = CorrFormat(users[i].id.ToString(), 3) + " ";
                                login = " " + CorrFormat(users[i].login, 17) + " ";
                                name = " " + CorrFormat(accounts[i].name, 20) + " ";
                                family = " " + CorrFormat(accounts[i].family, 20) + " ";
                                int[] index = accounts[i].tags.ToArray();
                                for (int j = 0; j < index.Length; j++)
                                {
                                    if (index[j] != -1)
                                    {
                                        tag = db.tags.Where(t => t.id == index[j]).First();
                                        titleTag += " " + CorrFormat(tag.title, 8);
                                    }
                                }
                                Console.WriteLine($"{id}|{login}|{name}|{family}|{titleTag}");
                                titleTag = "";
                            }
                        }
                        else
                            Console.WriteLine("У пользователей нету тэгов");
                    }
                }
                else
                    Console.WriteLine("В базе еще нету данных о пользователях");
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
        private string GetQueryResult(string query)
        {
            string result = "";
            NpgsqlConnection conn = new NpgsqlConnection("Host = localhost; Username = postgres; Password = 5dartyr5; Database = UsersAndTags");
            NpgsqlCommand npgsql = new NpgsqlCommand(query, conn);
            conn.Open();
            NpgsqlDataReader reader = npgsql.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    result = String.Format($"{reader.GetValue(0)}");
                }
            }
            conn.Close();
            return result;
        }
        private void InsertTagsInAccount()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                PrintUsersOfAccounts();
                string id;
                bool flag = true;
                users user = new users();
                while (flag)
                {
                    Console.Write("Укажите id пользователя для добавления тэга\nId: ");
                    id = Console.ReadLine();
                    while (Regex.IsMatch(id, @"\D") || id == "")
                    {
                        Console.Write("Атрибут id может содержать только цифры\nId: ");
                        id = Console.ReadLine();
                    }
                    user.id = Int32.Parse(id);
                    if (user.id != 0)
                        flag = false;
                }
                Console.Write("Укажите название тэга для указанного пользователя\nTitle: ");
                tags tag = new tags();
                tag.title = Console.ReadLine();
                while(tag.title == "")
                {
                    Console.Write("Указано не верное значение для тэга\nTitle: ");
                    tag.title = Console.ReadLine();
                }
                Console.WriteLine(GetQueryResult($"SELECT addtagforuser({user.id}, '{tag.title}')"));
                PrintUsersTags();

            }
            PrintMenu();
        }
        private void DeleteTagInAccount()
        {
            PrintUsersOfAccounts();
            string id;
            bool flag = true;
            users user = new users();
            accounts acc;
            while (flag)
            {
                Console.Write("Укажите id пользователя для удаления тэга с аккаунта\nId: ");
                id = Console.ReadLine();
                while (Regex.IsMatch(id, @"\D") || id == "")
                {
                    Console.Write("Атрибут id может содержать только цифры\nId: ");
                    id = Console.ReadLine();
                }
                user.id = Int32.Parse(id);
                if (user.id != 0)
                    flag = false;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                tags tag = new tags();
                flag = true;
                acc = db.accounts.Where(t => t.user_id == user.id).First();
                string listTags = "";
                for(int i = 1;i < acc.tags.Length;i++)
                    listTags += $"{CorrFormat(acc.tags[i].ToString(), 3)}   {CorrFormat(db.tags.Where(t => t.id == acc.tags[i]).First().title, 15)}\n";
                Console.Write($"{listTags}\n\n\nУкажите id тэга для удаления\nId: ");
                while (flag)
                {
                    Console.Write("Укажите id тэга для удаления\nId: ");
                    id = Console.ReadLine();
                    while (Regex.IsMatch(id, @"\D") || id == "")
                    {
                        Console.Write("Атрибут id может содержать только цифры\nId: ");
                        id = Console.ReadLine();
                    }
                    tag.id = Int32.Parse(id);
                    if (tag.id != 0)
                        flag = false;
                }
                Console.WriteLine(GetQueryResult($"SELECT deletetagwithuser({acc.user_id}, '{tag.id}')"));
                PrintUsersTags();
            }
            PrintMenu();
        }
        private void DeleteTag()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<tags> printTags = db.tags.ToList();
                foreach (tags tag in printTags)
                {
                    Console.WriteLine($"{CorrFormat(tag.id.ToString(), 3)}   {CorrFormat(tag.title, 15)}");
                }
                Console.Write("Введите id тэга для удаления\nId: ");
                string id;
                bool flag = true;
                tags checkId = new tags();
                while (flag)
                {
                    Console.Write("Укажите id пользователя для удаления тэга с аккаунта\nId: ");
                    id = Console.ReadLine();
                    while (Regex.IsMatch(id, @"\D") || id == "")
                    {
                        Console.Write("Атрибут id может содержать только цифры\nId: ");
                        id = Console.ReadLine();
                    }
                    checkId.id = Int32.Parse(id);
                    if (checkId.id != 0)
                        flag = false;
                }
                Console.WriteLine(GetQueryResult($"SELECT deletetag({checkId.id})"));
                PrintUsersTags();
            }
            PrintMenu();
        }

        public void PrintMenu()
        {
            Console.WriteLine("0.    Регистрация пользователя\n1.    Отображение всех пользователей\n2.    Добавление тэга к аккаунту\n3.    Удаление тэга с аккаунта\n4.    Удаление тэга");
            string line = Console.ReadLine();
            if (line.Length != 1 && (line != "0" || line != "1" || line != "2" || line != "3" || line != "4"))
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
                    case "2": InsertTagsInAccount(); break;
                    case "3": DeleteTagInAccount(); break;
                    case "4": DeleteTag(); break;
                }
            }
        }
    }
}
