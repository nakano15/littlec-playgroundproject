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
            while (true)
            {
                string Mes = "Money Change Per Day: $" + ExpensePerDay + "\n";
                Mes += "Weekdays to Count: ";
                {
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
                    Mes += "\n";
                }
                {
                    Mes += "Days to Ignore: ";
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
                    Mes += "\n";
                }
                DateTime dt = DateTime.Now;
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
                    Mes += "Your total expense until the next month: $" + Math.Round(TotalValue, 2) + ".";
                }
                switch (MessageBoxes.ConsoleDialogueWithOptions(Mes, 
                    new string[] { "Change Monetary Value", "Set Weekdays to Count", "Setup Days To Ignore", "Close" }))
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
                        MessageBoxes.ConsoleDialogue("Ending Calculator.");
                        return;
                }
            }
        }
    }
}
