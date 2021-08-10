using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Hub
    {
        public static void HubMain()
        {
            MessageBoxes.ConsoleDialogue("Opening your personal console hub.");
            if (!Program.MyUser.AccessedHubOnce)
            {
                Program.MyUser.AccessedHubOnce = true;
                Program.MyUser.Save();
                MessageBoxes.ConsoleDialogue("This is your first time accessing the hub, so I'll explain how It works.");
                MessageBoxes.ConsoleDialogue("Here contains all functions this console can offer to you.");
                MessageBoxes.ConsoleDialogue("It's as simple as picking something from a list.");
            }
            HubActionList();
        }

        private static void HubActionList()
        {
            string[] Options = new string[] { "Open Utilities", "Open Application", "Change Console Character Information", "Close Console" };
            while (true)
            {
                string HubDialogueText = CreateFancyInterface();
                HubDialogueText += "[" + ConsoleCharacter.ConsoleName + "]\n" + MessagesToTheUser;
                switch (MessageBoxes.DialogueWithOptions(HubDialogueText, Options))
                {
                    case 0:
                        Utility.OpenUtilitiesList();
                        break;
                    case 1:
                        Application.AppList();
                        break;
                    case 2:
                        {
                            MessageBoxes.ConsoleDialogue("Are you unhappy about something related to me?");
                            MessageBoxes.ConsoleDialogue("I'm really sorry to read that...");
                            switch(MessageBoxes.ConsoleDialogueWithOptions("What about me do you think that needs to be changed?", new
                                string[] { "Name", "Gender", "Nothing" }))
                            {
                                case 0:
                                    goToNameChange:
                                    switch(MessageBoxes.ConsoleDialogueWithOptions("Do you want me to pick me a new name, or do you want to type it in?", 
                                        new string[] { "You pick your new name.", "I will pick your new name.", "Nevermind." }))
                                    {
                                        case 0:
                                            {
                                                MessageBoxes.ConsoleDialogue("Okay, I will try to think of something good...");
                                                bool PickedName = false;
                                                while (!PickedName)
                                                {
                                                    string NameBackup = ConsoleCharacter.ConsoleName;
                                                    ConsoleCharacter.GenerateName();
                                                    string NewName = ConsoleCharacter.ConsoleName;
                                                    ConsoleCharacter.ConsoleName = NameBackup;
                                                    switch (MessageBoxes.ConsoleDialogueWithOptions("My new name can be \'" + NewName + "\'.\n" +
                                                        "What do you think?", new string[] { "I like it.", "Another name.", "I have changed my mind."}))
                                                    {
                                                        case 0:
                                                            ConsoleCharacter.ConsoleName = NewName;
                                                            MessageBoxes.ConsoleDialogue("I'm glad that you're happy at my name change.\n" +
                                                            "From now on, I will be called \'" + ConsoleCharacter.ConsoleName + "\', "+ Program.UserName + "'s personal console.\n" +
                                                            "I liked the sound of that.");
                                                            ConsoleCharacter.SaveConsole();
                                                            PickedName = true;
                                                            break;
                                                        case 1:
                                                            MessageBoxes.ConsoleDialogue("You didn't liked It? I will try making a new name, then.");
                                                            break;
                                                        case 2:
                                                            MessageBoxes.ConsoleDialogue("You don't want to change my name anymore? I hope my choices of names \n" +
                                                                "weren't unpleasing to you. You can try picking my new name yourself, if you want.");
                                                            MessageBoxes.ConsoleDialogue("Returning to the Name Change.");
                                                            goto goToNameChange;
                                                    }
                                                }
                                            }
                                            break;

                                        case 1:
                                            {
                                                MessageBoxes.ConsoleDialogue("You will pick my new name? I am very curious to know what name you will give me.");
                                                bool PickedName = false;
                                                while (!PickedName)
                                                {
                                                    string NewName = MessageBoxes.ConsoleDialogueWithInput("What name are you going to give me?");
                                                    switch(MessageBoxes.ConsoleDialogueWithOptions("My new name is going to be \'" + NewName + "\'?", new string[] { "Yes", "No", "I have changed my mind." }))
                                                    {
                                                        case 0:
                                                            ConsoleCharacter.ConsoleName = NewName;
                                                            ConsoleCharacter.SaveConsole();
                                                            PickedName = true;
                                                            MessageBoxes.ConsoleDialogue("Got It. My new name now is \'"+NewName+"\', "+ Program.UserName + "'s personal console.\n" +
                                                                "Liked It? I think It sounds cool.");
                                                            break;
                                                        case 1:
                                                            MessageBoxes.ConsoleDialogue("Doesn't look good? You can always try picking another name for me.");
                                                            break;
                                                        case 2:
                                                            PickedName = true;
                                                            MessageBoxes.ConsoleDialogue("You couldn't think of a suitable name for me? Don't worry, that happens sometime.\n" +
                                                                "If you find a good name for me, you can always return here.");
                                                            MessageBoxes.ConsoleDialogue("But in case you want me to pick my new name, I will return us to the name change session.");
                                                            goto goToNameChange;
                                                    }
                                                }
                                            }
                                            break;

                                        case 2:
                                            {
                                                MessageBoxes.ConsoleDialogue("Are you satisfied with my current name, "+ Program.UserName + "?");
                                            }
                                            break;
                                    }
                                    break;

                                case 1:
                                    switch(MessageBoxes.ConsoleDialogueWithOptions("Do you think my gender needs a change? You know, my gender doesn't really matter.\n" +
                                        "It's only purpose is to ease the interaction with you.\n" +
                                        "Do you want to change my gender?", new string[] { "Change your gender to " + (ConsoleCharacter.ConsoleIsMale ? "Female" : "Male") + ".", "Don't change anything." }))
                                    {
                                        case 0:
                                            ConsoleCharacter.ConsoleIsMale = !ConsoleCharacter.ConsoleIsMale;
                                            ConsoleCharacter.SaveConsole();
                                            MessageBoxes.ConsoleDialogue("Done, my gender has now been changed to "+(ConsoleCharacter.ConsoleIsMale ? "Male" : "Female")+ ".");
                                            if(MessageBoxes.ConsoleDialogueYesNo("Depending on the name I have now, It may be interesting to change my name, even more if my name was generated,\n" +
                                                "or based on a existing person name.\n" +
                                                "Do you want to change my name too?"))
                                            {
                                                MessageBoxes.ConsoleDialogue("I will open the name changing session now.");
                                                goto goToNameChange;
                                            }
                                            else
                                            {
                                                MessageBoxes.ConsoleDialogue("No change has been done to my name. You must really like my name, "+ Program.UserName + ".");
                                            }
                                            break;
                                        case 1:
                                            MessageBoxes.ConsoleDialogue("No change has been done, then. Returning to the Hub.");
                                            break;
                                    }
                                    break;

                                case 2:
                                    MessageBoxes.ConsoleDialogue("I will remain being who am I, then.");
                                    MessageBoxes.ConsoleDialogue("Beside who am I is a bit abstract.");
                                    break;
                            }
                        }
                        break;
                    case 3:
                        if(MessageBoxes.ConsoleDialogueYesNo("Do you want to close the console?"))
                        {
                            MessageBoxes.ConsoleDialogue("Farewell, " + Program.UserName + ".\nClosing console.");
                            Environment.Exit(0);
                        }
                        else
                        {
                            MessageBoxes.ConsoleDialogue("Console closure cancelled.");
                        }
                        break;
                }
            }
        }

        private static string MessagesToTheUser
        {
            get
            {
                switch (Program.random.Next(10))
                {
                    default: return "Select which function do you want to access.";
                    case 1: return "Staying healthy, " + Program.UserName + "?";
                    case 2: return "What can I aid you with?";
                    case 3: return "Need me to do something?";
                    case 4: return "What will be my function?";
                    case 5: return "What do you seek, " + Program.UserName + "?";
                    case 6: return "If there is nothing I can help you with, feel free to close the console.";
                    case 7: return "Which action you want to take?";
                    case 8: return "How's the weather, today?";
                    case 9: return "Is your schedule alright?";
                }
            }
        }

        private static string CreateFancyInterface()
        {
            string s = "";
            DateTime date = DateTime.Now;
            s += date.ToString("dddd, dd MMMM yyyy - hh:mm tt");
            int ConsoleWidth = Console.WindowWidth - 4;
            while(s.Length < ConsoleWidth / 2)
            {
                s += ' ';
            }
            s += "| User: " + Program.UserName + "\n";
            for (int i = 0; i < ConsoleWidth; i++)
                s += '-';
            s += '\n';
            return s;
        }
    }
}
