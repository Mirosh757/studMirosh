require('dotenv').config();
const mongoose = require('mongoose');
const Doctor = require('../models/doctor');
const Doctor_speciality = require('../models/doctor_speciality');
const Medical_instituction = require('../models/medical_instituction');
const Phone_number = require('../models/phone_number');
const Region = require('../models/region');
//const Requisites = require('../models/requisites');
const Speciality = require('../models/speciality');
const { specialities, regions, doctors, medical_instituctions, phone_numbers } = require('../migrations/dataInput');
async function seed() {
    await mongoose.connect(process.env.MONGODB_URL)

    await Doctor.deleteMany();
    await Doctor_speciality.deleteMany();
    await Medical_instituction.deleteMany();
    await Phone_number.deleteMany();
    await Region.deleteMany();
    await Speciality.deleteMany();

    
    const regionsArray = Object.values(regions).map(region => ({
        region_name: region.region_name,
    }));
    const insertedRegions = await Region.insertMany(regionsArray);
    
    const regionMap = new Map();
    insertedRegions.forEach(region => {
        regionMap.set(region.region_name, region._id);
    });

    const specialitiesArray = Object.values(specialities).map(spec => ({
        title: spec.title,
    }));
    const insertedSpecialities = await Speciality.insertMany(specialitiesArray);
    
    const specialityMap = new Map();
    insertedSpecialities.forEach(spec => {
        specialityMap.set(spec.title, spec._id);
    });

    const doctorsArray = Object.values(doctors).map(doctor => ({
        name: doctor.name,
            family: doctor.family,
            patronymic: doctor.patronymic,
            address: doctor.address,
            passport_details: doctor.passport_details,
            date_birth: doctor.date_birth,
    }));

    const insertedDoctors = await Doctor.insertMany(doctorsArray);

    const doctorMap = new Map();
    insertedDoctors.forEach(doct => {
        doctorMap.set(doct.passport_details, doct._id);
    })

    const MedicalInstituctionArray = Object.values(medical_instituctions).map(medical => ({
        title: medical.title,
        website: medical.website,
        address: medical.address,
        departments: medical.departments,
        region_id: medical.region_id,
        type: medical.type,
        requisites: medical.requisites
    }))
    const insertedMedicalInstituction = await Medical_instituction.insertMany(MedicalInstituctionArray);

    const medicalInstituctionMap = new Map();
    insertedMedicalInstituction.forEach(medical => {
        if(medical.type == "hospital")
            medicalInstituctionMap.set(medical.title, medical._id);
        else
            medicalInstituctionMap.set(`${medical.title} ${medical.address}`, medical._id)
    })

    console.log(medicalInstituctionMap)
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Государственное автономное учреждение здравоохранения <<Республиканская клиническая больница им. Н.А.Семашко>>')
    },
        {
            $set: {
                region_id: regionMap.get('Улан-Удэ'),
                departments: [medicalInstituctionMap.get('Хирургическое пр. Динамо, 3, 3 этаж, Санкт-Петербург, 197110'), medicalInstituctionMap.get('Педиатрическое пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Государственное автономное учреждение здравоохранения <<Детская республиканская клиническая больница>>')
    },
        {
            $set: {
                region_id: regionMap.get('Улан-Удэ'),
                departments: [medicalInstituctionMap.get('Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.15')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('ГАУЗ <<Республиканская клиническая больница скорой медицинской помощи им. В.В. Ангапова>> г. Улан-Удэ')
    },
        {
            $set: {
                region_id: regionMap.get('Улан-Удэ'),
                departments: [medicalInstituctionMap.get('Хирургическое пр. Строителей, 11, Улан-Удэ, Респ. Бурятия, 670042')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Государственное бюджетное учреждение здравоохранения <<Городская больница 2>>')
    },
        {
            $set: {
                region_id: regionMap.get('Москва'),
                departments: [medicalInstituctionMap.get('Неврологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.2')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Государственное бюджетное учреждение здравоохранения <<Городская больница 3>>')
    },
        {
            $set: {
                region_id: regionMap.get('Санкт-Петербург'),
                departments: [medicalInstituctionMap.get('Нефрологическое Улан-Удэ, Респ. Бурятия, 670031'), medicalInstituctionMap.get('Гинекологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Ранжурова, д.3')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Государственное бюджетное учреждение здравоохранения города Москвы <<Городская клиническая больница 13 Департамента здравоохранения города Москвы>>')
    },
        {
            $set: {
                region_id: regionMap.get('Екатеринбург'),
                departments: [medicalInstituctionMap.get('Психиотрическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Викторова, д.155')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('УНИВЕРСИТЕТСКАЯ КЛИНИЧЕСКАЯ БОЛЬНИЦА ИМЕНИ В.В.ВИНОГРАДОВА (ФИЛИАЛ) <<РОССИЙСКИЙ УНИВЕРСИТЕТ ДРУЖБЫ НАРОДОВ ИМЕНИ ПАТРИСА ЛУМУМБЫ>>')
    },
        {
            $set: {
                region_id: regionMap.get('Екатеринбург'),
                departments: [medicalInstituctionMap.get('Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Солнечная, д.5')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская Мариинская больница>>')
    },
        {
            $set: {
                region_id: regionMap.get('Калининград'),
                departments: [medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Великая, д.16')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская клиническая больница 31>>')
    },
        {
            $set: {
                region_id: regionMap.get('Курск'),
                departments: [medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.3')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Клиническая больница Святителя Луки>>')
    },
        {
            $set: {
                region_id: regionMap.get('Казань'),
                departments: [medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Воронежская, д.1')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Хирургическое пр. Динамо, 3, 3 этаж, Санкт-Петербург, 197110')
    },
        {
            $set: {
                region_id: regionMap.get('Улан-Удэ'),
                departments: [medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Воронежская, д.1')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Педиатрическое пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042')
    },
        {
            $set: {
                region_id: regionMap.get('Улан-Удэ'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.15')
    },
        {
            $set: {
                region_id: regionMap.get('Улан-Удэ'),
                departments: [medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.16')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Хирургическое пр. Строителей, 11, Улан-Удэ, Респ. Бурятия, 670042')
    },
        {
            $set: {
                region_id: regionMap.get('Улан-Удэ'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Педиатрическое пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042')
    },
        {
            $set: {
                region_id: regionMap.get('Улан-Удэ'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.15')
    },
        {
            $set: {
                region_id: regionMap.get('Москва'),
                departments: [medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.3')]
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Хирургическое пр. Строителей, 11, Улан-Удэ, Респ. Бурятия, 670042')
    },
        {
            $set: {
                region_id: regionMap.get('Москва'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Неврологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.2')
    },
        {
            $set: {
                region_id: regionMap.get('Санкт-Петербург'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Нефрологическое Улан-Удэ, Респ. Бурятия, 670031')
    },
        {
            $set: {
                region_id: regionMap.get('Санкт-Петербург'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Гинекологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Ранжурова, д.3')
    },
        {
            $set: {
                region_id: regionMap.get('Екатеринбург'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Психиотрическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Викторова, д.155')
    },
        {
            $set: {
                region_id: regionMap.get('Екатеринбург'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Солнечная, д.5')
    },
        {
            $set: {
                region_id: regionMap.get('Екатеринбург'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Великая, д.16')
    },
        {
            $set: {
                region_id: regionMap.get('Екатеринбург'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.3')
    },
        {
            $set: {
                region_id: regionMap.get('Курск'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.16')
    },
        {
            $set: {
                region_id: regionMap.get('Курск'),
            }
        }
    )
    await Medical_instituction.updateOne({
        "_id": medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Воронежская, д.1')
    },
        {
            $set: {
                region_id: regionMap.get('Калининград'),
            }
        }
    )

    const phoneNumberArray = Object.values(phone_numbers).map(phone => ({
        phone_number: phone.phone_number,
        description: phone.description
    }))

    const insertedPhoneNumber = await Phone_number.insertMany(phoneNumberArray);

    const phoneNumberMap = new Map();
    insertedPhoneNumber.forEach(phone => {
        phoneNumberMap.set(phone.phone_number, phone._id);
    })

    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012437005')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Государственное автономное учреждение здравоохранения <<Республиканская клиническая больница им. Н.А.Семашко>>')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012373040')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Государственное автономное учреждение здравоохранения <<Детская республиканская клиническая больница>>')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012556252')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('ГАУЗ <<Республиканская клиническая больница скорой медицинской помощи им. В.В. Ангапова>> г. Улан-Удэ')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012233524')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Государственное бюджетное учреждение здравоохранения <<Городская больница 2>>')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012437043')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Государственное бюджетное учреждение здравоохранения <<Городская больница 3>>')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012437004')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Государственное бюджетное учреждение здравоохранения города Москвы <<Городская клиническая больница 13 Департамента здравоохранения города Москвы>>')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012373042')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('УНИВЕРСИТЕТСКАЯ КЛИНИЧЕСКАЯ БОЛЬНИЦА ИМЕНИ В.В.ВИНОГРАДОВА (ФИЛИАЛ) <<РОССИЙСКИЙ УНИВЕРСИТЕТ ДРУЖБЫ НАРОДОВ ИМЕНИ ПАТРИСА ЛУМУМБЫ>>')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83017773042')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская Мариинская больница>>')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012233520')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Городская клиническая больница 31>>')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012437044')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Санкт-Петербургское государственное бюджетное учреждение здравоохранения <<Клиническая больница Святителя Луки>>')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012232027')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Хирургическое пр. Динамо, 3, 3 этаж, Санкт-Петербург, 197110')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012451898')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Педиатрическое пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012311112')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.15')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012412543')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Хирургическое пр. Строителей, 11, Улан-Удэ, Респ. Бурятия, 670042')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012437875')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Неврологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.2')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012333042')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Нефрологическое Улан-Удэ, Респ. Бурятия, 670031')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012222042')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Гинекологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Ранжурова, д.3')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012888042')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Психиотрическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Викторова, д.155')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012226042')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Солнечная, д.5')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012437872')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Великая, д.16')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012880000')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.3')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012234215')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.16')
            }   
        }
    )
    await Phone_number.updateOne({
        "_id": phoneNumberMap.get('83012437611')
    },
        {
            $set:{
                medical_instituction_id: medicalInstituctionMap.get('Кардиологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Воронежская, д.1')
            }   
        }
    )

    

    await Doctor_speciality.insertMany([

        {
            date_start: "1955-12-17",
            date_end: "1966-01-27",
            speciality_id: specialityMap.get('хирург'),
            doctor_id: doctorMap.get('8123432544'),
            department_id: medicalInstituctionMap.get('Хирургическое пр. Динамо, 3, 3 этаж, Санкт-Петербург, 197110')
        },
        {
            date_start: "1967-03-12",
            date_end: "2013-11-22",
            speciality_id: specialityMap.get('хирург'),
            doctor_id: doctorMap.get('8133431234'),
            department_id: medicalInstituctionMap.get('Хирургическое пр. Динамо, 3, 3 этаж, Санкт-Петербург, 197110')
        },
        {
            date_start: "1967-08-27",
            date_end: "2003-12-23",
            speciality_id: specialityMap.get('терапевт'),
            doctor_id: doctorMap.get('8133431234'),
            department_id: medicalInstituctionMap.get('Педиатрическое пр. Строителей, 2а, Улан-Удэ, Респ. Бурятия, 670042')
        },
        {
            date_start: "1971-01-10",
            date_end: "2021-11-11",
            speciality_id: specialityMap.get('терапевт'),
            doctor_id: doctorMap.get('7421532544'),
            department_id: medicalInstituctionMap.get('')
        },
        {
            date_start: "1988-02-28",
            date_end: "2023-11-27",
            speciality_id: specialityMap.get('терапевт'),
            doctor_id: doctorMap.get('7421532544'),
            department_id: medicalInstituctionMap.get('Терапевтическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Петрова, д.15')
        },
        {
            date_start: "1934-07-17",
            date_end: "1998-11-30",
            speciality_id: specialityMap.get('аллерголог'),
            doctor_id: doctorMap.get('8823432544'),
            department_id: medicalInstituctionMap.get('Хирургическое пр. Строителей, 11, Улан-Удэ, Респ. Бурятия, 670042')
        },
        {
            date_start: "1923-03-12",
            date_end: "1991-06-30",
            speciality_id: specialityMap.get('аллерголог'),
            doctor_id: doctorMap.get('8123432987'),
            department_id: medicalInstituctionMap.get('Неврологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.2')
        },
        {
            date_start: "1955-12-22",
            date_end: "2017-10-22",
            speciality_id: specialityMap.get('логопед'),
            doctor_id: doctorMap.get('8123432456'),
            department_id: medicalInstituctionMap.get('Неврологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.2')
        },
        {
            date_start: "1961-01-31",
            date_end: "2003-09-12",
            speciality_id: specialityMap.get('логопед'),
            doctor_id: doctorMap.get('8155532544'),
            department_id: medicalInstituctionMap.get('Неврологическое 670042, Республика Бурятия, г. Улан-Удэ, ул.Тобольская, д.2')
        },
        {
            date_start: "1969-08-22",
            date_end: "2015-05-15",
            speciality_id: specialityMap.get('уролог'),
            doctor_id: doctorMap.get('8155532544'),
            department_id: medicalInstituctionMap.get('Нефрологическое Улан-Удэ, Респ. Бурятия, 670031')
        },
    ]);

    console.log('База данных заполнена');
    mongoose.disconnect();
}

seed()