using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Classes
{
    public class CallLogEntry
    {
        public string Number { get; }
        public string Name { get; }
        public long Date { get; }
        public int Duration { get; }

        public CallLogEntry(string number, string name, long date, int duration)
        {
            Number = number;
            Name = name;
            Date = date;
            Duration = duration;
        }
    }
}
