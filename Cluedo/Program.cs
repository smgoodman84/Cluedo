using System;

namespace Cluedo
{
    internal class Program
    {
        public static void Main()
        {
            var game = new Game(Player.ColonelMustard, Player.MrsPeacock, Player.ProfessorPlum);

            Console.ReadKey();
        }
    }
}
