using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Messages
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
