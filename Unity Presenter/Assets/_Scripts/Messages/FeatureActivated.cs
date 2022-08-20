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
    public sealed class FeatureActivated : CommonMessage
    {
        [JsonProperty("feature")]
        public Feature Feature;
    }
}
