const mongoose = require('mongoose');

const departmentSchema = new mongoose.Schema({
    id:[{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'general_page'
    }],
    hospital_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'hospital'
    }
    
});

module.exports = mongoose.model('department', departmentSchema);