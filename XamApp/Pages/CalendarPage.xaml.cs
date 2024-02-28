using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamApp.Classes;
using XamApp.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        ICalendarManager _calendarManager;
        ISmsManager _smsManager;
        ICallJournalManager _callJournalManager;
        public CalendarPage()
        {
            InitializeComponent();
            _calendarManager = DependencyService.Get<ICalendarManager>();
            _calendarManager.CalendarHandler += getList_Event;

            _smsManager= DependencyService.Get<ISmsManager>();
            _smsManager.SmsHandler += getSms_Event;

            _callJournalManager = DependencyService.Get<ICallJournalManager>();
            _callJournalManager.CallJournalHandler += getCallJournal_Event;

        }
        private Label CreateLabel(string text)
        {
            var label = new Label();
            label.FontSize = 20;
            label.Text = text;
            return label;

        }
        public void getCallJournal_Event(object sender, EventArgs e)
        {

        var args = (CallLogEventArgs)e;
            eventsStack.Children.Clear();
            foreach (var ev in args.callLogEntries)
            {
                var label = CreateLabel($"Number: {ev.Number}, Name:{ev.Name}, Date:{ev.Date}, Duration: {ev.Duration}");
                eventsStack.Children.Add(label);
            }

        }

        public void getList_Event(object sender,EventArgs  e)
        {
        var args = (CalendarEventArgs)e;
            eventsStack.Children.Clear();
            foreach(var ev in args.calendarEvents)
            {
                var label = CreateLabel($"Title: {ev.Title}, Description:{ev.Description}, Start time:{ev.StartTime}, EndTime: {ev.EndTime}");
                eventsStack.Children.Add(label);
            }
        }
        public void getSms_Event(object sender, EventArgs e)
        {
            var args = (SmsEventArgs)e;
            eventsStack.Children.Clear();
            foreach (var ev in args.smsEntities)
            {
                var label = CreateLabel($"Body: {ev.Body}, Address:{ev.Address}");
                eventsStack.Children.Add(label);
            }

        }
        async Task<PermissionStatus> isCalendarPermission()
        {
            return await Permissions.CheckStatusAsync<Permissions.CalendarRead>();
        }
        async Task<PermissionStatus> isSmsPermission()
        {
            return await Permissions.CheckStatusAsync<Permissions.Sms>();
        }
        async Task<PermissionStatus> isContactsReadPermissions()
        {
            return await Permissions.CheckStatusAsync<Permissions.ContactsRead>();
        }
        async void callSmsList()
        {
            if (PermissionStatus.Granted != await isSmsPermission())
            {
                var status = await Permissions.RequestAsync<Permissions.Sms>();
            }
            _smsManager.getAllSmsList();
        }
        async void callCalendarList()
        {
            if (PermissionStatus.Granted != await isCalendarPermission())
            {
                var status = await Permissions.RequestAsync<Permissions.CalendarRead>();
            }
            _calendarManager.getCalendarEventList();
        }
        async void getCallJournalList()
        {
            if (PermissionStatus.Granted != await isContactsReadPermissions())
            {
                var status = await Permissions.RequestAsync<Permissions.ContactsRead>();
            }
            _callJournalManager.getCallJournalList();
        }

        private void showCalendarEvents(object sender, EventArgs e)
        {
            callCalendarList();
        }
        private void showAllSms(object sender, EventArgs e)
        {
            callSmsList();
        }
        private void showCallJournal(object sender, EventArgs e)
        {
            getCallJournalList();
        }
    }
}