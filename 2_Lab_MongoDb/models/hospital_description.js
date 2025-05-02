const mongoose = require('mongoose');

const hospital_descriptionSchema = new mongoose.Schema({
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
    type: String
});

module.exports = mongoose.model('hospital_description', hospital_descriptionSchema);