namespace AiClientLib.Client.Voice
{
    public interface IVoiceClient
    {
        public abstract Task<string> Transcribe(string filePath, Language language);

        public abstract Task<string> Transcribe(MemoryStream stream, Language language);

        public abstract Task<string> TranslateToEnglish(string filePath);

        public abstract Task<string> TranslateToEnglish(MemoryStream stream);
    }
}
