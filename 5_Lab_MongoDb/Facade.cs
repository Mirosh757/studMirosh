﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _4_Lab_MongoDb
{
    internal class Facade
    {
        private readonly IMongoCollection<Medical_instituction> _medical;
        private readonly IMongoCollection<Region> _region;
        public Facade()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("doctor");
            _medical = database.GetCollection<Medical_instituction>("medical_instituctions");
            _region = database.GetCollection<Region>("regions");
        }
        public void MedicalCreate()
        {
            var medical = new Medical_instituction();
            var regions = _region.Find(r => true).SortBy(reg => reg.region_name).ToList();
            Console.Write("\nДля добавления нового мед. учреждения введите данные\n");
            while (string.IsNullOrEmpty(medical.title))
            {
                Console.Write("Название: ");
                medical.title = Console.ReadLine().Trim();
            }
            while (string.IsNullOrEmpty(medical.website))
            {
                Console.Write("Веб-сайт: ");
                medical.website = Console.ReadLine().Trim();
            }
            while (string.IsNullOrEmpty(medical.address))
            {
                Console.Write("Адрес: ");
                medical.address = Console.ReadLine().Trim();
            }
            Console.WriteLine("\nСписок доступных городов:");
            for (int i = 0; i < regions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {regions[i].region_name}");
            }
            Console.Write("Введите номер города: ");
            string regionId = Console.ReadLine().Trim();
            while(true)
            {
                while (Regex.IsMatch(regionId, @"\D") || regionId == "")
                {
                    Console.Write("Введен не верный номер\nВведите номер города: ");
                    regionId = Console.ReadLine();
                }
                if ((Int32.Parse(regionId) - 1) < regions.Count)
                {
                    medical.region_id = regions[Int32.Parse(regionId) - 1].Id;
                    break;
                }
                else
                    Console.WriteLine("Доктора с введенным номером не существует в базе данных");
            }
            Console.Write("Укажите номер типа мед. учереждения\n1. Больница\n2. Отделение\nНомер типа учереждения: ");
            string numberOfType = Console.ReadLine().Trim();
            while(numberOfType != "1" && numberOfType != "2")
            {
                Console.Write("Указано не верное значение\nНомер типа учереждения: ");
                numberOfType = Console.ReadLine().Trim();
            }
            medical.type = numberOfType == "1" ? "hospital" : "department";
            string action = medical.type == "hospital" ? "Д" : "";
            while((action != "Д" && action != "Н") && medical.type == "department")
            {
                Console.Write("Желаете ли вы указать подотделения для отделения(Д-да, Н-нет): ");
                action = Console.ReadLine().Trim().ToUpper();
            }
            if (action == "Д")
            {
                var usedDepartmentIds = _medical
                    .Find(med => med.departments != null && med.departments.Length > 0)
                    .Project(med => med.departments)
                    .ToList()
                    .SelectMany(arr => arr) // Разворачиваем массивы в плоский список
                    .Distinct() // Убираем дубликаты
                    .ToList();

                // Теперь ищем отделения, которых нет в usedDepartmentIds
                List<Medical_instituction> freeDepartments = _medical
                    .Find(dep =>
                        dep.type == "department" &&
                        !usedDepartmentIds.Contains(dep.Id) && medical.region_id == dep.region_id)
                    .ToList();

                if (freeDepartments.Count == 0)
                    Console.WriteLine("В данном городе больше нету свободных отделений");
                else
                {
                    for (int i = 0; i < freeDepartments.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {freeDepartments[i].title}");
                    }
                    string[] departmentId;
                    Console.WriteLine("Введите номер отделения (если вы желаете указать сразу несколько отделений, то укажите их номера через пробел)");
                    departmentId = Console.ReadLine().Trim().Split(' ');
                    for(int i = 0; i < departmentId.Length;i++)
                    {
                        if (Regex.IsMatch(departmentId[i], @"^\d+$"))
                        {
                            if (((Int32.Parse(departmentId[i]) - 1) < freeDepartments.Count))
                            {
                                if(freeDepartments.Find(dep => dep.Id == freeDepartments[(Int32.Parse(departmentId[i]) - 1)].Id) == null)
                                    medical.departments = medical.departments
                                        .Concat(new[] { freeDepartments[(Int32.Parse(departmentId[i]) - 1)].Id }) // Добавляем новый ID
                                        .ToArray();
                            }
                        }
                    }
                }
                if(medical.type == "hospital")
                {
                    Requisites requisite = new Requisites();

                    while (string.IsNullOrEmpty(requisite.hospital_reduce_name))
                    {
                        Console.Write("Сокращенное название больницы: ");
                        requisite.hospital_reduce_name = Console.ReadLine().Trim();
                    }
                    while (string.IsNullOrEmpty(requisite.registration_date))
                    {
                        Console.Write("Дата регистрации(вводить в формате ГГГГ-ММ-ДД): ");
                        requisite.registration_date = Console.ReadLine().Trim();
                    }
                    Console.WriteLine("Дальше требуется вставить ФИО глав врача\nПример написания ФИО: Иванов И. И.");
                    while (string.IsNullOrEmpty(requisite.name_legal_faces))
                    {
                        Console.Write("ФИО: ");
                        requisite.name_legal_faces = Console.ReadLine().Trim();
                    }
                    while(true)
                    {
                        while (string.IsNullOrEmpty(requisite.ogrn))
                        {
                            Console.Write("ОГРН: ");
                            requisite.ogrn = Console.ReadLine().Trim();
                        }
                        if (IsUnique(requisite))
                            break;
                        else
                        {
                            Console.WriteLine("Введенный ОГРН уже существует в базе");
                            requisite.ogrn = "";
                        }
                    }
                    while (true)
                    {
                        while (string.IsNullOrEmpty(requisite.inn))
                        {
                            Console.Write("ИНН: ");
                            requisite.inn = Console.ReadLine().Trim();
                        }
                        if (IsUnique(requisite))
                            break;
                        else
                        {
                            Console.WriteLine("Введенный ИНН уже существует в базе");
                            requisite.inn = "";
                        }
                    }
                    while (true)
                    {
                        while (string.IsNullOrEmpty(requisite.kpp))
                        {
                            Console.Write("КПП: ");
                            requisite.kpp = Console.ReadLine().Trim();
                        }
                        if (IsUnique(requisite))
                            break;
                        else
                        {
                            Console.WriteLine("Введенный КПП уже существует в базе");
                            requisite.kpp = "";
                        }
                    }
                    medical.requisites = requisite;
                }
            }
            _medical.InsertOne(medical);
            Console.WriteLine("Мед. учереждение успешно добавлено в базу данных");
            PrintMenu();
        }
        public bool IsUnique(Requisites requisite)
        {
            var filter = Builders<Medical_instituction>.Filter.And(
                // Ищем документы с такими же OGRN/ИНН/КПП в requisites
                Builders<Medical_instituction>.Filter.Or(
                    Builders<Medical_instituction>.Filter.Eq("requisites.ogrn", requisite.ogrn),
                    Builders<Medical_instituction>.Filter.Eq("requisites.inn", requisite.inn),
                    Builders<Medical_instituction>.Filter.Eq("requisites.kpp", requisite.kpp)
                ),
                // Но исключаем текущий документ (если это обновление)
                Builders<Medical_instituction>.Filter.Not(
                    Builders<Medical_instituction>.Filter.Eq("_id", requisite.Id)
                )
            );
            return !_medical.Find(filter).Any();
        }
        public void MedicalRetrieveAll()
        {
            var medicalAll = _medical.Find(medicals => true).SortBy(f => f.Id).ToList();
            var regionAll = _region.Find(regions => true).ToList();
            string id, title, website, address, regionTitle, department_id = "";
            Console.WriteLine("№   | Название                           | Веб-сайт                  | Адрес                               | Город            | Номер отделения        ");

            for (int i = 0; i < medicalAll.Count; i++)
            {
                for (int j = 0; j < medicalAll[i].departments.Length; j++)
                    department_id += $"{medicalAll.FindIndex(dep => dep.Id == medicalAll[i].departments[j]) + 1}, ";
                id = CorrFormat((i + 1).ToString(), 3) + " ";
                title = " " + CorrFormat(medicalAll[i].title, 34) + " ";
                website = " " + CorrFormat(medicalAll[i].website, 25) + " ";
                address = " " + CorrFormat(medicalAll[i].address, 35) + " ";
                regionTitle = " " + CorrFormat(_region.Find(name => name.Id == medicalAll[i].region_id).FirstOrDefault().region_name, 16) + " ";
                department_id = " " + CorrFormat(department_id, 22) + " ";
                Console.WriteLine($"{id}|{title}|{website}|{address}|{regionTitle}|{department_id}");
                department_id = "";
            }
            PrintMenu();
        }
        private string CorrFormat(string value, int count)
        {
            if (value.Length <= count)
                return value.PadRight(count, ' ');
            else
                return value.Substring(0, count - 3) + "...";
        }
        public void MedicalRetrieve()
        {
            Console.Write("\nВведите номер мед. учереждения: ");
            string id = Console.ReadLine();
            while (Regex.IsMatch(id, @"\D") || id == "")
            {
                Console.Write("Введен не верный номер\nВведите номер мед. учереждения: ");
                id = Console.ReadLine();
            }
            var medicalAll = _medical.Find(medicals => true).SortBy(f => f.Id).ToList();
            var regionAll = _region.Find(regions => true).ToList();
            if ((Int32.Parse(id) - 1) < medicalAll.Count)
            {
                Medical_instituction medical = medicalAll[Int32.Parse(id) - 1];
                Console.WriteLine($"Название: {medical.title}");
                Console.WriteLine($"Веб-сайт: {medical.website}");
                Console.WriteLine($"Адрес: {medical.address}");
                Console.WriteLine($"Город: {regionAll.Find(city => city.Id == medical.region_id).region_name}");
                string departmentTitle = "";
                for( int i = 0; i < medical.departments.Length; i++)
                {
                    departmentTitle += $"{medicalAll.Find(dep => dep.Id == medical.departments[i]).title} ";
                }
                Console.WriteLine($"Номер отделения: {departmentTitle}");
                if(medical.type == "hospital")
                {
                    Console.WriteLine($"Сокращенное название города: {medical.requisites.hospital_reduce_name}");
                    Console.WriteLine($"Дата регистрации: {medical.requisites.registration_date}");
                    Console.WriteLine($"ФИО глав. врача: {medical.requisites.name_legal_faces}");
                    Console.WriteLine($"ОГРН: {medical.requisites.ogrn}");
                    Console.WriteLine($"ИНН: {medical.requisites.inn}");
                    Console.WriteLine($"КПП: {medical.requisites.kpp}");
                }
            }
            else
                Console.WriteLine("Мед. учереждение с введенным номером не существует в базе данных");
            PrintMenu();
        }
        public async void MedicalUpdate()
        {
            var medicalAll = _medical.Find(med => true).SortBy(m => m.Id).ToList();
            var regionAll = _region.Find(r => true).ToList();

            // Вывод списка медицинских учреждений
            Console.WriteLine("\nСписок медицинских учреждений:");
            for (int i = 0; i < medicalAll.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {medicalAll[i].title} ({medicalAll[i].type})");
            }

            // Выбор учреждения для редактирования
            Console.Write("\nВведите номер мед. учреждения для редактирования: ");
            string input = Console.ReadLine().Trim();
            while (!int.TryParse(input, out int index) || index < 1 || index > medicalAll.Count)
            {
                Console.Write("Неверный номер. Введите корректный номер: ");
                input = Console.ReadLine().Trim();
            }

            var medicalToUpdate = medicalAll[Int32.Parse(input) - 1];
            bool isHospital = medicalToUpdate.type == "hospital";

            // Меню редактирования
            while (true)
            {
                Console.WriteLine("\nТекущие данные:");
                Console.WriteLine($"1. Название: {medicalToUpdate.title}");
                Console.WriteLine($"2. Веб-сайт: {medicalToUpdate.website}");
                Console.WriteLine($"3. Адрес: {medicalToUpdate.address}");
                Console.WriteLine($"4. Город: {regionAll.Find(r => r.Id == medicalToUpdate.region_id).region_name}");
                Console.WriteLine($"5. Тип: {medicalToUpdate.type}");
                Console.WriteLine("6. Подразделения:");
                foreach (var depId in medicalToUpdate.departments)
                {
                    var dep = medicalAll.Find(d => d.Id == depId);
                    if (dep != null) Console.WriteLine($"   - {dep.title}");
                }

                if (isHospital && medicalToUpdate.requisites != null)
                {
                    Console.WriteLine($"7. Сокращенное название: {medicalToUpdate.requisites.hospital_reduce_name}");
                    Console.WriteLine($"8. Дата регистрации: {medicalToUpdate.requisites.registration_date}");
                    Console.WriteLine($"9. ФИО руководителя: {medicalToUpdate.requisites.name_legal_faces}");
                    Console.WriteLine($"10. ОГРН: {medicalToUpdate.requisites.ogrn}");
                    Console.WriteLine($"11. ИНН: {medicalToUpdate.requisites.inn}");
                    Console.WriteLine($"12. КПП: {medicalToUpdate.requisites.kpp}");
                }

                Console.WriteLine("\n0. Завершить редактирование и сохранить");
                Console.Write("Выберите поле для редактирования (номер): ");

                string choice = Console.ReadLine().Trim();
                if (choice == "0") break;

                switch (choice)
                {
                    case "1":
                        Console.Write("Новое название: ");
                        medicalToUpdate.title = Console.ReadLine().Trim();
                        break;
                    case "2":
                        Console.Write("Новый веб-сайт: ");
                        medicalToUpdate.website = Console.ReadLine().Trim();
                        break;
                    case "3":
                        Console.Write("Новый адрес: ");
                        medicalToUpdate.address = Console.ReadLine().Trim();
                        break;
                    case "4":
                        Console.WriteLine("\nДоступные города:");
                        for (int i = 0; i < regionAll.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {regionAll[i].region_name}");
                        }
                        Console.Write("Выберите новый город: ");
                        string regionInput = Console.ReadLine().Trim();
                        if (int.TryParse(regionInput, out int regionIndex) && regionIndex > 0 && regionIndex <= regionAll.Count)
                        {
                            medicalToUpdate.region_id = regionAll[regionIndex - 1].Id;
                        }
                        break;
                    case "5":
                        Console.Write("Укажите новый тип (1 - Hospital, 2 - Department): ");
                        string typeInput = Console.ReadLine().Trim();
                        if (typeInput == "1" || typeInput == "2")
                        {
                            medicalToUpdate.type = typeInput == "1" ? "hospital" : "department";
                            isHospital = medicalToUpdate.type == "hospital";
                        }
                        break;
                    case "6":
                        // Логика редактирования подразделений
                        Console.WriteLine("\nТекущие подразделения:");
                        foreach (var depId in medicalToUpdate.departments)
                        {
                            var dep = medicalAll.Find(d => d.Id == depId);
                            if (dep != null) Console.WriteLine($"- {dep.title}");
                        }

                        Console.WriteLine("\n1. Добавить подразделение");
                        if (medicalToUpdate.departments.Length > 0)
                            Console.WriteLine("2. Удалить подразделение");
                        Console.WriteLine("0. Назад");
                        Console.Write("Выберите действие: ");

                        string depAction = Console.ReadLine().Trim();
                        if (depAction == "1")
                        {
                            // Добавление подразделения
                            var usedDepartmentIds = _medical
                                .Find(med => med.departments != null && med.departments.Length > 0)
                                .Project(med => med.departments)
                                .ToList()
                                .SelectMany(arr => arr) // Разворачиваем массивы в плоский список
                                .Distinct() // Убираем дубликаты
                                .ToList();

                            // Теперь ищем отделения, которых нет в usedDepartmentIds
                            List<Medical_instituction> availableDeps = _medical
                                .Find(dep =>
                                    dep.type == "department" &&
                                    !usedDepartmentIds.Contains(dep.Id) && !medicalToUpdate.departments.Contains(dep.Id) && medicalToUpdate.region_id == dep.region_id)
                                .ToList();
                            if (availableDeps.Count == 0)
                            {
                                Console.WriteLine("Нет доступных подразделений в этом городе.");
                                break;
                            }

                            Console.WriteLine("\nДоступные подразделения:");
                            for (int i = 0; i < availableDeps.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {availableDeps[i].title}");
                            }

                            Console.Write("Выберите подразделение для добавления: ");
                            if (int.TryParse(Console.ReadLine(), out int depIndex) && depIndex > 0 && depIndex <= availableDeps.Count)
                            {
                                var newDepartments = medicalToUpdate.departments.ToList();
                                newDepartments.Add(availableDeps[depIndex - 1].Id);
                                medicalToUpdate.departments = newDepartments.ToArray();
                            }
                        }
                        else if (depAction == "2" && medicalToUpdate.departments.Length > 0)
                        {
                            // Удаление подразделения
                            Console.WriteLine("\nТекущие подразделения:");
                            for (int i = 0; i < medicalToUpdate.departments.Length; i++)
                            {
                                var dep = medicalAll.Find(d => d.Id == medicalToUpdate.departments[i]);
                                Console.WriteLine($"{i + 1}. {dep.title}");
                            }

                            Console.Write("Выберите подразделение для удаления: ");
                            if (int.TryParse(Console.ReadLine(), out int depIndex) && depIndex > 0 && depIndex <= medicalToUpdate.departments.Length)
                            {
                                var newDepartments = medicalToUpdate.departments.ToList();
                                newDepartments.RemoveAt(depIndex - 1);
                                medicalToUpdate.departments = newDepartments.ToArray();
                            }
                        }
                        break;
                    case "7" when isHospital:
                        Console.Write("Новое сокращенное название: ");
                        medicalToUpdate.requisites.hospital_reduce_name = Console.ReadLine().Trim();
                        break;
                    case "8" when isHospital:
                        Console.Write("Новая дата регистрации (ГГГГ-ММ-ДД): ");
                        medicalToUpdate.requisites.registration_date = Console.ReadLine().Trim();
                        break;
                    case "9" when isHospital:
                        Console.Write("Новое ФИО руководителя: ");
                        medicalToUpdate.requisites.name_legal_faces = Console.ReadLine().Trim();
                        break;
                    case "10" when isHospital:
                        while (true)
                        {
                            Console.Write("Новый ОГРН: ");
                            string newOgrn = Console.ReadLine().Trim();
                            if (newOgrn == medicalToUpdate.requisites.ogrn) break;

                            var tempReq = new Requisites { ogrn = newOgrn, Id = medicalToUpdate.Id };
                            if (IsUnique(tempReq))
                            {
                                medicalToUpdate.requisites.ogrn = newOgrn;
                                break;
                            }
                            Console.WriteLine("ОГРН должен быть уникальным!");
                        }
                        break;
                    case "11" when isHospital:
                        while (true)
                        {
                            Console.Write("Новый ИНН: ");
                            string newInn = Console.ReadLine().Trim();
                            if (newInn == medicalToUpdate.requisites.inn) break;

                            var tempReq = new Requisites { inn = newInn, Id = medicalToUpdate.Id };
                            if (IsUnique(tempReq))
                            {
                                medicalToUpdate.requisites.inn = newInn;
                                break;
                            }
                            Console.WriteLine("ИНН должен быть уникальным!");
                        }
                        break;
                    case "12" when isHospital:
                        while (true)
                        {
                            Console.Write("Новый КПП: ");
                            string newKpp = Console.ReadLine().Trim();
                            if (newKpp == medicalToUpdate.requisites.kpp) break;

                            var tempReq = new Requisites { kpp = newKpp, Id = medicalToUpdate.Id };
                            if (IsUnique(tempReq))
                            {
                                medicalToUpdate.requisites.kpp = newKpp;
                                break;
                            }
                            Console.WriteLine("КПП должен быть уникальным!");
                        }
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }

            // Сохранение изменений
            var result = await _medical.ReplaceOneAsync(
                m => m.Id == medicalToUpdate.Id,
                medicalToUpdate);

            if (result.ModifiedCount > 0)
                Console.WriteLine("Данные успешно обновлены!");
            else
                Console.WriteLine("Не удалось обновить данные.");
            PrintMenu();
        }
        public void MedicalDelete()
        {
            Console.Write("Введите номер мед. учереждения для удаления\nНомер: ");
            string id = Console.ReadLine();
            while (true)
            {
                while (Regex.IsMatch(id, @"\D") || id == "")
                {
                    Console.Write("Введен не верный номер\nВведите номер мед. учереждения: ");
                    id = Console.ReadLine();
                }
                List<Medical_instituction> medicalAll = _medical.Find(medical => true).SortBy(f => f.Id).ToList();
                if ((Int32.Parse(id) - 1) < medicalAll.Count && Int32.Parse(id) > 1)
                {
                    Medical_instituction medicalDelete = medicalAll[Int32.Parse(id) - 1];
                    if (medicalDelete.departments.Length == 0)
                    {
                        var result = _medical.DeleteOne(medical => medical.Id == medicalDelete.Id);
                        if (result.DeletedCount > 0)
                            Console.WriteLine("Удаление прошло успешно");
                        else
                            Console.WriteLine("Удаление не прошло успешно");
                        break;
                    }
                    else
                        Console.WriteLine("Данное мед. учереждение нельзя удалить, так как с ним связаны отделения");
                }
                else
                    Console.WriteLine("Мед. учереждение с введенным номером не существует в базе");
                id = "";
            }
            PrintMenu();
        }
        public void MedicalDeleteMany()
        {
            Console.Write("Введите номера мед. учереждений для удаления(разделителями считать пробелы)\nНомер: ");
            string[] idDelete = Console.ReadLine().Split(' ');
            long count = 0;
            List<Medical_instituction> medicalAll = _medical.Find(medical => true).SortBy(f => f.Id).ToList();
            foreach (string id in idDelete)
            {
                if (Regex.IsMatch(id, @"^\d+$") && id != "")
                {
                    if ((Int32.Parse(id) - 1) < medicalAll.Count && Int32.Parse(id) > 1)
                    {
                        if (medicalAll[Int32.Parse(id) - 1].departments.Length == 0)
                        {
                            var result = _medical.DeleteOne(medical => medical.Id == medicalAll[Int32.Parse(id) - 1].Id);
                            count += result.DeletedCount;
                        }
                    }
                }
            }
            if (count > 0)
                Console.WriteLine($"Удалено {count} записей");
            else
                Console.WriteLine("Ничего не удалено");
            PrintMenu();
        }

        public void MedicalSearch()
        {
            var searchParams = new SearchParameters();
            bool continueSearch = true;

            while (continueSearch)
            {
                Console.WriteLine("\nВыберите критерии поиска (поиск по подстроке):");
                Console.WriteLine("1. Название учреждения");
                Console.WriteLine("2. Веб-сайт");
                Console.WriteLine("3. Адрес");
                Console.WriteLine("4. Город");
                Console.WriteLine("5. Тип учреждения");
                Console.WriteLine("6. Сокращенное название больницы");
                Console.WriteLine("7. Реквизиты (ОГРН/ИНН/КПП)");
                Console.WriteLine("8. Показать результаты");
                Console.WriteLine("0. Вернуться в меню");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "1":
                        Console.Write("Введите часть названия: ");
                        searchParams.Title = Console.ReadLine().Trim();
                        break;
                    case "2":
                        Console.Write("Введите часть веб-сайта: ");
                        searchParams.Website = Console.ReadLine().Trim();
                        break;
                    case "3":
                        Console.Write("Введите часть адреса: ");
                        searchParams.Address = Console.ReadLine().Trim();
                        break;
                    case "4":
                        var regionAll = _region.Find(regions => true).SortBy(f => f.Id).ToList();
                        string regionTitle, id;
                        Console.WriteLine("№   | Город                                              ");
                        for (int i = 0; i < regionAll.Count; i++)
                        {
                            id = CorrFormat((i + 1).ToString(), 3) + " ";
                            regionTitle = " " + CorrFormat(regionAll[i].region_name, 50);
                            Console.WriteLine($"{id}|{regionTitle}");
                        }
                        Console.Write("Введите номер города: ");
                        while (true)
                        {
                            id = Console.ReadLine();
                            while (Regex.IsMatch(id, @"\D") || id == "")
                            {
                                Console.Write("Введен не верный номер\nВведите номер города: ");
                                id = Console.ReadLine();
                            }
                            if ((Int32.Parse(id) - 1) < regionAll.Count)
                            {
                                searchParams.Region = regionAll[Int32.Parse(id) - 1].region_name;
                                break;
                            }
                            else
                                Console.Write("Город с введенным номером не существует в базе данных\nВведите номер города: ");
                            id = "";
                        }
                        break;
                    case "5":
                        Console.Write("1. hospital\n2. department\nВведите тип: ");
                        string typeMed = Console.ReadLine().Trim();
                        while (typeMed != "1" && typeMed != "2")
                        {
                            Console.Write("Введено не верное значение\nВведите тип: ");
                            typeMed = Console.ReadLine();
                        }
                        searchParams.Type = typeMed == "1" ? "hospital" : "department";
                        break;
                    case "6":
                        Console.Write("Введите часть сокращенного названия: ");
                        searchParams.ReduceName = Console.ReadLine().Trim();
                        break;
                    case "7":
                        Console.WriteLine("\nВыберите реквизит для поиска:");
                        Console.WriteLine("1. ОГРН");
                        Console.WriteLine("2. ИНН");
                        Console.WriteLine("3. КПП");
                        Console.Write("Выберите: ");
                        string reqChoice = Console.ReadLine().Trim();

                        switch (reqChoice)
                        {
                            case "1":
                                Console.Write("Введите ОГРН: ");
                                string ogrn = Console.ReadLine().Trim();
                                while(Regex.IsMatch(ogrn, @"^\D+$") || ogrn.Length > 13)
                                {
                                    Console.Write("Введено не верное значение\n Введите ОГРН: ");
                                    ogrn = Console.ReadLine().Trim();
                                }
                                searchParams.Ogrn = ogrn;
                                break;
                            case "2":
                                Console.Write("Введите часть ИНН: ");
                                string inn = Console.ReadLine().Trim();
                                while (Regex.IsMatch(inn, @"^\D+$") || inn.Length > 12)
                                {
                                    Console.Write("Введено не верное значение\n Введите ИНН: ");
                                    inn = Console.ReadLine().Trim();
                                }
                                searchParams.Inn = inn;
                                break;
                            case "3":
                                Console.Write("Введите часть КПП: ");
                                string kpp = Console.ReadLine().Trim();
                                while (Regex.IsMatch(kpp, @"^\D+$") || kpp.Length > 9)
                                {
                                    Console.Write("Введено не верное значение\n Введите КПП: ");
                                    kpp = Console.ReadLine().Trim();
                                }
                                searchParams.Kpp = kpp;
                                break;
                            default:
                                Console.WriteLine("Неверный выбор реквизита");
                                break;
                        }
                        break;
                    case "8":
                        ExecuteMedicalSearch(searchParams);
                        continueSearch = false;
                        break;
                    case "0":
                        PrintMenu();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

        private class SearchParameters
        {
            public string Title { get; set; } = "";
            public string Website { get; set; } = "";
            public string Address { get; set; } = "";
            public string Region { get; set; } = "";
            public string Type { get; set; } = "";
            public string ReduceName { get; set; } = "";
            public string Ogrn { get; set; } = "";
            public string Inn { get; set; } = "";
            public string Kpp { get; set; } = "";
        }

        private void ExecuteMedicalSearch(SearchParameters searchParams)
        {
            try
            {
                // Настройки пагинации
                Console.Write("\nКоличество результатов на странице (по умолчанию 5): ");
                string pageSizeInput = Console.ReadLine().Trim();
                int pageSize = 0;
                if (pageSizeInput.Length > 10)
                    pageSize = Int32.MaxValue;
                else
                    pageSize = string.IsNullOrEmpty(pageSizeInput) ? 5 : int.Parse(pageSizeInput);

                Console.Write("Номер страницы (по умолчанию 1): ");
                string pageInput = Console.ReadLine().Trim();
                int page = 0;
                if(pageInput.Length > 10)
                    page = Int32.MaxValue;
                else
                    page = string.IsNullOrEmpty(pageInput) ? 1 : int.Parse(pageInput);

                // Строим фильтр
                var filterBuilder = Builders<Medical_instituction>.Filter;
                var filters = new List<FilterDefinition<Medical_instituction>>();

                // Добавляем условия поиска
                if (!string.IsNullOrEmpty(searchParams.Title))
                    filters.Add(filterBuilder.Regex("title", new BsonRegularExpression($".*{Regex.Escape(searchParams.Title)}.*", "i")));

                if (!string.IsNullOrEmpty(searchParams.Website))
                    filters.Add(filterBuilder.Regex("website", new BsonRegularExpression($".*{Regex.Escape(searchParams.Website)}.*", "i")));

                if (!string.IsNullOrEmpty(searchParams.Address))
                    filters.Add(filterBuilder.Regex("address", new BsonRegularExpression($".*{Regex.Escape(searchParams.Address)}.*", "i")));

                if (!string.IsNullOrEmpty(searchParams.Type))
                    filters.Add(filterBuilder.Eq("type", searchParams.Type));

                if (!string.IsNullOrEmpty(searchParams.ReduceName))
                    filters.Add(filterBuilder.Regex("requisites.hospital_reduce_name",
                        new BsonRegularExpression($".*{Regex.Escape(searchParams.ReduceName)}.*", "i")));

                if (!string.IsNullOrEmpty(searchParams.Ogrn))
                    filters.Add(filterBuilder.Regex("requisites.ogrn",
                        new BsonRegularExpression($".*{Regex.Escape(searchParams.Ogrn)}.*", "i")));

                if (!string.IsNullOrEmpty(searchParams.Inn))
                    filters.Add(filterBuilder.Regex("requisites.inn",
                        new BsonRegularExpression($".*{Regex.Escape(searchParams.Inn)}.*", "i")));

                if (!string.IsNullOrEmpty(searchParams.Kpp))
                    filters.Add(filterBuilder.Regex("requisites.kpp",
                        new BsonRegularExpression($".*{Regex.Escape(searchParams.Kpp)}.*", "i")));

                // Поиск по региону
                if (!string.IsNullOrEmpty(searchParams.Region))
                {
                    var regionFilter = Builders<Region>.Filter.Regex("region_name",
                        new BsonRegularExpression($".*{Regex.Escape(searchParams.Region)}.*", "i"));
                    var regionIds = _region.Find(regionFilter).Project(r => r.Id).ToList();

                    if (regionIds.Any())
                    {
                        filters.Add(filterBuilder.In("region_id", regionIds));
                    }
                    else
                    {
                        filters.Add(filterBuilder.Eq("_id", ObjectId.Empty));
                    }
                }

                var finalFilter = filters.Any() ? filterBuilder.And(filters) : filterBuilder.Empty;

                // Выполняем поиск
                var totalCount = _medical.CountDocuments(finalFilter);
                int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                page = Math.Max(1, Math.Min(page, totalPages));

                Console.WriteLine($"\nНайдено учреждений: {totalCount}. Страница {page} из {totalPages}");

                var results = _medical.Find(finalFilter)
                    .SortBy(m => m.title)
                    .Skip((page - 1) * pageSize)
                    .Limit(pageSize)
                    .ToList();

                var regions = _region.Find(r => true).ToList();

                // Вывод результатов
                if (results.Any())
                {
                    Console.WriteLine("\nРезультаты поиска:");
                    Console.WriteLine("№  | Название                           | Тип        | Адрес                               | Город ");
                    Console.WriteLine("--------------------------------------------------------------------------------------------------");

                    foreach (var (medical, index) in results.Select((m, i) => (m, i)))
                    {
                        var region = regions.FirstOrDefault(r => r.Id == medical.region_id);
                        Console.WriteLine($"{index + 1,-3}| {CorrFormat(medical.title, 34)} | {CorrFormat(medical.type, 10)} | {CorrFormat(medical.address, 35)} | {CorrFormat(region?.region_name ?? "N/A", 20)}");
                    }

                    // Навигация
                    if (totalPages > 1)
                    {
                        ShowPaginationMenu(searchParams, pageSize, page, totalPages);
                    }
                    else
                    {
                        WaitForMenu();
                    }
                }
                else
                {
                    ShowNoResultsMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении поиска: {ex.Message}");
                WaitForMenu();
            }
        }

        private void ShowPaginationMenu(SearchParameters searchParams, int pageSize, int page, int totalPages)
        {
            Console.WriteLine("\nНавигация:");
            if (page > 1) Console.WriteLine("P - Предыдущая страница");
            if (page < totalPages) Console.WriteLine("N - Следующая страница");
            Console.WriteLine("S - Новый поиск");
            Console.WriteLine("M - Вернуться в меню");
            Console.Write("Выберите действие: ");

            string action = Console.ReadLine().Trim().ToUpper();
            switch (action)
            {
                case "P" when page > 1:
                    ExecuteMedicalSearch(searchParams, pageSize, page - 1);
                    break;
                case "N" when page < totalPages:
                    ExecuteMedicalSearch(searchParams, pageSize, page + 1);
                    break;
                case "S":
                    MedicalSearch();
                    break;
                case "M":
                    PrintMenu();
                    break;
                default:
                    ExecuteMedicalSearch(searchParams, pageSize, page);
                    break;
            }
        }

        private void ExecuteMedicalSearch(SearchParameters searchParams, int pageSize, int page)
        {
            Console.Clear();
            Console.WriteLine("\nПродолжение поиска:");
            ExecuteMedicalSearch(searchParams);
        }

        private void ShowNoResultsMenu()
        {
            Console.WriteLine("Ничего не найдено.");
            Console.WriteLine("\n1. Повторить поиск");
            Console.WriteLine("2. Вернуться в меню");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine().Trim();
            if (choice == "1") MedicalSearch();
            else PrintMenu();
        }

        private void WaitForMenu()
        {
            Console.WriteLine("\nНажмите Enter для возврата в меню...");
            Console.ReadLine();
            PrintMenu();
        }
        public void PrintMenu()
        {
            Console.WriteLine("\nМеню управления мед. учереждениями:");
            Console.WriteLine("1. Добавить новое мед. учереждение");
            Console.WriteLine("2. Просмотреть все мед. учереждения");
            Console.WriteLine("3. Найти мед. учереждение по номеру");
            Console.WriteLine("4. Обновить данные мед. учереждения");
            Console.WriteLine("5. Удалить мед. учереждение");
            Console.WriteLine("6. Удалить несколько мед. учереждений");
            Console.WriteLine("7. Поиск экземпляров сущности по значениям атрибутов");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
            switch (Console.ReadLine())
            {
                case "1":
                    MedicalCreate();
                    break;
                case "2":
                    MedicalRetrieveAll();
                    break;
                case "3":
                    MedicalRetrieve();
                    break;
                case "4":
                    MedicalUpdate();
                    break;
                case "5":
                    MedicalDelete();
                    break;
                case "6":
                    MedicalDeleteMany();
                    break;
                case "0":
                    return;
                case "7":
                    MedicalSearch();
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова."); PrintMenu();
                    break;
            }
        }
    }
}
