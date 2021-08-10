using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1.Applications
{
    class MathChallenge : Application
    {
        public UserScoreInfo[] ScoreInfos = new UserScoreInfo[10];

        public MathChallenge() : base("Math Challenge")
        {

        }

        public override void StartProgram()
        {
            returnToMainMenu:
            switch(MessageBoxes.DialogueWithOptions("Math Challenge!\n" +
                "Challenge yourself with Math Questions.", new string[] { "Play", "Hi-Score", "Close" }))
            {
                case 0:
                    StartGame();
                    goto returnToMainMenu;

                case 1:
                    {
                        CheckHiScore();
                        goto returnToMainMenu;
                    }
            }
        }

        public void StartGame()
        {
            int CurrentStage = 0;
            byte StagesPassed = 0;
            int MinimumNumber = 0, MaximumNumber = 4;
            int DifficultyLevel = 0;
            float Score = 0;
            float ScoreMult = 0.1f;
            bool WrongResult = false;
            while (!WrongResult)
            {
                int LeftNumber = Program.random.Next(MinimumNumber, MaximumNumber + 1), 
                    RightNumber = Program.random.Next(MinimumNumber, MaximumNumber + 1);
                int ScoreReward = (int)Math.Max(1, (LeftNumber + RightNumber) * ScoreMult * 0.5f);
                OperationIndex Operation = 0;
                if (DifficultyLevel >= 12 && Program.random.Next(100 + MinimumNumber) < LeftNumber)
                {
                    LeftNumber *= -1;
                }
                if (DifficultyLevel >= 28 && Program.random.Next(100 + MaximumNumber) < RightNumber)
                {
                    RightNumber *= -1;
                }
                if (DifficultyLevel >= 8 && Program.random.Next(100 + DifficultyLevel) < MinimumNumber)
                    Operation = OperationIndex.Subtraction;
                if (DifficultyLevel >= 18 && Program.random.Next(75 + DifficultyLevel) < MinimumNumber)
                    Operation = OperationIndex.Multiplication;
                if (DifficultyLevel >= 18 && Program.random.Next(60 + DifficultyLevel) < MinimumNumber)
                    Operation = OperationIndex.Division;
                if(Operation == OperationIndex.Multiplication || Operation == OperationIndex.Division)
                {
                    LeftNumber = (int)(LeftNumber * 0.2f);
                    RightNumber = (int)(RightNumber * 0.2f);
                }
                int Result = 0;
                string Mes = "Stage "+CurrentStage+"\n\nCurrent math problem:\n";
                string Problem = "";
                switch (Operation)
                {
                    case OperationIndex.Sum:
                        Problem = GetNumberString(LeftNumber) + "+" + GetNumberString(RightNumber) + "=";
                        Mes += Problem + "?";
                        Result = LeftNumber + RightNumber;
                        break;
                    case OperationIndex.Subtraction:
                        Problem = GetNumberString(LeftNumber) + "-" + GetNumberString(RightNumber) + "=";
                        Mes += Problem + "?";
                        Result = LeftNumber - RightNumber;
                        break;
                    case OperationIndex.Multiplication:
                        Problem = GetNumberString(LeftNumber) + "x" + GetNumberString(RightNumber) + "=";
                        Mes += Problem + "?";
                        Result = LeftNumber * RightNumber;
                        break;
                    case OperationIndex.Division:
                        Problem = GetNumberString(LeftNumber) + "/" + GetNumberString(RightNumber) + "=";
                        Mes += Problem + "?\n(Integer number only)";
                        Result = LeftNumber / RightNumber;
                        break;
                }
                int PlayerAnswer = MessageBoxes.DialogueWithNumberInput(Mes, false, true);
                if(PlayerAnswer == Result)
                {
                    MessageBoxes.SimpleDialogue("You are right!\n"+Problem + Result+"!\nScore: " + Score + "+" + ScoreReward + ".");
                    Score += ScoreReward;
                    CurrentStage++;
                    MinimumNumber++;
                    MaximumNumber++;
                    StagesPassed++;
                    DifficultyLevel++;
                    ScoreMult += 0.01f;
                    if (StagesPassed >= 5)
                    {
                        int PowerUp = MessageBoxes.DialogueWithOptions("Pick one modifier.", new string[] { "Increase Difficulty", "Decrease Difficulty" });
                        StagesPassed = 0;
                        switch (PowerUp)
                        {
                            case 0:
                                MinimumNumber += Program.random.Next(2, 5);
                                MaximumNumber += Program.random.Next(4, 7);
                                DifficultyLevel += Program.random.Next(0, 3);
                                ScoreMult += 0.1f;
                                break;
                            case 1:
                                int ToReduce = Program.random.Next(1, 4);
                                if (ToReduce > MinimumNumber)
                                    ToReduce = MinimumNumber;
                                MinimumNumber -= ToReduce;
                                MaximumNumber -= ToReduce;
                                DifficultyLevel -= ToReduce;
                                ScoreMult += 0.05f;
                                break;
                        }
                    }
                }
                else
                {
                    MessageBoxes.SimpleDialogue("Wrong answer!\n"+Problem + Result);
                    MessageBoxes.SimpleDialogue("You managed to pass through "+CurrentStage+" Stages.\n\nFinal Score: " + (int)Score);
                    if(Score > 0)
                        AddScore(Program.UserName, (int)Score);
                    CheckHiScore();
                    WrongResult = true;
                }
            }
        }

        public void CheckHiScore()
        {
            string HiScoreText = "HI-SCORE";
            while (HiScoreText.Length < Console.WindowWidth * 0.5f + 2)
            {
                HiScoreText = " " + HiScoreText;
            }
            HiScoreText += "\n";
            for (int i = 2; i < Console.WindowWidth - 2; i++)
            {
                HiScoreText += '=';
            }
            int WidthDistancing = Console.WindowWidth / 2 - 2;
            foreach (UserScoreInfo score in ScoreInfos)
            {
                string ScoreText = score.Name;
                while (ScoreText.Length < WidthDistancing)
                    ScoreText += ' ';
                ScoreText += score.Score;
                HiScoreText += ScoreText + "\n";
            }
            MessageBoxes.SimpleDialogue(HiScoreText);
        }

        public void AddScore(string Name, int Score)
        {
            UserScoreInfo NewScore = new UserScoreInfo(Name, Score);
            List<UserScoreInfo> scores = new List<UserScoreInfo>();
            scores.AddRange(ScoreInfos);
            scores.Add(NewScore);
            ScoreInfos = scores.OrderByDescending(x => x.Score).Take(10).ToArray();
            Save();
        }

        public string GetNumberString(int Number)
        {
            if (Number >= 0)
                return Number.ToString();
            return "(" + Number + ")";
        }

        public override void OnLoad()
        {
            for(int i = 0; i < ScoreInfos.Length; i++)
            {
                ScoreInfos[i] = new UserScoreInfo("AAA", 0);
            }
            Load();
        }

        private void Save()
        {
            using (FileStream stream = SaveOnApplication("hiscore.sav"))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    foreach (UserScoreInfo score in ScoreInfos)
                    {
                        writer.Write(score.Name);
                        writer.Write(score.Score);
                    }
                }
            }
        }

        private void Load()
        {
            using (FileStream stream = LoadOnApplication("hiscore.sav"))
            {
                if(stream != null)
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        for(int i = 0; i < 10; i++)
                        {
                            ScoreInfos[i].Name = reader.ReadString();
                            ScoreInfos[i].Score = reader.ReadInt32();
                        }
                    }
                }
            }
        }

        public class UserScoreInfo
        {
            public string Name = "";
            public int Score = 0;

            public UserScoreInfo(string Name, int Score)
            {
                this.Name = Name;
                this.Score = Score;
            }
        }

        public enum OperationIndex: byte
        {
            Sum,
            Subtraction,
            Multiplication,
            Division
        }
    }
}
