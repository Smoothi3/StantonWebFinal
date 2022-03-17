var mainPage = function(req, res){
    res.render('index')
}

var highScore = function(req, res){
    res.render('highScore')
}

var editPage = function(req, res){
    res.render('editPage')
}

var loginPage = function(req, res){
    res.render('login')
}

var mostPlays = function(req, res){
    res.render('mostPlays')
}

module.exports = {
    mainPage,
    highScore,
    editPage,
    loginPage,
    mostPlays
}