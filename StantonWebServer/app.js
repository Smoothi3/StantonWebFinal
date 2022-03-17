var createError = require('http-errors');
var express = require('express');
var exphbs = require('express-handlebars')
var path = require('path');
var cookieParser = require('cookie-parser');
var logger = require('morgan');
var bodyParser = require('body-parser')
var mongoose = require('mongoose');

var indexRouter = require('./routes/index');
var usersRouter = require('./routes/users');

mongoose.connect("mongodb://localhost:27017/StantonDB", {
  useNewUrlParser:true,
  useUnifiedTopology:true
}).then(function(){
  console.log("Connected to MongoDB")
}).catch(function(err){
  console.log(err)
})

var app = express();

app.use(bodyParser.urlencoded({extended:true}));

// view engine setup
app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'hbs');

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());
app.use(express.static(path.join(__dirname, 'public')));

app.use('/', indexRouter);
app.use('/users', usersRouter);

// catch 404 and forward to error handler
app.use(function(req, res, next) {
  next(createError(404));
});

// error handler
app.use(function(err, req, res, next) {
  // set locals, only providing error in development
  res.locals.message = err.message;
  res.locals.error = req.app.get('env') === 'development' ? err : {};

  // render the error page
  res.status(err.status || 500);
  res.render('error');
});

module.exports = app;

app.post('/highScore', function(req, res){
  console.log(req.body)
  res.render(200, '/highScore', req.body)
})

app.post('/editPage', function(req, res){
  console.log(req.body)
  res.render(200, '/editPage', req.body)
})

app.post('/loginPage', function(req, res){
  console.log(req.body)
  res.render(200, '/login', req.body)
})

app.post('/playsPage', function(req, res){
  console.log(req.body)
  res.render(200, 'mostPlays', req.body)
})

/*
app.listen(3000, function(){
  console.log("Listenting on port 3000")
})
*/
