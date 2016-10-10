import requests
import serial
fullPath = 'https://wt-23birdsonfire-gmail-com-0.run.webtask.io/door?webtask_no_cache=1'
fullPathFire = 'https://wt-abdullah13314-hotmail-com-0.run.webtask.io/Webtask2?webtask_no_cache=1'
try:
  ser = serial.Serial(port='COM5', baudrate=9600, parity=serial.PARITY_NONE, stopbits=serial.STOPBITS_ONE, bytesize=serial.EIGHTBITS, timeout=0)
  print("connected to: " + ser.portstr)
  count=1
  while True:
    line = str(ser.readline())
    if len(line) > 5:
      print(line)
    if "WINDOW" in line:
      r = requests.post(fullPath)
    if "FAN" in line:
      r = requests.post(fullPathFire)
except KeyboardInterrupt:
  ser.close()
  print('^C received, shutting down the web server')
