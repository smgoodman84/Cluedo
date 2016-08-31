using System;
using System.Collections.Generic;
using System.Linq;

namespace Cluedo
{
    internal class Game
    {
        private readonly Weapon _murderWeapon;
        private readonly Character _murderer;
        private readonly Room _murderRoom;

        private readonly List<Player> _players;

        public Game(params Character[] characters) : this(characters.AsEnumerable())
        {
        }

        public Game(IEnumerable<Character> characters)
        {
            var activeCharacters = characters.ToList();

            var inactiveCharacters = Character.Characters.Where(p => !activeCharacters.Contains(p)).ToList();
            _players = activeCharacters.Select(c => new Player(c)).ToList();

            var random = new Random();
            var randomisedRooms = Room.Rooms.OrderBy(x => random.Next());

            LockstepForeach(inactiveCharacters, randomisedRooms, SetCharacterPosition);
            LockstepForeach(Weapon.Weapons, randomisedRooms, SetWeaponPosition);

            _murderWeapon = Weapon.Weapons.OrderBy(x => random.Next()).First();
            _murderer = Character.Characters.OrderBy(x => random.Next()).First();
            _murderRoom = Room.Rooms.OrderBy(x => random.Next()).First();

            var cards = Weapon.Weapons.Where(x => x != _murderWeapon).Select(x => new Card(x)).ToList();
            cards.AddRange(Character.Characters.Where(x => x != _murderer).Select(x => new Card(x)).ToList());
            cards.AddRange(Room.Rooms.Where(x => x != _murderRoom).Select(x => new Card(x)).ToList());

            var shuffledCards = cards.OrderBy(x => random.Next()).ToList();

            for (var playerIndex = 0; playerIndex < _players.Count; playerIndex++)
            {
                var playercards = shuffledCards
                    .Where((x, i) => i%_players.Count == playerIndex)
                    .ToList();

                _players[playerIndex].SetCards(playercards);
            }
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
