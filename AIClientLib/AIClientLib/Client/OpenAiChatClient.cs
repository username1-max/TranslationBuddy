using AiClientLib.Serialization;
using AiClientLib.Types;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AiClientLib.Client
{
    public class OpenAiChatClient : OpenAiClient
    {
        private readonly string ApiUrl;

        private HttpClient HttpClient = new HttpClient();

        public OpenAiChatClient(HttpClient httpClient)
        {
            this.ApiUrl = $"{this.OpenAiClientBaseApiUrl}/v1/chat/completions";
            this.HttpClient = httpClient;

            // Add headers to the request
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {this.OpenAiClientApiKey}");
            HttpClient.DefaultRequestHeaders.Add("OpenAI-Organization", this.OpenAiClientOrgId);
        }

        public async Task<string?> GetResponse(List<ChatMessage>? chatInput)
        {
            var request = new ChatApiRequest()
            {
                Messages = chatInput
            };

            var options = new JsonSerializerOptions
            {
                Converters = { new JsonEnumMemberStringEnumConverter() }
            };

            string jsonString = JsonSerializer.Serialize(request, options);
            HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Make the POST request
            HttpResponseMessage response = await HttpClient.PostAsync(ApiUrl, content);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var apiResponse = JsonSerializer.Deserialize<ChatApiResponse>(responseBody, options);

            return apiResponse?.Choices?[0]?.Message?.Content;
        }

        public class ChatApiRequest
        {
            [JsonPropertyName("messages")]
            public List<ChatMessage>? Messages { get; set; }

            [JsonPropertyName("model")]
            public string Model { get; private set; } = "gpt-4o-mini";

            [JsonPropertyName("temperature")]
            public double Temperature { get; private set; } = 0.5;
        }

        public class ChatApiResponse
        {
            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [JsonPropertyName("object")]
            public string? Object { get; set; }

            [JsonPropertyName("created")]
            public int? Created { get; set; }

            [JsonPropertyName("model")]
            public string? Model { get; set; }

            [JsonPropertyName("system_fingerprint")]
            public string? SystemFingerprint { get; set; }

            [JsonPropertyName("choices")]
            public Choice[]? Choices { get; set; }

            [JsonPropertyName("usage")]
            public ChatApiUsage? Usage { get; set; }
        }

        public class Choice
        {
            [JsonPropertyName("index")]
            public int? Index { get; set; }

            [JsonPropertyName("message")]
            public ChatMessage? Message { get; set; }

            [JsonPropertyName("logprobs")]
            public bool? LogProbs { get; set; }

            [JsonPropertyName("finish_reason")]
            public string? FinishReason { get; set; }
        }

        public class ChatApiUsage
        {
            [JsonPropertyName("prompt_tokens")]
            public int PromptTokens { get; set; }

            [JsonPropertyName("completion_tokens")]
            public int CompletionTokens { get; set; }

            [JsonPropertyName("total_tokens")]
            public int TotalTokens { get; set; }
        }
    }
}
