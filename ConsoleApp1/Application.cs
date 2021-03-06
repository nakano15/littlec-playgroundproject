using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Application
    {
        public const string ApplicationSaveFolder = "AppData";

        public string Name = "";
        public Application(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// Executes the running program script.
        /// </summary>
        public virtual void StartProgram()
        {

        }

        /// <summary>
        /// Executes when the program is loaded.
        /// </summary>
        public virtual void OnLoad()
        {

        }

        /// <summary>
        /// Don't forget to close stream.
        /// </summary>
        public FileStream SaveOnApplication(string FileName)
        {
            string SaveDirectory = ApplicationSaveFolder + "/" + Name + "/";
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
            string UserInfoFile = SaveDirectory + FileName;
            if (File.Exists(UserInfoFile))
                File.Delete(UserInfoFile);
            return new FileStream(UserInfoFile, FileMode.Create);
        }

        /// <summary>
        /// Don't forget to close stream.
        /// </summary>
        public FileStream LoadOnApplication(string FileName)
        {
            string SaveDirectory = ApplicationSaveFolder + "/" + Name + "/";
            string UserInfoFile = SaveDirectory + FileName;
            if (Directory.Exists(SaveDirectory) && File.Exists(UserInfoFile))
            {
                return new FileStream(UserInfoFile, FileMode.Open);
            }
            return null;
        }

        public static void LoadApps()
        {
            const string AppFolder = "Application";
            Program.Apps.Clear();
            Program.Apps.Add(new Applications.MathChallenge());
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);
            foreach(string Dll in Directory.GetFiles(AppFolder, "*.dll"))
            {
                string DllDirectory = Environment.CurrentDirectory + "/" + Dll;
                Assembly ass = Assembly.LoadFile(DllDirectory);
                foreach(Type type in ass.GetExportedTypes())
                {
                    if(type.BaseType == typeof(Application))
                    {
                        Program.Apps.Add((Application)Activator.CreateInstance(type));
                    }
                }
            }
            foreach (Application app in Program.Apps)
                app.OnLoad();
        }

        public static void AppList()
        {
            List<string> Apps = new List<string>();
            foreach(Application app in Program.Apps)
            {
                Apps.Add(app.Name);
            }
            int ClosePosition = Apps.Count;
            Apps.Add("Close");
            int PickedApp = MessageBoxes.ConsoleDialogueWithOptions("Which App do you want to open?", Apps.ToArray());
            if(PickedApp == ClosePosition)
            {
                MessageBoxes.ConsoleDialogue("Returning to the Hub.");
            }
            else
            {
                Program.Apps[PickedApp].StartProgram();
            }
        }
    }
}
