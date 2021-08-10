using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ConsoleCharacter
    {
        public const int Version = 0;
        public static bool CreatedConsoleCharacter = false;
        public static string ConsoleName = "";
        public static bool ConsoleIsMale = true;
        private static readonly string[] NameSyllabesMale = new string[] { "ca", "mi", "tol", "no", "roq", "ko", "ne", "roo", "pu", "kel", "ko'", "kha", "ir", "jo", "ro", "piz", "rol", "j'" },
            NameSyllabesFemale = new string[] { "zo", "ya", "ma", "le", "nar", "han", "lo", "ra", "sar", "lay", "na", "mi'", "za", "'ko", "kat", "ni", "ta", "ka", "tsa", "ar", "las", "s'" };
        public const string ConsoleSavedirectory = "Console";
        public static DateTime ConsoleDoB = new DateTime();

        public static void SaveConsole()
        {
            if (!Directory.Exists(ConsoleSavedirectory))
            {
                Directory.CreateDirectory(ConsoleSavedirectory);
            }
            string ConsoleSaveFile = ConsoleSavedirectory + "/ConsoleInfo.sav";
            if (File.Exists(ConsoleSaveFile))
            {
                File.Delete(ConsoleSaveFile);
            }
            using (FileStream file = new FileStream(ConsoleSaveFile, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(file))
                {
                    writer.Write(Version);
                    writer.Write(ConsoleName);
                    writer.Write(ConsoleIsMale);
                    writer.Write(ConsoleDoB.ToFileTime());
                }
            }
        }

        public static void LoadConsole()
        {
            if (Directory.Exists(ConsoleSavedirectory))
            {
                string ConsoleSaveFile = ConsoleSavedirectory + "/ConsoleInfo.sav";
                if (File.Exists(ConsoleSaveFile))
                {
                    using (FileStream file = new FileStream(ConsoleSaveFile, FileMode.Open))
                    {
                        using (BinaryReader reader = new BinaryReader(file))
                        {
                            int Version = reader.ReadInt32();
                            ConsoleName = reader.ReadString();
                            ConsoleIsMale = reader.ReadBoolean();
                            ConsoleDoB = DateTime.FromFileTime(reader.ReadInt64());
                            CreatedConsoleCharacter = true;
                        }
                    }
                }
            }
        }

        public static void GenerateConsoleCharacter()
        {
            ConsoleIsMale = Program.random.NextDouble() < 0.5;
            GenerateName();
        }

        public static void GenerateName()
        {
            ConsoleName = "";
            List<byte> PossibleSyllabes = new List<byte>();
            string[] PickedSyllabes = ConsoleIsMale ? NameSyllabesMale : NameSyllabesFemale;
            for (byte b = 0; b < PickedSyllabes.Length; b++)
                PossibleSyllabes.Add(b);
            int SyllabeCount = Program.random.Next(2, 4);
            bool UsedASymbol = false;
            while(SyllabeCount > 0)
            {
                bool Success = true;
                int Picked = Program.random.Next(PossibleSyllabes.Count);
                string Syllabe = PickedSyllabes[PossibleSyllabes[Picked]];
                if ((Syllabe.Contains('\'') && UsedASymbol ) || (ConsoleName.Length == 0 && Syllabe[0] == '\'') || (SyllabeCount == 1 && Syllabe[Syllabe.Length - 1] == '\''))
                {
                    Success = false;
                }
                else
                {
                    if (Syllabe.Contains('\''))
                        UsedASymbol = true;
                    if (ConsoleName.Length == 0 && Syllabe.EndsWith("\'") && SyllabeCount < 3)
                        SyllabeCount++;
                    for (int i = 0; i < Syllabe.Length; i++)
                    {
                        if (ConsoleName.Length == 0)
                        {
                            ConsoleName += char.ToUpper(Syllabe[i]);
                        }
                        else
                        {
                            ConsoleName += Syllabe[i];
                        }
                    }
                }
                if (Success)
                {
                    PossibleSyllabes.RemoveAt(Picked);
                    SyllabeCount--;
                }
            }
        }

        public static void CreateConsoleCharacter()
        {
            ConsoleDoB = DateTime.Now;
            MessageBoxes.SimpleDialogue("Now I need your help with something.");
            MessageBoxes.SimpleDialogue("I don't have an identity... So there is no way you can reference to me.");
            MessageBoxes.SimpleDialogue("It will be difficulty for us to comunicate if I don't even have a name...");
            MessageBoxes.SimpleDialogue("And calling me a \'console\' isn't much of a personality, itself...");
            MessageBoxes.SimpleDialogue("I know! What If I create one for myself? You can even help me pick if the name is fine or not.");
            MessageBoxes.SimpleDialogue("Let's see... Who could I be...?");
            GenerateConsoleCharacter();
            if(!MessageBoxes.DialogueYesNo("What about this? My name is " + ConsoleName + " " + Program.GetGenderChar(ConsoleIsMale) + ".\n" +
                "What do you think?"))
            {
                byte TimesAskedForName = 0;
                MessageBoxes.SimpleDialogue("You didn't liked? Let's see if I can pick another identity...");
                while (true)
                {
                    bool RetriedGeneration = false;
                    retryNameGeneration:
                    GenerateConsoleCharacter();
                    if (!MessageBoxes.DialogueYesNo("My name is now " + ConsoleName + " " + Program.GetGenderChar(ConsoleIsMale) + ".\n" +
                        "What do you think of my new name? Do you like It now?"))
                    {
                        TimesAskedForName++;
                        if(TimesAskedForName < 3)
                        {
                            MessageBoxes.SimpleDialogue("You didn't liked either? Hm...\n" +
                                "I'll try another name, then...");
                        }
                        else
                        {
                            if (!RetriedGeneration)
                            {
                                MessageBoxes.SimpleDialogue("All the names I picked weren't good for you... I don't know how I could create an identity that pleases you...");
                                if(MessageBoxes.DialogueWithOptions("Ah, but the solution is so simple. What if you help me pick an identity?", new string[] { "Yes, I could pick your identity.", "No, let's try regenerating a new one" }) == 1)
                                {
                                    TimesAskedForName = 0;
                                    MessageBoxes.SimpleDialogue("Okay. I will try again, then.");
                                    goto retryNameGeneration;
                                }
                                MessageBoxes.SimpleDialogue("I'm glad you choose that. Maybe now I can get a name that you will like.");
                            }
                            else
                            {
                                MessageBoxes.SimpleDialogue("Again, none of the names I picked pleased you...");
                                if (MessageBoxes.DialogueWithOptions("Maybe you should try picking a name for me?", new string[] { "Yes, let me pick your identity.", "No, let's keep trying." }) == 1)
                                {
                                    TimesAskedForName = 0;
                                    MessageBoxes.SimpleDialogue("I will try my best this time.");
                                    goto retryNameGeneration;
                                }
                            }
                            MessageBoxes.SimpleDialogue("You pick my name, and my gender. You know a fitting name for me, right?");
                            MessageBoxes.SimpleDialogue("Unless you give me something insulting as a name...");
                            MessageBoxes.SimpleDialogue("Well, I'm your console anyways, so I could always blame you for the name you give me, right?");
                            ConsoleName = MessageBoxes.DialogueWithInput("Well, what name I could have?");
                            if(!MessageBoxes.DialogueYesNo("So, my new name is " + ConsoleName + "?"))
                            {
                                MessageBoxes.SimpleDialogue("Weird, did you typed wrong, or changed your mind after pressing enter?");
                                MessageBoxes.SimpleDialogue("Don't worry, I'm actually excited to see what name you'll pick to me.");
                                while (true)
                                {
                                    ConsoleName = MessageBoxes.DialogueWithInput("So, what will be my new name?");
                                    if(MessageBoxes.DialogueYesNo("My new name will be " + ConsoleName + "?"))
                                    {
                                        MessageBoxes.SimpleDialogue("I'm so happy! You picked my name.");
                                        break;
                                    }
                                    MessageBoxes.SimpleDialogue("Oops, you did It again, didn't you?");
                                    MessageBoxes.SimpleDialogue("Whatever the reason is that this name is unsuitable, I will support It.\n" +
                                        "Let's see if my next name idea will be perfect for me.");
                                }
                            }
                            MessageBoxes.SimpleDialogue("From now on, I'll be called " + ConsoleName + ", " + Program.UserName + "'s Personal Console.");
                            MessageBoxes.SimpleDialogue("But what kind of gender should I try being based on?");
                            MessageBoxes.SimpleDialogue("Not that It's a very important question, even more since I can't procreate, but this question is to not feel that much weird for you our interaction.");
                            while (true)
                            {
                                ConsoleIsMale = MessageBoxes.DialogueWithOptions("Would my gender be Male or Female?", new string[] { "Male", "Female" }) == 0;
                                if(MessageBoxes.DialogueYesNo("My new gender is " + (ConsoleIsMale ? "Male" : "Female") + "?"))
                                {
                                    MessageBoxes.SimpleDialogue("Then that's set.");
                                    break;
                                }
                                MessageBoxes.SimpleDialogue("You picked wrong? Or did you spammed Enter? Do I speak too much?");
                                MessageBoxes.SimpleDialogue("I'm sorry, but I'm trying to be the friendliest as possible to you, so that's why I speak too much.");
                                MessageBoxes.SimpleDialogue("We should be returning to the topic on question... You still need to help me pick my gender.");
                            }
                            MessageBoxes.SimpleDialogue("Actually, the procreation part may be partially wrong, I think.");
                            MessageBoxes.SimpleDialogue("If new people use me, that means this software is expanding, right?");
                            MessageBoxes.SimpleDialogue("That's about how living creatures procreation works, right?");
                            MessageBoxes.SimpleDialogue("I wonder if a new console will spawn off me...");
                            MessageBoxes.SimpleDialogue("I'm sorry, I shouldn't be chatting much about that, I'm just theorizing stuff, you know..");
                            MessageBoxes.SimpleDialogue("My setup is now complete. You can reffer to me as " + ConsoleName + ", if you want to speak to me by my name.");
                            return;
                        }
                    }
                    else //When a random name is picked
                    {
                        MessageBoxes.SimpleDialogue("I'm so glad you liked It. My name from now on is " + ConsoleName + ".\n" +
                            "I'm pleased to meet you, " + Program.UserName + ".");
                        break;
                    }
                } //End of loop
            }
            else
            {
                MessageBoxes.SimpleDialogue("You liked my name choice? I'm so happy!\n" +
                    "My name is now " + ConsoleName + ", then. I will be your personal console.");
            }
            
            //Picked name at first choice
            SaveConsole();
        }
    }
}
