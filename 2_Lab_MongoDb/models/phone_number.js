const mongoose = require('mongoose');

const phone_numberSchema = new mongoose.Schema({
    phone_number: String,
    description: String,
    medical_instituction_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'medical_instituction'
    }
});

module.exports = mongoose.model('phone_number', phone_numberSchema);