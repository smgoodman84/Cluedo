using System;
using System.Collections.Generic;
using System.Linq;

namespace Cluedo
{
    internal class Game
    {
        private readonly List<Character> _activeCharacters;

        public Game(params Character[] characters) : this(characters.AsEnumerable())
        {
        }

        public Game(IEnumerable<Character> characters)
        {
            _activeCharacters = characters.ToList();

            var inactiveCharacters = Character.Characters.Where(p => !_activeCharacters.Contains(p)).ToList();

            var random = new Random();
            var randomisedRooms = Room.Rooms.OrderBy(x => random.Next());

            LockstepForeach(inactiveCharacters, randomisedRooms, SetCharacterPosition);
            LockstepForeach(Weapon.Weapons, randomisedRooms, SetWeaponPosition);
        }

        private static void SetCharacterPosition(Character p, BoardPosition bp)
        {
            p.SetPosition(bp);
        }

        private static void SetWeaponPosition(Weapon w, BoardPosition bp)
        {
            w.SetPosition(bp);
        }

        private static void LockstepForeach<T, T2>(IEnumerable<T> enumerable1, IEnumerable<T2> enumerable2, Action<T, T2> action)
        {
            var enumerator1 = enumerable1.GetEnumerator();
            var enumerator2 = enumerable2.GetEnumerator();
            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                action(enumerator1.Current, enumerator2.Current);
            }
        }
    }
}
