using System.Collections.Generic;
using System.Linq;

namespace Cluedo
{
    internal class Board
    {
        private static readonly BoardPosition Void = new BoardPosition("Void");
        private static readonly BoardPosition VerticalDoor = new BoardPosition("VerticalDoor");
        private static readonly BoardPosition HorizontalDoor = new BoardPosition("HorizontalDoor");

        public static readonly BoardPosition ReverendGreenStart = new BoardPosition("ReverendGreenStart");
        public static readonly BoardPosition MrsPeacockStart = new BoardPosition("MrsPeacockStart");
        public static readonly BoardPosition ProfessorPlumStart = new BoardPosition("ProfessorPlumStart");
        public static readonly BoardPosition MissScarletStart = new BoardPosition("MissScarletStart");
        public static readonly BoardPosition ColonelMustardStart = new BoardPosition("ColonelMustardStart");
        public static readonly BoardPosition MrsWhiteStart = new BoardPosition("MrsWhiteStart");

        public static readonly BoardPosition Kitchen = new BoardPosition("Kitchen");
        public static readonly BoardPosition BallRoom = new BoardPosition("BallRoom");
        public static readonly BoardPosition Conservatory = new BoardPosition("Conservatory");
        public static readonly BoardPosition DiningRoom = new BoardPosition("DiningRoom");
        public static readonly BoardPosition BilliardRoom = new BoardPosition("BilliardRoom");
        public static readonly BoardPosition Library = new BoardPosition("Library");
        public static readonly BoardPosition Lounge = new BoardPosition("Lounge");
        public static readonly BoardPosition Hall = new BoardPosition("Hall");
        public static readonly BoardPosition Study = new BoardPosition("Study");

        public static readonly BoardPosition KitchenStudyPassage = new BoardPosition("KitchenStudyPassage");
        public static readonly BoardPosition LoungeConservatoryPassage = new BoardPosition("LoungeConservatoryPassage");

        public static readonly List<BoardPosition> Rooms = new List<BoardPosition>
        {
            Kitchen,
            BallRoom,
            Conservatory,
            DiningRoom,
            BilliardRoom,
            Library,
            Lounge,
            Hall,
            Study,
            KitchenStudyPassage,
            LoungeConservatoryPassage
        };

        private const string Layout = @"
         W    G
KKKK|T ...AAAA... CCCCCC
KKKKKK..AAAAAAAA..CCCCCC
KKKKKK..AAAAAAAA..CCCCCC
KKKKKK..AAAAAAAA..-CCCCC
KKKKKK..|AAAAAA|...CC|U
 KKK-K..AAAAAAAA.......E
........A-AAAA-A.......
 .................BBBBBB
DDDDD.............|BBBBB
DDDDDDDD..     ...BBBBBB
DDDDDDDD..     ...BBBBBB
DDDDDDD|..     ...BBBB-B
DDDDDDDD..     ........
DDDDDDDD..     ...LL-LL
DDDDDD-D..     ..LLLLLLL
 .........     ..|LLLLLL
M................LLLLLLL
 ........HH--HH...LLLLL
U|OOOO-..HHHHHH........P
OOOOOOO..HHHHH|........
OOOOOOO..HHHHHH..-SSSS|T
OOOOOOO..HHHHHH..SSSSSSS
OOOOOOO..HHHHHH..SSSSSSS
OOOOOO R HHHHHH . SSSSSS
";

        private static BoardPosition MapPosition(char layoutCharacter)
        {
            switch (layoutCharacter)
            {
                case 'A': return BallRoom;
                case 'B': return BilliardRoom;
                case 'C': return Conservatory;
                case 'D': return DiningRoom;
                case 'E': return MrsPeacockStart;
                case 'G': return ReverendGreenStart;
                case 'H': return Hall;
                case 'K': return Kitchen;
                case 'L': return Library;
                case 'M': return ColonelMustardStart;
                case 'O': return Lounge;
                case 'P': return ProfessorPlumStart;
                case 'R': return MissScarletStart;
                case 'S': return Study;
                case 'T': return KitchenStudyPassage;
                case 'U': return LoungeConservatoryPassage;
                case 'W': return MrsWhiteStart;
                case '.': return new BoardPosition();
                case '|': return VerticalDoor;
                case '-': return HorizontalDoor;
                default: return Void;
            }
        }

        public Board()
        {
            var boardPositionArray = BoardPositionArray();

            var height = boardPositionArray.Length;
            var width = boardPositionArray[0].Length;

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var boardPosition = boardPositionArray[y][x];

                    if (boardPosition == HorizontalDoor)
                    {
                        boardPositionArray[y - 1][x].Neighbours.Add(boardPositionArray[y + 1][x]);
                        boardPositionArray[y + 1][x].Neighbours.Add(boardPositionArray[y - 1][x]);
                        continue;
                    }

                    if (boardPosition == VerticalDoor)
                    {
                        boardPositionArray[y][x - 1].Neighbours.Add(boardPositionArray[y][x + 1]);
                        boardPositionArray[y][x + 1].Neighbours.Add(boardPositionArray[y][x - 1]);
                        continue;
                    }

                    if (Rooms.Contains(boardPosition))
                    {
                        continue;
                    }

                    var neighbours = new List<BoardPosition>();
                    if (x > 0)
                    {
                        neighbours.Add(boardPositionArray[y][x - 1]);
                    }
                    if (x < width - 1)
                    {
                        neighbours.Add(boardPositionArray[y][x + 1]);
                    }
                    if (y > 0)
                    {
                        neighbours.Add(boardPositionArray[y - 1][x]);
                    }
                    if (y < height - 1)
                    {
                        neighbours.Add(boardPositionArray[y + 1][x]);
                    }

                    boardPosition.Neighbours.AddRange(
                        neighbours
                        .Where(n => n != Void)
                        .Where(n => n != boardPosition)
                        .Where(n => !Rooms.Contains(n))
                        .Where(n => !boardPosition.Neighbours.Contains(n))
                        .Distinct());

                    if (boardPosition.Name == null)
                    {
                        boardPosition.Name = $"Passageway[{x},{y}]";
                    }
                }
            }
        }

        private static BoardPosition[] MapLine(IEnumerable<char> line)
        {
            return line.Select(MapPosition).ToArray();
        }

        private static BoardPosition[][] BoardPositionArray()
        {
            var layoutLines = Layout
                .Replace("\r", "")
                .Split('\n')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            var width = layoutLines.Max(x => x.Length);

            var result = layoutLines
                .Select(x => x.PadRight(width, ' ').ToCharArray())
                .Select(MapLine)
                .ToArray();

            return result;
        }
    }
}
