using AiClientLib.Client;
using AiClientLib.Types;
using AiClientLib.Client.Voice;
using AudioLib;

namespace TranslationBuddy
{
    public class TranslationBuddyApp(
        IVoiceClient voiceClient,
        OpenAiChatClient chatClient,
        AudioPlayer audioPlayer,
        AudioRecorder audioRecorder)
    {
        private IVoiceClient VoiceClient { get; } = voiceClient;
        private OpenAiChatClient ChatClient { get; } = chatClient;

        private AudioPlayer AudioPlayer { get; } = audioPlayer;

        private AudioRecorder AudioRecorder { get; } = audioRecorder;

        public async Task Run()
        {
            while (true)
            {
                Console.Write($"Provide source language (1 for {Language.English}; 2 for {Language.Spanish}): ");
                Language sourceLanguage = (Language)(int.Parse(Console.ReadLine() ?? "1") - 1);
                Console.WriteLine();

                Console.Write($"Provide target language (1 for {Language.English}; 2 for {Language.Spanish}): ");
                Language targetLanguage = (Language)(int.Parse(Console.ReadLine() ?? "1") - 1);
                Console.WriteLine();

                Console.WriteLine("Press 'Enter' to start recording...");
                Console.ReadKey(); // Wait for the user to press Enter to start recording
                this.AudioRecorder.StartRecording();

                Console.WriteLine("Press 'Enter' again to stop recording...");
                Console.ReadKey(); // Wait for the user to press Enter to stop recording
                this.AudioRecorder.StopRecording();

                var result = await this.VoiceClient.Transcribe(this.AudioRecorder.GetAudioStream(), sourceLanguage);

                Console.WriteLine($"Text to translate: {result}");
                Console.WriteLine();

                if (result != null)
                {
                    var translation = await ChatClient.GetResponse(new List<ChatMessage>()
                    {
                        new ChatMessage()
                        {
                            Role = ChatRole.System,
                            Content = $"You are a {nameof(sourceLanguage)} to {nameof(targetLanguage)} translator. You respond with the translation."
                        },
                        new ChatMessage()
                        {
                            Role = ChatRole.User,
                            Content = result
                        }
                    });

                    Console.WriteLine($"Translation is: {translation}");

                    if (!string.IsNullOrWhiteSpace(translation))
                    {
                        this.AudioPlayer.Play(translation, targetLanguage);
                    }
                }
            }
        }
    }
}
