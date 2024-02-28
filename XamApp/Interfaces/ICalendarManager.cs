using System;
using System.Collections.Generic;
using System.Text;
using XamApp.Classes;

namespace XamApp.Interfaces
{
    public interface ICalendarManager
    {
        event EventHandler CalendarHandler;
        void getCalendarEventList();
    }
}
