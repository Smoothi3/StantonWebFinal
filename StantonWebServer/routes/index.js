var express = require('express');
var router = express.Router();
var mongoose = require('mongoose');
var bodyParser = require('body-parser');
const { json } = require('body-parser');
var ctrlGameData = require('./gameDataController')
var ctrlLocations = require('../routes/locations')

router.use(bodyParser.json())
router.use(bodyParser.urlencoded({extended:true}))
router.use(express.json())

require("../models/PlayerRecords")
var PlayerRecord = mongoose.model("playerRecords")

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Stanton Final' });
});

router.get('/highScore', ctrlLocations.highScore)
router.get('/editPage', ctrlLocations.editPage)
router.get('/loginPage', ctrlLocations.loginPage)
router.get('/playsPage', ctrlLocations.mostPlays)

router.get('/unity', ctrlGameData.getAllGameData)

router.get('/unityGetOne', ctrlGameData.getOneByName)

router.post('/unityPost', ctrlGameData.saveEntry)

router.post('/unityDeleteEntry', ctrlGameData.deleteEntry)

router.put('/unityUpdate', ctrlGameData.updateEntry)

router.get('/getHighScores', ctrlGameData.getHighScores)

router.get('/getMostPlays', ctrlGameData.getMostPlays)

router.post('/DELETE', ctrlGameData.playerDelete)

router.post('/PUT', ctrlGameData.updateEntry)

module.exports = router;
