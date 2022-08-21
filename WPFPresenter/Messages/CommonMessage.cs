using System;
using System.Text.Json.Serialization;

namespace WPFPresenter.Messages
{
    [Serializable]
    /// <summary>
    /// Host data common to all messages
    /// </summary>
    internal abstract class CommonMessage
    {
        [JsonPropertyName("score")]
        public int Score { get; init; }
        [JsonPropertyName("shots")]
        public int Shots { get; init; }
    }
}
