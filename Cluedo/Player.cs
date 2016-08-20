using System.Collections.Generic;

namespace Cluedo
{
    internal class Player
    {
        private BoardPosition _position;

        public Player(BoardPosition position)
        {
            _position = position;
        }

        public static readonly Player ReverendGreen = new Player(Board.ReverendGreenStart);
        public static readonly Player MrsPeacock = new Player(Board.MrsPeacockStart);
        public static readonly Player ProfessorPlum = new Player(Board.ProfessorPlumStart);
        public static readonly Player MissScarlet = new Player(Board.MissScarletStart);
        public static readonly Player ColonelMustard = new Player(Board.ColonelMustardStart);
        public static readonly Player MrsWhite = new Player(Board.MrsWhiteStart);

        public static readonly List<Player> Players = new List<Player>
        {
            ReverendGreen,
            MrsPeacock,
            ProfessorPlum,
            MissScarlet,
            ColonelMustard,
            MrsWhite
        };
    }
}