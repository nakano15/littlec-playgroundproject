using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MessageBoxes
    {
        public static char BoxUpperLeftChar = '╔', BoxHorizontalChar = '═', BoxUpperRightChar = '╗', BoxVerticalChar = '║',
            BoxLowerLeftChar = '╚', BoxLowerRightChar = '╝', BoxSplitterLeft = '╟', BoxSplitterHorizontal = '─', BoxSplitterRight = '╢';
        public static char SpeakerLeftNameChar = '[', SpeakerRightNameChar = ']';

        /// <summary>
        /// Changes dialogue boxes style, to have simple line borders, and on the corners have crosses.
        /// </summary>
        public static void FrameDialogueStyle()
        {
            BoxUpperLeftChar = BoxUpperRightChar = BoxLowerLeftChar = BoxLowerRightChar = '┼';
            BoxHorizontalChar = BoxSplitterHorizontal = '─';
            BoxVerticalChar = '│';
            BoxSplitterLeft = '├';
            BoxSplitterRight = '┤';
        }

        /// <summary>
        /// Resets the dialogue surround chars to the default.
        /// </summary>
        public static void ResetBoxCharacters()
        {
            BoxUpperLeftChar = '╔';
            BoxHorizontalChar = '═';
            BoxUpperRightChar = '╗';
            BoxVerticalChar = '║';
            BoxLowerLeftChar = '╚';
            BoxLowerRightChar = '╝';
            BoxSplitterLeft = '╟';
            BoxSplitterHorizontal = '─';
            BoxSplitterRight = '╢';
        }

        /// <summary>
        /// Resets the speaker surround characters to the default.
        /// </summary>
        public static void ResetDialogueSpeakerChars()
        {
            SpeakerLeftNameChar = '[';
            SpeakerRightNameChar = ']';            
        }

        /// <summary>
        /// A input dialogue in console's name that asks the user to input a real number.
        /// You can allow or unallow negative number input.
        /// Password hides the numbers by replacing them by asterisks.
        /// </summary>
        public static float ConsoleDialogueWithDecimalInput(string Message, bool Password = false, bool NegativesAllowed = false)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithDecimalInput(Message, Password, NegativesAllowed);
            return SomeonesDialogueWithDecimalInput(Message, ConsoleCharacter.ConsoleName, Password, NegativesAllowed);
        }

        /// <summary>
        /// A input dialogue in someone's name that asks the user to input a real number.
        /// You can allow or unallow negative number input.
        /// Password hides the numbers by replacing them by asterisks.
        /// </summary>
        public static float SomeonesDialogueWithDecimalInput(string Message, string Speaker, bool Password = false, bool NegativesAllowed = false)
        {
            return DialogueWithDecimalInput(SpeakerLeftNameChar + Speaker + SpeakerRightNameChar + "\n" + Message, Password, NegativesAllowed);
        }

        /// <summary>
        /// A input dialogue that asks the user to input a real number.
        /// You can allow or unallow negative number input.
        /// Password hides the numbers by replacing them by asterisks.
        /// </summary>
        public static float DialogueWithDecimalInput(string Message, bool Password = false, bool NegativesAllowed = false)
        {
            return float.Parse(DialogueWithInput(Message, Password, (NegativesAllowed ? Rules.NumbersOnlyWithDecimalAndNegative : Rules.NumbersOnlyWithDecimal)));
        }

        /// <summary>
        /// A input dialogue in the console's name that asks the user to input a number.
        /// You can allow or unallow negative number input.
        /// Password hides the numbers by replacing them by asterisks.
        /// </summary>
        public static int ConsoleDialogueWithNumberInput(string Message, bool Password = false, bool NegativesAllowed = false)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithNumberInput(Message, Password, NegativesAllowed);
            return SomeonesDialogueWithNumberInput(Message, ConsoleCharacter.ConsoleName, Password, NegativesAllowed);
        }

        /// <summary>
        /// A input dialogue in the name of someone that asks the user to input a number.
        /// You can allow or unallow negative number input.
        /// Password hides the numbers by replacing them by asterisks.
        /// </summary>
        public static int SomeonesDialogueWithNumberInput(string Message, string SpeakerName, bool Password = false, bool NegativesAllowed = false)
        {
            return DialogueWithNumberInput(SpeakerLeftNameChar + SpeakerName + SpeakerRightNameChar + "\n" + Message, Password, NegativesAllowed);
        }

        /// <summary>
        /// A input dialogue that asks the user to input a number.
        /// You can allow or unallow negative number input.
        /// Password hides the numbers by replacing them by asterisks.
        /// </summary>
        public static int DialogueWithNumberInput(string Message, bool Password = false, bool NegativesAllowed = false)
        {
            return int.Parse(DialogueWithInput(Message, Password, (NegativesAllowed ? Rules.NumbersOnlyWithNegative : Rules.NumbersOnly)));
        }

        /// <summary>
        /// Shows a dialogue in console's name that asks the user to input something.
        /// The Rules allows you to change the input rules, in case you seek something more specific.
        /// Setting as Password hides the letters by placing asterisks in it's places.
        /// </summary>
        public static string ConsoleDialogueWithInput(string Message, bool Password = false, Rules rule = Rules.Free)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithInput(Message, Password, rule);
            return SomeonesDialogueWithInput(Message, ConsoleCharacter.ConsoleName, Password, rule);
        }

        /// <summary>
        /// Shows a dialogue in the name of someone that asks the user to input something.
        /// The Rules allows you to change the input rules, in case you seek something more specific.
        /// Setting as Password hides the letters by placing asterisks in it's places.
        /// </summary>
        public static string SomeonesDialogueWithInput(string Message, string SpeakerName, bool Password = false, Rules rule = Rules.Free)
        {
            return DialogueWithInput(SpeakerLeftNameChar + SpeakerName + SpeakerRightNameChar + "\n" + Message, Password, rule);
        }

        /// <summary>
        /// Shows a dialogue that asks the user to input something.
        /// The Rules allows you to change the input rules, in case you seek something more specific.
        /// Setting as Password hides the letters by placing asterisks in it's places.
        /// </summary>
        public static string DialogueWithInput(string Message, bool Password = false, Rules rule = Rules.Free)
        {
            SimpleDialogue(Message, false);
            int Width = Console.WindowWidth; //Height doesn't matter.
            int BoxStart = Console.CursorTop;
            for (int x = 0; x < Width; x++)
            {
                if (x == 0)
                    Console.Write(BoxSplitterLeft);
                else if (x == Width - 1)
                    Console.Write(BoxSplitterRight);
                else
                    Console.Write(BoxSplitterHorizontal);
            }
            BoxStart++;
            Console.SetCursorPosition(0, BoxStart);
            Console.Write(BoxVerticalChar);
            Console.SetCursorPosition(Width - 1, BoxStart);
            Console.Write(BoxVerticalChar);
            for (int x = 0; x < Width; x++)
            {
                Console.SetCursorPosition(x, BoxStart + 1);
                if (x == 0)
                    Console.Write(BoxLowerLeftChar);
                else if (x == Width - 1)
                    Console.Write(BoxLowerRightChar);
                else
                    Console.Write(BoxHorizontalChar);
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

        /// <summary>
        /// Shows a dialogue in the console name that returns true or false, depending on if the user picked Yes or No as answer.
        /// </summary>
        public static bool ConsoleDialogueYesNo(string Message)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueYesNo(Message);
            return SomeonesDialogueYesNo(Message, ConsoleCharacter.ConsoleName);
        }

        /// <summary>
        /// Shows a dialogue with the name of someone that returns true or false, depending on if the user picked Yes or No as answer.
        /// </summary>
        public static bool SomeonesDialogueYesNo(string Message, string SpeakerName)
        {
            return DialogueYesNo(SpeakerLeftNameChar + SpeakerName + SpeakerRightNameChar + "\n" + Message);
        }

        /// <summary>
        /// Shows a dialogue that returns true or false, depending on if the user picked Yes or No as answer.
        /// </summary>
        public static bool DialogueYesNo(string Message)
        {
            return DialogueWithOptions(Message, new string[] { "Yes", "No" }) == 0;
        }

        /// <summary>
        /// Shows a dialogue with selectable single option.
        /// Returns the index of the option.
        /// Displays the name of your picked console character.
        /// </summary>
        public static int ConsoleDialogueWithOptions(string Message, string[] Option)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithOptions(Message, Option);
            return SomeonesDialogueWithOptions(Message, ConsoleCharacter.ConsoleName, Option);
        }

        /// <summary>
        /// Shows a dialogue with selectable single option.
        /// Returns the index of the option.
        /// Display the name of a speaker character.
        /// </summary>
        public static int SomeonesDialogueWithOptions(string Message, string SpeakerName, string[] Option)
        {
            return DialogueWithOptions(SpeakerLeftNameChar + SpeakerName + SpeakerRightNameChar + "\n" + Message, Option);
        }

        /// <summary>
        /// Shows a dialogue with selectable single option.
        /// Returns the index of the option.
        /// </summary>
        public static int DialogueWithOptions(string Message, string[] Options)
        {
            SimpleDialogue(Message, false);
            int Width = Console.WindowWidth; //Height doesn't matter.
            int OptionsStart = Console.CursorTop;
            for (int x = 0; x < Width; x++)
            {
                if (x == 0)
                    Console.Write(BoxSplitterLeft);
                else if (x == Width - 1)
                    Console.Write(BoxSplitterRight);
                else
                    Console.Write(BoxSplitterHorizontal);
            }
            OptionsStart++;
            bool OptionPicked = false;
            int SelectedOption = 0;
            while (!OptionPicked)
            {
                int OptionsEnd = OptionsStart;
                for (int i = 0; i < Options.Length; i++)
                {
                    Console.SetCursorPosition(2, OptionsEnd);
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
                    WriteFormattedMessage(Options[i], 2, Width - 2, i == SelectedOption);
                    //TryWritingMessage(Options[i]);
                    OptionsEnd = Console.CursorTop + 1;
                    //Console.Write(Options[i]);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                for (int o = OptionsStart; o <= OptionsEnd; o++)
                {
                    Console.SetCursorPosition(0, o);
                    Console.Write(BoxVerticalChar);
                    Console.SetCursorPosition(Width - 1, o);
                    Console.Write(BoxVerticalChar);
                }
                for (int x = 0; x < Width; x++)
                {
                    Console.SetCursorPosition(x, OptionsEnd);
                    if (x == 0)
                        Console.Write(BoxLowerLeftChar);
                    else if (x == Width - 1)
                        Console.Write(BoxLowerRightChar);
                    else
                        Console.Write(BoxHorizontalChar);
                }
                Console.SetCursorPosition(0, OptionsEnd + 1);
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (SelectedOption > 0)
                            SelectedOption--;
                        else
                            SelectedOption = Options.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (SelectedOption < Options.Length - 1)
                            SelectedOption++;
                        else
                            SelectedOption = 0;
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

        /// <summary>
        /// Shows a dialogue box in the name of the console with options of flipping between True and False.
        /// When the user thinks that their answer is correct, use the Done option to get the result.
        /// Returns if the options were answered correctly.
        /// </summary>
        public static bool[] ConsoleDialogueWithTrueFalseOption(string Message, string[] Options, bool[] CorrectAnswers)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithTrueFalseOptions(Message, Options, CorrectAnswers);
            return SomeonesDialogueWithTrueFalseOptions(Message, ConsoleCharacter.ConsoleName, Options, CorrectAnswers);
        }

        /// <summary>
        /// Shows a dialogue box in the name of someone with options of flipping between True and False.
        /// When the user thinks that their answer is correct, use the Done option to get the result.
        /// Returns if the options were answered correctly.
        /// </summary>
        public static bool[] SomeonesDialogueWithTrueFalseOptions(string Message, string Speaker, string[] Options, bool[] CorrectAnswers)
        {
            return DialogueWithTrueFalseOptions(SpeakerLeftNameChar + Speaker + SpeakerRightNameChar + "\n" + Message, Options, CorrectAnswers);
        }

        /// <summary>
        /// Shows a dialogue box with options of flipping between True and False.
        /// When the user thinks that their answer is correct, use the Done option to get the result.
        /// Returns if the options were answered correctly.
        /// </summary>
        public static bool[] DialogueWithTrueFalseOptions(string Message, string[] Options, bool[] CorrectAnswers)
        {
            return GetTrueFalseResult(DialogueWithTrueFalseOptions(Message, Options), CorrectAnswers);
        }

        /// <summary>
        /// Shows a dialogue box in the name of the console with options of flipping between True and False.
        /// When the user thinks that their answer is correct, use the Done option to get the result.
        /// Returns if the options the player picked are true or false, so there is no right or wrong here.
        /// </summary>
        public static bool[] ConsoleDialogueWithTrueFalseOption(string Message, string[] Options)
        {
            if (!ConsoleCharacter.CreatedConsoleCharacter)
                return DialogueWithTrueFalseOptions(Message, Options);
            return SomeonesDialogueWithTrueFalseOptions(Message, ConsoleCharacter.ConsoleName, Options);
        }

        /// <summary>
        /// Shows a dialogue box in the name of someone with options of flipping between True and False.
        /// When the user thinks that their answer is correct, use the Done option to get the result.
        /// Returns if the options the player picked are true or false, so there is no right or wrong here.
        /// </summary>
        public static bool[] SomeonesDialogueWithTrueFalseOptions(string Message, string Speaker, string[] Options)
        {
            return DialogueWithTrueFalseOptions(SpeakerLeftNameChar + Speaker + SpeakerRightNameChar+ "\n" + Message, Options);
        }

        /// <summary>
        /// Shows a dialogue box with options of flipping between True and False.
        /// When the user thinks that their answer is correct, use the Done option to get the result.
        /// Returns if the options the player picked are true or false, so there is no right or wrong here.
        /// </summary>
        public static bool[] DialogueWithTrueFalseOptions(string Message, string[] Options)
        {
            bool[] Results = new bool[Options.Length];
            SimpleDialogue(Message, false);
            int Width = Console.WindowWidth;
            int OptionsStart = Console.CursorTop;
            for(int x = 0; x < Width; x++)
            {
                if (x == 0)
                    Console.Write(BoxSplitterLeft);
                else if(x == Width - 1)
                    Console.Write(BoxSplitterRight);
                else
                    Console.Write(BoxSplitterHorizontal);
            }
            OptionsStart++;
            int SelectedOption = 0;
            bool Done = false;
            while (!Done)
            {
                int OptionsEnd = OptionsStart;
                for(int i = 0; i <= Options.Length; i++)
                {
                    Console.SetCursorPosition(2, OptionsEnd);
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
                    string Text = "";
                    if(i < Options.Length)
                    {
                        Text += "[";
                        if (Results[i])
                            Text += "T";
                        else
                            Text += "F";
                        Text += "] " + Options[i];
                    }
                    else
                    {
                        Text = "Done";
                    }
                    WriteFormattedMessage(Text, 2, Width - 2, i == SelectedOption);
                    OptionsEnd = Console.CursorTop + 1;
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                for(int o = OptionsStart; o <= OptionsEnd; o++)
                {
                    Console.SetCursorPosition(0, o);
                    Console.Write(BoxVerticalChar);
                    Console.SetCursorPosition(Width - 1, o);
                    Console.Write(BoxVerticalChar);
                }
                for(int x = 0; x < Width; x++)
                {
                    Console.SetCursorPosition(x, OptionsEnd);
                    if (x == 0)
                        Console.Write(BoxLowerLeftChar);
                    else if (x == Width - 1)
                        Console.Write(BoxLowerRightChar);
                    else
                        Console.Write(BoxHorizontalChar);
                }
                Console.SetCursorPosition(0, OptionsEnd + 1);
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        if (SelectedOption > 0)
                            SelectedOption--;
                        else
                            SelectedOption = Options.Length;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        if (SelectedOption < Options.Length)
                            SelectedOption++;
                        else
                            SelectedOption = 0;
                        break;
                    case ConsoleKey.Enter:
                        if(SelectedOption < Options.Length)
                        {
                            Results[SelectedOption] = !Results[SelectedOption];
                        }
                        else
                        {
                            Done = true;
                        }
                        break;
                }
            }
            return Results;
        }

        public static bool[] GetTrueFalseResult(bool[] PickedAnswers, bool[] CorrectAnswers)
        {
            if (PickedAnswers.Length != CorrectAnswers.Length)
                return new bool[0];
            bool[] CorrectOnes = new bool[PickedAnswers.Length];
            for(int i = 0; i < PickedAnswers.Length; i++)
            {
                CorrectOnes[i] = PickedAnswers[i] == CorrectOnes[i];
            }
            return CorrectOnes;
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
            SimpleDialogue(SpeakerLeftNameChar + SpeakerName + SpeakerRightNameChar + "\n" + Message);
        }

        /// <summary>
        /// Only shows a normal dialogue box.
        /// If it's a simple message box, It will be closed at the bottom, and pressing Enter will close it.
        /// Only use IsSingleMessageBox = false, if you want to make the dialogue only show a message, and want to add an alternative input method for the user.
        /// </summary>
        public static void SimpleDialogue(string Message, bool IsSingleMessageBox = true)
        {
            Console.Clear();
            int Width = Console.WindowWidth; //Height doesn't matter.
            //Draws the top of the message.
            for (int i = 0; i < Width; i++)
            {
                Console.SetCursorPosition(i, 0);
                if (i == 0)
                    Console.Write(BoxUpperLeftChar);
                else if (i == Width - 1)
                    Console.Write(BoxUpperRightChar);
                else
                    Console.Write(BoxHorizontalChar);
            }
            //Draw the message, and the borders.
            {
                Console.SetCursorPosition(2, 1);
                WriteFormattedMessage(Message, 2, Width - 2);
                int EndY = Console.CursorTop;
                for(int y = 1; y <= EndY; y++)
                {
                    Console.SetCursorPosition(0, y);
                    Console.Write(BoxVerticalChar);
                    Console.SetCursorPosition(Width - 1, y);
                    Console.Write(BoxVerticalChar);
                }
                Console.SetCursorPosition(0, EndY + 1);
            }
            //Draw the bottom border, if it is a simple message box.
            if (IsSingleMessageBox)
            {
                for (int i = 0; i < Width; i++)
                {
                    Console.SetCursorPosition(i, Console.CursorTop);
                    if (i == 0)
                        Console.Write(BoxLowerLeftChar);
                    else if (i == Width - 1)
                        Console.Write(BoxLowerRightChar);
                    else
                        Console.Write(BoxHorizontalChar);
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

        public static void WriteFormattedMessage(string Message, int StartX = 0, int EndX = 0, bool IsSelected = false)
        {
            if (EndX == 0)
                EndX = Console.WindowWidth;
            string LastMessage = "";
            string Command = "";
            bool CommandStart = false;
            foreach (char c in Message)
            {
                if (c == '[' && !CommandStart)
                {
                    TryWritingMessage(LastMessage, EndX);
                    LastMessage = "";
                    CommandStart = true;
                    Command = "[";
                }
                else if (c == ']' && CommandStart)
                {
                    Command += ']';
                    ParseWord(IsSelected, ref Command);
                    Command = "";
                    CommandStart = false;
                }
                else if (CommandStart)
                {
                    Command += c;
                }
                else if (c == '\n')
                {
                    TryWritingMessage(LastMessage, EndX);
                    LastMessage = "";
                    Console.CursorLeft = 2;
                    Console.CursorTop++;
                }
                else if (char.IsSymbol(c) || c == ' ')
                {
                    TryWritingMessage(LastMessage, EndX);
                    LastMessage = "";
                    TryWritingMessage(c.ToString(), EndX);
                }
                else
                {
                    LastMessage += c;
                }
            }
            if (LastMessage.Length > 0)
                TryWritingMessage(LastMessage, EndX);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void TryWritingMessage(string Message)
        {
            TryWritingMessage(Message, Console.WindowWidth - 2);
        }

        private static void TryWritingMessage(string Message, int MaxWidth)
        {
            if (Message.Length + Console.CursorLeft <= MaxWidth)
            {
                Console.Write(Message);
            }
            else
            {
                Console.CursorLeft = 2;
                Console.CursorTop++;
                Console.Write(Message);
            }
        }

        private static void ParseWord(bool IsSelectedText, ref string Word)
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
                    if(!DoCommand(CommandType, CommandValue, IsSelectedText))
                    {
                        TryWritingMessage('[' + CommandType + (CommandValue == "" ? "]" : ":" + CommandValue + ']')); //It's not a command
                    }
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

        private static bool DoCommand(string Command, string Value, bool IsSelectedText = false)
        {
            if (IsSelectedText)
            {
                if (Command == "c")
                    Command = "b";
                else if (Command == "b")
                    Command = "c";
            }
            switch (Command)
            {
                default:
                    return false;
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
                case "r":
                    {
                        if (IsSelectedText)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                    }
                    break;
            }
            return true;
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
