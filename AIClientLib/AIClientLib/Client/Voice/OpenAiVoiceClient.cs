using System.Net.Http.Headers;

namespace AiClientLib.Client.Voice
{
    public class OpenAiVoiceClient : OpenAiClient, IVoiceClient
    {

        private const string Model = "whisper-1";

        private string TranscriptionUrl;
        private string TranslationUrl;

        private HttpClient HttpClient { get; }

        public OpenAiVoiceClient(HttpClient client)
        {
            this.TranscriptionUrl = $"{OpenAiClientBaseApiUrl}/v1/audio/transcriptions";
            this.TranslationUrl = $"{OpenAiClientBaseApiUrl}/v1/audio/translations";
            this.HttpClient = client;
            this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OpenAiClientApiKey);
        }

        public async Task<string> Transcribe(string filePath, Language language)
        {
            return await GetResult(filePath, TranscriptionUrl);
        }

        public async Task<string> Transcribe(MemoryStream stream, Language language)
        {
            return await GetResult(stream, TranscriptionUrl);
        }

        public async Task<string> TranslateToEnglish(string filePath)
        {
            return await GetResult(filePath, TranslationUrl);
        }

        public async Task<string> TranslateToEnglish(MemoryStream stream)
        {
            return await GetResult(stream, TranslationUrl);
        }

        private async Task<string> GetResult(string filePath, string resultUrl)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var fs = File.OpenRead(filePath))
                {
                    fs.CopyTo(memoryStream);
                    return await GetResult(memoryStream, resultUrl);
                }
            }
        }

        private async Task<string> GetResult(MemoryStream stream, string resultUrl)
        {
            using (stream)
            {
                var content = new MultipartFormDataContent
                    {
                        { new StringContent(Model), "model" },
                        { new StringContent("srt"), "response_format" },
                        { new ByteArrayContent(stream.ToArray()), "file", "audio.mp4" }
                    };
                var response = await HttpClient.PostAsync(resultUrl, content);
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
