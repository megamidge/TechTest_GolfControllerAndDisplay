using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Messages.Enums
{
    internal enum Feature : int
    {
        // Don't start at 0; int's default is 0, so if a 0 is sent (or received) we can detect error.
        SuperTube = 1,
        Hazard = 2
    }
}
