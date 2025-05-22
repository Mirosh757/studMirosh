const mongoose = require('mongoose');

const doctor_specialitySchema = new mongoose.Schema({
    date_start: Date,
    date_end: Date,
    speciality_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'speciality'
    },
    doctor_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'doctor'
    },
    department_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'medical_instituction'
    }
});

module.exports = mongoose.model('doctor_speciality', doctor_specialitySchema);