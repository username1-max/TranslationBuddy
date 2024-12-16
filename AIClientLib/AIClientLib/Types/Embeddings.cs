using System.Text.Json.Serialization;

namespace AiClientLib.Types
{
    public class EmbeddingRequest
    {
        [JsonPropertyName("input")]
        public string Input { get; set; } = string.Empty;

        [JsonPropertyName("model")]
        public string Model { get; private set; } = "text-embedding-3-small";

        [JsonPropertyName("encoding_format")]
        public string EncodingFormat { get; private set; } = "float";
    }

    public class EmbeddingsApiResponse
    {
        [JsonPropertyName("object")]
        public string? Object { get; set; }

        [JsonPropertyName("data")]
        public EmbeddingResult[]? Data { get; set; }

        [JsonPropertyName("model")]
        public string? Model { get; set; }

        [JsonPropertyName("usage")]
        public EmbeddingsApiUsage? Usage { get; set; }
    }

    public class EmbeddingResult
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("embedding")]
        public double[]? Embedding { get; set; }

        [JsonPropertyName("object")]
        public string? Object { get; set; }
    }

    public class EmbeddingsApiUsage
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
