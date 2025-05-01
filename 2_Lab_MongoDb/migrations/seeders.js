require('dotenv').config();
const mongoose = require('mongoose');
const Doctor = require('../models/doctor');
const Doctor_speciality = require('../models/doctor_speciality');
const General_page = require('../models/general_page');
const Hospital = require('../models/hospital');
const Phone_number = require('../models/phone_number');
const Region = require('../models/region');
const Requisites = require('../models/requisites');
const Speciality = require('../models/speciality');

async function seed() {
    await mongoose.connect(process.env.MONGODB_URL)
    
    await Doctor.deleteMany();
    await Doctor_speciality.deleteMany();
    await General_page.deleteMany();
    await Hospital.deleteMany();
    await Phone_number.deleteMany();
    await Region.deleteMany();
    await Requisites.deleteMany();
    await Speciality.deleteMany();

    const region = await Region.insertMany([
        {region_name: "Улан-Удэ"},
        {region_name: "Москва"},
        {region_name: "Санкт-Петербург"},
        {region_name: "Екатеринбург"},
        {region_name: "Калининград"},
        {region_name: "Курск"},
        {region_name: "Казань"},
        {region_name: "Омск"},
        {region_name: "Томск"},
        {region_name: "Новосибирск"},
    ]);

    const doctor = await Doctor.insertMany([
        {
            name: "Георгий",
            family:"Кондратов",
            patronymic:"Васильевич",
            address: "ул. Революции 1905 года; 36; Улан-Удэ; Респ. Бурятия; 670034", 
            passport_details: "8123432544", 
            date_birth: "1954-10-27"
        },
        {
            name: "Иван", 
            family:"Овчинников",
            patronymic:"Афанасьевич",
            address: "ул. Комсомольская; 1Б; Улан-Удэ; Респ. Бурятия; 670002", 
            passport_details: "8133431234", 
            date_birth: "1922-09-14"
        },
        {
            name: "Эрдэн", 
            family:"Раднаев",
            patronymic:"Раднаевич",
            address: "ул. Октябрьская; 36а; Улан-Удэ; Респ. Бурятия; 670002", 
            passport_details: "7421532544", 
            date_birth: "1954-02-27"
        },
        {
            name: "Валентин", 
            family:"Ильков",
            patronymic:"Николаевич",
            address: "ул. Цивилева; 2; Улан-Удэ; Респ. Бурятия; 670034", 
            passport_details: "8823432544", 
            date_birth: "1909-09-09"
        },
        {
            name: "Дмитрий", 
            family:"Плишкин",
            patronymic:"Николаевич",
            address: "ул. Цивилева; д.41; Улан-Удэ; Респ. Бурятия; 670034", 
            passport_details: "8123432987", 
            date_birth: "1944-12-22"
        },
        {
            name: "Александр", 
            family:"Алексеев",
            patronymic:"Владимирович",
            address: "ул. Модогоева; 1/1; Улан-Удэ; Респ. Бурятия; 670000", 
            passport_details: "8155532544", 
            date_birth: "1945-05-09"
        },
        {
            name: "Иннокентий", 
            family:"Ботвин",
            patronymic:"Иннокентьевич",
            address: "ул. Ключевская; 45Б; Улан-Удэ; Респ. Бурятия; 670013", 
            passport_details: "8123432541", 
            date_birth: "1941-05-08"
        },
        {
            name: "Петр", 
            family:"Васильев",
            patronymic:"Васильевич",
            address: "ул. Каландаришвили; 27; Улан-Удэ; Респ. Бурятия; 670000", 
            passport_details: "1126432544", 
            date_birth: "1921-12-21"
        },
        {
            name: "Герман", 
            family:"Головач",
            patronymic:"Артемьевич",
            address: "ул. Юного Коммунара; 3; Улан-Удэ; Респ. Бурятия; 670000", 
            passport_details: "6447632544", 
            date_birth: "1912-08-18"
        },
        {
            name: "Леонид", 
            family:"Гофланд",
            patronymic:"Арвидович",
            address: "ул. Хоца Намсараева; 2Б; Улан-Удэ; Респ. Бурятия; 670034", 
            passport_details: "8123432456", 
            date_birth: "1933-07-17"
        },
    ]);

    const speciality = await Speciality.insertMany([
        {title: "хирург"},
        {title: "терапевт"},
        {title: "аллерголог"},
        {title: "логопед"},
        {title: "уролог"},
        {title: "невролог"},
        {title: "нефролог"},
        {title: "кардиолог"},
        {title: "педиатр"},
        {title: "имунолог"},
    ]);

    const general_page = await General_page.insertMany([
        {
            title: "Государственное автономное учреждение здравоохранения <<Республиканская клиническая больница им. Н.А.Семашко>>", 
            website: "http://www.rkbsemashko.ru/",
            address: "корп. 2, ул. Пирогова, 3а, Улан-Удэ, Респ. Бурятия, 670047"
        },
        {
            title: "Государственное автономное учреждение здравоохранения <<Детская республиканская клиническая больница>>", 
            website: "http://drkbrb.ru/",
            address: "пр. Строителей, 2А, Улан-Удэ, Респ. Бурятия, 670042"
        },
        {
            title: "ГАУЗ <<Республиканская клиническая больница скорой медицинской помощи им. В.В. Ангапова>> г. Улан-Удэ", 
            website: "http://bsmp03.ru/",
            address: "пр. Строителей, 1, Улан-Удэ, Респ. Бурятия, 670042"
        },
        {
            title: "Государственное бюджетное учреждение здравоохранения <<Городская больница 2>>", 
            website: "https://xn--2-btbfp1ai/",
            address: "ул. Павлова, 12, Улан-Удэ, Респ. Бурятия, 670031"
        },
        {
            title: "Государственное бюджетное учреждение здравоохранения <<Городская больница 3>>", 
            website: "http://gp3uu.ru/",
            address: "ул. Павлова, 2а, Улан-Удэ, Респ. Бурятия, 670031"
        },
        {
            title: "Государственное бюджетное учреждение здравоохранения города Москвы <<Городская клиническая больница 13 Департамента здравоохранения города Москвы>>", 
            website: "https://gkb13.ru/",
            address: "670004, Республика Бурятия, город Улан-Удэ, п. Стеклозавод, улица Воронежская 1а"
        },
        {
            title: "УНИВЕРСИТЕТСКАЯ КЛИНИЧЕСКАЯ БОЛЬНИЦА ИМЕНИ В.В.ВИНОГРАДОВА (ФИЛИАЛ) <<РОССИЙСКИЙ УНИВЕРСИТЕТ ДРУЖБЫ НАРОДОВ ИМЕНИ ПАТРИСА ЛУМУМБЫ>>", 
            website: "https://gkb64.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.155"
        },
        {
            title: "Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская Мариинская больница>>", 
            website: "https://mariin.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.1"
        },
        {
            title: "Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская клиническая больница 31>>", 
            website: "https://www.spbsverdlovka.ru/",
            address: "ул. Вавилова, 61 строение 11, Москва, 117292"
        },
        {
            title: "Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Клиническая больница Святителя Луки>>", 
            website: "https://lucaclinic.ru/",
            address: "Литейный пр., 56, Санкт-Петербург, 191014"
        },
        {
            title: "Хирургическое", 
            website: "http://www.rkbsemashko.ru/",
            address: "пр. Динамо, 3, 3 этаж, Санкт-Петербург, 197110"
        },
        {
            title: "Педиатрическое", 
            website: "http://www.rkbsemashko.ru/",
            address: "пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042"
        },
        {
            title: "Терапевтическое", 
            website: "http://www.rkbsemashko.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.15"
        },
        {
            title: "Хирургическое", 
            website: "http://drkbrb.ru/",
            address: "пр. Строителей, 11, Улан-Удэ, Респ. Бурятия, 670042"
        },
        {
            title: "Неврологическое", 
            website: "http://drkbrb.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.2"
        },
        {
            title: "Нефрологическое", 
            website: "http://bsmp03.ru/",
            address: "Улан-Удэ, Респ. Бурятия, 670031"
        },
        {
            title: "Гинекологическое", 
            website: "https://lucaclinic.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Ранжурова, д.3"
        },
        {
            title: "Психиотрическое", 
            website: "https://lucaclinic.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Викторова, д.155"
        },
        {
            title: "Терапевтическое", 
            website: "https://www.spbsverdlovka.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Солнечная, д.5"
        },
        {
            title: "Кардиологическое", 
            website: "http://bsmp03.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Великая, д.16"
        },
        {
            title: "Кардиологическое", 
            website: "http://drkbrb.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.3"
        },
        {
            title: "Кардиологическое", 
            website: "http://bsmp03.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.16"
        },
        {
            title: "Кардиологическое", 
            website: "http://rkbsemashko.ru/",
            address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Воронежская, д.1"
        },
    ]);

    await Phone_number.insertMany([
        {phone_number: "83012437005", general_page_id: general_page[0]._id},
        {phone_number: "83012373040", general_page_id: general_page[1]._id},
        {phone_number: "83012556252", general_page_id: general_page[2]._id},
        {phone_number: "83012233524", general_page_id: general_page[3]._id},
        {phone_number: "83012437043", general_page_id: general_page[4]._id},
        {phone_number: "83012437004", general_page_id: general_page[5]._id},
        {phone_number: "83012373042", general_page_id: general_page[6]._id},
        {phone_number: "83017773042", general_page_id: general_page[7]._id},
        {phone_number: "83012233520", general_page_id: general_page[8]._id},
        {phone_number: "83012437044", general_page_id: general_page[9]._id},
        {phone_number: "83012232027", general_page_id: general_page[10]._id},
        {phone_number: "83012451898", general_page_id: general_page[11]._id},
        {phone_number: "83012311112", general_page_id: general_page[12]._id},
        {phone_number: "83012412543", general_page_id: general_page[13]._id},
        {phone_number: "83012437875", general_page_id: general_page[14]._id},
        {phone_number: "83012333042", general_page_id: general_page[15]._id},
        {phone_number: "83012222042", general_page_id: general_page[16]._id},
        {phone_number: "83012888042", general_page_id: general_page[17]._id},
        {phone_number: "83012226042", general_page_id: general_page[18]._id},
        {phone_number: "83012437872", general_page_id: general_page[19]._id},
        {phone_number: "83012880000", general_page_id: general_page[20]._id},
        {phone_number: "83012234215", general_page_id: general_page[21]._id},
        {phone_number: "83012437611", general_page_id: general_page[22]._id},
    ]);

    const hospital = await Hospital.insertMany([
        {
            id: general_page[0]._id,
            department_id:[
                general_page[10]._id,
            ],
            region_id: region[0]._id
        },
        {
            id: general_page[1]._id, 
            department_id:[
                general_page[11]._id,
                general_page[20]._id,
            ],
            region_id: region[0]._id
        },
        {
            id: general_page[2]._id, 
            department_id:[
                general_page[12]._id,
            ],
            region_id: region[0]._id
        },
        {
            id: general_page[3]._id, 
            department_id:[
                general_page[13]._id,
                general_page[21]._id,
            ],
            region_id: region[1]._id
        },
        {
            id: general_page[4]._id, 
            department_id:[
                general_page[14]._id,
            ],
            region_id: region[2]._id
        },
        {
            id: general_page[5]._id, 
            department_id:[
                general_page[15]._id,
            ],
            region_id: region[3]._id
        },
        {
            id: general_page[6]._id, 
            department_id:[
                general_page[16]._id,
                general_page[22]._id,
            ],
            region_id: region[4]._id
        },
        {
            id: general_page[7]._id, 
            department_id:[
                general_page[17]._id,
            ],
            region_id: region[5]._id
        },
        {
            id: general_page[8]._id, 
            department_id:[
                general_page[18]._id,
            ],
            region_id: region[5]._id
        },
        {
            id: general_page[9]._id, 
            department_id:[
                general_page[19]._id,
            ],
            region_id: region[6]._id
        }
    ]);

    await Requisites.insertMany([
        {
            registration_date: "1965-03-12", 
            hospital_reduce_name: "РКБ им. Н.А.Семашко", 
            name_legal_faces: "Овечкин В.А.", 
            ogrn: "1122315577445", 
            inn: "463248513644", 
            kpp: "642478976", 
            hospital_id: hospital[0].id[0]
        },
        {
            registration_date: "1987-11-19", 
            hospital_reduce_name: "ДРКБ", 
            name_legal_faces: "Оверин В.А.", 
            ogrn: "1122315571445", 
            inn: "465136443248", 
            kpp: "478976642", 
            hospital_id: hospital[0].id[1]
        },
        {
            registration_date: "1955-02-01", 
            hospital_reduce_name: "ГАУЗ городская больница №1", 
            name_legal_faces: "Иванов И.И.", 
            ogrn: "1155771223445", 
            inn: "485134632644", 
            kpp: "478642976", 
            hospital_id: hospital[0].id[2]
        },
        {
            registration_date: "2000-02-29", 
            hospital_reduce_name: "ГБУЗ городская больница №2", 
            name_legal_faces: "Нечкин В.Ф", 
            ogrn: "1122315577415", 
            inn: "463248513641", 
            kpp: "786424976", 
            hospital_id: hospital[1].id[3]
        },
        {
            registration_date: "1879-02-11", 
            hospital_reduce_name: "ГБУЗ городская больница №3", 
            name_legal_faces: "Добров Е.П", 
            ogrn: "2112377441555", 
            inn: "851463243644", 
            kpp: "649762478", 
            hospital_id: hospital[2].id[4]
        },
        {
            registration_date: "1965-12-30", 
            hospital_reduce_name: "УКБ им В.В.Виноградова", 
            name_legal_faces: "Петров Д.Н.", 
            ogrn: "1122531557744", 
            inn: "465133248644", 
            kpp: "789766424", 
            hospital_id: hospital[3].id[5]
        },
        {
            registration_date: "1977-09-12", 
            hospital_reduce_name: "ГБУЗ города Москвы городская больница №13", 
            name_legal_faces: "Синицин Н.Д.", 
            ogrn: "1155774451223", 
            inn: "463213644854", 
            kpp: "642894776", 
            hospital_id: hospital[4].id[6]
        },
        {
            registration_date: "1695-11-19", 
            hospital_reduce_name: "ГБУЗ городская больница", 
            name_legal_faces: "Иванов А.А.", 
            ogrn: "1182315577445", 
            inn: "463245136448", 
            kpp: "642976478", 
            hospital_id: hospital[5].id[7]
        },
        {
            registration_date: "1888-06-17", 
            hospital_reduce_name: "ГБУЗ городская больница №31", 
            name_legal_faces: "Сидоров В.П.", 
            ogrn: "5112744523157", 
            inn: "513644463248", 
            kpp: "678974246", 
            hospital_id: hospital[5].id[8]
        },
        {
            registration_date: "1811-10-16", 
            hospital_reduce_name: "ГБУЗ Клиническая больница Святителя Луки", 
            name_legal_faces: "Багданов К.В.", 
            ogrn: "1125577423145", 
            inn: "463213644485", 
            kpp: "648976247", 
            hospital_id: hospital[6].id[9]
        },
    ]);

    await Doctor_speciality.insertMany([
        
        {
            date_start: "1955-12-17", 
            date_end: "1966-01-27", 
            speciality_id: speciality[0]._id,
            doctor_id: doctor[0]._id,
            department_id: hospital[0].department_id[0]
        },
        {
            date_start: "1967-03-12", 
            date_end: "2013-11-22", 
            speciality_id: speciality[0]._id, 
            doctor_id: doctor[0]._id, 
            department_id: hospital[0].department_id[0]
        },
        {
            date_start: "1967-08-27", 
            date_end: "2003-12-23", 
            speciality_id: speciality[1]._id, 
            doctor_id: doctor[1]._id, 
            department_id: hospital[0].department_id[0]
        },
        {
            date_start: "1971-01-10", 
            date_end: "2021-11-11", 
            speciality_id: speciality[2]._id, 
            doctor_id: doctor[0]._id, 
            department_id: hospital[1].department_id[2]
        },
        {
            date_start: "1988-02-28", 
            date_end: "2023-11-27", 
            speciality_id: speciality[3]._id, 
            doctor_id: doctor[2]._id, 
            department_id: hospital[2].department_id[1]
        },
        {
            date_start: "1934-07-17", 
            date_end: "1998-11-30", 
            speciality_id: speciality[4]._id, 
            doctor_id: doctor[5]._id, 
            department_id: hospital[3].department_id[0]
        },
        {
            date_start: "1923-03-12", 
            date_end: "1991-06-30", 
            speciality_id: speciality[5]._id, 
            doctor_id: doctor[4]._id, 
            department_id: hospital[3].department_id[0]
        },
        {
            date_start: "1955-12-22", 
            date_end: "2017-10-22", 
            speciality_id: speciality[6]._id, 
            doctor_id: doctor[0]._id, 
            department_id: hospital[4].department_id[1]
        },
        {
            date_start: "1961-01-31", 
            date_end: "2003-09-12", 
            speciality_id: speciality[7]._id, 
            doctor_id: doctor[3]._id, 
            department_id: hospital[4].department_id[2]
        },
        {
            date_start: "1969-08-22", 
            date_end: "2015-05-15", 
            speciality_id: speciality[8]._id, 
            doctor_id: doctor[0]._id, 
            department_id: hospital[5].department_id[0]
        },
    ]);

    /*
    await Doctor_speciality.insertMany([
        
        {
            date_start: "1955-12-17", 
            date_end: "1966-01-27", 
            speciality_id: speciality[0]._id, 
            doctor_id: doctor[0]._id,
            department_id: department[0]._id
        },
        {
            date_start: "1967-03-12", 
            date_end: "2013-11-22", 
            speciality_id: speciality[0]._id, 
            doctor_id: doctor[0]._id, 
            department_id: department[0]._id
        },
        {
            date_start: "1967-08-27", 
            date_end: "2003-12-23", 
            speciality_id: speciality[1]._id, 
            doctor_id: doctor[1]._id, 
            department_id: department[0]._id
        },
        {
            date_start: "1971-01-10", 
            date_end: "2021-11-11", 
            speciality_id: speciality[2]._id, 
            doctor_id: doctor[0]._id, 
            department_id: department[1]._id
        },
        {
            date_start: "1988-02-28", 
            date_end: "2023-11-27", 
            speciality_id: speciality[3]._id, 
            doctor_id: doctor[2]._id, 
            department_id: department[2]._id
        },
        {
            date_start: "1934-07-17", 
            date_end: "1998-11-30", 
            speciality_id: speciality[4]._id, 
            doctor_id: doctor[5]._id, 
            department_id: department[3]._id
        },
        {
            date_start: "1923-03-12", 
            date_end: "1991-06-30", 
            speciality_id: speciality[5]._id, 
            doctor_id: doctor[4]._id, 
            department_id: department[3]._id
        },
        {
            date_start: "1955-12-22", 
            date_end: "2017-10-22", 
            speciality_id: speciality[6]._id, 
            doctor_id: doctor[0]._id, 
            department_id: department[4]._id
        },
        {
            date_start: "1961-01-31", 
            date_end: "2003-09-12", 
            speciality_id: speciality[7]._id, 
            doctor_id: doctor[3]._id, 
            department_id: department[4]._id
        },
        {
            date_start: "1969-08-22", 
            date_end: "2015-05-15", 
            speciality_id: speciality[8]._id, 
            doctor_id: doctor[0]._id, 
            department_id: department[5]._id
        },
    ]);
    */

    console.log('База данных заполнена');
    mongoose.disconnect();
}

seed()