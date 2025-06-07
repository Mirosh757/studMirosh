using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _2_Lab_MongoDb
{
    internal class Facade
    {
        private readonly IMongoCollection<Doctor> _doctors;
        public Facade()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("doctor");
            _doctors = database.GetCollection<Doctor>("doctors");
        }
        public void DoctorCreate()
        {
            Doctor doctor = new Doctor();
            Console.Write("Для добавления нового доктора введите данные\n");
            while(doctor.name == "")
            {
                Console.Write("Name: ");
                doctor.name = Console.ReadLine();
            }
            while (doctor.family == "")
            {
                Console.Write("Family: ");
                doctor.family = Console.ReadLine();
            }
            Console.WriteLine("Если у доктора нету отчества, то укажите прочерк");
            while (doctor.patronymic == "")
            {
                Console.Write("Patronymic: ");
                doctor.patronymic = Console.ReadLine().Trim();
            }
            bool passportUnique = true;
            string passporthelp = "";
            while (passportUnique)
            {
                while (passporthelp == "")
                {
                    Console.Write("Passport details: ");
                    passporthelp = Console.ReadLine().Trim();
                    if (!Regex.IsMatch(passporthelp, @"^\d{10}$"))
                    {
                        Console.WriteLine("Паспортные данные должны состоять из 10 цифр");
                        passporthelp = "";
                    }
                }
                passportUnique = _doctors.Find(d => d.passport_details == passporthelp).Any();
                if (passportUnique)
                {
                    Console.WriteLine("Пасспортные данные должны быть уникальными");
                    passporthelp = "";
                }
                else
                {
                    doctor.passport_details = passporthelp;
                    passportUnique = false;
                }
            }
            while (doctor.address == "")
            {
                Console.Write("Address: ");
                doctor.address = Console.ReadLine();
            }
            string[] date_birth;
            while (doctor.date_birth == "")
            {
                Console.Write("Date birth(Вводить в формате ГГГГ-ММ-ДД): ");
                date_birth = Console.ReadLine().Trim().Split('-');
                if (date_birth.Length != 3)
                    Console.Write("Введена некорректная дата рождения\n");
                else
                {
                    try
                    {
                        doctor.date_birth = $"{date_birth[0].Trim()}-{date_birth[1].Trim()}-{date_birth[2].Trim()}";
                    }
                    catch
                    {
                        Console.WriteLine("Введена не корректная дата рождения");
                    }
                }
            }
            _doctors.InsertOne(doctor);
            Console.WriteLine("Доктор успешно добавлен в базу данных");

            PrintMenu();
        }
        public void DoctorRetrieveAll()
        {
            List<Doctor> doctorsAll = _doctors.Find(doctor => true).SortBy(f => f.family).ToList();
            string id,name, family, patronymic, birth_date, passport_details, address;
            Console.WriteLine("№   | Family               | Name                 | Patronymic           | Birth_Date | Address                                            | Passport   ");
            
            for (int i = 0; i < doctorsAll.Count; i++)
            {
                id = CorrFormat((i+1).ToString(), 3) + " ";
                name = " " + CorrFormat(doctorsAll[i].name, 20) + " ";
                family = " " + CorrFormat(doctorsAll[i].family, 20) + " ";
                patronymic = " " + CorrFormat(doctorsAll[i].patronymic, 20) + " ";
                birth_date = " " + CorrFormat(doctorsAll[i].date_birth, 10) + " ";
                passport_details = " " + CorrFormat(doctorsAll[i].passport_details, 10) + " ";
                address = " " + CorrFormat(doctorsAll[i].address, 50) + " ";
                Console.WriteLine($"{id}|{family}|{name}|{patronymic}|{birth_date}|{address}|{passport_details}");
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
        public void DoctorRetrieve()
        {
            Console.Write("\nВведите ID врача: ");
            string id = Console.ReadLine();
            while(Regex.IsMatch(id, @"\D") || id == "")
            {
                Console.Write("Введен не верный id\nВведите ID врача: ");
                id = Console.ReadLine();
            }
            List<Doctor> doctorsAll = _doctors.Find(doctor => true).SortBy(f => f.family).ToList();
            if ((Int32.Parse(id) - 1) < doctorsAll.Count)
            {
                Doctor doctor = doctorsAll[Int32.Parse(id) - 1];
                Console.WriteLine($"Family: {doctor.family}");
                Console.WriteLine($"Name: {doctor.name}");
                Console.WriteLine($"Patronymic: {doctor.patronymic}");
                Console.WriteLine($"Date birth: {doctor.date_birth}");
                Console.WriteLine($"Address: {doctor.address}");
                Console.WriteLine($"Passport details: {doctor.passport_details}");
            }
            else
                Console.WriteLine("Доктора с введенным ID не существует в базе данных");
            PrintMenu();
        }
        public async void DoctorUpdate()
        {
            Console.Write("\nВведите ID врача: ");
            string id = Console.ReadLine();
            while (Regex.IsMatch(id, @"\D") || id == "")
            {
                Console.Write("Введен не верный ID\nВведите ID врача: ");
                id = Console.ReadLine();
            }
            List<Doctor> doctorsAll = _doctors.Find(doctor => true).SortBy(f => f.family).ToList();
            if ((Int32.Parse(id) - 1) < doctorsAll.Count)
            {
                Doctor doctor = doctorsAll[Int32.Parse(id) - 1];
                string name = "", family = "", patronymic = "", address = "";
                Doctor doctorUpdate = new Doctor();
                Console.Write("Введите новые данные (оставьте пустым, чтобы не изменять)\n");
                while (doctorUpdate.name == "")
                {
                    Console.Write("Name: ");
                    name = Console.ReadLine();
                    if(name == "")
                        doctorUpdate.name = doctor.name;
                    else
                        doctorUpdate.name = name;
                }
                while (doctorUpdate.family == "")
                {
                    Console.Write("Family: ");
                    family = Console.ReadLine();
                    if (family == "")
                        doctorUpdate.family = doctor.family;
                    else
                        doctorUpdate.family = family;
                }
                Console.WriteLine("Если у доктора нету отчества, то укажите прочерк");
                while (doctorUpdate.patronymic == "")
                {
                    Console.Write("Patronymic: ");
                    patronymic = Console.ReadLine().Trim();
                    if (patronymic == "")
                        doctorUpdate.patronymic = doctor.patronymic;
                    else
                        doctorUpdate.patronymic = patronymic;
                }
                bool passportUnique = true;
                string passporthelp = "";
                while (passportUnique)
                {
                    while (passporthelp == "")
                    {
                        Console.Write("Passport details: ");
                        passporthelp = Console.ReadLine();
                        if(passporthelp == "")
                        {
                            doctorUpdate.passport_details = doctor.passport_details;
                            passportUnique = false;
                            break;
                        }
                        else if (!Regex.IsMatch(passporthelp, @"^\d{10}$"))
                        {
                            Console.WriteLine("Паспортные данные должны состоять из 10 цифр");
                            passporthelp = "";
                        }
                    }
                    Doctor doctorPassport = _doctors.Find(d => d.passport_details == passporthelp).First();
                    passportUnique = _doctors.Find(d => d.passport_details == passporthelp).Any();
                    if (passportUnique && doctorPassport.Id != doctor.Id)
                    {
                        Console.WriteLine("Пасспортные данные должны быть уникальными");
                        passporthelp = "";
                    }
                    else
                    {
                        doctorUpdate.passport_details = passporthelp;
                        passportUnique = false;
                    }
                }
                while (doctorUpdate.address == "")
                {
                    Console.Write("Address: ");
                    address = Console.ReadLine();
                    if (address == "")
                        doctorUpdate.address = doctor.address;
                    else
                        doctorUpdate.address = address;
                }
                string[] date_birth;
                while (doctorUpdate.date_birth == "")
                {
                    Console.Write("Date birth(Вводить в формате ГГГГ-ММ-ДД): ");
                    date_birth = Console.ReadLine().Split('-');
                    if (date_birth[0] == "")
                    {
                        doctorUpdate.date_birth = doctor.date_birth;
                        break;
                    }
                    if (date_birth.Length != 3)
                        Console.Write("Введена некорректная дата рождения\n");
                    else
                    {
                        try
                        {
                            doctorUpdate.date_birth = $"{date_birth[0].Trim()}-{date_birth[1].Trim()}-{date_birth[2].Trim()}";
                        }
                        catch 
                        {
                            Console.WriteLine("Введена не корректная дата рождения");
                        }
                    }
                }
                var filter = new BsonDocument("_id", doctor.Id);
                var updateSettings = new BsonDocument("$set", new BsonDocument { 
                    { "name", doctorUpdate.name }, 
                    { "family", doctorUpdate.family }, 
                    { "patronymic", doctorUpdate.patronymic }, 
                    { "passport_details", doctorUpdate.passport_details },
                    { "address", doctorUpdate.address },
                    { "date_birth", doctorUpdate.date_birth }
                });
                var result = _doctors.UpdateOne(filter, updateSettings);
                if (result.MatchedCount != 0)
                    Console.WriteLine("Изменение прошло успешно");
                else
                    Console.WriteLine("Ничего не изменилось");
            }
            else
                Console.WriteLine("Доктора с введенным ID не существует в базе данных");
            PrintMenu();
        }
        public void DoctorDelete()
        {
            Console.Write("Введите ID врача для удаления\nId: ");
            string id = Console.ReadLine();
            while (true)
            {
                while (Regex.IsMatch(id, @"\D") || id == "")
                {
                    Console.Write("Введен не верный ID\nВведите ID врача: ");
                    id = Console.ReadLine();
                }
                List<Doctor> doctorsAll = _doctors.Find(doctor => true).SortBy(f => f.family).ToList();
                if ((Int32.Parse(id) - 1) < doctorsAll.Count)
                {
                    Doctor doctorDelete = doctorsAll[Int32.Parse(id) - 1];
                    var result = _doctors.DeleteOne(doctor => doctor.Id == doctorDelete.Id);
                    if (result.DeletedCount > 0)
                        Console.WriteLine("Удаление прошло успешно");
                    else
                        Console.WriteLine("Удаление не прошло успешно");
                    break;
                }
                else
                    Console.WriteLine("Доктора с введенным Id не существует в базе");
                id = "";
            }
            PrintMenu();
        }
        public void DoctorDeleteMany()
        {
            Console.Write("Введите ID врачей для удаления(разделителями считать пробелы)\nId: ");
            string[] idDelete = Console.ReadLine().Split(' ');
            long count = 0;
            List<Doctor> doctorsAll = _doctors.Find(doctor => true).SortBy(f => f.family).ToList();
            foreach (string id in idDelete)
            {
                if(Regex.IsMatch(id, @"^\d+$") && id != "")
                {
                    if ((Int32.Parse(id) - 1) < doctorsAll.Count)
                    {
                        var result = _doctors.DeleteOne(doctor => doctor.Id == doctorsAll[Int32.Parse(id) - 1].Id);
                        count += result.DeletedCount;
                    }
                }
            }
            if(count > 4)
                Console.WriteLine($"Удалены {count} докторов");
            else if(count == 1)
                Console.WriteLine($"Удален {count} доктор");
            else if (count <= 4)
                Console.WriteLine($"Удалено {count} доктора");
            else
                Console.WriteLine("Ничего не работает");

            PrintMenu();
        }
        public void PrintMenu()
        {
            Console.WriteLine("\nМеню управления врачами:");
            Console.WriteLine("1. Добавить нового врача");
            Console.WriteLine("2. Просмотреть всех врачей");
            Console.WriteLine("3. Найти врача по ID");
            Console.WriteLine("4. Обновить данные врача");
            Console.WriteLine("5. Удалить врача");
            Console.WriteLine("6. Удалить нескольких врачей");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
            switch (Console.ReadLine())
            {
                case "1":
                    DoctorCreate();
                    break;
                case "2":
                    DoctorRetrieveAll();
                    break;
                case "3":
                    DoctorRetrieve();
                    break;
                case "4":
                    DoctorUpdate();
                    break;
                case "5":
                    DoctorDelete();
                    break;
                case "6":
                    DoctorDeleteMany();
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
