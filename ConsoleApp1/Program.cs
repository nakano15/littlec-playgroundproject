using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static Random random = new Random();
        public static UserInfo MyUser = null;
        public const string UsersSaveFolder = "Users", ConsoleFile = "ConsoleInfos.sav";
        public static List<Application> Apps = new List<Application>();
        private const string MagicWords = "nakanoisawesomewoahraccoonsarecool";
        public static string UserName
        {
            get
            {
                if (MyUser == null)
                    return "User";
                return MyUser.Name;
            }
        }

        static void Main(string[] args)
        {
            //TestConsoleNames();
            ConsoleCharacter.LoadConsole();
            MessageBoxes.ConsoleDialogue("Hello and good "+(GetTimeOfDayString(false)) +"!");
            if (ConsoleCharacter.CreatedConsoleCharacter)
            {
                MessageBoxes.ConsoleDialogue("If you don't know me yet, I'm " + ConsoleCharacter.ConsoleName+ ", your personal console.");
            }
            else
            {
                if(!MessageBoxes.ConsoleDialogueYesNo("If this is your first time opening this console, be sure that It's placed inside a folder for itself.\n" +
                    "This program will create folders to save information necessary for it, and would be horrible having your personal things mixed up with this program informations.\n" +
                    "In case you didn't placed this program inside its own folder, select No, and the program will close."))
                {
                    MessageBoxes.ConsoleDialogue("I will be closing the program now. Place It inside a folder before opening it again.");
                    Environment.Exit(0);
                }
                else
                {
                    MessageBoxes.ConsoleDialogue("Since this program is inside it's folder, based on what you said, then we can continue.");
                }
            }
            returnToUserSelection:
            UserInfo.UserSelection();
            if (!ConsoleCharacter.CreatedConsoleCharacter)
            {
                ConsoleCharacter.CreateConsoleCharacter();
            }
            if(MyUser == null)
            {
                MessageBoxes.ConsoleDialogue("Something went wrong. Somehow, your user files weren't loaded.\n" +
                    "Please try again.");
                goto returnToUserSelection;
            }
            Application.LoadApps();
            Hub.HubMain();
        }

        public static string GetTimeOfDayString(bool UppercasedStart)
        {
            int Hour = DateTime.Now.Hour;
            string ToD;
            if (Hour < 4 || Hour >= 21)
            {
                ToD = "Night";
            }
            else if (Hour >= 18)
                ToD = "Evening";
            else if (Hour >= 12)
                ToD = "Day";
            else
                ToD = "Morning";
            if (!UppercasedStart)
                return ToD.ToLower();
            return ToD;
        }

        public static bool IsDay()
        {
            return DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 18;
        }

        public static char GetGenderChar(bool IsMale)
        {
            return IsMale ? '♂' : '♀';
        }

        private static void TestConsoleNames()
        {
            string Message = "";
            for(int i = 0; i < 50; i++)
            {
                if (i > 0)
                    Message += "\n";
                ConsoleCharacter.ConsoleIsMale = true;
                ConsoleCharacter.GenerateName();
                Message += "Male: " + ConsoleCharacter.ConsoleName;
                ConsoleCharacter.ConsoleIsMale = false;
                ConsoleCharacter.GenerateName();
                Message += "    Female: " + ConsoleCharacter.ConsoleName;
                Message += "    ";
                ConsoleCharacter.ConsoleIsMale = true;
                ConsoleCharacter.GenerateName();
                Message += "Male: " + ConsoleCharacter.ConsoleName;
                ConsoleCharacter.ConsoleIsMale = false;
                ConsoleCharacter.GenerateName();
                Message += "    Female: " + ConsoleCharacter.ConsoleName;
            }
            MessageBoxes.SimpleDialogue(Message);
            Environment.Exit(0);
        }
    }
}
