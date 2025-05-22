const mongoose = require('mongoose');

const medical_instituctionSchema = new mongoose.Schema({
    title: String,
    website: String,
    address: String,
    departments:[{
            type: mongoose.Schema.Types.ObjectId,
            ref: 'hospital_description'
    }],
    region_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'region'
    },
    type: String,
    requisites: {
        registration_date: { type: Date }, // дата регистрации
        hospital_reduce_name: { type: String }, // сокращенное название
        name_legal_faces: { type: String }, // ФИО ответственного лица
        ogrn: { type: String }, // ОГРН
        inn: { type: String }, // ИНН
        kpp: { type: String }, // КПП
    },
});

module.exports = mongoose.model('medical_instituction', medical_instituctionSchema);