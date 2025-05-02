require('dotenv').config();
const mongoose = require('mongoose');
require('../models/doctor');
require('../models/doctor_speciality');
require('../models/hospital_description');
require('../models/phone_number');
require('../models/region');
require('../models/requisites');
require('../models/speciality');

mongoose.connect(process.env.MONGODB_URL)
    .then(() => {
        console.log('Коллекции готовы');
        mongoose.disconnect();
    })
    .catch(err => {
        console.log(err);
    });