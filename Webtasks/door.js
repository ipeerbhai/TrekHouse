/*
https://wt-23birdsonfire-gmail-com-0.run.webtask.io/door?webtask_no_cache=1
  POST: (no body) will flip the state of the door
  GET: returns JSON object: { doorOpen: boolean }
*/

module.exports = function(ctx, req, res) {

  if (req.method === "POST") {
    ctx.storage.get(function (error, data) {
        if (error) return cb(error);
        var update;
        if (data) {
          update = { doorOpen: !data.doorOpen };
        } else {
          update = { doorOpen: false };
        }
        ctx.storage.set(update, function (error) {
            if (error) return cb(error);
            res.writeHead(200, { 'Content-Type': 'application/json '});
            res.end();
        });
    });
  } else {
    ctx.storage.get(function (error, data) {
      res.writeHead(200, { 'Content-Type': 'application/json '});
      res.end(JSON.stringify(data));
    });
  }
}