using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Applications.AgendaAppClasses;

namespace ConsoleApp1.Applications
{
    public class Agenda : Application
    {
        private static List<Event> Events = new List<Event>();
        private static DateTime Today = new DateTime();

        public Agenda() : base("Agenda")
        {

        }

        public override void OnLoad()
        {

        }

        public override void StartProgram()
        {
            Today = DateTime.Now;

        }
    }
}
