using System.Collections.Generic;

namespace Cluedo
{
    internal class Character
    {
        private readonly string _name;
        private BoardPosition _position;

        public Character(string name, BoardPosition position)
        {
            _name = name;
            SetPosition(position);
        }

        public void SetPosition(BoardPosition newPosition)
        {
            _position?.RemoveCharacter(this);
            _position = newPosition;
            _position.AddCharacter(this);
        }

        public static readonly Character ReverendGreen = new Character("ReverendGreen", Board.ReverendGreenStart);
        public static readonly Character MrsPeacock = new Character("MrsPeacock", Board.MrsPeacockStart);
        public static readonly Character ProfessorPlum = new Character("ProfessorPlum", Board.ProfessorPlumStart);
        public static readonly Character MissScarlet = new Character("MissScarlet", Board.MissScarletStart);
        public static readonly Character ColonelMustard = new Character("ColonelMustard", Board.ColonelMustardStart);
        public static readonly Character MrsWhite = new Character("MrsWhite", Board.MrsWhiteStart);

        public static readonly List<Character> Characters = new List<Character>
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