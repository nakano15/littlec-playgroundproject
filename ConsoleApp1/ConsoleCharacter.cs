using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ConsoleCharacter
    {
        public static string ConsoleName = "";
        public static bool ConsoleIsMale = true;
        private static readonly string[] NameSyllabesMale = new string[] { "ca", "me", "tol", "no", "roq", "ko", "ne", "roo", "kel", "k'" },
            NameSyllabesFemale = new string[] { "zo", "ya", "ma", "le", "nar", "han", "lo", "ra", "ser", "lay", "na", "mi'", "za" };

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
            while(SyllabeCount-- > 0)
            {
                int Picked = Program.random.Next(PossibleSyllabes.Count);
                string Syllabe = PickedSyllabes[PossibleSyllabes[Picked]];
                for(int i = 0; i < Syllabe.Length; i++)
                {
                    if(ConsoleName.Length == 0)
                    {
                        ConsoleName += char.ToUpper(Syllabe[i]);
                    }
                    else
                    {
                        ConsoleName += Syllabe[i];
                    }
                }
                PossibleSyllabes.RemoveAt(Picked);
            }
        }
    }
}
