export const specialities = {
    'surgeon': {title : "хирург"},
    'therapist': {title : "терапевт"},
    'allergist': {title : "аллерголог"},
    'logopedist': {title : "логопед"},
    'urologist': {title : "уролог"},
    'neurologist': {title : "невролог"},
    'nephrologist': {title : "нефролог"},
    'cardiologist': {title : "кардиолог"},
    'pediatrician': {title : "педиатр"},
    'imunologist': {title : "имунолог"}
}
export const regions = {
    'Ulan_Ude': { region_name: "Улан-Удэ" },
    'Moscow': { region_name: "Москва" },
    'Saint_Petersburg': { region_name: "Санкт-Петербург" },
    'Ekaterinburg': { region_name: "Екатеринбург" },
    'Kaliningrad': { region_name: "Калининград" },
    'Kursk': { region_name: "Курск" },
    'Kazan': { region_name: "Казань" },
    'Omsk': { region_name: "Омск" },
    'Tomsk': { region_name: "Томск" },
    'Novosibirsk': { region_name: "Новосибирск" }
}
export const doctors = {
    '8123432544': {
        name: "Георгий",
        family: "Кондратов",
        patronymic: "Васильевич",
        address: "ул. Революции 1905 года; 36; Улан-Удэ; Респ. Бурятия; 670034",
        passport_details: "8123432544",
        date_birth: "1954-10-27"
    },
    '8133431234': {
        name: "Иван",
        family: "Овчинников",
        patronymic: "Афанасьевич",
        address: "ул. Комсомольская; 1Б; Улан-Удэ; Респ. Бурятия; 670002",
        passport_details: "8133431234",
        date_birth: "1922-09-14"
    },
    '7421532544': {
        name: "Эрдэн",
        family: "Раднаев",
        patronymic: "Раднаевич",
        address: "ул. Октябрьская; 36а; Улан-Удэ; Респ. Бурятия; 670002",
        passport_details: "7421532544",
        date_birth: "1954-02-27"
    },
    '8823432544': {
        name: "Валентин",
        family: "Ильков",
        patronymic: "Николаевич",
        address: "ул. Цивилева; 2; Улан-Удэ; Респ. Бурятия; 670034",
        passport_details: "8823432544",
        date_birth: "1909-09-09"
    },
    '8123432987': {
        name: "Дмитрий",
        family: "Плишкин",
        patronymic: "Николаевич",
        address: "ул. Цивилева; д.41; Улан-Удэ; Респ. Бурятия; 670034",
        passport_details: "8123432987",
        date_birth: "1944-12-22"
    },
    '8155532544': {
        name: "Александр",
        family: "Алексеев",
        patronymic: "Владимирович",
        address: "ул. Модогоева; 1/1; Улан-Удэ; Респ. Бурятия; 670000",
        passport_details: "8155532544",
        date_birth: "1945-05-09"
    },
    '8123432541': {
        name: "Иннокентий",
        family: "Ботвин",
        patronymic: "Иннокентьевич",
        address: "ул. Ключевская; 45Б; Улан-Удэ; Респ. Бурятия; 670013",
        passport_details: "8123432541",
        date_birth: "1941-05-08"
    },
    '1126432544': {
        name: "Петр",
        family: "Васильев",
        patronymic: "Васильевич",
        address: "ул. Каландаришвили; 27; Улан-Удэ; Респ. Бурятия; 670000",
        passport_details: "1126432544",
        date_birth: "1921-12-21"
    },
    '6447632544': {
        name: "Герман",
        family: "Головач",
        patronymic: "Артемьевич",
        address: "ул. Юного Коммунара; 3; Улан-Удэ; Респ. Бурятия; 670000",
        passport_details: "6447632544",
        date_birth: "1912-08-18"
    },
    '8123432456': {
        name: "Леонид",
        family: "Гофланд",
        patronymic: "Арвидович",
        address: "ул. Хоца Намсараева; 2Б; Улан-Удэ; Респ. Бурятия; 670034",
        passport_details: "8123432456",
        date_birth: "1933-07-17"
    },
}

