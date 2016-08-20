using System.Collections.Generic;

namespace Cluedo
{
    internal class BoardPosition
    {
        public string Name;

        public BoardPosition()
        {
        }

        public BoardPosition(string name)
        {
            Name = name;
        }

        public List<BoardPosition> Neighbours = new List<BoardPosition>();
        List<Player> Players = new List<Player>();
        List<Weapon> Weapons = new List<Weapon>();

        public override string ToString()
        {
            return Name ?? "Unknown";
        }
    }
}