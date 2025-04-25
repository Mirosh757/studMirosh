const mongoose = require('mongoose');

const specialitySchema = new mongoose.Schema({
    title: String
});

module.exports = mongoose.model('speciality', specialitySchema);