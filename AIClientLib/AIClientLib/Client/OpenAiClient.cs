namespace AiClientLib.Client
{
    public abstract class OpenAiClient
    {
        protected readonly string OpenAiClientBaseApiUrl = "https://api.openai.com";
        protected readonly string OpenAiClientApiKey = "<OpenAIApiKey here>";
        protected readonly string OpenAiClientOrgId = "<OpenAIOrgId here>";
    }
}
