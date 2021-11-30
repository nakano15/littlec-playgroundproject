using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Applications.AgendaAppClasses;

namespace ConsoleApp1.Applications
{
    public class Agenda : Application
    {
        private static List<Event> Events = new List<Event>();
        private static DateTime Today = new DateTime();

        public Agenda() : base("Agenda")
        {

        }

        public override void OnLoad()
        {

        }

        public override void StartProgram()
        {
            Today = DateTime.Now;
            string LobbyMessage = "Welcome to the agenda. What do you want to do?";
        lobby:
            const int LobbyCheckEvents = 0, LobbyAddEvent = 1, LobbyCloseBtn = 2;
            switch (MessageBoxes.ConsoleDialogueWithOptions(LobbyMessage, 
                new string[] { "Check Events", "Add New Event", "Close" }))
            {
                case LobbyCheckEvents:
                    {
                        int EventPages = Events.Count / 10;
                        int Page = 0;
                        do
                        {
                            List<string> Options = new List<string>();
                            byte OtherOptionsPosition = 0, NextPageButtonPos = 255, PreviousPageButtonPos = 255, CloseButtonPos = 255;
                            for (int i = 0; i < 10; i++)
                            {
                                int index = i + Page * 10;
                                if (index >= Events.Count)
                                    break;
                                OtherOptionsPosition++;
                                Options.Add(Events[index].EventName);
                            }
                            if (Page < EventPages)
                            {
                                NextPageButtonPos = (byte)Options.Count;
                                Options.Add("Next Page");
                            }
                            if (Page > 0)
                            {
                                PreviousPageButtonPos = (byte)Options.Count;
                                Options.Add("Previous Page");
                            }
                            CloseButtonPos = (byte)Options.Count;
                            Options.Add("Return");
                            int PickedOption = MessageBoxes.ConsoleDialogueWithOptions("Which event do you want to check?\n\nPage ["+(Page + 1)+"/"+(EventPages + 1)+"]", Options.ToArray());
                            if(PickedOption == CloseButtonPos)
                            {
                                LobbyMessage = "Do you want to check something else?";
                                break;
                            }
                            else if(PickedOption == NextPageButtonPos)
                            {
                                Page++;
                            }
                            else if(PickedOption == PreviousPageButtonPos)
                            {
                                Page--;
                            }
                            else
                            {
                                Event e = Events[PickedOption + Page * 10];
                                string EventInfoString = "Event: " + e.EventName + "\n" +
                                    "Description: " + e.Description;
                                if (e.StartDay.HasValue)
                                    EventInfoString = "\nStarts Date: " + e.StartDay.Value.ToLongDateString();
                                if (e.EndDay.HasValue)
                                    EventInfoString = "\nEnd Date: " + e.EndDay.Value.ToLongDateString();
                                {
                                    string DaysItHappensString = "";
                                    byte DaysOfWeek = 0;
                                    for(byte i = 0; i < 7; i++)
                                    {
                                        if (e.DaysOfWeekEventHappens[i])
                                        {
                                            DaysOfWeek++;
                                            switch (i)
                                            {
                                                case 0:
                                                    DaysItHappensString += "[Sun] ";
                                                    break;
                                                case 1:
                                                    DaysItHappensString += "[Mon] ";
                                                    break;
                                                case 2:
                                                    DaysItHappensString += "[Tue] ";
                                                    break;
                                                case 3:
                                                    DaysItHappensString += "[Wed] ";
                                                    break;
                                                case 4:
                                                    DaysItHappensString += "[Thu] ";
                                                    break;
                                                case 5:
                                                    DaysItHappensString += "[Fri] ";
                                                    break;
                                                case 6:
                                                    DaysItHappensString += "[Sat] ";
                                                    break;
                                            }
                                        }
                                    }
                                    if (DaysOfWeek == 7)
                                        DaysItHappensString = "Any Weekday";
                                    EventInfoString += "\nWeek Days Active: " + DaysItHappensString;
                                }
                                const int EventName = 0, EventDesc = 1, EventStartDate = 2, EventEndDate = 3,
                                    EventDaysOfWeek = 4, EventDelete = 5, EventReturn = 6;
                            returnToEventManaging:
                                int PickedEventManagementItem = 0;
                                switch (PickedEventManagementItem = MessageBoxes.ConsoleDialogueWithOptions(EventInfoString, 
                                    new string[] { "Change Event Name",
                                        "Change Event Description", "Change Start Date",
                                        "Change End Date", "Change Days of Week",
                                        "Delete Event", "Return" }))
                                {
                                    case EventName:
                                        {
                                            string NewName = MessageBoxes.ConsoleDialogueWithInput("Input the new event name.\nIf you want to cancel name change, just leave the text field empty.\nPrevious Name: " + e.EventName);
                                            if(NewName == "")
                                            {
                                                MessageBoxes.ConsoleDialogue("No change was done, then.");
                                            }
                                            else
                                            {
                                                e.EventName = NewName;
                                                MessageBoxes.ConsoleDialogue("Event name was changed to \""+NewName+"\".");
                                            }
                                            goto returnToEventManaging;
                                        }

                                    case EventDesc:
                                        {
                                            string NewDesc = MessageBoxes.ConsoleDialogueWithInput("Input the new event description.\n" +
                                                "If you want to cancel description change, leave the text field empty.\n" +
                                                "Previous Description: " + e.Description);
                                            if(NewDesc != "")
                                            {
                                                MessageBoxes.ConsoleDialogue("No change was done to the description.");
                                            }
                                            else
                                            {
                                                e.Description = NewDesc;
                                                MessageBoxes.ConsoleDialogue("Description changed successfully.\nNew Description: " + NewDesc);
                                            }
                                        }
                                        goto returnToEventManaging;

                                    case EventStartDate:
                                    case EventEndDate:
                                        {
                                            bool IsEndDate = PickedEventManagementItem == EventEndDate;
                                            bool HasDaySet = (IsEndDate ? e.EndDay.HasValue : e.StartDay.HasValue);
                                            returnToDateManagement:
                                            if (!HasDaySet)
                                            {
                                                if(MessageBoxes.ConsoleDialogueWithOptions("This event has no "+(IsEndDate ? "End" : "Start")+" date set. Create one?",new string[] { "Yes", "No" }) == 0)
                                                {
                                                    DateTime NewDate = DateTime.Now;
                                                    if (IsEndDate)
                                                        e.EndDay = NewDate;
                                                    else
                                                        e.StartDay = NewDate;
                                                    MessageBoxes.ConsoleDialogue("Date created. Now you have to setup it.");
                                                    HasDaySet = true;
                                                    goto returnToDateManagement;
                                                }
                                                else
                                                {
                                                    MessageBoxes.ConsoleDialogue("No change has been done.");
                                                }
                                            }
                                            else
                                            {
                                                DateTime date = (IsEndDate ? e.EndDay.Value : e.StartDay.Value);
                                                ChangeDateMethod(ref date);
                                                if (IsEndDate) e.EndDay = date;
                                                else e.StartDay = date;
                                            }
                                        }
                                        goto returnToEventManaging;
                                }
                            }
                        } while (true);
                    }
                    goto lobby;

                case LobbyAddEvent:
                    break;

                case LobbyCloseBtn:
                    MessageBoxes.ConsoleDialogue("Returning to the hub.");
                    return;
            }
        }

