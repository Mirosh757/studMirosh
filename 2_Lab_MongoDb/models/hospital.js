const mongoose = require('mongoose');

const hospitalSchema = new mongoose.Schema({
    id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'general_page'
    },
    department_id:[{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'general_page'
    }],
    region_id:{
        type: mongoose.Schema.Types.ObjectId,
        ref: 'region'
    }
    
});

module.exports = mongoose.model('hospital', hospitalSchema);