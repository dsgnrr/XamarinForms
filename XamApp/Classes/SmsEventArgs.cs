using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Classes
{
    public class SmsEventArgs:EventArgs
    {
        public List<SmsEntity> smsEntities { get; set; }
    }
}
