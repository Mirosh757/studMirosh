require('dotenv').config();
const mongoose = require('mongoose');
const Doctor = require('../models/doctor');
const Doctor_speciality = require('../models/doctor_speciality');
const Medical_instituction = require('../models/medical_instituction');
const Phone_number = require('../models/phone_number');
const Region = require('../models/region');
//const Requisites = require('../models/requisites');
const Speciality = require('../models/speciality');
const { specialities, regions, doctors, medical_instituctions, phone_numbers, doctor_specialities } = require('../migrations/dataInput');
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

    const medicalInstituctionArray = Object.values(medical_instituctions).map(medical => ({
        title: medical.title,
        website: medical.website,
        address: medical.address,
        departments: [],
        region_id: regionMap.get(medical.region_id),
        type: medical.type,
        requisites: medical.requisites
    }))
    const insertedMedicalInstitutions = await Medical_instituction.insertMany(medicalInstituctionArray);

    const medicalInstitutionMap = new Map();
    insertedMedicalInstitutions.forEach(med => {
        const key = med.type === "hospital" ? med.title : `${med.title} ${med.address}`;
        medicalInstitutionMap.set(key, med._id);
    });

    for (const medical of insertedMedicalInstitutions) {
    const originalData = medical_instituctions[medical.title];
    if (!originalData?.departments) continue;

    let departmentIds = [];
    if (Array.isArray(originalData.departments)) {
        departmentIds = originalData.departments
            .map(dept => medicalInstitutionMap.get(dept))
            .filter(Boolean); // Отфильтровываем undefined
    } else if (medicalInstitutionMap.has(originalData.departments)) {
        departmentIds = [medicalInstitutionMap.get(originalData.departments)];
    }

    if (departmentIds.length > 0) {
        await Medical_instituction.updateOne(
            { _id: medical._id },
            { $set: { departments: departmentIds } }
        );
    }
}

    const phoneNumbersArray = Object.values(phone_numbers).map(phone => ({
        phone_number: phone.phone_number,
        description: phone.description,
        medical_instituction_id: medicalInstitutionMap.get(phone.medical_instituction_id)
    }));
    await Phone_number.insertMany(phoneNumbersArray);

    const doctorSpecialitiesArray = Object.values(doctor_specialities).map(ds => ({
        date_start: ds.date_start,
        date_end: ds.date_end,
        speciality_id: specialityMap.get(ds.speciality_id),
        doctor_id: doctorMap.get(ds.doctor_id),
        department_id: medicalInstitutionMap.get(ds.department_id)
    }));
    await Doctor_speciality.insertMany(doctorSpecialitiesArray);

    console.log('База данных заполнена');
    mongoose.disconnect();
}

seed()