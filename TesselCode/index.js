
var http = require('https');
var tessel = require('tessel');

var statusCode = 200;
var count = 1;
var currentState = false;
var pin2 = tessel.port.A.pin[4];
var pin3 = tessel.port.A.pin[5];
var pin4 = tessel.port.A.pin[6];
var pin5 = tessel.port.A.pin[7];
var currentStateTemperature = false;

function turnOff() {
  pin2.output(0);
  pin3.output(0);
}

setImmediate(function start () {
  pin2.output(0);
  pin3.output(0);
  pin4.output(0);
  pin5.output(0);

  setInterval(function () {
  console.log('http request #' + (count++))
  http.get("https://wt-23birdsonfire-gmail-com-0.run.webtask.io/door?webtask_no_cache=1", function (res) {
    console.log('# statusCode', res.statusCode)
    var bufs = [];
    res.on('data', function (data) {
      bufs.push(new Buffer(data));
      console.log('# received', new Buffer(data).toString());
       obj = JSON.parse(data);
       if(currentState === false) {
        if(obj.doorOpen === true) {
          currentState = true;
          pin2.output(1);
          pin3.output(0);

        }
      } else {
        if(obj.doorOpen === false) {
          currentState = false;
          pin2.output(0);
          pin3.output(1);
        }
      }
      setTimeout(turnOff, 80);
    })
    res.on('close', function () {
      console.log('done.');
      setImmediate(start);
    })
  }).on('error', function (e) {
    console.log('not ok -', e.message, 'error event')
    setImmediate(start);
  });
 }, 2000); 
 setInterval(function () {
  console.log('http request #' + (count++))
  http.get("https://wt-abdullah13314-hotmail-com-0.run.webtask.io/Webtask2?webtask_no_cache=1", function (res) {
    console.log('# statusCode', res.statusCode)
    var bufs = [];
    res.on('data', function (data) {
      bufs.push(new Buffer(data));
      console.log('# received', new Buffer(data).toString());
       obj = JSON.parse(data);
       if(currentStateTemperature === false) {
        if(obj.highTemperature === true) {
          currentStateTemperature = true;
          pin4.output(1);
          pin5.output(0);
        }
      } else {
        if(obj.highTemperature === false) {
          currentStateTemperature = false;
          pin4.output(0);
          pin5.output(0);
        }
      }
    })
    res.on('close', function () {
      console.log('done.');
      setImmediate(start);
    })
  }).on('error', function (e) {
    console.log('not ok -', e.message, 'error event')
    setImmediate(start);
  });
 }, 2000); 
});