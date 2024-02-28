using System;
using System.Collections.Generic;
using System.Text;

namespace XamApp.Interfaces
{
    public interface ICallJournalManager
    {
        event EventHandler CallJournalHandler;
        void getCallJournalList();
    }
}
