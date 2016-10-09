using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoiceClient
{
    class LocalSpeechSynthesizer
    {
        private System.Speech.Synthesis.SpeechSynthesizer m_mainTalker;
        //-------------------------------------------------------------------------------------------------------------------------------
        public LocalSpeechSynthesizer()
        {
            Init();
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        public void Init()
        {
            System.Speech.Synthesis.SpeechSynthesizer mySynth = new System.Speech.Synthesis.SpeechSynthesizer();
            var Voices = mySynth.GetInstalledVoices();
            for (int count = 0; count < Voices.Count; count++)
            {
                if (Voices[count].VoiceInfo.Name.ToLowerInvariant().Contains("hazel"))
                {
                    mySynth.SelectVoice(Voices[count].VoiceInfo.Name);
                    break;
                }
            }
            m_mainTalker = mySynth;
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        public void talk(string textTSpeak)
        {
            m_mainTalker.Speak(textTSpeak);
        }
        //-------------------------------------------------------------------------------------------------------------------------------
        public Task asyncTalk(string textToSpeak)
        {
            Task speakTask = Task.Factory.StartNew(() => {
                m_mainTalker.Speak(textToSpeak);
            });

            return (speakTask);
        }

    }
}
