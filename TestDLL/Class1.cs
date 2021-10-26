using ConsoleApp1;

namespace TestDLL
{
    public class Class1 : Application
    {
        public Class1() : base("Test App")
        {

        }

        public override void StartProgram()
        {
            MessageBoxes.ConsoleDialogue("Program executed correctly!");
        }
    }
}
