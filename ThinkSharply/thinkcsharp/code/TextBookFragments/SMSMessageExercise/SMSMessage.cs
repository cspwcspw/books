using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSMessageExercise
{
    class SMSMessage
    {
        public string MessageText { get; private set; }
        public string OtherNumber { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public bool HasBeenRead { get; private set; }

        public SMSMessage(string fromNum, string theMessage)
        {
            OtherNumber = fromNum;
            MessageText = theMessage;
            HasBeenRead = false;
            TimeStamp = DateTime.Now;
        }

        public void MarkAsRead()
        {
            HasBeenRead = true;
        }
    }
}
