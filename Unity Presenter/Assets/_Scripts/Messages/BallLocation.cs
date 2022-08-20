using Assets._Scripts.Messages.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Messages
{
    [Serializable]
    public sealed class BallLocation : CommonMessage
    {
        [JsonProperty("location")]
        public BallLocations Location;
    }
}
