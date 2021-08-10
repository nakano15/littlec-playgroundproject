using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Utility
    {
        public static void OpenUtilitiesList()
        {
            while(true)
            {
                switch(MessageBoxes.ConsoleDialogueWithOptions("Those are the utilities I can offer to you.\n" +
                    "Which one do you wish to open?", new string[] { "Month Expense Calculator", "Return" }))
                {
                    case 0:
                        Utilities.MonthExpenseCalculator.Run();
                        break;
                    case 1:
                        MessageBoxes.ConsoleDialogue("Returning to the Hub.");
                        return;
                }
            }
        }
    }
}
