using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        public static Random random = new Random();

        static void Main(string[] args)
        {
            TestConsoleNames();
            MessageBoxes.SimpleDialogue("Hello and good day!");
            CreateNewUser();
        }

        private static void TestConsoleNames()
        {
            string Message = "";
            for(int i = 0; i < 10; i++)
            {
                if (i > 0)
                    Message += "\n";
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

        public static void CreateNewUser()
        {
            UserInfo user = new UserInfo();
            user.IsMale = MessageBoxes.DialogueWithOptions("Are you a boy, or a girl?", new string[] { "Boy", "Girl" }) == 0;
            if (!MessageBoxes.DialogueYesNo("Good to read that you're a " + (user.IsMale ? "Boy" : "Girl") + ".\nIs that right, by the way?"))
            {
                bool Done = false;
                while (!Done)
                {
                    MessageBoxes.SimpleDialogue("You picked the wrong option then?");
                    user.IsMale = MessageBoxes.DialogueWithOptions("Let me ask you again... Are you a boy, or a girl?", new string[] { "Boy", "Girl" }) == 0;
                    Done = MessageBoxes.DialogueYesNo("You are a " + (user.IsMale ? "Boy" : "Girl") + ", right?");
                }
                MessageBoxes.SimpleDialogue("Good to see that the confusion has been solved.");
            }
            user.Name = MessageBoxes.DialogueWithInput("Sorry for asking that after just meeting you, maybe I should begin asking this instead:\nWhat is your name?");
            if (MessageBoxes.DialogueYesNo("So, your name is '" + user.Name + "'?"))
            {
                MessageBoxes.SimpleDialogue("So glad to get It right.");
            }
            else
            {
                bool GotItRight = false;
                while (!GotItRight)
                {
                    MessageBoxes.SimpleDialogue("Then you typed It wrong.");
                    user.Name = MessageBoxes.DialogueWithInput("What is your name?");
                    GotItRight = MessageBoxes.DialogueYesNo("Your name is '" + user.Name + "'?");
                }
                MessageBoxes.SimpleDialogue("I'm glad that you got It right this time.");
            }
            MessageBoxes.SimpleDialogue("I'm sorry for asking too many infos, but this setup is really necessary.");
            user.Age = (ushort)MessageBoxes.DialogueWithNumberInput("Well, how old are you?");
            if (user.Age <= 3)
            {
                MessageBoxes.SimpleDialogue("I think that you're lying, but this is your infos anyways. Let's hope nothing important needs those.");
            }
            else if (user.Age <= 7)
            {
                MessageBoxes.SimpleDialogue("Aren't you too young for this?\nAnyways, my role is to aid you, not judge.");
            }
            else if (user.Age >= 100)
            {
                MessageBoxes.SimpleDialogue("You're THAT old?! Wow! I don't know if I should congratulate you, or ask if you're actually lying.\nAnyways, congratulations on that feat!");
            }
            else if (user.Age >= 60)
            {
                MessageBoxes.SimpleDialogue("You're really old. People of your age tend to stay at home enjoying the rest of their lives, while others still works on what they love. Which one of those are you?");
            }
            else if (user.Age >= 18)
            {
                MessageBoxes.SimpleDialogue("You're already grown up? I partially regret calling you a " + (user.IsMale ? "boy" : "girl") + " now. No... I don't plan on lowering your self steem, I mean...");
            }
            if(MessageBoxes.DialogueWithOptions("Sorry, I shouldn't be judging your age. Is your age really '" + user.Age + "'? Or did you do an oopsie?", new string[] { "The age is right." , "I did an oopsie." }) == 1)
            {
                bool Done = false;
                while (!Done)
                {
                    MessageBoxes.SimpleDialogue("You did an oopsie. Well, that may happen sometimes, or did you wanted to see what I would say?");
                    user.Age = (ushort)MessageBoxes.DialogueWithNumberInput("Whatever the reason is, it doesn't matter. Can you tell me your correct age now?");
                    Done = MessageBoxes.DialogueYesNo("I see... Your age is '" + user.Age + "'?");
                }
                MessageBoxes.SimpleDialogue("Perfect, I logged that in.");
            }
            MessageBoxes.SimpleDialogue("Wow, you're '" + user.Age + "'... I wonder how much you've passed through to reach that age.");
            MessageBoxes.SimpleDialogue("Me? Well.... This is my first year old...");
            MessageBoxes.SimpleDialogue("Sorry, this shouldn't be about me. Anyways, we still need to know your country.");
            user.Country = MessageBoxes.DialogueWithInput("Which country do you come from?");
            if(!MessageBoxes.DialogueYesNo("You live in '" + user.Country + "'?"))
            {
                MessageBoxes.SimpleDialogue("You typed something wrong? Or didn't understud the question?\nMy question asks which country in your world you live.");
                bool Done = false;
                while (!Done)
                {
                    user.Country = MessageBoxes.DialogueWithInput("Tell me, which country do you live on?");
                    Done = MessageBoxes.DialogueYesNo("Your country is '" + user.Country + "'?");
                    if (!Done)
                        MessageBoxes.SimpleDialogue("Are you tripping on your fingers? Don't worry, sometimes that happens when typing.");
                }
                MessageBoxes.SimpleDialogue("Whew, done. Sorry for my reaction, but mistakes tires me a bit, so I have to answer people again if their infos are right.\n" +
                    "You also feel tired when doing mistakes and repeating everything again, right?");
            }

        }
    }
}
