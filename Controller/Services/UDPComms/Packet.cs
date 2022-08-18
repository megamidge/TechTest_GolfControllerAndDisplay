using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Services.UDPComms
{
    internal class Packet
    {
        public string ContentType { get; init; }
        public string Content { get; init; }

        public Packet(string contentType, string content)
        {
            ContentType = contentType;
            Content = content;
        }
    }
}
