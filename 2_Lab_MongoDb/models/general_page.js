const mongoose = require('mongoose');

const general_pageSchema = new mongoose.Schema({
    title: String,
    website: String,
    address: String
});

module.exports = mongoose.model('general_page', general_pageSchema);