        public void ChangeDateMethod(ref DateTime date)
        {
            const int DayChange = 0, MonthChange = 1, YearChange = 2, HourChange = 3, MinuteChange = 4, Close = 5;
            while (true)
            {
                switch (MessageBoxes.ConsoleDialogueWithOptions("Current date set is: " + date.ToShortDateString() + "\nCurrent hour set is: " + date.ToShortTimeString() + "\nWant to change something about it?", new string[] {
                "Change Day", "Change Month", "Change Year", "Change Hour", "Change Minute", "Close" }))
                {
                    case DayChange:
                        {
                            int MaxDateDays = DateTime.DaysInMonth(date.Year, date.Month) + 1;
                            returnToDayChange:
                            int PickedDay = MessageBoxes.ConsoleDialogueWithNumberInput("Input a day between 1 and " + MaxDateDays + ".\nType 0 to cancel.");
                            if (PickedDay == 0)
                            {
                                MessageBoxes.ConsoleDialogue("Cancelling day change.");
                            }
                            else if(PickedDay < 1 || PickedDay > MaxDateDays)
                            {
                                MessageBoxes.ConsoleDialogue("Invalid day set.");
                                goto returnToDayChange;
                            }
                            else
                            {
                                date = new DateTime(date.Year, date.Month, PickedDay, date.Hour, date.Minute,date.Second);
                                MessageBoxes.ConsoleDialogue("Day changed.");
                            }
                        }
                        break;
                    case MonthChange:
                        {
                            string[] MonthsList = new string[]{
                                "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", "Change nothing."
                                };
                            int NewMonth = MessageBoxes.ConsoleDialogueWithOptions("Pick the month the event will happen:", MonthsList);
                            if(NewMonth == 12)
                            {
                                MessageBoxes.ConsoleDialogue("Changed your mind? Nothing will be changed then.");
                            }
                            else
                            {
                                date = new DateTime(date.Year, NewMonth + 1, date.Day, date.Hour, date.Minute, date.Second);
                                MessageBoxes.ConsoleDialogue("New month set to " + MonthsList[NewMonth] + ".");
                            }
                        }
                        break;
                    case YearChange:
                        {
                            int ThisYear = DateTime.Now.Year;
                            retryTypingYear:
                            int NewYear = MessageBoxes.ConsoleDialogueWithNumberInput("Input a year between 1 and 9999.\nIt doesn't makes much sense setting the year to anything before " + ThisYear + ".\nOr, you can type 0 to cancel that.");
                            if(NewYear == 0)
                            {
                                MessageBoxes.ConsoleDialogue("The year got no changes.");
                            }
                            else
                            {
                                if(NewYear < ThisYear)
                                {
                                    //TODO - Continue coding this.
                                }
                            }
                        }
                        break;
                    case HourChange:
                        break;
                    case MinuteChange:
                        break;
                    case Close:
                        MessageBoxes.ConsoleDialogue("Altering date done.");
                        return;
                }
            }
        }
    }
}
