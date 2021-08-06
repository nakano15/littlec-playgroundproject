using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MessageBoxes
    {
        public static int DialogueWithNumberInput(string Message, bool Password = false)
        {
            return int.Parse(DialogueWithInput(Message, Password, Rules.NumbersOnly));
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

        /// <summary>
        /// Returns true if Yes is selected.
        /// </summary>
        public static bool DialogueYesNo(string Message)
        {
            return DialogueWithOptions(Message, new string[] { "Yes", "No" }) == 0;
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
                    else if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9')))
                    {
                        if (LastMessageSplit.Length >= Width - 5)
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
                    if (LastMessageSplit.Length + LastWord.Length >= Width - 5)
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
        }

        public enum Rules : byte
        {
            Free,
            NumbersOnly,
            LettersOnly
        }
    }
}
