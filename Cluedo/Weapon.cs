using System.Collections.Generic;

namespace Cluedo
{
    internal class Weapon
    {
        private Weapon()
        {
        }

        public static Weapon Dagger = new Weapon();
        public static Weapon CandleStick = new Weapon();
        public static Weapon LeadPiping = new Weapon();
        public static Weapon Rope = new Weapon();
        public static Weapon Spanner = new Weapon();
        public static Weapon Revolver = new Weapon();

        public static List<Weapon> Weapons = new List<Weapon>()
        {
            Dagger,
            CandleStick,
            LeadPiping,
            Rope,
            Spanner,
            Revolver
        };
    }
}