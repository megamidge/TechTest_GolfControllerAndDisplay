using System;

namespace Assets._Scripts.Messages
{
    [Serializable]
    /// <summary>
    /// Host data common to all messages
    /// </summary>
    public abstract class CommonMessage
    {
        public int Score;

        public int Shots;
    }
}
