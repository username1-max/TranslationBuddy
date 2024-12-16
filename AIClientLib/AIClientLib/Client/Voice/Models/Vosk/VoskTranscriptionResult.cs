namespace AiClientLib.Client.Voice.Models.Vosk
{
    public class VoskTranscriptionResult
    {
        public List<Result> Result { get; set; } = new List<Result>();
        public string Text { get; set; } = string.Empty;
    }

    public class Result
    {
        public double Conf { get; set; }
        public double End { get; set; }
        public double Start { get; set; }
        public string Word { get; set; } = string.Empty;
    }
}
