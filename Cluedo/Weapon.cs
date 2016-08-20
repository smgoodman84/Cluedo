using System.Collections.Generic;

namespace Cluedo
{
    internal class Weapon
    {
        private readonly string _name;
        private BoardPosition _position;

        public Weapon(string name)
        {
            _name = name;
        }

        public void SetPosition(BoardPosition newPosition)
        {
            _position?.RemoveWeapon(this);
            _position = newPosition;
            _position.AddWeapon(this);
        }

        public static Weapon Dagger = new Weapon("Dagger");
        public static Weapon CandleStick = new Weapon("CandleStick");
        public static Weapon LeadPiping = new Weapon("LeadPiping");
        public static Weapon Rope = new Weapon("Rope");
        public static Weapon Spanner = new Weapon("Spanner");
        public static Weapon Revolver = new Weapon("Revolver");

        public static List<Weapon> Weapons = new List<Weapon>()
        {
            Dagger,
            CandleStick,
            LeadPiping,
            Rope,
            Spanner,
            Revolver
        };

        public override string ToString()
        {
            return _name;
        }
    }
}