// Set up dependency injection
using AiClientLib.Client;
using AiClientLib.Client.Voice;
using AiClientLib.Client.Voice.Models.Vosk;
using AudioLib;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Versioning;
using System.Text;
using TranslationBuddy;

[assembly: SupportedOSPlatform("windows")]

var serviceProvider = new ServiceCollection()
    .AddSingleton<HttpClient>()
    .AddSingleton<IVoiceClient, OpenSourceVoiceClient>()
    .AddSingleton<OpenAiChatClient>()
    .AddSingleton<AudioRecorder>()
    .AddSingleton<AudioPlayer>()
    .AddSingleton<TranslationBuddyApp>()
    .BuildServiceProvider();

VoiceModelProvider.Load();

// Resolve dependencies and run the application
var app = serviceProvider.GetService<TranslationBuddyApp>();

if (app != null)
{
    Console.OutputEncoding = Encoding.UTF8;
    await app.Run();
}
