using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2_SRS_DB
{
    internal class Facade
    {
        private void VehicleCreate()
        {
            var repository = new VehicleRepository();
            Console.WriteLine("Выберите вид транспорта:");
            Console.WriteLine("1. Машина");
            Console.WriteLine("2. Грузовик");
            Console.Write("3. Мотоцикл\nВид: ");
            string type = Console.ReadLine().Trim();
            while(type != "1" && type != "2" && type != "3")
            {
                Console.Write("Введен не верный вид транспорта\nВид: ");
                type = Console.ReadLine().Trim();
            }
            type = type == "1" ? "car" : type == "2" ? "truck" : "motorcycle";
            var createVehicle = new Vehicle();
            while(createVehicle.Brand == "")
            {
                Console.Write("Бренд автомобиля: ");
                createVehicle.Brand = Console.ReadLine().Trim();
            }
            while (createVehicle.Model == "")
            {
                Console.Write("Модель автомобиля: ");
                createVehicle.Model = Console.ReadLine().Trim();
            }
            while (createVehicle.Year == 0)
            {
                Console.Write("Год выпуска автомобиля: ");
                string helpYear = Console.ReadLine().Trim();
                if (Regex.IsMatch(helpYear, @"^\d+$") && helpYear.Length < 5)
                    createVehicle.Year = Int32.Parse(helpYear);
                else
                    Console.WriteLine("Введено не верное значение");
            }
            createVehicle.CreatedAt = DateTime.Now;
            switch(type)
            {
                case "car":
                    var createCar = new Car();
                    while(createCar.PassengerCapacity == 0)
                    {
                        Console.Write("Кол-во пассажирских мест: ");
                        string helpPassenger = Console.ReadLine().Trim();
                        if (helpPassenger == "" || Regex.IsMatch(helpPassenger, @"\D") || helpPassenger.Length > 5)
                            Console.WriteLine("Введено не верное значение");
                        else
                            createCar.PassengerCapacity = Int32.Parse(helpPassenger);
                    }
                    while (createCar.BodyType == "")
                    {
                        Console.WriteLine("1. Sedan");
                        Console.WriteLine("2. Hatchback");
                        Console.WriteLine("3. SUV");
                        Console.WriteLine("4. Coupe");
                        Console.WriteLine("5. StationWagon");
                        Console.WriteLine("6. Convertible");
                        Console.WriteLine("7. Minivan");
                        Console.WriteLine("8. Pickup");
                        while (true)
                        {
                            Console.Write("Укажите номер типа кузова: ");
                            string number = Console.ReadLine().Trim();
                            while (number == "" || Regex.IsMatch(number, @"\D") || number.Length > 5)
                            {
                                Console.Write("Введено не верное значение\nНомер: ");
                                number = Console.ReadLine().Trim();
                            }
                            if (Int32.Parse(number) < 1 || Int32.Parse(number) > 8)
                            {
                                Console.WriteLine("Такого номера не существет");
                                number = " ";
                            }
                            else
                            {
                                number = number == "1" ? "Sedan" :
                                    number == "2" ? "Hatchback" :
                                    number == "3" ? "SUV" :
                                    number == "4" ? "Coupe" :
                                    number == "5" ? "StationWagon" :
                                    number == "6" ? "Convertible" :
                                    number == "7" ? "Minivan" : "Pickup";
                                createCar.BodyType = number;
                                break;
                            }
                        }
                    }
                    createCar.Brand = createVehicle.Brand;
                    createCar.Model = createVehicle.Model;
                    createCar.Year = createVehicle.Year;
                    createCar.CreatedAt = createVehicle.CreatedAt;
                    repository.AddCar(createCar);
                    break;
                case "truck":
                    var createTruck = new Truck();
                    while (createTruck.LoadCapacity == 0)
                    {
                        Console.Write("Грузоподъемность: ");
                        string helpLoad = Console.ReadLine().Trim();
                        if (helpLoad == "" || Regex.IsMatch(helpLoad, @"\D") || helpLoad.Length > 5)
                            Console.WriteLine("Введено не верное значение");
                        else
                            createTruck.LoadCapacity = Decimal.Parse(helpLoad);
                    }
                    while (createTruck.AxleCount == 0)
                    {
                        Console.Write("Кол-во осей(пар колес): ");
                        string helpAxle = Console.ReadLine().Trim();
                        if (helpAxle == "" || Regex.IsMatch(helpAxle, @"\D") || helpAxle.Length > 5)
                            Console.WriteLine("Введено не верное значение");
                        else
                            createTruck.AxleCount = Int32.Parse(helpAxle);
                    }

                    createTruck.Brand = createVehicle.Brand;
                    createTruck.Model = createVehicle.Model;
                    createTruck.Year = createVehicle.Year;
                    createTruck.CreatedAt = createVehicle.CreatedAt;
                    repository.AddTruck(createTruck);
                    break;
                case "motorcycle":
                    var createMotorcycle = new Motorcycle();
                    while (createMotorcycle.EngineVolume == 0)
                    {
                        Console.Write("Рабочий объем двигателя: ");
                        string helpEngine = Console.ReadLine().Trim();
                        if (helpEngine == "" || Regex.IsMatch(helpEngine, @"\D") || helpEngine.Length > 5)
                            Console.WriteLine("Введено не верное значение");
                        else
                            createMotorcycle.EngineVolume = Decimal.Parse(helpEngine);
                    }
                    Console.Write("1. Гоночный\n2. Спортивный\nУкажите номер типа мотоцикла: ");
                    string helpRacing = Console.ReadLine().Trim();
                    while(helpRacing != "1" && helpRacing != "2")
                    {
                        Console.Write("Введено не верное значение\nНомер: ");
                        helpRacing = Console.ReadLine().Trim();
                    }
                    createMotorcycle.IsRacing = helpRacing == "1" ? true : false;

                    createMotorcycle.Brand = createVehicle.Brand;
                    createMotorcycle.Model = createVehicle.Model;
                    createMotorcycle.Year = createVehicle.Year;
                    createMotorcycle.CreatedAt = createVehicle.CreatedAt;
                    repository.AddMotorcycle(createMotorcycle);
                    break;
            }
            Console.WriteLine("Транспорт успешно вставлен");
            PrintMenu();
        }
        private void VehicleRetrieveAll()
        {
            var repository = new VehicleRepository();
            Console.WriteLine("Выберите вид транспорта:");
            Console.WriteLine("1. Машина");
            Console.WriteLine("2. Грузовик");
            Console.WriteLine("3. Мотоцикл");
            Console.WriteLine("4. Все транспорты");
            Console.Write("Номер: ");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    var cars = repository.GetAllCars();
                    Console.WriteLine("№   | Бренд                                         | Модель                                        | Год выпуска | Кол-во мест | Тип кузова           ");
                    foreach (var car in cars)
                    {
                        Console.WriteLine($"{CorrFormat(car.Id.ToString(), 3)} | {CorrFormat(car.Brand, 45)} | {CorrFormat(car.Model, 45)} | {CorrFormat(car.Year.ToString(), 11)} | {CorrFormat(car.PassengerCapacity.ToString(), 11)} | {CorrFormat(car.BodyType, 20)}");
                    }
                    break;
                case "2":
                    var trucks = repository.GetAllTrucks();
                    Console.WriteLine("№   | Бренд                                         | Модель                                        | Год выпуска | Грузоподъемность | Кол-во осей     ");
                    foreach (var truck in trucks)
                    {
                        Console.WriteLine($"{CorrFormat(truck.Id.ToString(), 3)} | {CorrFormat(truck.Brand, 45)} | {CorrFormat(truck.Model, 45)} | {CorrFormat(truck.Year.ToString(), 11)} | {CorrFormat(truck.LoadCapacity.ToString(), 16)} | {CorrFormat(truck.AxleCount.ToString(), 15)}");
                    }
                    break; 
                case "3":
                    var motorcycles = repository.GetAllMotorcycles();
                    Console.WriteLine("№   | Бренд                                         | Модель                                        | Год выпуска | Объем двигателя | гоночный/спортивный");
                    foreach (var motorcycle in motorcycles)
                    {
                        Console.WriteLine($"{CorrFormat(motorcycle.Id.ToString(), 3)} | {CorrFormat(motorcycle.Brand, 45)} | {CorrFormat(motorcycle.Model, 45)} | {CorrFormat(motorcycle.Year.ToString(), 11)} | {CorrFormat(motorcycle.EngineVolume.ToString(), 15)} | {CorrFormat(motorcycle.IsRacing == true ? "гоночный" : "спортивный", 16)}");
                    }
                    break;
                case "4": // наверное надо сделать сортировку по Id
                    var vehicles = repository.GetAllVehicles().ToList();
                    Console.WriteLine("№   | Бренд                                         | Модель                                        | Год выпуска");
                    foreach (var vehicle in vehicles)
                    {
                        Console.WriteLine($"{CorrFormat(vehicle.Id.ToString(), 3)} | {CorrFormat(vehicle.Brand, 45)} | {CorrFormat(vehicle.Model, 45)} | {CorrFormat(vehicle.Year.ToString(), 11)}");
                    }
                    break;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова."); PrintMenu();
                    break;
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
        private void VehicleRetrieve()
        {
            var repository = new VehicleRepository();
            Console.Write("Укажите id транспорта\nId: ");
            string id = Console.ReadLine().Trim();
            while(id == "" || Regex.IsMatch(id, @"\D") || id.Length > 5)
            {
                Console.Write("Указан не верный Id\nId: ");
                id = Console.ReadLine().Trim();
            }
            var vehicles = repository.GetAllVehicles().ToList();
            var vehicle = vehicles.Find(veh => veh.Id == Int32.Parse(id));
            if (vehicle == null)
            {
                Console.WriteLine("Транспорта с текущим id не существует");
            }
            else
            {
                Console.Write($"\nБренд: {vehicle.Brand}\nМодель: {vehicle.Model}\nГод выпуска: {vehicle.Year}\n");
                if (vehicle is Car car)
                    Console.WriteLine($"Кол-во пассажиров: {car.PassengerCapacity}\nТип кузова: {car.BodyType}");
                else if (vehicle is Truck truck)
                    Console.WriteLine($"Грузоподъемность: {truck.LoadCapacity}\nКол-во осей: {truck.AxleCount}");
                else if (vehicle is Motorcycle motorcycle)
                    Console.WriteLine($"Объем двигателя: {motorcycle.EngineVolume}\nТип мотоцикла: {motorcycle.IsRacing}");
            }
            PrintMenu();
        }
        private void VehicleUpdate()
        {
            var repository = new VehicleRepository();
            Console.Write("Укажите id транспорта для обновления\nId: ");
            string id = Console.ReadLine().Trim();

            // Проверка корректности ID
            while (id == "" || Regex.IsMatch(id, @"\D") || id.Length > 5)
            {
                Console.Write("Указан не верный Id\nId: ");
                id = Console.ReadLine().Trim();
            }

            int vehicleId = Int32.Parse(id);
            var vehicles = repository.GetAllVehicles().ToList();
            var vehicle = vehicles.Find(veh => veh.Id == vehicleId);

            if (vehicle == null)
            {
                Console.WriteLine("Транспорта с текущим id не существует");
            }
            else
            {
                Console.WriteLine($"Редактирование: {vehicle.Brand} {vehicle.Model} {vehicle.Year}");
                Console.WriteLine("Оставьте поле пустым, если не нужно изменять значение");

                // Обновление общих полей
                Console.Write($"Бренд [{vehicle.Brand}]: ");
                string brandInput = Console.ReadLine().Trim();
                if (!string.IsNullOrWhiteSpace(brandInput))
                {
                    vehicle.Brand = brandInput;
                    while(vehicle.Brand.ToLower() != brandInput.ToLower())
                    {
                        Console.Write($"Бренд [{vehicle.Brand}]: ");
                        brandInput = Console.ReadLine().Trim();
                        vehicle.Brand = brandInput;
                    }
                }

                Console.Write($"Модель [{vehicle.Model}]: ");
                string modelInput = Console.ReadLine().Trim();
                if (!string.IsNullOrWhiteSpace(modelInput))
                {
                    vehicle.Model = modelInput;
                    while (vehicle.Model.ToLower() != modelInput.ToLower())
                    {
                        Console.Write($"Модель [{vehicle.Model}]: ");
                        modelInput = Console.ReadLine().Trim();
                        vehicle.Model = modelInput;
                    }
                }

                Console.Write($"Год выпуска [{vehicle.Year}]: ");
                var yearInput = Console.ReadLine().Trim();
                if (!string.IsNullOrWhiteSpace(yearInput))
                {
                    while (true)
                    {
                        while (yearInput == "" || Regex.IsMatch(yearInput, @"\D") || yearInput.Length > 5)
                        {
                            Console.Write("Введено неверное значение года\nГод выпуска: ");
                            yearInput = Console.ReadLine().Trim();
                        }
                        vehicle.Year = Int32.Parse(yearInput);
                        if (vehicle.Year == Int32.Parse(yearInput))
                            break;
                    }
                }

                // Обновление специфичных полей в зависимости от типа
                if (vehicle is Car car)
                {
                    Console.Write($"Кол-во пассажиров [{car.PassengerCapacity}]: ");
                    var capacityInput = Console.ReadLine().Trim();
                    if (!string.IsNullOrWhiteSpace(capacityInput))
                    {
                        while (true)
                        {
                            while (capacityInput == "" || Regex.IsMatch(capacityInput, @"\D") || capacityInput.Length > 5)
                            {
                                Console.Write("Введено неверное значение\nКол-во пассажиров: ");
                                capacityInput = Console.ReadLine().Trim();
                            }
                            car.PassengerCapacity = Int32.Parse(capacityInput);
                            if(car.PassengerCapacity == Int32.Parse(capacityInput))
                                break;
                        }
                    }
                    Console.WriteLine("1. Sedan");
                    Console.WriteLine("2. Hatchback");
                    Console.WriteLine("3. SUV");
                    Console.WriteLine("4. Coupe");
                    Console.WriteLine("5. StationWagon");
                    Console.WriteLine("6. Convertible");
                    Console.WriteLine("7. Minivan");
                    Console.WriteLine("8. Pickup");
                    Console.Write($"Текущий тип кузова [{car.BodyType}]: ");
                    Console.Write("Укажите номер типа кузова: ");
                    string number = Console.ReadLine().Trim();
                    if (!string.IsNullOrWhiteSpace(number))
                    {
                        while (true)
                        {
                            while (number == "" || Regex.IsMatch(number, @"\D") || number.Length > 5)
                            {
                                Console.Write("Введено не верное значение\nНомер: ");
                                number = Console.ReadLine().Trim();
                            }
                            if (Int32.Parse(number) < 1 || Int32.Parse(number) > 8)
                            {
                                Console.WriteLine("Такого номера не существет");
                                number = " ";
                            }
                            else
                            {
                                number = number == "1" ? "Sedan" :
                                    number == "2" ? "Hatchback" :
                                    number == "3" ? "SUV" :
                                    number == "4" ? "Coupe" :
                                    number == "5" ? "StationWagon" :
                                    number == "6" ? "Convertible" :
                                    number == "7" ? "Minivan" : "Pickup";
                                car.BodyType = number;
                                break;
                            }
                        }
                    }

                    repository.UpdateCar(car);
                }
                else if (vehicle is Truck truck)
                {
                    Console.Write($"Грузоподъемность [{truck.LoadCapacity}]: ");
                    var loadInput = Console.ReadLine().Trim();
                    if (!string.IsNullOrWhiteSpace(loadInput))
                    {
                        while (true)
                        {
                            while (loadInput == "" || Regex.IsMatch(loadInput, @"\D") || loadInput.Length > 5)
                            {
                                Console.Write("Введено неверное значение\nГрузоподъемность: ");
                                loadInput = Console.ReadLine().Trim();
                            }
                            truck.LoadCapacity = Decimal.Parse(loadInput);
                            if(truck.LoadCapacity == Decimal.Parse(loadInput))
                                break;
                        }
                    }

                    Console.Write($"Кол-во осей [{truck.AxleCount}]: ");
                    var axleInput = Console.ReadLine().Trim();
                    if (!string.IsNullOrWhiteSpace(axleInput))
                    {
                        while (true)
                        {
                            while (axleInput == "" || Regex.IsMatch(axleInput, @"\D") || axleInput.Length > 5)
                            {
                                Console.Write("Введено неверное значение\nКол-во осей: ");
                                axleInput = Console.ReadLine().Trim();
                            }
                            truck.AxleCount = Int32.Parse(axleInput);
                            if(truck.AxleCount == Int32.Parse(axleInput))
                                break;
                        }
                    }

                    repository.UpdateTruck(truck);
                }
                else if (vehicle is Motorcycle motorcycle)
                {
                    Console.Write($"Объем двигателя [{motorcycle.EngineVolume}]: ");
                    var engineInput = Console.ReadLine().Trim();
                    if (!string.IsNullOrWhiteSpace(engineInput))
                    {
                        while (true)
                        {
                            while (engineInput == "" || Regex.IsMatch(engineInput, @"\D") || engineInput.Length > 5)
                            {
                                Console.Write("Введено неверное значение\nОбъем двигателя: ");
                                engineInput = Console.ReadLine().Trim();
                            }
                            motorcycle.EngineVolume = Decimal.Parse(engineInput);
                            if(motorcycle.EngineVolume == Decimal.Parse(engineInput))
                                break;
                        }
                    }
                    Console.WriteLine("1. Гоночный\n2. Спортивный");
                    Console.Write($"Тип мотоцикла [{motorcycle.IsRacing}]: ");
                    string helpRacing = Console.ReadLine().Trim();
                    if(!string.IsNullOrWhiteSpace(helpRacing))
                    {
                        while (helpRacing != "1" && helpRacing != "2")
                        {
                            Console.Write("Введено не верное значение\nНомер: ");
                            helpRacing = Console.ReadLine().Trim();
                        }
                        motorcycle.IsRacing = helpRacing == "1" ? true : false;
                    }

                    repository.UpdateMotorcycle(motorcycle);
                }

                Console.WriteLine("Данные успешно обновлены");
            }
            PrintMenu();
        }
        private void VehicleDelete()
        {
            var repository = new VehicleRepository();
            Console.Write("Укажите id транспорта для удаления\nId: ");
            string id = Console.ReadLine().Trim();
            while (id == "" || Regex.IsMatch(id, @"\D") || id.Length > 5)
            {
                Console.Write("Указан не верный Id\nId: ");
                id = Console.ReadLine().Trim();
            }
            var vehicles = repository.GetAllVehicles().ToList();
            var vehicle = vehicles.Find(veh => veh.Id == Int32.Parse(id));
            if (vehicle == null)
            {
                Console.WriteLine("Транспорта с текущим id не существует");
            }
            else
            {
                if (vehicle is Car car)
                    repository.DeleteCar(vehicle.Id);
                else if (vehicle is Truck truck)
                    repository.DeleteTruck(vehicle.Id);
                else if (vehicle is Motorcycle motorcycle)
                    repository.DeleteMotorcycle(vehicle.Id);
                Console.WriteLine("Удаление прошло успешно");
            }
            PrintMenu();
        }
        private void VehicleDeleteMany()
        {
            var repository = new VehicleRepository();
            Console.Write("Укажите id транспортов для удаления(разделителями считать пробелы)\nId: ");
            string[] ids = Console.ReadLine().Trim().Split(' ');
            int count = 0;
            foreach (string id in ids)
            {
                if (id != "" && Regex.IsMatch(id, @"^\d+$") && id.Length < 5)
                {
                    var vehicles = repository.GetAllVehicles().ToList();
                    var vehicle = vehicles.Find(veh => veh.Id == Int32.Parse(id));
                    if (vehicle != null)
                    {
                        count++;
                        if (vehicle is Car car)
                            repository.DeleteCar(vehicle.Id);
                        else if (vehicle is Truck truck)
                            repository.DeleteTruck(vehicle.Id);
                        else if (vehicle is Motorcycle motorcycle)
                            repository.DeleteMotorcycle(vehicle.Id);
                    }
                }
            }
            Console.WriteLine($"Удалено {count}");
            PrintMenu();
        }
        public void PrintMenu()
        {
            Console.WriteLine("\nМеню управления транспортами:");
            Console.WriteLine("1. Добавить новый транспорт");
            Console.WriteLine("2. Просмотреть все все транспорты");
            Console.WriteLine("3. Найти транспорт по номеру");
            Console.WriteLine("4. Обновить данные транспорта");
            Console.WriteLine("5. Удалить транспорт");
            Console.WriteLine("6. Удалить несколько транспортов");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите действие: ");
            switch (Console.ReadLine())
            {
                case "1":
                    VehicleCreate();
                    break;
                case "2":
                    VehicleRetrieveAll();
                    break;
                case "3":
                    VehicleRetrieve();
                    break;
                case "4":
                    VehicleUpdate();
                    break;
                case "5":
                    VehicleDelete();
                    break;
                case "6":
                    VehicleDeleteMany();
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
