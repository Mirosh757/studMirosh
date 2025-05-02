const mongoose = require('mongoose');

const phone_numberSchema = new mongoose.Schema({
    phone_number: String,
    description: String,
    hospital_description_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'hospital_description'
    }
});

module.exports = mongoose.model('phone_number', phone_numberSchema);