export const medical_instituctions = {
    'Государственное автономное учреждение здравоохранения <<Республиканская клиническая больница им. Н.А.Семашко>>': {
        title: "Государственное автономное учреждение здравоохранения <<Республиканская клиническая больница им. Н.А.Семашко>>",
        website: "http://www.rkbsemashko.ru/",
        address: "корп. 2, ул. Пирогова, 3а, Улан-Удэ, Респ. Бурятия, 670047",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "1965-03-12",
            hospital_reduce_name: "РКБ им. Н.А.Семашко",
            name_legal_faces: "Овечкин В.А.",
            ogrn: "1122315577445",
            inn: "463248513644",
            kpp: "642478976",
        }
    },
    'Государственное автономное учреждение здравоохранения <<Детская республиканская клиническая больница>>': {
        title: "Государственное автономное учреждение здравоохранения <<Детская республиканская клиническая больница>>",
        website: "http://drkbrb.ru/",
        address: "пр. Строителей, 2А, Улан-Удэ, Респ. Бурятия, 670042",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "1987-11-19",
            hospital_reduce_name: "ДРКБ",
            name_legal_faces: "Оверин В.А.",
            ogrn: "1122315571445",
            inn: "465136443248",
            kpp: "478976642",
        }
    },
    'ГАУЗ <<Республиканская клиническая больница скорой медицинской помощи им. В.В. Ангапова>> г. Улан-Удэ': {
        title: "ГАУЗ <<Республиканская клиническая больница скорой медицинской помощи им. В.В. Ангапова>> г. Улан-Удэ",
        website: "http://bsmp03.ru/",
        address: "пр. Строителей, 1, Улан-Удэ, Респ. Бурятия, 670042",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "1955-02-01",
            hospital_reduce_name: "ГАУЗ городская больница №1",
            name_legal_faces: "Иванов И.И.",
            ogrn: "1155771223445",
            inn: "485134632644",
            kpp: "478642976",
        }
    },
    'Государственное бюджетное учреждение здравоохранения <<Городская больница 2>>': {
        title: "Государственное бюджетное учреждение здравоохранения <<Городская больница 2>>",
        website: "https://xn--2-btbfp1ai/",
        address: "ул. Павлова, 12, Улан-Удэ, Респ. Бурятия, 670031",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "2000-02-29",
            hospital_reduce_name: "ГБУЗ городская больница №2",
            name_legal_faces: "Нечкин В.Ф",
            ogrn: "1122315577415",
            inn: "463248513641",
            kpp: "786424976",
        }
    },
    'Государственное бюджетное учреждение здравоохранения <<Городская больница 3>>': {
        title: "Государственное бюджетное учреждение здравоохранения <<Городская больница 3>>",
        website: "http://gp3uu.ru/",
        address: "ул. Павлова, 2а, Улан-Удэ, Респ. Бурятия, 670031",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "1879-02-11",
            hospital_reduce_name: "ГБУЗ городская больница №3",
            name_legal_faces: "Добров Е.П",
            ogrn: "2112377441555",
            inn: "851463243644",
            kpp: "649762478",
        }
    },
    'Государственное бюджетное учреждение здравоохранения города Москвы <<Городская клиническая больница 13 Департамента здравоохранения города Москвы>>': {
        title: "Государственное бюджетное учреждение здравоохранения города Москвы <<Городская клиническая больница 13 Департамента здравоохранения города Москвы>>",
        website: "https://gkb13.ru/",
        address: "670004, Республика Бурятия, город Улан-Удэ, п. Стеклозавод, улица Воронежская 1а",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "1965-12-30",
            hospital_reduce_name: "УКБ им В.В.Виноградова",
            name_legal_faces: "Петров Д.Н.",
            ogrn: "1122531557744",
            inn: "465133248644",
            kpp: "789766424",
        }
    },
    'УНИВЕРСИТЕТСКАЯ КЛИНИЧЕСКАЯ БОЛЬНИЦА ИМЕНИ В.В.ВИНОГРАДОВА (ФИЛИАЛ) <<РОССИЙСКИЙ УНИВЕРСИТЕТ ДРУЖБЫ НАРОДОВ ИМЕНИ ПАТРИСА ЛУМУМБЫ>>': {
        title: "УНИВЕРСИТЕТСКАЯ КЛИНИЧЕСКАЯ БОЛЬНИЦА ИМЕНИ В.В.ВИНОГРАДОВА (ФИЛИАЛ) <<РОССИЙСКИЙ УНИВЕРСИТЕТ ДРУЖБЫ НАРОДОВ ИМЕНИ ПАТРИСА ЛУМУМБЫ>>",
        website: "https://gkb64.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.155",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "1977-09-12",
            hospital_reduce_name: "ГБУЗ города Москвы городская больница №13",
            name_legal_faces: "Синицин Н.Д.",
            ogrn: "1155774451223",
            inn: "463213644854",
            kpp: "642894776",
        }
    },
    'Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская Мариинская больница>>': {
        title: "Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская Мариинская больница>>",
        website: "https://mariin.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.1",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "1695-11-19",
            hospital_reduce_name: "ГБУЗ городская больница",
            name_legal_faces: "Иванов А.А.",
            ogrn: "1182315577445",
            inn: "463245136448",
            kpp: "642976478",
        }
    },
    'Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская клиническая больница 31>>': {
        title: "Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская клиническая больница 31>>",
        website: "https://www.spbsverdlovka.ru/",
        address: "ул. Вавилова, 61 строение 11, Москва, 117292",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "1888-06-17",
            hospital_reduce_name: "ГБУЗ городская больница №31",
            name_legal_faces: "Сидоров В.П.",
            ogrn: "5112744523157",
            inn: "513644463248",
            kpp: "678974246",
        }
    },
    'Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Клиническая больница Святителя Луки>>': {
        title: "Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Клиническая больница Святителя Луки>>",
        website: "https://lucaclinic.ru/",
        address: "Литейный пр., 56, Санкт-Петербург, 191014",
        departments: [],
        region_id: null,
        type: "hospital",
        requisites:
        {
            registration_date: "1811-10-16",
            hospital_reduce_name: "ГБУЗ Клиническая больница Святителя Луки",
            name_legal_faces: "Багданов К.В.",
            ogrn: "1125577423145",
            inn: "463213644485",
            kpp: "648976247",
        }
    },
    'Хирургическое пр. Динамо, 3, 3 этаж, Санкт-Петербург, 197110': {
        title: "Хирургическое",
        website: "http://www.rkbsemashko.ru/",
        address: "пр. Динамо, 3, 3 этаж, Санкт-Петербург, 197110",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Педиатрическое пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042': {
        title: "Педиатрическое",
        website: "http://www.rkbsemashko.ru/",
        address: "пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.15': {
        title: "Терапевтическое",
        website: "http://www.rkbsemashko.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.15",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Хирургическое пр. Строителей, 11, Улан-Удэ, Респ. Бурятия, 670042': {
        title: "Хирургическое",
        website: "http://drkbrb.ru/",
        address: "пр. Строителей, 11, Улан-Удэ, Респ. Бурятия, 670042",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Неврологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.2': {
        title: "Неврологическое",
        website: "http://drkbrb.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.2",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Нефрологическое Улан-Удэ, Респ. Бурятия, 670031': {
        title: "Нефрологическое",
        website: "http://bsmp03.ru/",
        address: "Улан-Удэ, Респ. Бурятия, 670031",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Гинекологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Ранжурова, д.3': {
        title: "Гинекологическое",
        website: "https://lucaclinic.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Ранжурова, д.3",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Психиотрическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Викторова, д.155': {
        title: "Психиотрическое",
        website: "https://lucaclinic.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Викторова, д.155",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Солнечная, д.5': {
        title: "Терапевтическое",
        website: "https://www.spbsverdlovka.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Солнечная, д.5",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Великая, д.16': {
        title: "Кардиологическое",
        website: "http://bsmp03.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Великая, д.16",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.3': {
        title: "Кардиологическое",
        website: "http://drkbrb.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.3",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.16': {
        title: "Кардиологическое",
        website: "http://bsmp03.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.16",
        departments: [],
        region_id: null,
        type: "department"
    },
    'Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Воронежская, д.1': {
        title: "Кардиологическое",
        website: "http://rkbsemashko.ru/",
        address: "670042, Республика Бурятия, г. Улан-Удэ, ул.Воронежская, д.1",
        departments: [],
        region_id: null,
        type: "department"
    }
}

