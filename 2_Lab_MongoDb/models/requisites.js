const mongoose = require('mongoose');

const requisitesSchema = new mongoose.Schema({
    registration_date: Date,
    hospital_reduce_name: String,
    name_legal_faces: String,
    ogrn: String,
    inn: String,
    kpp: String,
    hospital_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'hospital'
    }
});

module.exports = mongoose.model('requisites', requisitesSchema);