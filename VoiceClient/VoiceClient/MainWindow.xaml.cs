using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech;
using System.Speech.Recognition;
using Microsoft.Speech;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Windows.Media.Animation;

namespace VoiceClient
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpeechRecognitionEngine sre = null; // this is the MS speech 11 recognizer from Vista, can be downloaded online.
        private string[] m_StartStopCommands = { "trek house call sam", "trek house open window", "trek house close window" };
        static bool m_bCallInitiated = false;

        public MainWindow()
        {
            if (sre == null)
            {
                CultureInfo ci = new CultureInfo("en-us");
                sre = new SpeechRecognitionEngine(ci);
                sre.SetInputToDefaultAudioDevice();
                Choices ch_StartStopCommands = new Choices();
                foreach (string strThisStartOrStopCommand in m_StartStopCommands)
                {
                    ch_StartStopCommands.Add(strThisStartOrStopCommand);
                }
                GrammarBuilder gb_StartStop = new GrammarBuilder();
                gb_StartStop.Append(ch_StartStopCommands);
                Grammar startStopGrammar = new Grammar(gb_StartStop);

                // register the handler and start the speechrecognizer thread
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.LoadGrammarAsync(startStopGrammar);
                sre.RecognizeAsync(RecognizeMode.Multiple); // multiple grammars
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void BeginGlyph(string storyboardName)
        {
            Storyboard S = (Storyboard)TryFindResource(storyboardName);
            if (S != null)
                S.Begin();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        private void EndGlyph(string storyboardName)
        {
            Storyboard S = (Storyboard)TryFindResource(storyboardName);
            if (S != null)
                S.Stop();
        }

        //-------------------------------------------------------------------------------------------------------------------------------
        public void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {

            // pull the data about the speech we heard out of the event.
            string txt = e.Result.Text;
            float confidence = e.Result.Confidence;
            CloudHouse myTinyhouse = new CloudHouse();
            LocalSpeechSynthesizer mysynth = new LocalSpeechSynthesizer();

            if (confidence < 0.60)
            {
                return; // we are not sure what we heard.
            }
            BeginGlyph("hearingBoard");

            // check to see what we got from speech, then dispatch to appropriate handler.
            if (
                (txt.ToLowerInvariant().Contains("call")) &&
                (txt.ToLowerInvariant().Contains("sam"))
                )
            {
                // Make a phone call.
                if (!m_bCallInitiated)
                {
                    // start a task to do the work.
                    mysynth.asyncTalk("Calling sam.");
                    Task RunningTask = Task.Factory.StartNew(() =>
                    {
                        //string url = @"file:///C:/Github/TrekHouse/Kandy%20Content/IMtest-User1.html?address=1";
                        string url = @"https://ipeerbhai.github.io/TrekHouse/Kandy%20Content/AutoCallSam.html";
                        IWebDriver driver = new ChromeDriver(@"C:\ChromeDriver");
                        driver.Navigate().GoToUrl(url);
                        driver.Manage().Window.Maximize();
                        IWebElement element = driver.FindElement(By.XPath("/"));
                        element.SendKeys(Keys.Tab);
                        element.SendKeys(Keys.Tab);
                        element.SendKeys(Keys.Enter);
                        m_bCallInitiated = true;
                    });

                }
            }
            else if 
                (
                (txt.ToLowerInvariant().Contains("open")) &&
                (txt.ToLowerInvariant().Contains("window"))
                )
            {
                mysynth.asyncTalk("Opening the window.");
                myTinyhouse.OpenWindow();
            }
            else if
                (
                (txt.ToLowerInvariant().Contains("close")) &&
                (txt.ToLowerInvariant().Contains("window"))
                )
            {
                mysynth.asyncTalk("Closing the window.");
                myTinyhouse.OpenWindow();
            }

            EndGlyph("hearingBoard");

        }

    }
}