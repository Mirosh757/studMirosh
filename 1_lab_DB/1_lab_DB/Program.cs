using _1_lab_DB;
Facade facade = new Facade();
facade.PrintMenu();
/*
//DoctorMapper doctorMapper = new DoctorMapper();

List<Doctor> _doctors = doctorMapper.FindAll();
for(int i = 0;i < _doctors.Count;i++)
{
    Console.WriteLine($"{_doctors[i].Id}; {_doctors[i]._created_at}; {_doctors[i]._updated_at}; {_doctors[i]._name}; {_doctors[i]._address}; {_doctors[i]._passport_details}; {_doctors[i]._date_birth}");
}
//doctorMapper.Delete(14);
//Doctor doctor = new Doctor(DateTime.Now.ToString(), DateTime.Now.ToString(), "Машанов Виктор Генадьевич", "ул. Каландаришвили; 27; Улан-Удэ; Респ. Бурятия; 670000", "1126432543", "21.12.1921 0:00:00");
//doctorMapper.Insert(doctor);
//doctorMapper.Delete(6);
Doctor doctor = (Doctor)doctorMapper.Find(15);
doctor.PassportDetails = "1211221212";
//Console.WriteLine( doctorMapper.Update(doctor));
//doctorMapper.Delete(16);
doctor.PassportDetails = "1212121211";
doctorMapper.Update(doctor);
*/
/*
ЛБ №1. CRUD
Реализовать CRUD операции для одной из сущностей из ЛБ №0. Разработанное приложение может быть консольным или веб. Интерфейс не обязателен, должен присутствовать пользовательский ввод.
Метод Model_nameCreate() принимает список значений атрибутов модели и записывает данные в базу.
Метод Model_nameRetrieveAll() отдает все экземпляры модели.
Метод Model_nameRetrieve() принимает значение первичного ключа модели и отдает конкретный экземпляр модели.
Метод Model_nameUpdate() принимает первичный ключ модели и список значений атрибутов модели.
Метод Model_nameDelete() принимает значение первичного ключа модели и удаляет ее из базы.
Метод Model_nameDeleteMany() принимает список значений первичного ключа модели и удаляет их базы.
Дополнительная задача 1.
- код закоммичен в репозиторий git из ЛБ №0

ЛБ №2. Поиск и пагинация
Реализовать функцию поиска экземпляров сущности по указанным пользователем значениям атрибутов.
Метод Model_nameSearch() принимает список значений атрибутов поиска модели и параметры количества выдаваемых результатов (по умолчанию = 5) и смещения (по умолчанию = 0).
Дополнительная задача 1.
- код закоммичен в репозиторий git из ЛБ №0
*/