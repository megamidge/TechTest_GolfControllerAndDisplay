using WPFPresenter.Messages.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WPFPresenter.Messages
{
    [Serializable]
    internal sealed class FeatureActivated : CommonMessage
    {
        [JsonPropertyName("feature")]
        public Feature Feature { get; set; }
    }
}
