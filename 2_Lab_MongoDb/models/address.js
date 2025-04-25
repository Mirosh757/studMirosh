const mongoose = require('mongoose');

const addressSchema = new mongoose.Schema({
    address: String,
    general_page_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'general_page'
    }
});

module.exports = mongoose.model('address', addressSchema);