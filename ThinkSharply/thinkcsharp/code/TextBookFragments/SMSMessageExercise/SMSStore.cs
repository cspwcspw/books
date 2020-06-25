using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSMessageExercise
{
    class SMSStore
    {
        private List<SMSMessage> content;

        public SMSStore()
        {
            content = new List<SMSMessage>();
        }

        public void AddSMS(string fromNumber, string textOfSMS)
        {
            SMSMessage theMsg = new SMSMessage(fromNumber, textOfSMS);
            content.Add(theMsg);
        }

        public int GetCount()
        {
            return content.Count;
        }

        public List<int> GetUnreadIndexes()
        {
            List<int> result = new List<int>();
            for (int i = 0; i < content.Count; i++)
            {
                if (!content[i].HasBeenRead)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public SMSMessage GetMessage(int i)
        {
            return content[i];
        }

        public void Remove(int i)
        {
            content.RemoveAt(i);
        }

        public void Clear()
        {
            content.Clear();
        }
    }
}
