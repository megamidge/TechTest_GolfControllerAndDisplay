using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    [Serializable]
    public class Packet
    {
        public string ContentType;
        public string Content;

        public Packet(string contentType, string content)
        {
            
            ContentType = contentType;
            Content = content;
        }
    }
}
