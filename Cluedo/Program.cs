using System;

namespace Cluedo
{
    internal class Program
    {
        public static void Main()
        {
            var game = new Game(Character.ColonelMustard, Character.MrsPeacock, Character.ProfessorPlum);

            Console.ReadKey();
        }
    }
}