export const phone_numbers = {
    '83012437005': {
            phone_number: "83012437005",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83012373040': {
            phone_number: "83012373040",
            description: "Приемная гл. врача",
            medical_instituction_id: null
        },
       '83012556252': {
            phone_number: "83012556252",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83012233524': {
            phone_number: "83012233524",
            description: "Дневной стационар",
            medical_instituction_id: null
        },
       '83012437043': {
            phone_number: "83012437043",
            description: "Дневной стационар",
            medical_instituction_id: null
        },
       '83012437004': {
            phone_number: "83012437004",
            description: "единый номер горячей линии",
            medical_instituction_id: null
        },
       '83012373042': {
            phone_number: "83012373042",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83017773042': {
            phone_number: "83017773042",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83012233520': {
            phone_number: "83012233520",
            description: "Дневной стационар",
            medical_instituction_id: null
        },
       '83012437044': {
            phone_number: "83012437044",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83012232027': {
            phone_number: "83012232027",
            description: "Дневной стационар",
            medical_instituction_id: null
        },
       '83012451898': {
            phone_number: "83012451898",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83012311112': {
            phone_number: "83012311112",
            description: "приемная гл. врача",
            medical_instituction_id: null
        },
       '83012412543': {
            phone_number: "83012412543",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83012437875': {
            phone_number: "83012437875",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83012333042': {
            phone_number: "83012333042",
            description: "Дневной стационар",
            medical_instituction_id: null
        },
       '83012222042': {
            phone_number: "83012222042",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83012888042': {
            phone_number: "83012888042",
            description: "Дневной стационар",
            medical_instituction_id: null
        },
       '83012226042': {
            phone_number: "83012226042",
            description: "Дневной стационар",
            medical_instituction_id: null
        },
       '83012437872': {
            phone_number: "83012437872",
            description: "Регистратура",
            medical_instituction_id: null
        },
        '83012880000': {
            phone_number: "83012880000",
            description: "Дневной стационар",
            medical_instituction_id: null
        },
       '83012234215': {
            phone_number: "83012234215",
            description: "Регистратура",
            medical_instituction_id: null
        },
       '83012437611': {
            phone_number: "83012437611",
            description: "Регистратура",
            medical_instituction_id: null
        },
}