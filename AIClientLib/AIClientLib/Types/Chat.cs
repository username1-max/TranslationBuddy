using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AiClientLib.Types
{
    public class ChatMessage
    {
        [JsonPropertyName("content")]
        public string Content { get; set; } = string.Empty;
        [JsonPropertyName("role")]
        public ChatRole Role { get; set; }
    }

    public enum ChatRole
    {
        [EnumMember(Value = "system")]
        System,
        [EnumMember(Value = "user")]
        User,
        [EnumMember(Value = "assistant")]
        Assistant
    }
}
