from BaseHTTPServer import BaseHTTPRequestHandler
import requests
import json
import serial
fullPath = 'https://wt-23birdsonfire-gmail-com-0.run.webtask.io/door?webtask_no_cache=1'

class RequestHandler(BaseHTTPRequestHandler):
  #Handler for the GET requests

  def do_POST(self):
    print (self.path)
    fullPath = 'https://wt-23birdsonfire-gmail-com-0.run.webtask.io/door?webtask_no_cache=1'
    r = requests.get(fullPath)
    data = r.json()
    self.send_response(200)
    self.send_header('Content-type', 'application/json')
    self.send_header('Access-Control-Allow-Origin', '*')
    self.send_header('Access-Control-Allow-Headers', '*')
    self.send_header('Access-Control-Allow-Methods', '*')
    self.end_headers()
    self.wfile.write(json.dumps(data))
    return

try:
  ser = serial.Serial(port='COM5', baudrate=9600, parity=serial.PARITY_NONE, stopbits=serial.STOPBITS_ONE, bytesize=serial.EIGHTBITS, timeout=0)
  print("connected to: " + ser.portstr)
  count=1
  while True:
    line = ser.readLine()
    if "door" in line:
      r = requests.post(fullPath)
except KeyboardInterrupt:
  ser.close()
  print('^C received, shutting down the web server')
