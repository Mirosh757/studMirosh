using System;
using System.Collections.Generic;
using System.Globalization;
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
            Console.Write("\nУкажите номер типа мед. учреждения\n1. Больница\n2. Отделение\nНомер типа учреждения: ");
            string numberOfType = Console.ReadLine().Trim();
            while (numberOfType != "1" && numberOfType != "2")
            {
                Console.Write("Указано не верное значение\nНомер типа учреждения: ");
                numberOfType = Console.ReadLine().Trim();
            }

            medical.type = numberOfType == "1" ? "hospital" : "department";
            var regions = _region.Find(r => true).SortBy(reg => reg.region_name).ToList();
            Console.WriteLine("\nСписок доступных городов:");
            for (int i = 0; i < regions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {regions[i].region_name}");
            }
            Console.Write("Введите номер города: ");
            string regionId = Console.ReadLine().Trim();
            while (true)
            {
                while (Regex.IsMatch(regionId, @"\D") || regionId == "" || regionId.Length > 10)
                {
                    Console.Write("Введен не верный номер\nВведите номер города: ");
                    regionId = Console.ReadLine().Trim();
                }
                if ((Int32.Parse(regionId) - 1) < regions.Count)
                {
                    medical.region_id = regions[Int32.Parse(regionId) - 1].Id;
                    if (medical.type == "department")
                    {
                        var hospitals = _medical.Find(h => h.type == "hospital" && h.region_id == medical.region_id).ToList();

                        if (hospitals.Count == 0)
                        {
                            Console.WriteLine("В данном городе нет больниц. Сначала создайте больницу.");
                            PrintMenu();
                            return;
                        }
                        break;
                    }
                    else
                        break;
                }
                else
                    Console.WriteLine("Город с введенным номером не существует в базе данных");
                regionId = "";
            }

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

            if (medical.type == "department")
            {

                _medical.InsertOne(medical);

                var insertedMedical = _medical.Find(dep => dep.address == medical.address && dep.title == medical.title).FirstOrDefault();

                var hospitals = _medical.Find(h => h.type == "hospital" && h.region_id == medical.region_id).ToList();

                Console.WriteLine("\nСписок доступных больниц:");
                for (int i = 0; i < hospitals.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {hospitals[i].title}");
                }

                Console.Write("Введите номер больницы, к которой привязать отделение: ");
                string hospitalId = Console.ReadLine().Trim();
                while (true)
                {
                    while (Regex.IsMatch(hospitalId, @"\D") || hospitalId == "" || hospitalId.Length > 10)
                    {
                        Console.Write("Введен не верный номер\nВведите номер больницы: ");
                        hospitalId = Console.ReadLine().Trim();
                    }
                    if ((Int32.Parse(hospitalId) - 1) < hospitals.Count)
                    {
                        var selectedHosp = hospitals[Int32.Parse(hospitalId) - 1];
                        
                        var update = Builders<Medical_instituction>.Update.Push(h => h.departments, insertedMedical.Id);
                        _medical.UpdateOne(h => h.Id == selectedHosp.Id, update);
                        break;
                    }
                    else
                        Console.WriteLine("Больница с введенным номером не существует в базе данных");
                    hospitalId = "";
                }
            }
            else // Если создаем больницу - заполняем реквизиты
            {
                medical.requisites = new Requisites();

                while (string.IsNullOrEmpty(medical.requisites.hospital_reduce_name))
                {
                    Console.Write("Сокращенное название больницы: ");
                    medical.requisites.hospital_reduce_name = Console.ReadLine().Trim();
                }
                while (string.IsNullOrEmpty(medical.requisites.registration_date))
                {
                    Console.Write("Дата регистрации(вводить в формате ГГГГ-ММ-ДД): ");
                    medical.requisites.registration_date = Console.ReadLine().Trim();
                }
                Console.WriteLine("Дальше требуется вставить ФИО глав врача\nПример написания ФИО: Иванов И. И.");
                while (string.IsNullOrEmpty(medical.requisites.name_legal_faces))
                {
                    Console.Write("ФИО: ");
                    medical.requisites.name_legal_faces = Console.ReadLine().Trim();
                }
                while (true)
                {
                    while (string.IsNullOrEmpty(medical.requisites.ogrn))
                    {
                        Console.Write("ОГРН: ");
                        medical.requisites.ogrn = Console.ReadLine().Trim();
                    }
                    if (IsUnique(medical.requisites))
                        break;
                    else
                    {
                        Console.WriteLine("Введенный ОГРН уже существует в базе");
                        medical.requisites.ogrn = "";
                    }
                }
                while (true)
                {
                    while (string.IsNullOrEmpty(medical.requisites.inn))
                    {
                        Console.Write("ИНН: ");
                        medical.requisites.inn = Console.ReadLine().Trim();
                    }
                    if (IsUnique(medical.requisites))
                        break;
                    else
                    {
                        Console.WriteLine("Введенный ИНН уже существует в базе");
                        medical.requisites.inn = "";
                    }
                }
                while (true)
                {
                    while (string.IsNullOrEmpty(medical.requisites.kpp))
                    {
                        Console.Write("КПП: ");
                        medical.requisites.kpp = Console.ReadLine().Trim();
                    }
                    if (IsUnique(medical.requisites))
                        break;
                    else
                    {
                        Console.WriteLine("Введенный КПП уже существует в базе");
                        medical.requisites.kpp = "";
                    }
                }
                _medical.InsertOne(medical);
            }
            Console.WriteLine("Мед. учреждение успешно добавлено в базу данных");
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
            while (Regex.IsMatch(id, @"\D") || id == "" || id.Length > 10)
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
                for (int i = 0; i < medical.departments.Length; i++)
                {
                    departmentTitle += $"{medicalAll.Find(dep => dep.Id == medical.departments[i]).title} ";
                }
                Console.WriteLine($"Номер отделения: {departmentTitle}");
                if (medical.type == "hospital")
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
                    if (dep != null) 
                        Console.WriteLine($"   - {dep.title}");
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
                    case "4": // здесь наверное нужно ограничение сделать так чтобы после смены города он привязывался к определеному городу в котором есть больницы
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
                while (Regex.IsMatch(id, @"\D") || id == "" || id.Length > 10)
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
                if (Regex.IsMatch(id, @"^\d+$") && id != "" && id.Length < 10)
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
        private void RegionCreate()
        {
            var region = new Region();
            string helpreg = "";
            int countreg = 0;
            Console.Write("\nДля добавления нового города введите данные\n");
            while (true)
            {
                while (string.IsNullOrEmpty(region.region_name) || helpreg == region.region_name)
                {
                    if(helpreg == region.region_name && countreg > 0)
                        Console.WriteLine("Название города должно быть уникальным");
                    Console.Write("Название: ");
                    region.region_name = Console.ReadLine().Trim();
                }
                var res = _region.Find(reg => reg.region_name == CultureInfo.CurrentCulture.TextInfo.ToTitleCase(region.region_name)).ToList();
                if (res.Count == 0)
                    break;
                else
                {
                    //Console.WriteLine("Название города должно быть уникальным");
                    countreg++;
                    helpreg = region.region_name;
                }
            }
            _region.InsertOne(region);
            Console.WriteLine("Город успешно вставлен");
            PrintMenu();
        }
        private void RegionRetriveAll()
        {
            var regionAll = _region.Find(regions => true).SortBy(f => f.Id).ToList();
            string regionTitle, id;
            Console.WriteLine("№   | Город                                              ");

            for (int i = 0; i < regionAll.Count; i++)
            {
                id = CorrFormat((i + 1).ToString(), 3) + " ";
                regionTitle = " " + CorrFormat(regionAll[i].region_name, 50);
                Console.WriteLine($"{id}|{regionTitle}");
            }
            PrintMenu();
        }
        private void RegionRetrive()
        {
            Console.Write("\nВведите номер города: ");
            string id = Console.ReadLine();
            while (Regex.IsMatch(id, @"\D") || id == "" || id.Length > 10)
            {
                Console.Write("Введен не верный номер\nВведите номер города: ");
                id = Console.ReadLine();
            }
            var regionAll = _region.Find(regions => true).SortBy(f => f.Id).ToList();
            if ((Int32.Parse(id) - 1) < regionAll.Count)
            {
                Region region = regionAll[Int32.Parse(id) - 1];
                Console.WriteLine($"Город: {region.region_name}");
            }
            else
                Console.WriteLine("Город с введенным номером не существует в базе данных");
            PrintMenu();
        }
        private void RegionUpdate()
        {
            try
            {
                var regionAll = _region.Find(r => true).SortBy(r => r.Id).ToList();

                // Вывод списка регионов
                Console.WriteLine("\nСписок городов:");
                for (int i = 0; i < regionAll.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {regionAll[i].region_name}");
                }

                // Выбор региона для редактирования
                Console.Write("\nВведите номер города для редактирования: ");
                string input = Console.ReadLine().Trim();

                while (!int.TryParse(input, out int index) || index < 1 || index > regionAll.Count)
                {
                    Console.Write("Неверный номер. Введите корректный номер города: ");
                    input = Console.ReadLine().Trim();
                }

                var regionToUpdate = regionAll[Int32.Parse(input) - 1];

                // Запрос нового названия
                Console.WriteLine($"\nТекущее название: {regionToUpdate.region_name}");
                Console.Write("Введите новое название (оставьте пустым, чтобы не изменять): ");
                string newName = Console.ReadLine().Trim();

                if (!string.IsNullOrEmpty(newName))
                {
                    // Проверка на уникальность имени
                    var nameExists = _region.Find(r =>
                        r.region_name == newName && r.Id != regionToUpdate.Id).Any();

                    if (nameExists)
                    {
                        Console.WriteLine("Город с таким названием уже существует!");
                        PrintMenu();
                        return;
                    }

                    // Обновление документа
                    var update = Builders<Region>.Update
                        .Set(r => r.region_name, newName);

                    var result = _region.UpdateOne(
                        r => r.Id == regionToUpdate.Id,
                        update);

                    if (result.ModifiedCount > 0)
                    {
                        Console.WriteLine("Название города успешно обновлено!");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось обновить название города.");
                    }
                }
                else
                {
                    Console.WriteLine("Название города не изменено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка при обновлении: {ex.Message}");
            }
            PrintMenu();
        }
        private void RegionDelete()
        {
            Console.Write("Введите номер города для удаления\nНомер: ");
            string id = Console.ReadLine();
            while (true)
            {
                while (Regex.IsMatch(id, @"\D") || id == "" || id.Length > 10)
                {
                    Console.Write("Введен не верный номер\nВведите номер города: ");
                    id = Console.ReadLine();
                }
                List<Region> regionAll = _region.Find(reg => true).SortBy(f => f.Id).ToList();
                if ((Int32.Parse(id) - 1) < regionAll.Count && Int32.Parse(id) > 0)
                {
                    Region regionDelete = regionAll[Int32.Parse(id) - 1];
                    List<Medical_instituction> medicalAll = _medical.Find(reg => reg.region_id == regionDelete.Id).ToList();
                    if (medicalAll.Count == 0)
                    {
                        var result = _region.DeleteOne(reg => reg.Id == regionDelete.Id);
                        if (result.DeletedCount > 0)
                            Console.WriteLine("Удаление прошло успешно");
                        else
                            Console.WriteLine("Удаление не прошло успешно");
                    }
                    else
                        Console.WriteLine("Нельзя удалить данный город, так как с ним связаны учереждения");
                    break;
                }
                else
                    Console.WriteLine("Город с введенным номером не существует в базе");
                id = "";
            }
            PrintMenu();
        }
        private void RegionDeleteMany()
        {
            Console.Write("Введите номера городов для удаления(разделителями считать пробелы)\nНомер: ");
            string[] idDelete = Console.ReadLine().Split(' ');
            long count = 0;
            List<Region> regionAll = _region.Find(reg => true).SortBy(f => f.Id).ToList();
            foreach (string id in idDelete)
            {
                if (Regex.IsMatch(id, @"^\d+$") && id != "" && id.Length < 10)
                {
                    if ((Int32.Parse(id) - 1) < regionAll.Count && Int32.Parse(id) > 0)
                    {
                        List<Medical_instituction> medicalAll = _medical.Find(reg => reg.region_id == regionAll[Int32.Parse(id) - 1].Id).ToList();
                        if (medicalAll.Count == 0)
                        {
                            var result = _region.DeleteOne(reg => reg.Id == regionAll[Int32.Parse(id) - 1].Id);
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
        public void PrintMenu()
        {
            Console.WriteLine("\nМеню управления мед. учереждениями:");
            Console.WriteLine("1. Добавить новое мед. учереждение");
            Console.WriteLine("2. Просмотреть все мед. учереждения");
            Console.WriteLine("3. Найти мед. учереждение по номеру");
            Console.WriteLine("4. Обновить данные мед. учереждения");
            Console.WriteLine("5. Удалить мед. учереждение");
            Console.WriteLine("6. Удалить несколько мед. учереждений");
            Console.WriteLine("7. Добавить новый город");
            Console.WriteLine("8. Просмотреть все города");
            Console.WriteLine("9. Найти город по номеру");
            Console.WriteLine("10. Обновить данные города");
            Console.WriteLine("11. Удалить город");
            Console.WriteLine("12. Удалить несколько городов");
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
                case "7":
                    RegionCreate();
                    break;
                case "8":
                    RegionRetriveAll();
                    break;
                case "9":
                    RegionRetrive();
                    break;
                case "10":
                    RegionUpdate();
                    break;
                case "11":
                    RegionDelete();
                    break;
                case "12":
                    RegionDeleteMany();
                    break;

                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова."); PrintMenu();
                    break;
            }
        }
    }
}
