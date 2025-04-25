const mongoose = require('mongoose');

const regionSchema = new mongoose.Schema({
    region_name: String
});

module.exports = mongoose.model('region', regionSchema);