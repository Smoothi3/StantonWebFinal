var mongoose = require("mongoose")
var Schema = mongoose.Schema;

var PlayerRecordSchema = new Schema({
    myName:{
        type:String,
        required:true
    },
    myScore:{
        type:Number
    },
    myPlays:{
        type:Number
    }
})

let PlayerRecord = mongoose.model('playerRecords', PlayerRecordSchema)
module.exports = PlayerRecord