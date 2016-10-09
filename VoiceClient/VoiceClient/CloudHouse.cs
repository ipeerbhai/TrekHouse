using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.Threading;
using LattePanda.Firmata;

namespace VoiceClient
{
    /// <summary>
    /// CloudHouse is the actual physical house's REST endpoints and methods.
    /// </summary>
    class CloudHouse
    {
        static bool m_bIsBlinking = false;
        //-------------------------------------------------------------------------------------------------------------------------------
        public async Task OpenWindow()
        {
            string URI = @"https://wt-23birdsonfire-gmail-com-0.run.webtask.io/door?webtask_no_cache=1";
            CancellationToken cancellationToken;

            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            var client = new HttpClient(handler);

            var request = new HttpRequestMessage(HttpMethod.Post, URI);
            HttpResponseMessage httpTask = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (!httpTask.IsSuccessStatusCode)
            {
                throw new Exception("invalid response status");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void BlinkLED()
        {
            if (!m_bIsBlinking)
            {
//                Task blinkTask = Task.Factory.StartNew();
                Arduino arduino = new Arduino();
                arduino.pinMode(13, Arduino.OUTPUT);//Set the digital pin 13 as output
                while (true)
                {
                    // ==== set the led on or off
                    arduino.digitalWrite(13, Arduino.HIGH);//set the LED　on
                    Thread.Sleep(1000);//delay a seconds
                    arduino.digitalWrite(13, Arduino.LOW);//set the LED　off
                    Thread.Sleep(1000);//delay a seconds
                }
            }
        }
    }
}
