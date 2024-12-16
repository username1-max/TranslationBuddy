using Vosk;

namespace AiClientLib.Client.Voice.Models.Vosk
{
    public static class VoiceModelProvider
    {
        private static readonly string BasePath = Path.Combine("<ProvideBasePathToModels>");

        private static Model EnglishModel;
        private static Model SpanishModel;
        private static Dictionary<Language, Model> Models;

        private static Dictionary<Language, string> SynthesisVoice = new Dictionary<Language, string>()
        {
            [Language.English] = "Microsoft David Desktop",
            [Language.Spanish] = "Microsoft Raul Desktop"
        };

        public static void Load()
        {
            VoiceModelProvider.EnglishModel = new Model(Path.Combine(VoiceModelProvider.BasePath, "<EnglishModelName>"));
            VoiceModelProvider.SpanishModel = new Model(Path.Combine(VoiceModelProvider.BasePath, "<SpanishModelName>"));

            VoiceModelProvider.Models = new Dictionary<Language, Model>()
            {
                [Language.English] = VoiceModelProvider.EnglishModel,
                [Language.Spanish] = VoiceModelProvider.SpanishModel
            };
        }

        public static Model GetModel(Language language)
        {
            return VoiceModelProvider.Models[language];
        }

        public static string GetSynthesisVoice(Language language)
        {
            return VoiceModelProvider.SynthesisVoice[language];
        }
    }
}
