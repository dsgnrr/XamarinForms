using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidApp = Android.App.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamApp.Classes;
using XamApp.Droid.AndroidServices;
using XamApp.Interfaces;
using Xamarin.Forms;
using Android.Database;
using AndroidX.Core.Content;
using Android;
using AndroidX.Core.App;

[assembly: Dependency(typeof(CalendarManager))]
namespace XamApp.Droid.AndroidServices
{
    public class CalendarManager : ICalendarManager, ISmsManager, ICallJournalManager
    {
        public event EventHandler CalendarHandler;
        public event EventHandler SmsHandler;
        public event EventHandler CallJournalHandler;

        public void getAllSmsList()
        {
            List<SmsEntity> smsList = new List<SmsEntity>();

            // Получение всех SMS входящих
            var contentResolver = AndroidApp.Context.ContentResolver;
            var cursor = contentResolver.Query(Android.Net.Uri.Parse("content://sms/inbox"), null, null, null, null);

            if (cursor != null)
            {
                while (cursor.MoveToNext())
                {
                    string address = cursor.GetString(cursor.GetColumnIndexOrThrow("address"));
                    string body = cursor.GetString(cursor.GetColumnIndexOrThrow("body"));

                    SmsEntity sms = new SmsEntity(address, body);
                    smsList.Add(sms);
                }

                cursor.Close();
                SmsHandler.Invoke(this, new SmsEventArgs() { smsEntities = smsList });
            }

        }
        public void getCalendarEventList()
        {
            var calendarsUri = CalendarContract.Events.ContentUri;
            string[] projection = {
            CalendarContract.Events.InterfaceConsts.Id,
            CalendarContract.Events.InterfaceConsts.Title,
            CalendarContract.Events.InterfaceConsts.Description,
            CalendarContract.Events.InterfaceConsts.Dtstart,
            CalendarContract.Events.InterfaceConsts.Dtend
        };
            var contentResolver = AndroidApp.Context.ContentResolver;
            Android.Database.ICursor cursor = contentResolver.Query(calendarsUri, projection, null, null, null);

            List<CalendarEvent> eventsList = new List<CalendarEvent>();

            if (cursor != null)
            {
                while (cursor.MoveToNext())
                {
                    var eventId = cursor.GetInt(cursor.GetColumnIndex(projection[0]));
                    var title = cursor.GetString(cursor.GetColumnIndex(projection[1]));
                    var description = cursor.GetString(cursor.GetColumnIndex(projection[2]));
                    var dtStart = cursor.GetLong(cursor.GetColumnIndex(projection[3]));
                    var dtEnd = cursor.GetLong(cursor.GetColumnIndex(projection[4]));

                    eventsList.Add(new CalendarEvent
                    {
                        Id = eventId,
                        Title = title,
                        Description = description,
                        StartTime = DateTimeOffset.FromUnixTimeMilliseconds(dtStart).DateTime,
                        EndTime = DateTimeOffset.FromUnixTimeMilliseconds(dtEnd).DateTime
                    });
                }

                cursor.Close();
                CalendarHandler.Invoke(this, new CalendarEventArgs() { calendarEvents = eventsList });
            }
        }

        public void getCallJournalList()
        {
            List<CallLogEntry> callLogList = new List<CallLogEntry>();
            string[] projection = {
                CallLog.Calls.Number,
                CallLog.Calls.CachedName,
                CallLog.Calls.Date,
                CallLog.Calls.Duration
            };

            if (ContextCompat.CheckSelfPermission(AndroidApp.Context, Manifest.Permission.ReadCallLog) != Android.Content.PM.Permission.Granted)
            {
                ActivityCompat.RequestPermissions(MainActivity.mainActivity, new string[] { Manifest.Permission.ReadCallLog }, 453454); 
            }
            var contentResolver = AndroidApp.Context.ContentResolver;
            ICursor callLogCursor = contentResolver.Query(
                CallLog.Calls.ContentUri,
                projection,
                null,
                null,
                CallLog.Calls.DefaultSortOrder
            );

            if (callLogCursor != null)
            {
                while (callLogCursor.MoveToNext())
                {
                    string number = callLogCursor.GetString(callLogCursor.GetColumnIndex(projection[0]));
                    string name = callLogCursor.GetString(callLogCursor.GetColumnIndex(projection[1]));
                    long date = callLogCursor.GetLong(callLogCursor.GetColumnIndex(projection[2]));
                    int duration = callLogCursor.GetInt(callLogCursor.GetColumnIndex(projection[3]));

                    CallLogEntry entry = new CallLogEntry(number, name, date, duration);
                    callLogList.Add(entry);
                }
                callLogCursor.Close();
                CallJournalHandler.Invoke(this, new CallLogEventArgs() { callLogEntries = callLogList });
            }
        }
    }
}