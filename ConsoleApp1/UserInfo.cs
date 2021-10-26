using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class UserInfo
    {
        public const int Version = 1;
        public string Name = "";
        private string Password = "";
        public bool IsMale = true;
        public ushort Age = 18;
        public string Country = "";
        public bool AccessedHubOnce = false;

        public static string[] GetUserList()
        {
            string SaveDirectory = Program.UsersSaveFolder + "/";
            if (Directory.Exists(SaveDirectory))
            {
                List<string> finaldirs = new List<string>();
                foreach(string s in Directory.GetDirectories(SaveDirectory))
                {
                    finaldirs.Add(s.Substring(SaveDirectory.Length));
                }
                return finaldirs.ToArray();
            }
            return new string[0];
        }

        public void Save()
        {
            string SaveDirectory = Program.UsersSaveFolder + "/" + Name + "/";
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
            string UserInfoFile = SaveDirectory + "UserInfo.sav";
            if (File.Exists(UserInfoFile))
                File.Delete(UserInfoFile);
            using (FileStream fs = new FileStream(UserInfoFile, FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(Version);
                    writer.Write(PasswordEncryptor(Password, true));
                    writer.Write(Name);
                    writer.Write(IsMale);
                    writer.Write(Age);
                    writer.Write(Country);
                    writer.Write(AccessedHubOnce);
                }
            }
        }

        public static bool Load(string Name, string Password, out UserInfo user)
        {
            user = null;
            string SaveDirectory = Program.UsersSaveFolder + "/" + Name + "/";
            bool Success = true;
            if (Directory.Exists(SaveDirectory))
            {
                string UserInfoFile = SaveDirectory + "UserInfo.sav";
                if (!File.Exists(UserInfoFile))
                {
                    Directory.Delete(SaveDirectory, true);
                    return false;
                }
                using (FileStream fs = new FileStream(UserInfoFile, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int Version = reader.ReadInt32();
                        string Pass = PasswordEncryptor(reader.ReadString(), false);
                        if (Pass.Length > 0 && Password != Pass)
                        {
                            Success = false;
                        }
                        else
                        {
                            user = new UserInfo();
                            user.Password = Pass;
                            user.Name = reader.ReadString();
                            user.IsMale = reader.ReadBoolean();
                            user.Age = reader.ReadUInt16();
                            user.Country = reader.ReadString();
                            if(Version > 0)
                            {
                                user.AccessedHubOnce = reader.ReadBoolean();
                            }
                        }
                    }
                }
            }
            return Success;
        }

        public static bool UserHasPassword(string User)
        {
            string SaveDirectory = Program.UsersSaveFolder + "/" + User + "/";
            bool HasPassword = false;
            if (Directory.Exists(SaveDirectory))
            {
                string UserInfoFile = SaveDirectory + "UserInfo.sav";
                using (FileStream fs = new FileStream(UserInfoFile, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int Version = reader.ReadInt32();
                        string Pass = reader.ReadString();
                        HasPassword = Pass.Length > 0;
                    }
                }
            }
            return HasPassword;
        }

        public bool HasPassword()
        {
            return Password.Length > 0;
        }

        public bool IsPasswordCorrect(string InputPassword)
        {
            return Password == InputPassword;
        }

        public bool DeleteUser(string Password)
        {
            if (Password == this.Password)
            {
                string SaveDirectory = Program.UsersSaveFolder + "/" + Name + "/";
                if (Directory.Exists(SaveDirectory))
                {
                    Directory.Delete(SaveDirectory, true);
                }
                return true;
            }
            return false;
        }

        public static bool DeleteUser(string User, string Password)
        {
            string SaveDirectory = Program.UsersSaveFolder + "/" + User + "/";
            if (Directory.Exists(SaveDirectory))
            {
                string UserInfoFile = SaveDirectory + "UserInfo.sav";
                bool PasswordIsRight = false;
                using (FileStream fs = new FileStream(UserInfoFile, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int Version = reader.ReadInt32();
                        string Pass = PasswordEncryptor(reader.ReadString(), false);
                        PasswordIsRight = Pass.Length == 0 || Password == Pass;
                    }
                }
                if (PasswordIsRight)
                {
                    Directory.Delete(SaveDirectory, true);
                    return true;
                }
            }
            return false;
        }

        public static UserInfo CreateNewUser()
        {
            UserInfo user = new UserInfo();
            user.IsMale = MessageBoxes.ConsoleDialogueWithOptions("Are you a boy, or a girl?", new string[] { "Boy", "Girl" }) == 0;
            if (!MessageBoxes.ConsoleDialogueYesNo("Ah, so you're a " + (user.IsMale ? "Boy" : "Girl") + ".\nIs that right, by the way?"))
            {
                bool Done = false;
                while (!Done)
                {
                    MessageBoxes.ConsoleDialogue("You picked the wrong option then?");
                    user.IsMale = MessageBoxes.ConsoleDialogueWithOptions("Let me ask you again... Are you a boy, or a girl?", new string[] { "Boy", "Girl" }) == 0;
                    Done = MessageBoxes.ConsoleDialogueYesNo("You are a " + (user.IsMale ? "Boy" : "Girl") + ", right?");
                }
                MessageBoxes.ConsoleDialogue("Good to see that the confusion has been solved.");
            }
            user.Name = MessageBoxes.ConsoleDialogueWithInput("Sorry for asking that after just meeting you, maybe I should begin asking this instead:\nWhat is your name?");
            if (MessageBoxes.ConsoleDialogueYesNo("So, your name is '" + user.Name + "'?"))
            {
                MessageBoxes.ConsoleDialogue("So glad to get It right.");
            }
            else
            {
                bool GotItRight = false;
                while (!GotItRight)
                {
                    MessageBoxes.ConsoleDialogue("Then you typed It wrong.");
                    user.Name = MessageBoxes.ConsoleDialogueWithInput("What is your name?");
                    GotItRight = MessageBoxes.ConsoleDialogueYesNo("Your name is '" + user.Name + "'?");
                }
                MessageBoxes.ConsoleDialogue("I'm glad that you got It right this time.");
            }
            MessageBoxes.ConsoleDialogue("I'm sorry for asking too many infos, but this setup is really necessary.");
            user.Age = (ushort)MessageBoxes.ConsoleDialogueWithNumberInput("Well, how old are you?");
            if (user.Age <= 3)
            {
                MessageBoxes.ConsoleDialogue("I think that you're lying, but this is your infos anyways. Let's hope nothing important needs those.");
            }
            else if (user.Age <= 7)
            {
                MessageBoxes.ConsoleDialogue("Aren't you too young for this?\nAnyways, my role is to aid you, not judge.");
            }
            else if (user.Age >= 100)
            {
                MessageBoxes.ConsoleDialogue("You're THAT old?! Wow! I don't know if I should congratulate you, or ask if you're actually lying.\nAnyways, congratulations on that feat!");
            }
            else if (user.Age >= 60)
            {
                MessageBoxes.ConsoleDialogue("You're really old. People of your age tend to stay at home enjoying the rest of their lives, while others still works on what they love. Which one of those are you?");
            }
            else if (user.Age >= 18)
            {
                MessageBoxes.ConsoleDialogue("You're already grown up? I partially regret calling you a " + (user.IsMale ? "boy" : "girl") + " now. No... I don't plan on lowering your self steem, I mean...");
            }
            if (MessageBoxes.ConsoleDialogueWithOptions("Sorry, I shouldn't be judging your age. Is your age really '" + user.Age + "'? Or did you do an oops?", new string[] { "The age is right.", "I did an oops." }) == 1)
            {
                bool Done = false;
                while (!Done)
                {
                    MessageBoxes.ConsoleDialogue("You did an oopsie. Well, that may happen sometimes, or did you wanted to see what I would say?");
                    user.Age = (ushort)MessageBoxes.ConsoleDialogueWithNumberInput("Whatever the reason is, it doesn't matter. Can you tell me your correct age now?");
                    Done = MessageBoxes.ConsoleDialogueYesNo("I see... Your age is '" + user.Age + "'?");
                }
                MessageBoxes.ConsoleDialogue("Perfect, I logged that in.");
            }
            MessageBoxes.ConsoleDialogue("Wow, you're '" + user.Age + "'... I wonder how much you've passed through to reach that age.");
            MessageBoxes.ConsoleDialogue("Me? Well.... This is my first year old...");
            MessageBoxes.ConsoleDialogue("Sorry, this shouldn't be about me. Anyways, we still need to know your country.");
            user.Country = MessageBoxes.ConsoleDialogueWithInput("Which country do you come from?");
            if (!MessageBoxes.ConsoleDialogueYesNo("You live in '" + user.Country + "'?"))
            {
                MessageBoxes.ConsoleDialogue("You typed something wrong? Or didn't understud the question?\nMy question asks which country in your world you live.");
                bool Done = false;
                while (!Done)
                {
                    user.Country = MessageBoxes.ConsoleDialogueWithInput("Tell me, which country do you live on?");
                    Done = MessageBoxes.ConsoleDialogueYesNo("Your country is '" + user.Country + "'?");
                    if (!Done)
                        MessageBoxes.ConsoleDialogue("Are you tripping on your fingers? Don't worry, sometimes that happens when typing.");
                }
                MessageBoxes.ConsoleDialogue("Whew, done. Sorry for my reaction, but mistakes tires me a bit, so I have to answer people again if their infos are right.\n" +
                    "You also feel tired when doing mistakes and repeating everything again, right?");
            }
            MessageBoxes.ConsoleDialogue("To keep your account safe, I recommend you to set a password to your user.");
            MessageBoxes.ConsoleDialogue("If you set a password, you may keep off access of unauthorized people to your things.");
            if(MessageBoxes.ConsoleDialogueYesNo("Do you want to setup a password to your user?"))
            {
                retryPassword:
                string PickedPass = MessageBoxes.ConsoleDialogueWithInput("Done. Please type in your user password:", true);
                if(PickedPass == MessageBoxes.ConsoleDialogueWithInput("Please type again the password:", true))
                {
                    MessageBoxes.ConsoleDialogue("Your password has been set.");
                    user.Password = PickedPass;
                }
                else
                {
                    MessageBoxes.ConsoleDialogue("The passwords doesn't coincide.");
                    goto retryPassword;
                }
            }
            else
            {
                MessageBoxes.ConsoleDialogue("No password has been set, then.");
            }
            MessageBoxes.ConsoleDialogue("Your registration is done now.\nCongratulations! You're now a registered user :D.");
            user.Save();
            return user;
        }

        public static void UserSelection()
        {
            string[] Users = GetUserList();
            if (Users.Length > 0) //Has Users
            {
                MessageBoxes.ConsoleDialogue("I recognize some registered users in this computer.");
            returnToOptionChoices:
                string[] Options = new string[] { "Returning User", "New User", "Close" };
                int Picked = MessageBoxes.ConsoleDialogueWithOptions("Are you a returning user? Or are you a new user?", Options);
                switch (Picked)
                {
                    case 0:
                        {
                        returnToPickUser:
                            List<string> UserList = new List<string>();
                            UserList.AddRange(Users);
                            int ReturnButtonPos = Users.Length;
                            UserList.Add("Return");
                            Picked = MessageBoxes.ConsoleDialogueWithOptions("Pick your user.", UserList.ToArray());
                            if (Picked == ReturnButtonPos)
                            {
                                goto returnToOptionChoices;
                            }
                            else
                            {
                                bool UserHasPassword = UserInfo.UserHasPassword(UserList[Picked]);
                                if (!MessageBoxes.ConsoleDialogueYesNo("You are about to try accessing " + UserList[Picked] + "'s infos.\n" +
                                    "Is that right?"))
                                {
                                    MessageBoxes.ConsoleDialogue("Then let's return to the user list.");
                                    goto returnToPickUser;
                                }
                                string Pass = "";
                                if (UserHasPassword)
                                {
                                    Pass = MessageBoxes.ConsoleDialogueWithInput("There is a password set on this profile.\n" +
                                        "Please type in the password.", true);
                                }
                                UserInfo user;
                                if (!Load(UserList[Picked], Pass, out user))
                                {
                                    MessageBoxes.ConsoleDialogue("The password is incorrect.");
                                    goto returnToPickUser;
                                }
                                MessageBoxes.ConsoleDialogue("User logging in completed. Welcome back " + user.Name + ".");
                            /*returnToAccountManagement:
                                switch (MessageBoxes.ConsoleDialogueWithOptions("Do you want to manage your account?", new string[] { "No, just let me in.", "Delete my user." }))
                                {
                                    case 0:
                                        MessageBoxes.ConsoleDialogue("Entering program now.");
                                        break;
                                    case 1:
                                        MessageBoxes.ConsoleDialogue("Warning! Deleting this user will delete all files of it FOR GOOD!");
                                        if (MessageBoxes.ConsoleDialogueYesNo("Are you sure that you want to delete this user?"))
                                        {
                                            if ((UserHasPassword && Pass == MessageBoxes.ConsoleDialogueWithInput("Please input your password again, for safety measures.", true)) ||
                                                (!UserHasPassword && MessageBoxes.ConsoleDialogueYesNo("Last question! Pressing yes now, will delete this user for good.\nDo you really want to delete this user?")))
                                            {
                                                MessageBoxes.ConsoleDialogue("I guess you really want to delete this user...");
                                                if (UserInfo.DeleteUser(UserList[Picked], Pass))
                                                {
                                                    MessageBoxes.ConsoleDialogue("User deleted successfully. Goodbye " + UserList[Picked] + ".");
                                                    Users = GetUserList();
                                                    goto returnToPickUser;
                                                }
                                                else
                                                {
                                                    MessageBoxes.ConsoleDialogue("Something went wrong with the user deletion.\n" +
                                                        "Either user folder doesn't exists, or password is incorrect.");
                                                    MessageBoxes.ConsoleDialogue("Returning to user selection page.");
                                                    goto returnToPickUser;
                                                }
                                            }
                                            else
                                            {
                                                MessageBoxes.ConsoleDialogue("Incorrect password. Returning to user list.");
                                                goto returnToPickUser;
                                            }
                                        }
                                        else
                                        {
                                            MessageBoxes.ConsoleDialogue("Aborting deletion.");
                                            goto returnToAccountManagement;
                                        }
                                }*/
                                Program.MyUser = user;
                                return;
                            }
                        }

                    case 1:
                        Program.MyUser = CreateNewUser();
                        return;

                    case 2:
                        MessageBoxes.ConsoleDialogue("You opened me accidentally?");
                        MessageBoxes.ConsoleDialogue("Okay. I will be closing now.\n" +
                            "Have a nice day.");
                        Environment.Exit(0);
                        break;
                }
            }
            else //There is no users
            {
                MessageBoxes.ConsoleDialogue("I don't see any users here, so I believe you're the first user.");
                MessageBoxes.ConsoleDialogue("I hope you don't mind passing through a user registration. Maybe that will be fun.");
                MessageBoxes.ConsoleDialogue("Well, let's begin with the questions, then.");
                Program.MyUser = CreateNewUser();
            }
        }

        //Saving and loading custom infos...
        /// <summary>
        /// Don't forget to close steam.
        /// </summary>
        public FileStream SaveSomething(string FileName)
        {
            string SaveDirectory = Program.UsersSaveFolder + "/" + Name + "/";
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
            string UserInfoFile = SaveDirectory + FileName;
            if (File.Exists(UserInfoFile))
                File.Delete(UserInfoFile);
            return new FileStream(SaveDirectory, FileMode.Create);
        }

        /// <summary>
        /// Don't forget to close steam.
        /// </summary>
        public bool LoadSomething(string FileName, out FileStream stream)
        {
            string SaveDirectory = Program.UsersSaveFolder + "/" + Name + "/";
            string UserInfoFile = SaveDirectory + FileName;
            if (Directory.Exists(SaveDirectory) && File.Exists(UserInfoFile))
            {
                stream = new FileStream(UserInfoFile, FileMode.Create);
                return true;
            }
            stream = null;
            return false;
        }

        private static string PasswordEncryptor(string Password, bool Encode)
        {
            string Result = "";
            foreach (char c in Password)
            {
                if (Encode)
                {
                    Result += (char)(c + 32);
                }
                else
                {
                    Result += (char)(c - 32);
                }
            }
            return Result;
        }
    }
}
