const mongoose = require('mongoose');

const doctorSchema = new mongoose.Schema({
    name: String,
    address: String,
    passport_details: String,
    date_birth: Date
});

module.exports = mongoose.model('doctor', doctorSchema);