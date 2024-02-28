using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Classes
{
    public class CalendarEventArgs : EventArgs
    {
        public List<CalendarEvent> calendarEvents { get; set; }
    }
}
