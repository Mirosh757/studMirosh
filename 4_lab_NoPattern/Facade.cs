using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace _3_lab_NoPattern
{
    internal class Facade
    {
        private void GettingDirectParent()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите id: ");
                string id = Console.ReadLine().Trim();
                if (Regex.IsMatch(id, @"\d\Z"))
                {
                    try
                    {
                        Int32.Parse(id);
                        var animals = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE id = {id}").ToList();
                        if (animals.Count == 0)
                            Console.WriteLine("Ничего не найдено");
                        else
                        {
                            string[] strings = animals[0].path.Split('/');
                            if (strings.Length != 2)
                            {
                                animals = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE id = {strings[strings.Length - 3]}").ToList();
                                Console.WriteLine($"{animals[0].id}    {animals[0].title}");
                            }
                            else
                                Console.WriteLine("Ничего не найдено");
                        }

                    }
                    catch
                    {
                        Console.WriteLine("Не корректный ввод id");
                        GettingDirectParent();
                    }    
                }
                else
                {
                    Console.WriteLine("Не корректный ввод id");
                    GettingDirectParent();
                }
            }
            PrintMenu();
        }
        private void GettingAllParents()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите id: ");
                string id = Console.ReadLine().Trim();
                if (Regex.IsMatch(id, @"\d\Z"))
                {
                    try
                    {
                        Int32.Parse(id);
                        int predId = 0, teckId = 0;
                        var animals = db.animals.FromSqlRaw($@"
                        SELECT * FROM animals
                        WHERE (
                            SELECT path FROM animals WHERE id = {id}
                        )LIKE path || '%'").ToList();
                        if (animals.Count > 1)
                        {
                            bool flag = false;
                            int i = 0;
                            foreach(var animal in animals)
                            {
                                if (!flag)
                                {
                                    animal.path = "0";
                                    flag = true;
                                }
                                else
                                    animal.path = i.ToString();
                                i = Int32.Parse(animal.id.ToString());
                            }
                            CorrectPrintTree(animals);
                        }
                        else
                            Console.WriteLine("Ничего не найдено");
                    }
                    catch
                    {
                        Console.WriteLine("Не корректный ввод id");
                        GettingAllParents();
                    }
                }
                else
                {
                    Console.WriteLine("Не корректный ввод id");
                    GettingAllParents();
                }
            }
            PrintMenu();
        }
        private void GettingDirectChildren()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите id: ");
                string id = Console.ReadLine().Trim();
                if (Regex.IsMatch(id, @"\d\Z"))
                {
                    try
                    {
                        Int32.Parse(id);
                        var animals = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE path LIKE '%/{id}/%'").ToList();
                        if (animals.Count != 0 && id != "0")
                        {
                            string[] strings;
                            foreach (var animal in animals)
                            {
                                strings = animal.path.Split('/');
                                if (strings.Length != 2)
                                {
                                    if (strings[strings.Length - 3] == id)
                                        Console.WriteLine($"{animal.id}     {animal.title}");
                                }
                            }
                        }
                        else 
                            Console.WriteLine("Ничего не найдено");
                    }
                    catch
                    {
                        Console.WriteLine("Не корректный ввод id");
                        GettingDirectChildren();
                    }
                }
                else
                {
                    Console.WriteLine("Не корректный ввод id");
                    GettingDirectChildren();
                }
            }
            PrintMenu();
        }
        private void GettingAllChildren()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите id: ");
                string id = Console.ReadLine().Trim();
                if (Regex.IsMatch(id, @"\d\Z"))
                {
                    try
                    {
                        Int32.Parse(id);
                        if (id == "1")
                        {
                            PrintTree();
                        }
                        else
                        {
                            int predId = 0, teckId = 0;
                            var animals = db.animals.FromSqlRaw($@"
                        SELECT * FROM animals 
                            WHERE path LIKE(
                                SELECT path || '%' FROM animals WHERE id = {id})
                        ").ToList();
                            if (animals.Count != 0)
                            {
                                string[] parent;
                                foreach (animals animal in animals)
                                {
                                    parent = animal.path.Split("/");
                                    animal.path = parent[parent.Length - 3];
                                }
                                CorrectPrintTree(animals);
                            }
                            else
                                Console.WriteLine("Ничего не найдено");
                        }
                    }
                    catch 
                    {
                        Console.WriteLine("Не корректный ввод id");
                        GettingAllChildren();
                    }
                }
                else
                {
                    Console.WriteLine("Не корректный ввод id");
                    GettingAllChildren();
                }
            }
            PrintMenu();
        }
        private void InsertLeaf()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Для вставки введите значения атрибутов\nTitle: ");
                string title = Console.ReadLine().Trim().ToLower();
                while (Regex.IsMatch(title, @"[A-Za-z!№;%:?*()_+,.=<>{}]") || Regex.IsMatch(title, "[0-9]") || title.Length < 5)
                {
                    Console.WriteLine("В названии не может содержаться буквы латинского алфавита и цифры, и спец. символы, и длина названия не меньше 5");
                    Console.Write("Title: ");
                    title = Console.ReadLine().Trim().ToLower();
                }
                List<animals> checkUnique = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE LOWER(title) = '{title.ToLower()}'").ToList();
                if(checkUnique.Count != 0)
                {
                    Console.WriteLine("Название должно быть уникальным");
                    InsertLeaf();
                    return;
                }
                char help = Convert.ToChar(title[0].ToString().ToUpper());
                List<char> chars = new List<char>();
                for (int i = 0; i < title.Length; i++)
                {
                    if(i == 0)
                        chars.Add(help);
                    else
                        chars.Add(title[i]);
                }
                string hel = "";
                for (int i = 0; i < chars.Count; i++)
                    hel += chars[i];
                title = hel;
                Console.Write("Parent_id: ");
                string parent_id = Console.ReadLine().Trim();
                while (!Regex.IsMatch(parent_id, @"\d\Z"))
                {
                    Console.WriteLine("В id предка содержатся только цифры");
                    Console.Write("Parent_id: ");
                    parent_id = Console.ReadLine().Trim();
                }
                List<animals> k = db.animals.FromSqlRaw($@"
                    INSERT INTO animals (title, path) VALUES('{title}', '-1');
                    SELECT * FROM animals 
                    WHERE title = '{title}'").ToList();
                int insert = db.Database.ExecuteSqlRaw($@"
                    UPDATE animals SET path = (
                        SELECT path FROM animals WHERE id = {parent_id})
                    || {k[0].id} || '/'
                    WHERE id = {k[0].id}");
                if (insert != 0)
                {
                    foreach(var kHelp in k)
                        Console.WriteLine($"Объект вставлен с id = {kHelp.id}");
                    PrintTree();
                }
                else
                    Console.WriteLine("Вставка не удалась");
            }
            PrintMenu();
        }
        private void DeleteNode()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите id: ");
                string id = Console.ReadLine().Trim();
                if (Regex.IsMatch(id, @"\d\Z"))
                {
                    try
                    {
                        Int32.Parse(id);
                        List<animals> animals = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE path LIKE '%/{id}/%'").ToList();
                        if (animals.Count == 1)
                        {
                            int k = db.Database.ExecuteSqlRaw($"DELETE FROM animals WHERE id = {id}");
                            if (k != 0)
                                Console.WriteLine($"Объект с id = {id} удален");
                            else
                                Console.WriteLine($"Объект с id = {id} не удален");
                        }
                        else if (animals.Count > 1)
                        {
                            foreach (animals ani in animals)
                            {
                                if (id.Length == 1)
                                    ani.path = ani.path.Remove(ani.path.IndexOf(id), 2);
                                else
                                    ani.path = ani.path.Remove(ani.path.IndexOf(id), 3);
                            }
                            int k = db.SaveChanges();
                            if (k != 0)
                            {
                                Console.WriteLine("Переподчинение прошло успешно");
                                k = db.Database.ExecuteSqlRaw($"DELETE FROM animals WHERE id = {id}");
                                if (k != 0)
                                {
                                    Console.WriteLine("Удаление прошло успешно");
                                    PrintTree();
                                }
                                else
                                    Console.WriteLine("Удаление не прошло успешно");

                            }
                            else
                                Console.WriteLine("Переподчинение прошло успешно");
                        }
                        else
                            Console.WriteLine($"id = {id} не существет в базе");
                    }
                    catch
                    {
                        Console.WriteLine("Не корректный ввод id");
                        DeleteNode();
                    }
                }
                else
                {
                    Console.WriteLine("Не корректный ввод id");
                    DeleteNode();
                }
            }
            PrintMenu();
        }
        private void DeleteSubTree()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //Здесь сначала надо вставить код из поиска всех потомков, после вручную удалить по всем id которые пришли
                Console.Write("Введите id: ");
                string id = Console.ReadLine().Trim();
                if (Regex.IsMatch(id, @"\d\Z"))
                {
                    try
                    {
                        Int32.Parse(id);
                        int deleteAnimalSubTree = db.Database.ExecuteSqlRaw($"DELETE FROM animals WHERE path LIKE '%/{id}/%'");
                        Console.WriteLine($"{deleteAnimalSubTree} объектов удалено");
                        PrintTree();
                    }
                    catch
                    {
                        Console.WriteLine("Не корректный ввод id");
                        GettingAllChildren();
                    }
                }
                else
                {
                    Console.WriteLine("Не корректный ввод id");
                    GettingAllChildren();
                }
            }
            PrintMenu();
        }
        private void PrintTree()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<animals> animals = db.animals.ToList();
                if (animals.Count != 0)
                {
                    string[] parent;
                    int i = 2;
                    foreach (animals animal in animals)
                    {
                        parent = animal.path.Split("/");
                        if (parent.Length != 2)
                            animal.path = parent[parent.Length - 3];
                        else
                            animal.path = "0";
                    }
                }
                else
                    Console.WriteLine("Ничего не найдено");
                CorrectPrintTree(animals);
            }
            PrintMenu();
        }
        public void PrintMenu()
        {
            Console.WriteLine("0.    Получение прямого родителя\n1.    Получение всех родителей\n2.    Получение прямых потомков\n3.    Получение всех потомков\n4.    Вставка листа\n5.    Удаление узла\n6.    Удаление поддерева\n7.    Вывод всего дерева");
            string line = Console.ReadLine();
            if (line.Length != 1 && (line != "0" || line != "1" || line != "2" || line != "3" || line != "4" || line != "5" || line != "6" || line != "7"))
            {
                Console.WriteLine("Неверная команда");
                PrintMenu();
            }
            else
            {
                switch (line)
                {
                    case "0": GettingDirectParent(); break;
                    case "1": GettingAllParents(); break;
                    case "2": GettingDirectChildren(); break;
                    case "3": GettingAllChildren(); break;
                    case "4": InsertLeaf(); break;
                    case "5": DeleteNode(); break;
                    case "6": DeleteSubTree(); break;
                    case "7": PrintTree(); break;
                }
            }
        }
        private void CorrectPrintTree(List<animals> animals)
        {
            string space = "       ";
            int i = 0, iPred = 0;
            Console.WriteLine($"\n\n\n{animals.First().id}     {animals.First().title}");
            iPred = animals.IndexOf(animals.First());
            animals listAnimals = animals.Find(p => Int32.Parse(p.path) == animals.First().id);
            while(listAnimals != null)
            {
                i = animals.IndexOf(listAnimals);
                Console.Write($"{listAnimals.id}");
                if(listAnimals.id < 10)
                    Console.Write(' ');
                    Console.Write(space);
                Console.WriteLine(listAnimals.title);
                if(Int32.Parse(listAnimals.path) != -1)
                    iPred = animals.IndexOf(animals.Find(p => p.id == Int32.Parse(animals[i].path))) + 1;
                //i = (int)listAnimals.id - 1;
                listAnimals = animals.Find(p => Int32.Parse(p.path) == (listAnimals.id));
                while (listAnimals == null)
                {
                    animals[i].path = "-1";
                    if (iPred == 0)
                        break;
                    listAnimals = animals[iPred - 1];
                    i = iPred - 1;
                    iPred = animals.IndexOf(animals.Find(p => p.id == Int32.Parse(animals[i].path))) + 1;
                    if (animals.Find(p => Int32.Parse(p.path) == listAnimals.id) != null)
                        listAnimals = animals.Find(p => Int32.Parse(p.path) == listAnimals.id);
                    else
                        listAnimals = null;
                    space = space.Remove(space.Length - 4, 3);
                }
                if (listAnimals != null)
                    i = animals.IndexOf(listAnimals);
                space += "   ";
            }
            Console.WriteLine("\n\n\n");
            /*
            string space = "         ";
            int i = 0, iPred = 0;
            Console.WriteLine($"{animals[i].id}     {animals[i].title}");
            animals listAnimals = animals.Find(p => p.parent_id == animals[i].id);
            while(listAnimals != null)
            {
                Console.Write($"{listAnimals.id}");
                if(listAnimals.id < 10)
                    Console.Write(' ');
                    Console.Write(space);
                Console.WriteLine(listAnimals.title);
                if(listAnimals.parent_id != -1)
                    iPred = (int)listAnimals.parent_id;
                i = (int)listAnimals.id - 1;
                listAnimals = animals.Find(p => p.parent_id == animals[i].id);
                while (listAnimals == null)
                    {
                        animals[i].parent_id = -1;
                        if (iPred == 0)
                            break;
                        listAnimals = animals[iPred - 1];
                        i = iPred - 1;
                        iPred = (int)listAnimals.parent_id;
                        if (animals.Find(p => p.parent_id == listAnimals.id) != null)
                            listAnimals = animals.Find(p => p.parent_id == listAnimals.id);
                        else
                            listAnimals = null;
                        space = space.Remove(space.Length - 4, 3);
                    }
                    space += "   ";
            }
            */
        }
        /*
        int i = 0, countSpace = 0;
        string space = "   ", outPrint = "";
        private string Rec(List<animals> animals)
        {
            if(animals.Find(p => p.parent_id == animals[i].id) == null)
            {
                return "";
            }
            outPrint += animals[i].id + " ";
            for(int j = 0;j < countSpace;j++)
                outPrint += space;
            outPrint += animals[i].title + "\n";
            i = (int)animals.Find(p => p.parent_id == animals[i].id).id;
            return Rec(animals);
        }
        */
    }
}
