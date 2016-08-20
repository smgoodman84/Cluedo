using System.Collections.Generic;

namespace Cluedo
{
    internal class Player
    {
        private readonly string _name;
        private BoardPosition _position;

        public Player(string name, BoardPosition position)
        {
            _name = name;
            SetPosition(position);
        }

        public void SetPosition(BoardPosition newPosition)
        {
            _position?.RemovePlayer(this);
            _position = newPosition;
            _position.AddPlayer(this);
        }

        public static readonly Player ReverendGreen = new Player("ReverendGreen", Board.ReverendGreenStart);
        public static readonly Player MrsPeacock = new Player("MrsPeacock", Board.MrsPeacockStart);
        public static readonly Player ProfessorPlum = new Player("ProfessorPlum", Board.ProfessorPlumStart);
        public static readonly Player MissScarlet = new Player("MissScarlet", Board.MissScarletStart);
        public static readonly Player ColonelMustard = new Player("ColonelMustard", Board.ColonelMustardStart);
        public static readonly Player MrsWhite = new Player("MrsWhite", Board.MrsWhiteStart);

        public static readonly List<Player> Players = new List<Player>
        {
            ReverendGreen,
            MrsPeacock,
            ProfessorPlum,
            MissScarlet,
            ColonelMustard,
            MrsWhite
        };

        public override string ToString()
        {
            return _name;
        }
    }
}