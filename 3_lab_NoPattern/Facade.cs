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
                        var animals = db.animals.FromSqlRaw($"SELECT * FROM Animals WHERE Id = (SELECT Parent_Id FROM Animals WHERE Id = {id})").ToList();
                        if (animals.Count == 0)
                            Console.WriteLine("Ничего не найдено");
                        else
                        {
                            foreach (var animal in animals)
                                Console.WriteLine($"{animal.id}    {animal.title}");
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
                        WITH RECURSIVE r AS (
	                    SELECT id, title, parent_id
	                    FROM animals
	                    WHERE id = {id}
	
	                    UNION

	                    SELECT animals.id, animals.title, animals.parent_id
	                    FROM animals
		                    JOIN r
			                    ON animals.id = r.parent_id
                        )
                        SELECT * FROM r;").ToList();
                        if (animals.Count != 0)
                        {
                            animals.Reverse();
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
                        var animals = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE parent_id = {id}").OrderBy(x => x.id).ToList();
                        if (animals.Count != 0)
                        {
                            for (int i = 0; i < animals.Count; i++)
                                Console.WriteLine($"{animals[i].id}    {animals[i].title}");
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

                        int predId = 0, teckId = 0;
                        var animals = db.animals.FromSqlRaw($@"
                        WITH RECURSIVE r AS (
	                    SELECT id, title, parent_id
	                    FROM animals
	                    WHERE id = {id}
	
	                    UNION ALL

	                    SELECT animals.id, animals.title, animals.parent_id
	                    FROM animals
		                    JOIN r
			                    ON r.id = animals.parent_id
                        )
                        SELECT * FROM r;").ToList();
                        if (animals.Count != 0)
                            CorrectPrintTree(animals);
                        else
                            Console.WriteLine("Ничего не найдено");
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
                string title = Console.ReadLine().Trim();
                while (Regex.IsMatch(title, @"[A-Za-z!№;%:?*()_+,.=<>{}]") || Regex.IsMatch(title, "[0-9]") || title.Length < 5)
                {
                    Console.WriteLine("В названии не может содержаться буквы латинского алфавита и цифры, и спец. символы, и длина названия не меньше 5");
                    Console.Write("Title: ");
                    title = Console.ReadLine().Trim();
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
                var k = db.animals.FromSqlRaw($@"
                    INSERT INTO animals (title, parent_id) VALUES('{title}', {Int32.Parse(parent_id)}); 
                    SELECT * FROM animals 
                    WHERE title = '{title}' 
                    AND parent_id = {Int32.Parse(parent_id)} 
                    ORDER BY id DESC 
                    LIMIT 1");
                if (k != null)
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
                        List<animals> animals = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE parent_id = {id}").ToList();
                        bool flag = true;
                        if(animals.Count != 0)
                        {
                            flag = false;
                            var findAnimals = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE id = {id}").ToList();
                            int newParent = db.Database.ExecuteSqlRaw($"UPDATE animals SET parent_id = {findAnimals[0].parent_id} WHERE parent_id = {id}");
                            //Console.WriteLine(newParent);
                            if (newParent != 0)
                            {
                                Console.WriteLine("Переподчинение прошло успешно");
                                newParent = db.Database.ExecuteSqlRaw($"DELETE FROM animals WHERE id = {id}");
                                if (newParent != 0)
                                {
                                    Console.WriteLine("Удаление узла прошло успешно");
                                    PrintTree();
                                }
                            }
                            else
                                Console.WriteLine("Переподчинение не прошло успешно");
                        }
                        if (flag)
                        {
                            int deleteAnimal = db.Database.ExecuteSqlRaw($"DELETE FROM animals WHERE id = {id}");
                            if (deleteAnimal != 0)
                            {
                                Console.WriteLine("Удаление листа прошло успешно");
                                PrintTree();
                            }
                            else
                                Console.WriteLine("Удаление листа не прошло успешно");
                        }
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

                        int predId = 0, teckId = 0;
                        List<animals> animals = db.animals.FromSqlRaw($@"
                        WITH RECURSIVE r AS (
	                    SELECT id, title, parent_id
	                    FROM animals
	                    WHERE id = {id}
	
	                    UNION ALL

	                    SELECT animals.id, animals.title, animals.parent_id
	                    FROM animals
		                    JOIN r
			                    ON r.id = animals.parent_id
                        )
                        SELECT * FROM r;").ToList();

                        //CorrectPrintTree(animals, countLine - animals.Count);
                        int deleteAnimal = 0;
                        if(animals.Count != 0)
                        foreach(var animal in animals)
                        {
                            deleteAnimal += db.Database.ExecuteSqlRaw($"DELETE FROM animals WHERE id = {animal.id}");
                        }
                        Console.WriteLine($"{deleteAnimal} объектов удалено");
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
        private void MovingNode()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите id объекта для переноса: ");
                string firstId = Console.ReadLine().Trim();
                if (Regex.IsMatch(firstId, @"\d\Z"))
                {
                    try
                    {
                        Int32.Parse(firstId);
                        Console.Write($"Введте id объекта к которому нужно переместить: ");
                        string secondId = Console.ReadLine().Trim();
                        while (!Regex.IsMatch(secondId, @"\d\Z"))
                        {
                            Console.WriteLine("Введен не корректный id");
                            Console.Write("Введте id объекта к которому нужно переместить: ");
                            secondId = Console.ReadLine().Trim();
                        }
                        Int32.Parse(secondId);
                        var animals = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE parent_id = {firstId}").ToList();
                        bool flag = true;

                        if (animals.Count != 0)
                        {
                            flag = false;
                            var findAnimals = db.animals.FromSqlRaw($"SELECT * FROM animals WHERE id = {firstId}").ToList();
                            int newParent = db.Database.ExecuteSqlRaw($"UPDATE animals SET parent_id = {findAnimals[0].parent_id} WHERE parent_id = {firstId}");
                            newParent = db.Database.ExecuteSqlRaw($"UPDATE animals SET parent_id = {secondId} WHERE id = {firstId}");
                            if (newParent != 0)
                            {
                                Console.WriteLine("Переподчинение узла прошло успешно");
                                PrintTree();
                            }
                            else
                                Console.WriteLine("Переподчинение узла не прошло успешно");
                        }
                        if (flag)
                        {
                            int deleteAnimal = db.Database.ExecuteSqlRaw($"UPDATE animals SET parent_id = {secondId} WHERE id = {firstId}");
                            if (deleteAnimal != 0)
                            {
                                Console.WriteLine("Переподчинение листа прошло успешно");
                                PrintTree();
                            }
                            else
                                Console.WriteLine("Переподчинение листа не прошло успешно");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Не корректный ввод id");
                        MovingNode();
                    }
                }
                else
                {
                    Console.WriteLine("Не корректный ввод id");
                    MovingNode();
                }
            }
            PrintMenu();
        }
        private void MovingSubTree()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Console.Write("Введите id объекта для переноса: ");
                string firstId = Console.ReadLine().Trim();
                if (Regex.IsMatch(firstId, @"\d\Z"))
                {
                    Console.Write($"Введте id объекта к которому нужно переместить: ");
                    string secondId = Console.ReadLine().Trim();
                    while(!Regex.IsMatch(secondId, @"\d\Z"))
                    {
                        Console.WriteLine("Введен не корректный id");
                        Console.Write("Введте id объекта к которому нужно переместить: ");
                        secondId = Console.ReadLine().Trim();
                    }
                    int animals = db.Database.ExecuteSqlRaw($"UPDATE animals SET parent_id = {secondId} WHERE id = {firstId}");
                    if (animals != 0)
                    {
                        Console.WriteLine("Изменение прошло успешно");
                        PrintTree();
                    }
                    else
                        Console.WriteLine("Изменение не увенчалось успехом");
                }
                else
                {
                    Console.WriteLine("Не корректный ввод id");
                    MovingSubTree();
                }
            }
            PrintMenu();
        }
        private void PrintTree()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<animals> animals = db.animals.FromSqlRaw($@"
                    WITH RECURSIVE r AS (
	                SELECT id, title, parent_id
	                FROM animals
	                WHERE parent_id = 0
	
                    UNION

                    SELECT animals.id, animals.title, animals.parent_id
                    FROM animals
	                    JOIN r
		                    ON r.id = animals.parent_id
                    )
                    SELECT * FROM r;").ToList();
                CorrectPrintTree(animals);
            }
            PrintMenu();
        }
        public void PrintMenu()
        {
            Console.WriteLine("0.    Получение прямого родителя\n1.    Получение всех родителей\n2.    Получение прямых потомков\n3.    Получение всех потомков\n4.    Вставка листа\n5.    Удаление узла\n6.    Удаление поддерева\n7.    Перемещение узла\n8.    Перемещение поддерева\n9.    Вывод всего дерева");
            string line = Console.ReadLine();
            if (line.Length != 1 && (line != "0" || line != "1" || line != "2" || line != "3" || line != "4" || line != "5" || line != "6" || line != "7" || line != "8" || line != "9"))
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
                    case "7": MovingNode(); break;
                    case "8": MovingSubTree(); break;
                    case "9": PrintTree(); break;
                }
            }
        }
        private void CorrectPrintTree(List<animals> animals)
        {
            string space = "       ";
            int i = 0, iPred = 0;
            Console.WriteLine($"\n\n\n{animals.First().id}     {animals.First().title}");
            iPred = animals.IndexOf(animals.First());
            animals listAnimals = animals.Find(p => p.parent_id == animals.First().id);
            while(listAnimals != null)
            {
                i = animals.IndexOf(listAnimals);
                Console.Write($"{listAnimals.id}");
                if(listAnimals.id < 10)
                    Console.Write(' ');
                    Console.Write(space);
                Console.WriteLine(listAnimals.title);
                if(listAnimals.parent_id != -1)
                    iPred = animals.IndexOf(animals.Find(p => p.id == (int)animals[i].parent_id)) + 1;
                //i = (int)listAnimals.id - 1;
                listAnimals = animals.Find(p => p.parent_id == (listAnimals.id));
                while (listAnimals == null)
                {
                    animals[i].parent_id = -1;
                    if (iPred == 0)
                        break;
                    listAnimals = animals[iPred - 1];
                    i = iPred - 1;
                    iPred = animals.IndexOf(animals.Find(p => p.id == (int)listAnimals.parent_id)) + 1;
                    if (animals.Find(p => p.parent_id == listAnimals.id) != null)
                        listAnimals = animals.Find(p => p.parent_id == listAnimals.id);
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
