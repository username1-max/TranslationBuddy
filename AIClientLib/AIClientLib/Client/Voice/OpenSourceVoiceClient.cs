using System.Text.Json;
using AiClientLib.Client.Voice.Models.Vosk;
using Vosk;

namespace AiClientLib.Client.Voice
{
    public class OpenSourceVoiceClient : IVoiceClient
    {
        public Task<string> Transcribe(string filePath, Language language)
        {
            throw new NotImplementedException();
        }

        public Task<string> Transcribe(MemoryStream stream, Language language)
        {
            return Task.FromResult(this.GetResult(stream, language));
        }

        public Task<string> TranslateToEnglish(string filePath)
        {
            throw new NotImplementedException();
        }

        public Task<string> TranslateToEnglish(MemoryStream stream)
        {
            throw new NotImplementedException();
        }

        public string GetResult(MemoryStream stream, Language language)
        {
            Model model = VoiceModelProvider.GetModel(language);
            VoskRecognizer rec = new VoskRecognizer(model, 16000.0f);
            rec.SetMaxAlternatives(0);
            rec.SetWords(true);
            using (stream)
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (rec.AcceptWaveform(buffer, bytesRead))
                    {
                        Console.WriteLine(rec.Result());
                    }
                    else
                    {
                        Console.WriteLine(rec.PartialResult());
                    }
                }
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            VoskTranscriptionResult result = JsonSerializer.Deserialize<VoskTranscriptionResult>(rec.FinalResult(), options)
                ?? new VoskTranscriptionResult();

            return result.Text;
        }
    }
}
