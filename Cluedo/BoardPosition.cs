using System.Collections.Generic;
using System.Linq;

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
        private readonly List<Character> _characters = new List<Character>();
        private readonly List<Weapon> _weapons = new List<Weapon>();

        public override string ToString()
        {
            var items = _characters.Select(x => x.ToString()).ToList();
            items.AddRange(_weapons.Select(x => x.ToString()));

            return $"{Name ?? "Unknown"} [{string.Join(",", items)}]";
        }

        public void AddCharacter(Character character)
        {
            _characters.Add(character);
        }

        public void RemoveCharacter(Character character)
        {
            _characters.Remove(character);
        }

        public void AddWeapon(Weapon weapon)
        {
            _weapons.Add(weapon);
        }

        public void RemoveWeapon(Weapon weapon)
        {
            _weapons.Remove(weapon);
        }
    }
}