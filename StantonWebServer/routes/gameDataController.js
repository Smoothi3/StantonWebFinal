var PlayerRecord = require('../models/PlayerRecords')

module.exports.getAllGameData = function(req, res){
    var sort = {"myScore":-1}
    PlayerRecord.find().sort(sort).then(function(gamedata){
        console.log(gamedata)
        res.json(gamedata)
    })
}
module.exports.deleteEntry = function(req, res){
    console.log("Name to delete: ", req.query.myName);
    PlayerRecord.deleteOne({"myName":req.query.myName}).then(function(){
        console.log("Entry deleted")
    }).catch(function(err){
        console.log(err)
    })
}

module.exports.updateEntry = function(req, res){
    console.log("Name to update: ", req.body.myName);

    PlayerRecord.updateOne(
        {myName: req.body.myName},
        {myName: req.body.myName,
        myScore: req.body.myScore,
        myPlays: req.body.myPlays
        }
    ).catch(function(err){
        console.log(err)
    })
}

module.exports.getHighScores = function(req, res){
    PlayerRecord.find().sort({myScore:-1}).then(function(myScore){
        console.log("Retrieving Scores")
        res.json({myScore})
    })
}

module.exports.getMostPlays = function(req, res){
    PlayerRecord.find().sort({myPlays:-1}).then(function(myPlays){
        console.log("Retrieving plays")
        res.json({myPlays})
    })
}

module.exports.getOneByName = function(req, res){
    console.log("Selected by myName", req.query.myName);
    PlayerRecord.findOne({"myName":req.query.myName}).then(function(gamedata){
        console.log(gamedata)
        res.json(gamedata)
    })
}

module.exports.saveEntry = function(req, res){
    var errors = []

    if(req.body.myName == ""){
        errors.push({text:"Name not added"})
    }
    if(req.body.myScore == 0){
        errors.push({text:"Score not added"})
    }
    if(req.body.myPlays == 0){
        errors.push({text:"Plays not added"})
    }

    if(errors.length > 0){
        console.log({
            errors:errors
        })
    } else {
        console.log("Hello from Unity Post ", req.body)
        var playerData = {
            myName:req.body.myName,
            myScore:req.body.myScore,
            myPlays:req.body.myPlays
        }
        console.log(playerData)
        PlayerRecord(playerData).save().then(function(data){
            console.log("Data Saved")
        }).catch(function(err){
            console.log(err)
        })
    }
}

module.exports.playerDelete = function(req, res){
    console.log(req.body.myName)
    PlayerRecord.deleteOne({"myName":req.body.myName}).then(function(){
        console.log("Entry deleted")
    }).catch(function(err){
        console.log(err)
    })
}

module.exports.testDelete = function(req, res){
    console.log("Deleted :D")
}

module.exports.testUpdate = function(req, res){
    console.log("Updated :D")
}