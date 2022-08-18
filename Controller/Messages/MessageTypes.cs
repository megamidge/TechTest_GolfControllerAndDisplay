using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Messages
{
    /// <summary>
    /// An enum would also work but would require some attributes to get a string value - this does the same job; Not going to actually enumerate or use the message type further than string comparisons.
    /// Perhaps not the greatest setup but function over form for the scope of this initially.
    /// </summary>
    public static class MessageTypes {
        public const string HelloWorld = "helloworld";
        public const string BallLocation = "location";
        public const string Feature = "feature";
        public const string Status = "status";
    }

}
