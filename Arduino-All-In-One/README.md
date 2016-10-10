Arduino-All-In-One.ino - A program for the Arduino Software IDE.  It reads data on the supersonic distance measurement device and the BME280, which measures pressure, temperature, and humidity.

The main loop monitors the devices and executes commands when conditions chagne.

When the temperature rises above 80 degrees F, a command is issued to begin the fan.  When the temperature drops below 77 degrees F, a command is issued to halt the fan.

When humidity rises above 75% relative humidity, a command is issued to open the window.  When the humidity falls below 60% relative humidity, a command is issued to close the window.
