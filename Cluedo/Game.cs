using System;
using System.Collections.Generic;
using System.Linq;

namespace Cluedo
{
    internal class Game
    {
        private readonly List<Player> _activePlayers;

        public Game(params Player[] players) : this(players.AsEnumerable())
        {
        }

        public Game(IEnumerable<Player> players)
        {
            _activePlayers = players.ToList();

            var inactivePlayers = Player.Players.Where(p => !_activePlayers.Contains(p)).ToList();

            var random = new Random();
            var randomisedRooms = Board.Rooms.OrderBy(x => random.Next());

            LockstepForeach(inactivePlayers, randomisedRooms, SetPlayerPosition);
            LockstepForeach(Weapon.Weapons, randomisedRooms, SetWeaponPosition);
        }

        private static void SetPlayerPosition(Player p, BoardPosition bp)
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
