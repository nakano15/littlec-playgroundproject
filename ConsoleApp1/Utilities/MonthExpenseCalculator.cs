using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Utilities
{
    public class MonthExpenseCalculator
    {
        public static void Run()
        {
            bool[] WeekDaysToCount = new bool[7];
            for (int i = 1; i < 6; i++)
                WeekDaysToCount[i] = true;
            List<int> DaysToIgnore = new List<int>();
            float ExpensePerDay = 0;
            MessageBoxes.ConsoleDialogue("Opening Month Expense Calculator.\n" +
                "This utility will allow you to calculate how much money in expense you will be spending on the month.\n" +
                "The days counted will be based on today until the last day of this month.");
            DateTime dt = DateTime.Now;
            bool YearsAgoCommentMade = false;
            while (true)
            {
                string Mes = "Calculation based on [c:Yellow]" + GetMonthName(dt.Month) + ", " + dt.Year + "[r].\n" +
                    "Calculations starting from day [c:Yellow]" + dt.Day + "[r].\n" +
                    "Money Change Per Day: [c:Red]$" + ReturnMonetaryValue(ExpensePerDay) + "[r].\n";
                //Show weekdays to count
                {
                    Mes += "Weekdays to Count: [c:Blue]";
                    bool FirstDay = true;
                    for(int i = 0; i < 7;  i++)
                    {
                        if (WeekDaysToCount[i])
                        {
                            if (!FirstDay)
                                Mes += ", ";
                            FirstDay = false;
                            Mes += Enum.GetName(typeof(DayOfWeek), i);
                        }
                    }
                    if (!FirstDay)
                        Mes += ".";
                    else
                        Mes += "[c:Red]None?";
                    Mes += "[r]\n";
                }
                //Show Days to Ignore
                {
                    Mes += "Days to Ignore: [c:Cyan]";
                    bool First = true;
                    foreach(int d in DaysToIgnore)
                    {
                        if (!First)
                            Mes += ", ";
                        First = false;
                        Mes += d.ToString();
                    }
                    if (!First)
                        Mes += ".";
                    else
                        Mes += "[r]None.";
                    Mes += "[r]\n";
                }
                //Show total expenses
                {
                    int MonthDays = DateTime.DaysInMonth(dt.Year, dt.Month);
                    float TotalValue = 0;
                    for(int i = dt.Day; i <= MonthDays; i++)
                    {
                        if (DaysToIgnore.Contains(i))
                            continue;
                        DateTime day = new DateTime(dt.Year, dt.Month, i);
                        if (WeekDaysToCount[(int)day.DayOfWeek])
                        {
                            TotalValue += ExpensePerDay;
                        }
                    }
                    Mes += "Your total expense until the next month: [c:Red]$" + ReturnMonetaryValue(TotalValue) + "[r].";
                }
                switch (MessageBoxes.ConsoleDialogueWithOptions(Mes, 
                    new string[] { "Change Monetary Value", "Set Weekdays to Count", "Setup Days To Ignore", "Change Date", "Close" }))
                {
                    case 0:
                        ExpensePerDay = MessageBoxes.ConsoleDialogueWithDecimalInput("Set how much is spent per day.");
                        MessageBoxes.ConsoleDialogue("Monetary value changed.");
                        break;
                    case 1:
                        switch(MessageBoxes.ConsoleDialogueWithOptions("Which weekdays should I count?", 
                            new string[] { "Monday to Friday", "Saturday and Sunday", "All Days", "I'll Pick", "Return" }))
                        {
                            case 0:
                                WeekDaysToCount[0] = WeekDaysToCount[6] = false;
                                for (int i = 1; i < 6; i++)
                                    WeekDaysToCount[i] = true;
                                MessageBoxes.ConsoleDialogue("Will only count days from Monday until Friday, now.");
                                break;
                            case 1:
                                WeekDaysToCount[0] = WeekDaysToCount[6] = true;
                                for (int i = 1; i < 6; i++)
                                    WeekDaysToCount[i] = false;
                                MessageBoxes.ConsoleDialogue("Will only count days from Saturday and Sunday, now.");
                                break;
                            case 2:
                                for (int i = 0; i < 7; i++)
                                    WeekDaysToCount[i] = true;
                                MessageBoxes.ConsoleDialogue("All days will now be count.");
                                break;
                            case 3:
                                {
                                    bool Done = false;
                                    while (!Done)
                                    {
                                        string DaysEnabled = "";
                                        bool First = true;
                                        for (int i = 0; i < 7; i++)
                                        {
                                            if (WeekDaysToCount[i])
                                            {
                                                if (First)
                                                    DaysEnabled += ", ";
                                                First = false;
                                                DaysEnabled += Enum.GetName(typeof(DayOfWeek), i);
                                            }
                                        }
                                        if (DaysEnabled.Length > 0)
                                            DaysEnabled += ".";
                                        else
                                            DaysEnabled = "None.";
                                        int PickedDay = MessageBoxes.ConsoleDialogueWithOptions("Days of the Week currently counting: \n" + DaysEnabled + "\nWhich day do you want to flip on/off?",
                                            new string[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "I'm done here." });
                                        if (PickedDay < 7)
                                        {
                                            WeekDaysToCount[PickedDay] = !WeekDaysToCount[PickedDay];
                                            MessageBoxes.ConsoleDialogue(Enum.GetName(typeof(DayOfWeek), PickedDay) + " has been " + (WeekDaysToCount[PickedDay] ? "Enabled" : "Disabled") + ".");
                                        }
                                        else if (PickedDay == 7)
                                        {
                                            Done = true;
                                            MessageBoxes.ConsoleDialogue("Let's check how much expense this month will have, now.");
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case 2:
                        {
                            bool Done = false;
                            while (!Done)
                            {
                                string IgnoredDays = "";
                                bool First = true;
                                foreach(int i in DaysToIgnore)
                                {
                                    if (!First)
                                        IgnoredDays += ", ";
                                    First = false;
                                    IgnoredDays += i.ToString();
                                }
                                if (IgnoredDays.Length == 0) IgnoredDays = "None.";
                                switch(MessageBoxes.ConsoleDialogueWithOptions("Current days ignored: \n" +
                                    IgnoredDays + "\nWhich action do you want to take?", new string[]{
                                        "Add Day", "Remove Day", "Return"
                                        }))
                                {
                                    case 0:
                                        int d = MessageBoxes.ConsoleDialogueWithNumberInput("Insert the day number.");
                                        if (d < 1 || d > DateTime.DaysInMonth(dt.Year, dt.Month))
                                        {
                                            MessageBoxes.ConsoleDialogue("That's not a valid day.\n" +
                                                "You can only pick days between 1 and " + DateTime.DaysInMonth(dt.Year, dt.Month) + ".");
                                        }
                                        else if (DaysToIgnore.Contains(d))
                                        {
                                            MessageBoxes.ConsoleDialogue("That day has already been added to the list.");
                                        }
                                        else
                                        {
                                            DaysToIgnore.Add(d);
                                            MessageBoxes.ConsoleDialogue("Day " + d + " will now be ignored.");
                                        }
                                        break;
                                    case 1:
                                        {
                                            if (DaysToIgnore.Count == 0)
                                            {
                                                MessageBoxes.ConsoleDialogue("There are no ignored days here...");
                                            }
                                            else
                                            {
                                                bool Return = false;
                                                while (!Return)
                                                {
                                                    List<string> DayList = new List<string>();
                                                    foreach (int i in DaysToIgnore)
                                                    {
                                                        DayList.Add("Day " + i);
                                                    }
                                                    int ReturnPosition = DayList.Count;
                                                    DayList.Add("Return");
                                                    int PickedOption = MessageBoxes.ConsoleDialogueWithOptions("Which day you want to remove from ignore list?", DayList.ToArray());
                                                    if (PickedOption < ReturnPosition)
                                                    {
                                                        if (MessageBoxes.ConsoleDialogueYesNo("Do you want to delete " + DayList[PickedOption] + "?"))
                                                        {
                                                            DaysToIgnore.RemoveAt(PickedOption);
                                                        }
                                                        else
                                                        {
                                                            MessageBoxes.ConsoleDialogue("Removal aborted.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Return = true;
                                                        MessageBoxes.ConsoleDialogue("Let's return then.");
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case 2:
                                        {
                                            MessageBoxes.ConsoleDialogue("Returning to days setup.");
                                            Done = true;
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case 3:
                        {
                            bool DoneWithDateChange = false;
                            while (!DoneWithDateChange) {
                                switch (MessageBoxes.ConsoleDialogueWithOptions("What do you want to change from the date?\n" +
                                    "I recommend setting up Year and Month before day, since they reset day back to 1.", new string[] { "Year", "Month", "Start Day", "Nothing" }))
                                {
                                    case 0:
                                        {
                                            returnToYearCalculation:
                                            int NewYear = MessageBoxes.ConsoleDialogueWithNumberInput("Which year do you want to calculate expenses of?");
                                            if (!YearsAgoCommentMade && NewYear < dt.Year)
                                            {
                                                MessageBoxes.ConsoleDialogue("Are you feeling nostalgic? Or is it for some kind of weird research?");
                                                MessageBoxes.ConsoleDialogue("Oh, sorry about this. I never expected someone to calculate something from years ago.");
                                                YearsAgoCommentMade = true;
                                            }
                                            if (MessageBoxes.ConsoleDialogueYesNo("Do you want to calculate expenses based on the year " + NewYear + "?"))
                                            {
                                                dt = new DateTime(NewYear, dt.Month, 1);
                                                MessageBoxes.ConsoleDialogue("Year changed to [c:Cyan]" + NewYear + "[r].\n" +
                                                    "Due to changing the year, I resetted the day back to 1.");
                                            }
                                            else
                                            {
                                                if(MessageBoxes.ConsoleDialogueYesNo("Did you do a typo? Or changed your mind?\n" +
                                                    "Do you want to try picking the new year to calculate?"))
                                                {
                                                    goto returnToYearCalculation;
                                                }
                                                else
                                                {
                                                    MessageBoxes.ConsoleDialogue("Then let's return to Date setup.");
                                                }
                                            }
                                        }
                                        break;
                                    case 1:
                                        MessageBoxes.ConsoleDialogue("We will be changing the month, then.");
                                        switch (MessageBoxes.ConsoleDialogueWithOptions("Current month is [c:Yellow][" + GetMonthName(dt.Month) + "][r].\n" +
                                            "Which month do you want to check expenses?",
                                    new string[] { "Next Month", "Pick Month", "Nevermind" }))
                                        {
                                            case 0:
                                                dt = dt.AddMonths(1);
                                                MessageBoxes.ConsoleDialogue("I will now calculate the expenses for [c:Yellow][" + GetMonthName(dt.Month) + "][r], since first day.");
                                                break;
                                            case 1:
                                                {
                                                    List<string> MonthNames = new List<string>();
                                                    for (int i = 1; i <= 12; i++)
                                                        MonthNames.Add(GetMonthName(i));
                                                    int ReturnPosition = MonthNames.Count;
                                                    MonthNames.Add("Nevermind");
                                                    int PickedOption = MessageBoxes.ConsoleDialogueWithOptions("Which month do you want to calculate?", MonthNames.ToArray());
                                                    if (PickedOption == ReturnPosition)
                                                    {
                                                        MessageBoxes.ConsoleDialogue("I didn't changed anything, then.");
                                                    }
                                                    else
                                                    {
                                                        string MonthName = MonthNames[PickedOption];
                                                        if (MessageBoxes.ConsoleDialogueYesNo("Do you want to change month to [c:Yellow][" + MonthName + "]?"))
                                                        {
                                                            dt = new DateTime(dt.Year, PickedOption + 1, 1);
                                                            DaysToIgnore.Clear();
                                                            MessageBoxes.ConsoleDialogue("Month changed to [c:Yellow][" + MonthName + "][r].\n" +
                                                                "I took the liberty of setting the day back to 1, and erasing the days ignored list to ease calculation.");
                                                        }
                                                        else
                                                        {
                                                            MessageBoxes.ConsoleDialogue("Oops. Reverting that...\n" +
                                                                "Done. Let's go back to date selection.");
                                                        }
                                                    }
                                                }
                                                break;
                                            case 2:
                                                MessageBoxes.ConsoleDialogue("It's fine to pick the wrong option some time.\n" +
                                                    "I have all the patience for that. :3");
                                                break;
                                        }
                                        break;
                                    case 2:
                                        {
                                            int MaxDaysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
                                            returnToDayPicking:
                                            int NewDay = MessageBoxes.ConsoleDialogueWithNumberInput("Pick the new day to start calculation.\n" +
                                                "Must be a day between 1 and " + MaxDaysInMonth + ".");
                                            if(NewDay < 1 || NewDay > MaxDaysInMonth)
                                            {
                                                if (MessageBoxes.ConsoleDialogueYesNo("That is not a valid day.\n" +
                                                    "Do you want to pick the day number again, or do you want to return to the date picking?"))
                                                    goto returnToDayPicking;
                                                MessageBoxes.ConsoleDialogue("Returning to Date picking...");
                                            }
                                            else
                                            {
                                                dt = new DateTime(dt.Year, dt.Month, NewDay);
                                                MessageBoxes.ConsoleDialogue("Day changed successfully! Yay!");
                                            }
                                        }
                                        break;
                                    case 3:
                                        DoneWithDateChange = true;
                                        MessageBoxes.ConsoleDialogue("With everything setup, let's see what the calculation will say.\n" +
                                            "Beside you may want to setup days to ignore, in case there's days in the way that shouldn't be counted.");
                                        break;
                                }
                            }
                        }
                        break;
                    case 4:
                        MessageBoxes.ConsoleDialogue("Ending Calculator.");
                        return;
                }
            }
        }

        public static string ReturnMonetaryValue(float Value)
        {
            string Result = "";
            byte DigitsPastComma = 0;
            bool PassedComma = false;
            foreach(char c in Value.ToString())
            {
                if(c == ',')
                {
                    PassedComma = true;
                    Result += c;
                }
                else
                {
                    if (PassedComma)
                    {
                        if(DigitsPastComma < 2)
                        {
                            Result += c;
                        }
                        DigitsPastComma++;
                    }
                    else
                    {
                        Result += c;
                    }
                }
            }
            if(PassedComma && DigitsPastComma < 2)
            {
                Result += '0';
            }
            return Result;
        }

        private static string GetMonthName(int Month)
        {
            switch (Month)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
            }
            return "Is this a new month?";
        }
    }
}
