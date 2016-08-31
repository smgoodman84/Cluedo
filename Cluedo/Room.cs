using System.Collections.Generic;

namespace Cluedo
{
    internal class Room : BoardPosition
    {
        public Room(string name) : base(name)
        {
        }

        public static readonly Room Kitchen = new Room("Kitchen");
        public static readonly Room BallRoom = new Room("BallRoom");
        public static readonly Room Conservatory = new Room("Conservatory");
        public static readonly Room DiningRoom = new Room("DiningRoom");
        public static readonly Room BilliardRoom = new Room("BilliardRoom");
        public static readonly Room Library = new Room("Library");
        public static readonly Room Lounge = new Room("Lounge");
        public static readonly Room Hall = new Room("Hall");
        public static readonly Room Study = new Room("Study");

        public static readonly List<Room> Rooms = new List<Room>
        {
            Kitchen,
            BallRoom,
            Conservatory,
            DiningRoom,
            BilliardRoom,
            Library,
            Lounge,
            Hall,
            Study
        };
    }
}
