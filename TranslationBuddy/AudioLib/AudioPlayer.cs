using AiClientLib.Client.Voice;
using AiClientLib.Client.Voice.Models.Vosk;
using System.Runtime.Versioning;
using System.Speech.Synthesis;

[assembly: SupportedOSPlatform("windows")]
namespace AudioLib
{
    public class AudioPlayer
    {
        public void Play(string text, Language language)
        {
            // Basic TTS
            SpeechSynthesizer synth = new();
            foreach (var voice in synth.GetInstalledVoices())
            {
                if (voice.VoiceInfo.Name == VoiceModelProvider.GetSynthesisVoice(language))
                {
                    synth.SelectVoice(voice.VoiceInfo.Name);
                    break;
                }
            }
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak(text);
        }
    }
}
