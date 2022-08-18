using Controller.Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Controller.Messages
{
    [Serializable]
    internal sealed class BallLocation : CommonMessage
    {
        [JsonPropertyName("location")]
        public BallLocations Location { get; init; }
    }
}
