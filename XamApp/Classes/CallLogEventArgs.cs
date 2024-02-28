using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Classes
{
    public class CallLogEventArgs:EventArgs
    {
        public List<CallLogEntry> callLogEntries { get; set; }
    }
}
