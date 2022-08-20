using Newtonsoft.Json;
using System;

namespace Assets._Scripts.Messages
{
    [Serializable]
    /// <summary>
    /// Host data common to all messages
    /// </summary>
    public abstract class CommonMessage
    {
        [JsonProperty("score")]
        public int Score;
        [JsonProperty("shots")]
        public int Shots;
    }
}
