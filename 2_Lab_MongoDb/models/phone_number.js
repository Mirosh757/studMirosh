const mongoose = require('mongoose');

const phone_numberSchema = new mongoose.Schema({
    phone_number: String,
    general_page_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'general_page'
    }
});

module.exports = mongoose.model('phone_number', phone_numberSchema);