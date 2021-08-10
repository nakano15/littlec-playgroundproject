using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MessageBoxes
    {
        public static float ConsoleDialogueWithDecimalInput(string Message, bool Password = false, bool NegativesAllowed = false)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithDecimalInput(Message, Password, NegativesAllowed);
            return SomeonesDialogueWithDecimalInput(Message, ConsoleCharacter.ConsoleName, Password, NegativesAllowed);
        }

        public static float SomeonesDialogueWithDecimalInput(string Message, string Speaker, bool Password = false, bool NegativesAllowed = false)
        {
            return DialogueWithDecimalInput("[" + Speaker + "]\n" + Message, Password, NegativesAllowed);
        }

        public static float DialogueWithDecimalInput(string Message, bool Password = false, bool NegativesAllowed = false)
        {
            return float.Parse(DialogueWithInput(Message, Password, (NegativesAllowed ? Rules.NumbersOnlyWithDecimalAndNegative : Rules.NumbersOnlyWithDecimal)));
        }

        public static int ConsoleDialogueWithNumberInput(string Message, bool Password = false, bool NegativesAllowed = false)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithNumberInput(Message, Password, NegativesAllowed);
            return SomeonesDialogueWithNumberInput(Message, ConsoleCharacter.ConsoleName, Password, NegativesAllowed);
        }

        public static int SomeonesDialogueWithNumberInput(string Message, string SpeakerName, bool Password = false, bool NegativesAllowed = false)
        {
            return DialogueWithNumberInput("[" + SpeakerName + "]\n" + Message, Password, NegativesAllowed);
        }

        public static int DialogueWithNumberInput(string Message, bool Password = false, bool NegativesAllowed = false)
        {
            return int.Parse(DialogueWithInput(Message, Password, (NegativesAllowed ? Rules.NumbersOnlyWithNegative : Rules.NumbersOnly)));
        }

        public static string ConsoleDialogueWithInput(string Message, bool Password = false, Rules rule = Rules.Free)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithInput(Message, Password, rule);
            return SomeonesDialogueWithInput(Message, ConsoleCharacter.ConsoleName, Password, rule);
        }

        public static string SomeonesDialogueWithInput(string Message, string SpeakerName, bool Password = false, Rules rule = Rules.Free)
        {
            return DialogueWithInput("[" + SpeakerName + "]\n" + Message, Password, rule);
        }

        public static string DialogueWithInput(string Message, bool Password = false, Rules rule = Rules.Free)
        {
            SimpleDialogue(Message, false);
            int Width = Console.WindowWidth; //Height doesn't matter.
            int BoxStart = Console.CursorTop;
            for (int x = 0; x < Width; x++)
            {
                if (x == 0)
                    Console.Write('╟');
                else if (x == Width - 1)
                    Console.Write('╢');
                else
                    Console.Write('─');
            }
            BoxStart++;
            Console.SetCursorPosition(0, BoxStart);
            Console.Write('║');
            Console.SetCursorPosition(Width - 1, BoxStart);
            Console.Write('║');
            for (int x = 0; x < Width; x++)
            {
                Console.SetCursorPosition(x, BoxStart + 1);
                if (x == 0)
                    Console.Write('╚');
                else if (x == Width - 1)
                    Console.Write('╝');
                else
                    Console.Write('═');
            }
            string FinalMessage = "";
            bool Done = false;
            while (!Done)
            {
                Console.SetCursorPosition(2 + FinalMessage.Length, BoxStart);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        if (FinalMessage.Length > 0)
                        {
                            FinalMessage = FinalMessage.Remove(FinalMessage.Length - 1, 1);
                            Console.SetCursorPosition(2 + FinalMessage.Length, BoxStart);
                            Console.Write(' ');
                        }
                        break;
                    case ConsoleKey.Enter:
                        {
                            if (FinalMessage.Length > 0)
                                Done = true;
                        }
                        break;
                    default:
                        if (FinalMessage.Length < Width - 4)
                        {
                            bool Input = true;
                            switch (rule)
                            {
                                case Rules.Free:
                                    Input = (keyInfo.KeyChar == ' ' ||
                                        char.IsLetterOrDigit(keyInfo.KeyChar) ||
                                        char.IsPunctuation(keyInfo.KeyChar));
                                    break;
                                case Rules.LettersOnly:
                                    Input = (keyInfo.KeyChar == ' ' ||
                                        char.IsLetter(keyInfo.KeyChar));
                                    break;
                                case Rules.NumbersOnly:
                                    Input = char.IsDigit(keyInfo.KeyChar);
                                    break;
                                case Rules.NumbersOnlyWithNegative:
                                    Input = char.IsDigit(keyInfo.KeyChar) || keyInfo.KeyChar == '-';
                                    break;
                                case Rules.NumbersOnlyWithDecimal:
                                    Input = char.IsDigit(keyInfo.KeyChar) || keyInfo.KeyChar == ',';
                                    break;
                                case Rules.NumbersOnlyWithDecimalAndNegative:
                                    Input = char.IsDigit(keyInfo.KeyChar) || keyInfo.KeyChar == '-' || keyInfo.KeyChar == ',';
                                    break;
                            }
                            if (Input)
                            {
                                FinalMessage += keyInfo.KeyChar;
                                Console.Write(!Password ? keyInfo.KeyChar : '*');
                            }
                        }
                        break;
                }
            }
            return FinalMessage;
        }

        public static bool ConsoleDialogueYesNo(string Message)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueYesNo(Message);
            return SomeonesDialogueYesNo(Message, ConsoleCharacter.ConsoleName);
        }

        public static bool SomeonesDialogueYesNo(string Message, string SpeakerName)
        {
            return DialogueYesNo("[" + SpeakerName + "]\n" + Message);
        }

        /// <summary>
        /// Returns true if Yes is selected.
        /// </summary>
        public static bool DialogueYesNo(string Message)
        {
            return DialogueWithOptions(Message, new string[] { "Yes", "No" }) == 0;
        }

        public static int ConsoleDialogueWithOptions(string Message, string[] Option)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithOptions(Message, Option);
            return SomeonesDialogueWithOptions(Message, ConsoleCharacter.ConsoleName, Option);
        }

        public static int SomeonesDialogueWithOptions(string Message, string SpeakerName, string[] Option)
        {
            return DialogueWithOptions("[" + SpeakerName + "]\n" + Message, Option);
        }

        public static int DialogueWithOptions(string Message, string[] Options)
        {
            SimpleDialogue(Message, false);
            int Width = Console.WindowWidth; //Height doesn't matter.
            int BoxStart = Console.CursorTop;
            for (int x = 0; x < Width; x++)
            {
                if (x == 0)
                    Console.Write('╟');
                else if (x == Width - 1)
                    Console.Write('╢');
                else
                    Console.Write('─');
            }
            BoxStart++;
            for (int o = 0; o < Options.Length; o++)
            {
                Console.SetCursorPosition(0, BoxStart + o);
                Console.Write('║');
                Console.SetCursorPosition(Width - 1, BoxStart + o);
                Console.Write('║');
            }
            for (int x = 0; x < Width; x++)
            {
                Console.SetCursorPosition(x, BoxStart + Options.Length);
                if (x == 0)
                    Console.Write('╚');
                else if (x == Width - 1)
                    Console.Write('╝');
                else
                    Console.Write('═');
            }
            bool OptionPicked = false;
            int SelectedOption = 0;
            while (!OptionPicked)
            {
                for (int i = 0; i < Options.Length; i++)
                {
                    Console.SetCursorPosition(2, BoxStart + i);
                    if (i == SelectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    Console.Write(Options[i]);
                }
                Console.SetCursorPosition(0, BoxStart + Options.Length + 1);
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (SelectedOption > 0)
                            SelectedOption--;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (SelectedOption < Options.Length - 1)
                            SelectedOption++;
                        break;
                    case ConsoleKey.Enter:
                        OptionPicked = true;
                        break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            return SelectedOption;
        }

        public static void ConsoleDialogue(string Message)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
            {
                SimpleDialogue(Message);
                return;
            }
            SomeonesDialogue(Message, ConsoleCharacter.ConsoleName);
        }

        public static void SomeonesDialogue(string Message, string SpeakerName)
        {
            SimpleDialogue("[" + SpeakerName + "]\n" + Message);
        }

        public static void SimpleDialogue(string Message, bool IsSingleMessageBox = true)
        {
            Console.Clear();
            int Width = Console.WindowWidth; //Height doesn't matter.
            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                if (i == 0)
                    Console.Write('╔');
                else if (i == Width - 1)
                    Console.Write('╗');
                else
                    Console.Write('═');
            }
            {
                string LastMessageSplit = "";
                List<string> FinalMessage = new List<string>();
                string LastWord = "";
                foreach (char c in Message)
                {
                    if (c == '\n')
                    {
                        LastMessageSplit += LastWord;
                        FinalMessage.Add(LastMessageSplit);
                        LastMessageSplit = LastWord = "";
                    }
                    else if (!char.IsLetterOrDigit(c))
                    {
                        if (LastMessageSplit.Length + LastWord.Length >= Width - 4)
                        {
                            FinalMessage.Add(LastMessageSplit);
                            LastMessageSplit = LastWord + c;
                        }
                        else
                        {
                            LastMessageSplit += LastWord + c;
                        }
                        LastWord = "";
                    }
                    else
                    {
                        LastWord += c;
                    }
                }
                if (LastWord.Length > 0)
                {
                    if (LastMessageSplit.Length + LastWord.Length >= Width - 4)
                    {
                        FinalMessage.Add(LastMessageSplit);
                        LastMessageSplit = "";
                        FinalMessage.Add(LastWord);
                    }
                    else
                    {
                        LastMessageSplit += LastWord;
                    }
                }
                if (LastMessageSplit.Length > 0)
                {
                    FinalMessage.Add(LastMessageSplit);
                }
                for (int m = 0; m < FinalMessage.Count; m++)
                {
                    Console.SetCursorPosition(0, m + 1);
                    Console.Write('║');
                    Console.SetCursorPosition(2, m + 1);
                    Console.Write(FinalMessage[m]);
                    Console.SetCursorPosition(Width - 1, m + 1);
                    Console.Write('║');
                }
                Console.SetCursorPosition(0, FinalMessage.Count + 1);
            }
            if (IsSingleMessageBox)
            {
                for (int i = 0; i < Width; i++)
                {
                    Console.SetCursorPosition(i, Console.CursorTop);
                    if (i == 0)
                        Console.Write('╚');
                    else if (i == Width - 1)
                        Console.Write('╝');
                    else
                        Console.Write('═');
                }
                while (true)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void ParseWord(ref string Word)
        {
            string OldWord = Word;
            Word = "";
            string CommandType = "", CommandValue = "";
            bool FinishedInputtingCommand = false, BegunCommand = false;
            for (int i = 0; i < OldWord.Length; i++)
            {
                char c = OldWord[i];
                if (c == '[')
                {
                    BegunCommand = true;
                }
                else if (c == ']')
                {
                    DoCommand(CommandType, CommandValue);
                    CommandType = "";
                    CommandValue = "";
                    FinishedInputtingCommand = false;
                    BegunCommand = false;
                }
                else if (BegunCommand)
                {
                    if (c == ':')
                    {
                        FinishedInputtingCommand = true;
                    }
                    else
                    {
                        if (!FinishedInputtingCommand)
                        {
                            CommandType += c;
                        }
                        else
                        {
                            CommandValue += c;
                        }
                    }
                }
                else
                {
                    Word += c;
                }
            }
        }

        private static void DoCommand(string Command, string Value)
        {
            switch (Command)
            {
                case "c": //Change Foreground Color
                    {
                        ConsoleColor color;
                        if (Enum.TryParse(Value, out color))
                            Console.ForegroundColor = color;
                    }
                    break;
                case "b": //Change Background Color
                    {
                        ConsoleColor color;
                        if (Enum.TryParse(Value, out color))
                            Console.BackgroundColor = color;
                    }
                    break;
            }
        }

        public enum Rules : byte
        {
            Free,
            NumbersOnly,
            NumbersOnlyWithDecimal,
            NumbersOnlyWithNegative,
            NumbersOnlyWithDecimalAndNegative,
            LettersOnly
        }
    }
}
