using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Messages.Enums
{
    public enum BallLocations : int
    {
        // Don't start at 0 - int defaults to 0 so would be difficult to tell if data was correctly received or not.
        Tee = 1,
        Hole = 2,
        Other = 1000 // High value, leave space so that 'other' never changes but is always last
    }
}
