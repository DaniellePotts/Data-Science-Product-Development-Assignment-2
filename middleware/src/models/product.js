const mongoose = require('mongoose');

const Schema = mongoose.Schema;

const Product = new Schema({
    id: String,
    name: String,
    productLine: String,
    price: Number
})

const ProductModel = mongoose.model('Product', Product );