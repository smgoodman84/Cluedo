namespace Cluedo
{
    internal class Card
    {
        private readonly Room _room;
        private readonly Weapon _weapon;
        private readonly Character _character;

        public Card(Character character)
        {
            _character = character;
        }

        public Card(Weapon weapon)
        {
            _weapon = weapon;
        }

        public Card(Room room)
        {
            _room = room;
        }

        public override string ToString()
        {
            return _room?.ToString() ?? _weapon?.ToString() ?? _character?.ToString();
        }
    }
}
