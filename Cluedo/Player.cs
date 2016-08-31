using System.Collections.Generic;

namespace Cluedo
{
    internal class Player
    {
        private readonly Character _character;
        private List<Card> _cards;

        public Player(Character character)
        {
            _character = character;
        }

        public void SetCards(List<Card> cards)
        {
            _cards = cards;
        }

        public override string ToString()
        {
            return _character.ToString();
        }
    }
}
