using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Classes
{
    public class SmsEntity
    {
        public string Address { get; set; }
        public string Body { get; set; }

        public SmsEntity(string address, string body)
        {
            Address = address;
            Body = body;
        }
    }
}
