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